using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public TenantController(UserManager<AppUser> userManager, IUserService userService,
            RoleManager<Role> roleManager, ApplicationDbContext dbContext, ILogger logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _logger = logger;
            this.configuration = configuration;
        }

        [HttpPost("create-tenant-user")]
        public async Task<IActionResult> CreateTenantUserAndRole([FromBody] CreateTenantUserModel formUserModel)
        {
            _logger.Warning($"*************** Creating Tenant User ***************");
            var tenant = HttpContext.GetCurrentTenant();
            try
            {
                _logger.Warning($"*************** The current tenant is: {tenant.Name} ***************");

                var password = _userService.GenerateStrongPassword();

                _logger.Warning($"*************** Generating strong password: {password} ***************");

                var user = new AppUser
                {
                    TenantId = tenant.Id,
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

                    var errors = result.Errors.ToList().Select(error => error.Description);
                    _logger.Error($"*************** Could not create user: {errors} ***************");
                    return BadRequest(errors);
                }

                var newUser = await _userManager.FindByEmailAsync(user.Email);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var callbackUrl = Url.Action("VerifyEmail", "Account", new {UserId = newUser.Id, Code = code},
                    protocol: HttpContext.Request.Scheme);

                var appDomain = configuration["AppDomain"] ?? "localhost:5200";
                var emailVerificationModel = new EmailVerificationViewModel
                {
                    Email = newUser.Email,
                    Password = password,
                    UserId = newUser.Id,
                    UserName = newUser.FullName,
                    VerificationLink = callbackUrl.Replace("localhost:5200", appDomain)
                };

                TempData["EmailVerified"] = "sent";

                _logger.Warning($"*************** Sending email verification mail ***************");
                _userService.SendEmailVerificationEmail(emailVerificationModel);

                var role = new Role();
                role.DisplayName = formUserModel.RoleName;
                role.Name = $"{tenant.Domain}_{role.DisplayName}".ToUpper();
                result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.ToList().Select(r => r.Description);
                    _logger.Error($"*************** Could not create role: {errors} ***************");
                    return BadRequest(errors);
                }

                role = await _roleManager.FindByNameAsync(role.Name);
                RoleClaimsSeeder.UpdateRoleClaims(role, _logger, _dbContext);
                var userRole = new UserRole
                {
                    Role = role,
                    User = newUser
                };
                _logger.Warning($"***************  Assigning User The New Role ***************");
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
