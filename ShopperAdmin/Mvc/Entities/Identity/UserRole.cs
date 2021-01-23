using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ShopperAdmin.Mvc.Entities.Identity
{
    [Table("user_role")]
    public class UserRole : IdentityUserRole<long>
    {
        [Column("user_id")] public new long UserId { get; set; }

        [Column("role_id")] public new long RoleId { get; set; }

        [ForeignKey(nameof(UserId))] public AppUser User { get; set; }

        [ForeignKey(nameof(RoleId))] public Role Role { get; set; }
    }
}
