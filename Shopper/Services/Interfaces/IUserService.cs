using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Mvc.Entities;
using Shared.Mvc.Entities.Identity;
using Shared.Mvc.ViewModels;
using Shared.Mvc.ViewModels.Emails;

namespace Shopper.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<AppUser> GetAllUsers();
        IQueryable<Role> GetAllRoles();
        Task<AppUser> FindByIdAsync(long id, bool eager=false);
        Task<AppUser> FindByEmail(string email);
        Task DeleteUserAsync(AppUser user);
        void SendEmailVerificationEmail(EmailVerificationViewModel emailVerificationViewModel);
        void SendPasswordResetMail(ForgotPasswordModel forgotPasswordModel);
        string GenerateStrongPassword();
        Task<string> ChangePasswordAsync(AppUser user);
        Task UpdateUserRolesAsync(AppUser user, List<string> roles);

        bool ExistsById(long userId);
        bool ExistsByEmail(string email);
        bool ExistsByUserName(string username);
        string GenerateUserName(string username);
        bool UsersForInstitution(uint getInstitutionId, HashSet<long> selectedUsers);
    }
}