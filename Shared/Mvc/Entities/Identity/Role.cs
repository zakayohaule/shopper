using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Mvc.Entities.Identity
{
    [Table("roles")]
    public class Role : IdentityRole<long>
    {
        [Column("id")]
        public override long Id { get; set; }

        [Column("tenant_id")]
        public Guid TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }

        [Column("name")]
        public override string Name { get; set; }

        [Column("normalized_name")]
        public override string NormalizedName { get; set; }

        [Column("display_name")]
        [Required]
        [Remote("ValidateRoleDisplayName", AdditionalFields = "Id"), DisplayName("Role Name")]
        public string DisplayName { get; set; }

        [Column("concurrency_stamp")]
        public override string ConcurrencyStamp { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<UserRole> UserRoles { get; set; }

        public List<RoleClaim> RoleClaims { get; set; }
    }
}
