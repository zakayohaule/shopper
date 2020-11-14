﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using FluentEmail.Core;
 using Microsoft.AspNetCore.Identity;
 using Microsoft.EntityFrameworkCore;
 using Microsoft.Extensions.Hosting;
 using PasswordGenerator;
 using Serilog;
 using Shared.Extensions.Helpers;
 using ShopperAdmin.Database;
 using ShopperAdmin.Extensions.Helpers;
 using Shared.Mvc.Entities.Identity;
 using Shared.Mvc.ViewModels;
 using Shared.Mvc.ViewModels.Emails;
 using ShopperAdmin.Services.Interfaces;

 namespace ShopperAdmin.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailQueueService _emailQueueService;
        private readonly IFluentEmail _fluentEmail;
        private readonly IHostEnvironment _environment;
        private readonly ILogger _logger;
        private readonly UserManager<AppUser> _userManager;

        public UserService(ApplicationDbContext dbContext, IEmailQueueService emailQueueService, IFluentEmail fluentEmail, IHostEnvironment environment, ILogger logger, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _emailQueueService = emailQueueService;
            _fluentEmail = fluentEmail;
            _environment = environment;
            _logger = logger;
            _userManager = userManager;
        }

        public IQueryable<AppUser> GetAllUsers()
        {
            return _dbContext
                .Users
                .AsNoTracking()
                .Include(user => user.UserRoles);
        }

        public IQueryable<Role> GetAllRoles()
        {
            return _dbContext.Roles.AsNoTracking();
        }

        public async Task<AppUser> FindByIdAsync(long id, bool eager=false)
        {
            if (!eager)
            {
                return await _dbContext
                    .Users
                    .FindAsync(id);
            }

            return await _dbContext
                .Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> FindByEmail(string email)
        {
            return await _dbContext.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(user => String.Equals(user.Email, email, StringComparison.CurrentCultureIgnoreCase));
        }

        public Task<AppUser> SaveUserAsync(UserViewModel userViewModel)
        {
            return null;
        }

        public Task<AppUser> UpdateUserAsync(UserViewModel userViewModel)
        {
            return null;
        }

        public async Task DeleteUserAsync(AppUser user)
        {
            user.IsDeleted = true;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public void SendEmailVerificationEmail(EmailVerificationViewModel model)
        {
            var email = _fluentEmail
                .To(model.Email)
                .Subject("Email Verification")
                .UsingTemplateFromFile(_environment.GetEmailTemplate("EmailVerification"), model)
                .SendAsync();
            _emailQueueService.QueueMail(token => email);
        }

        public void SendPasswordResetMail(ForgotPasswordModel forgotPassword)
        {
            var email = _fluentEmail
                .To(forgotPassword.Email)
                .Subject("Reset Password Link")
                .UsingTemplateFromFile(_environment.GetEmailTemplate("ForgotPasswordEmail"), forgotPassword)
                .SendAsync();
            _emailQueueService.QueueMail(token => email);
        }

        public string GenerateStrongPassword()
        {
            return new Password(8)
                .IncludeLowercase()
                .IncludeNumeric()
                .IncludeUppercase()
                .IncludeSpecial()
                .Next();;
        }

        public async Task<string> ChangePasswordAsync(AppUser user)
        {
            var password = GenerateStrongPassword();
            var hashedPassword = _userManager.PasswordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;
            await _dbContext.SaveChangesAsync();
            return password;
        }

        public async Task UpdateUserRolesAsync(AppUser user, List<string> roles)
        {
            await _dbContext.Entry(user).Collection(r => r.UserRoles).LoadAsync();
            List<string> added;
            if (user.UserRoles.IsNotNull())
            {
                var currentRoles = user.UserRoles.Select(role => role.RoleId.ToString()).ToList();

                var deleted = user.UserRoles.Where(role => currentRoles.Except(roles).Contains(role.RoleId.ToString())).ToList();
                added = roles.Except(currentRoles).ToList();
                _dbContext.UserRoles.RemoveRange(deleted);
            }
            else
            {
                added = roles;
            }

            added.ForEach(roleString =>
            {
                if (!_dbContext.UserRoles.Any(role => role.RoleId.ToString() == roleString && role.UserId == user.Id))
                {
                    _dbContext.UserRoles.Add(new UserRole
                    {
                        RoleId = long.Parse(roleString),
                        UserId = user.Id
                    });
                }
            });
            await _dbContext.SaveChangesAsync();
        }

        public bool WasDeleted(UserViewModel viewModel)
        {
            return _dbContext.Users
                .IgnoreQueryFilters()
                .Any(user => (string.Equals(user.Email, viewModel.Email, StringComparison.OrdinalIgnoreCase) 
                || Equals(user.Id, viewModel.Id) && user.IsDeleted));
        }

        public bool ExistsByEmail(string email)
        {
            return _dbContext.Users.Any(user =>
                string.Equals(user.Email, email, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool ExistsById(long userId)
        {
            return _dbContext
                .Users
                .IgnoreQueryFilters().Any(user => user.Id == userId);
        }

        public bool ExistsByUserName(string userName)
        {
            return _dbContext.Users.Any(user => string.Equals(user.UserName, userName, StringComparison.CurrentCultureIgnoreCase));
        }

        public string GenerateUserName(string username)
        {
            username = username.Replace(" ", "");
            var existingCount = _dbContext.Users.Count(user => user.UserName.Contains(username));
            return existingCount == 0 ? username : $"{username}{existingCount}";
        }

        public bool UsersForInstitution(uint institutionId, HashSet<long> selectedUsers)
        {
            var users = GetAllUsers().Where(user => user.InstitutionId == institutionId).Select(user => user.Id).ToHashSet();
            return selectedUsers.IsSubsetOf(users);
        }
    }
}