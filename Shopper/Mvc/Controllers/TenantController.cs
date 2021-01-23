using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Shopper.Database;
using Shopper.Database.Seeders;
using Shopper.Extensions.Helpers;
using Shopper.Mvc.Entities.Identity;
using Shopper.Mvc.ViewModels;
using Shopper.Mvc.ViewModels.Emails;
using Shopper.Services.Interfaces;

namespace Shopper.Mvc.Controllers
{
    [Authorize(AuthenticationSchemes = "jwt")]
    [ApiController]
    public class TenantController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;


        public TenantController(UserManager<AppUser> userManager, IUserService userService,
            RoleManager<Role> roleManager, ApplicationDbContext dbContext, ILogger logger)
        {
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost("create-tenant-user")]
        public async Task<IActionResult> CreateTenantUserAndRole([FromBody] CreateTenantUserModel formUserModel)
        {
            var tenant = HttpContext.GetCurrentTenant();
            try
            {
                var password = _userService.GenerateStrongPassword();

                var user = new AppUser
                {
                    Email = formUserModel.AdminEmail,
                    FullName = formUserModel.AdminFullName,
                    EmailConfirmed = false,
                    UserName = _userService.GenerateUserName(formUserModel.AdminFullName)
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
                    return BadRequest(result.Errors.ToList().Select(error => error.Description));
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

                var role = new Role();
                role.DisplayName = formUserModel.RoleName;
                role.Name = $"{tenant.Domain}_{role.DisplayName}".ToUpper();
                result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.ToList().Select(r => r.Description));
                }

                role = await _roleManager.FindByNameAsync(role.Name);
                RoleClaimsSeeder.UpdateRoleClaims(role, _logger, _dbContext);
                var userRole = new UserRole
                {
                    Role = role,
                    User = newUser
                };
                await _dbContext.UserRoles.AddAsync(userRole);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
