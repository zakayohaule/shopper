using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Shared.Mvc.Entities.Identity;

namespace Shared.Mvc.Entities.Identity
{
    [Table("user_logins")]
    public class UserLogin : IdentityUserLogin<long>
    {
        [Column("user_id")] public new long UserId { get; set; }

        [ForeignKey(nameof(UserId))] public AppUser User { get; set; }
    }
}