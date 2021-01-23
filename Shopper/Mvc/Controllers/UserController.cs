    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Serilog;
    using Shopper.Attributes;
    using Shopper.Mvc.Entities.Identity;
    using Shopper.Mvc.ViewModels;
    using Shopper.Mvc.ViewModels.Emails;
    using Shopper.Services.Interfaces;

    namespace Shopper.Mvc.Controllers
{
    [Route("users"), Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;

        public UserController(IUserService userService,
            UserManager<AppUser> userManager, ILogger logger)
        {
            _userService = userService;
            _userManager = userManager;
            _logger = logger;
            Title = "Users";
        }


        [Permission("user_view"), Toast]
        [HttpGet("", Name = "users.view")]
        public IActionResult Index()
        {
            Title = "Users";
            AddPageHeader("Manage users");
            var users = _userService
                .GetAllUsers()
                .AsNoTracking()
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(role => role.Role).ToList(),
                })
                .ToList();

            return View(users);
        }

        [HttpPost(""), Permission("user_add"), ValidateAntiForgeryToken /*, ValidateModel*/]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            var password = _userService.GenerateStrongPassword();

            var user = new AppUser
            {
                Email = userViewModel.Email,
                FullName = userViewModel.FullName,
                EmailConfirmed = false,
                UserName = _userService.GenerateUserName(userViewModel.FullName)
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                TempData["ModelError"] = JsonConvert.SerializeObject(ModelState, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                return RedirectToAction(nameof(Index));
            }

            var newUser = await _userManager.FindByEmailAsync(user.Email);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var callbackUrl = Url.Action("VerifyEmail", "Account", new {UserId = newUser.Id, Code = code},
                protocol: HttpContext.Request.Scheme);

            var emailVerificationModel = new EmailVerificationViewModel
            {
                Email = newUser.Email,
                Password = password,
                UserId = newUser.Id,
                UserName = newUser.FullName,
                VerificationLink = callbackUrl
            };

            TempData["EmailVerified"] = "sent";
            _userService.SendEmailVerificationEmail(emailVerificationModel);

            ToastSuccess("User created successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}"), Permission("user_edit"), ValidateAntiForgeryToken,
         ValidateModelWithRedirect(nameof(UserController), nameof(Index), null)]
        public async Task<IActionResult> Update(UserViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            // user.Email = viewModel.Email;
            user.FullName = viewModel.FullName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            result.Errors.ToList().ForEach(error => { ModelState.AddModelError(error.Code, error.Description); });
            TempData["Error"] = JsonConvert.SerializeObject(ModelState, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            ToastSuccess("User updated successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/delete"), Permission("user_delete")]
        public async Task<IActionResult> Delete(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(user);

            ToastSuccess("User deleted successfully!");
            return RedirectToAction(nameof(Index));
        }


        [AcceptVerbs("GET", Route = "validate-user-email", Name = "ValidateUserEmail")]
        public JsonResult ExistsByEmail(string email)
        {
            if (_userService.ExistsByEmail(email))
            {
                return Json("A user with this email already exists!");
            };
            return Json(true);
        }

        [HttpPost("{id}/roles"), Permission("user_assign_role")]
        public async Task<IActionResult> UpdateUserRoles(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var roles = HttpContext.Request.Form["roles"].ToList();
            await _userService.UpdateUserRolesAsync(user, roles);

            ToastSuccess("User roles updated successfully!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal"), Permission("user_edit")]
        public async Task<JsonResult> EditUserModal(long id)
        {
            var user = await _userService.FindByIdAsync(id);
            var viewModel = new UserViewModel
            {
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
            };

            return Json(viewModel, new JsonSerializerSettings{ContractResolver = null});
        }

        [HttpGet("{id}/user-roles-modal")]
        public async Task<PartialViewResult> UserRolesModal(long id)
        {
            var user = await _userService.FindByIdAsync(id, true);

            var viewModel = new UserViewModel
            {
                Id = user.Id,
                Roles = user.UserRoles.Select(ur => ur.Role).ToList()
            };
            TempData["Roles"] = _userService.GetAllRoles().ToList();
            ViewData["Modal-Title"] = "Assign Roles";
            return PartialView("../User/_UpdateUserRolesModal", viewModel);
        }
    }
}
