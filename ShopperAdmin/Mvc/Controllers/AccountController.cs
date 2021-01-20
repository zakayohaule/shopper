using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Helpers;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.ViewModels;
using Shared.Mvc.ViewModels.Emails;
using ShopperAdmin.Attributes;
using ShopperAdmin.Extensions.Helpers;
using ShopperAdmin.Services.Interfaces;

namespace ShopperAdmin.Mvc.Controllers
{
    [AllowAnonymous]
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserClaimService _userClaimService;
        private readonly IUserService _userService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IUserClaimService userClaimService, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userClaimService = userClaimService;
            _userService = userService;
        }

        [HttpGet("login", Name = "login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginModel());
        }

        [HttpPost("login", Name = "login"), ValidateAntiForgeryToken, ValidateModel]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnTo)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            // return Ok(loginModel);
            if (user.IsNull())
            {
                AddPageAlerts(PageAlertType.Error, "Invalid login credentials!");
                return View();
            }

            if (!user.EmailConfirmed)
            {
                TempData["EmailVerification"] = "unverified";
                return View();
            }

            var validCredentials = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            var result =
                await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, true);

            if (result.Succeeded)
            {
                if (!user.HasResetPassword)
                {
                    var callbackUrl = await CreatePasswordResetCallbackUrlAsync(user);
                    return Redirect(callbackUrl);
                }

                var claims = _userClaimService.GetUserClaims(user.Id);
                _userClaimService.CacheClaims(user.Id, claims);

                if (returnTo.IsNotNull())
                {
                    return LocalRedirect(returnTo);
                }

                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                var offset = (user.LockoutEnd - DateTimeOffset.Now);
                if (offset.HasValue)
                {
                    AddPageAlerts(PageAlertType.Error,
                        $"Your account has been locked due to many failed login attempts!. Wait for {offset.Value.Minutes} minutes, then try again!");
                }
            }
            else if (result.IsNotAllowed)
            {
                AddPageAlerts(PageAlertType.Error,
                    $"You are not allowed to login!Please contact our administrators");
            }
            else
            {
                AddPageAlerts(PageAlertType.Error,
                    $"{user.AccessFailedCount} failed login attempt(s). Your account will be locked after 5 failed attempts");
            }

            return View();
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(long userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded) return null;
            TempData["EmailVerification"] = "verified";
            return View("Login", new LoginModel());
        }

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel viewModel)
        {
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user.IsNull())
            {
                return NotFound();
            }

            var callBackUrl = await CreatePasswordResetCallbackUrlAsync(user);
            viewModel.ResetLink = callBackUrl;
            _userService.SendPasswordResetMail(viewModel);

            AddPageAlerts(PageAlertType.Success, "A password reset link has been sent to your email!");
            return View("Login");
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetForgottenPassword(long id, string token)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var validLink = await _userManager.VerifyUserTokenAsync(user,
                _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);

            if (!validLink)
            {
                AddPageAlerts(PageAlertType.Error, "Invalid password reset link");
                // ViewBag.Error = "Invalid password reset link";

                return View("Login");
            }

            var viewModel = new ResetPasswordModel
            {
                Email = user.Email,
                Token = token
            };

            return View(viewModel);
        }

        [HttpPost("reset-password"), ValidateAntiForgeryToken, ValidateModel]
        public async Task<IActionResult> ResetForgottenPassword(ResetPasswordModel viewModel)
        {
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user.IsNull())
            {
                return NotFound();
            }

            var pwdResetResult = await _userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.NewPassword);
            if (!pwdResetResult.Succeeded)
            {
                pwdResetResult.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(error.Code, error.Description);
                });
                return View(viewModel);
            }

            if (user.HasResetPassword == false)
            {
                user.HasResetPassword = true;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return View(viewModel);
                }

                await _signInManager.PasswordSignInAsync(user, viewModel.NewPassword, false, false);
                return RedirectToAction("Index", "Home");
            }

            AddPageAlerts(PageAlertType.Success, "Your password has been reset successfully, Please login!");
            return View(nameof(Login));
        }

        [HttpGet("reset-password-confirmation")]
        public ViewResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet("reset-password-email-sent")]
        public ViewResult PasswordResetEmailSent()
        {
            return View();
        }

        [Authorize, HttpGet("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize, HttpPost("change-password"), ValidateAntiForgeryToken, ValidateModel]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());

            var passwordResetResult = await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword,
                changePasswordModel.NewPassword);

            if (!passwordResetResult.Succeeded)
            {
                passwordResetResult.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(error.Code, error.Description);
                });
                return View();
            }

            await _signInManager.SignOutAsync();

            
            AddPageAlerts(PageAlertType.Success, "Your password has been reset successfully, Please login!");
            return View(nameof(Login));
        }

        [HttpGet("send-email-verification")]
        public IActionResult VerificationEmail()
        {
            return View(new SendVerificationViewModel());
        }

        [HttpPost("send-email-verification")]
        [ValidateAntiForgeryToken, ValidateModel]
        public async Task<IActionResult> VerificationEmail(SendVerificationViewModel viewModel)
        {
            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            // todo check if user exists


            //if exists
            var password = await _userService.ChangePasswordAsync(user);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("VerifyEmail", "Account", new {UserId = user.Id, Code = code},
                protocol: HttpContext.Request.Scheme);

            var emailVerificationModel = new EmailVerificationViewModel
            {
                Email = user.Email,
                Password = password,
                UserId = user.Id,
                UserName = user.FullName,
                VerificationLink = callbackUrl
            };

            TempData["EmailVerification"] = "sent";
            _userService.SendEmailVerificationEmail(emailVerificationModel);

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet("logout", Name = "logout")]
        public async Task<IActionResult> Logout()
        {
            _userClaimService.RemoveClaims(User.GetUserId());
            await _signInManager.SignOutAsync();
            return RedirectToRoute("login");
        }

        [Authorize]
        [HttpPost("logout", Name = "logout-post")]
        public async Task<IActionResult> PostLogout()
        {
            _userClaimService.RemoveClaims(User.GetUserId());
            await _signInManager.SignOutAsync();
            return RedirectToRoute("login");
        }

        public async Task<string> CreatePasswordResetCallbackUrlAsync(AppUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callBackUrl = Url.Action("ResetForgottenPassword", "Account", new {user.Id, token},
                HttpContext.Request.Scheme);

            return callBackUrl;
        }
    }
}