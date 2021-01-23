using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ShopperAdmin.Mvc.Entities.Identity
{
    [Table("role_claims")]
    public class RoleClaim : IdentityRoleClaim<long>
    {
        [Column("id")] public override int Id { get; set; }

        [Column("role_id")] public override long RoleId { get; set; }

        [Column("claim_type")] public override string ClaimType { get; set; }

        [Column("claim_value")] public override string ClaimValue { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
    }
}
