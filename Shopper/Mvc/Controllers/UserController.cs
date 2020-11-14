using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.ViewModels;
using Shopper.Attributes;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Route("users"), Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;


        [ViewData] public string Title { get; set; } = "Users";

        public UserController(IUserService userService,
            UserManager<AppUser> userManager, ILogger logger)
        {
            _userService = userService;
            _userManager = userManager;
            _logger = logger;
        }


        [Permission("user_view")]
        [HttpGet("", Name = "users.view")]
        public async Task<IActionResult> Index()
        {
            Title = "Users";
            var users = _userService
                .GetAllUsers()
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(role => role.RoleId).ToList()
                })
                .ToList();

            return View(users);
        }

        /*[HttpPost(""), Permission("user_add"), ValidateAntiForgeryToken /*, ValidateModel#1#]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            var wasDeleted = _userService.WasDeleted(userViewModel);

            if (wasDeleted)
            {
                var deletedUser = await _userService.FindByEmail(userViewModel.Email);
                deletedUser.IsDeleted = false;
                deletedUser.FullName = userViewModel.FullName;
                deletedUser.InstitutionId = userViewModel.InstitutionId;
                var updateResult = await _userManager.UpdateAsync(deletedUser);
                if (updateResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in updateResult.Errors)
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

            var password = _userService.GenerateStrongPassword();

            var user = new AppUser
            {
                Email = userViewModel.Email,
                FullName = userViewModel.FullName,
                InstitutionId = userViewModel.InstitutionId,
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

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}"), Permission("user_edit"), ValidateAntiForgeryToken,
         ValidateModelWithRedirect(nameof(UserController), nameof(Index), null)]
        public async Task<IActionResult> Update(UserViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id.ToString());

            if (user.IsNull())
            {
                return NotFound();
            }

            // user.Email = viewModel.Email;
            user.InstitutionId = viewModel.InstitutionId;
            user.FullName = viewModel.FullName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                result.Errors.ToList().ForEach(error => { ModelState.AddModelError(error.Code, error.Description); });
                TempData["Error"] = JsonConvert.SerializeObject(ModelState, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("{id}/delete"), Permission("user_delete")]
        public async Task<IActionResult> Delete(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user.IsNull())
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(user);

            return RedirectToAction(nameof(Index));
        }


        [AcceptVerbs("GET", Route = "validate-user-email", Name = "ValidateUserEmail")]
        public JsonResult ExistsByEmail(string email)
        {
            if (_userService.ExistsByEmail(email))
            {
                return Json("A user with this email already exists!");
            }

            ;

            return Json(true);
        }

        [HttpPost("{id}/roles")]
        public async Task<IActionResult> UpdateUserRoles(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user.IsNull())
            {
                return NotFound();
            }

            var roles = HttpContext.Request.Form["roles"].ToList();
            await _userService.UpdateUserRolesAsync(user, roles);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/open-edit-modal")]
        public async Task<PartialViewResult> EditUserModal(long id)
        {
            var user = await _userService.FindByIdAsync(id, true);
            var viewModel = new UserViewModel
            {
                Email = user.Email,
                FullName = user.FullName,
                InstitutionId = user.InstitutionId,
                InstitutionName = user.Institution.Name,
                Id = user.Id,
            };

            ViewData["InstitutionSelectItems"] = _institutionService
                .GetAllInstitutions()
                .Select(inst => new SelectListItem
                {
                    Value = inst.Id.ToString(),
                    Text = inst.Name
                }).ToList();

            return PartialView("../User/_EditUserModal", viewModel);
        }

        [HttpGet("{id}/user-roles-modal")]
        public async Task<PartialViewResult> UserRolesModal(long id)
        {
            var user = await _userService.FindByIdAsync(id, true);

            var viewModel = new UserViewModel
            {
                Id = user.Id,
                Roles = user.UserRoles.Select(role => role.RoleId).ToList()
            };
            TempData["Roles"] = _userService.GetAllRoles().ToList();

            return PartialView("../User/_UpdateUserRolesModal", viewModel);
        }*/
    }
}