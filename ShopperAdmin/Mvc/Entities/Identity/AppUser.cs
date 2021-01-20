using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShopperAdmin.Mvc.Entities.Identity
{
    [Table("users")]
    public class AppUser : IdentityUser<long>
    {
        [Column("id")] public override long Id { get; set; }

        [Column("user_name")] public override string UserName { get; set; } = null;

        [Column("normalized_user_name")] public override string NormalizedUserName { get; set; } = null;

        [Column("full_name"), DisplayName("Full Name")]
        public string FullName { get; set; }

        [Column("email"), EmailAddress]
        public override string Email { get; set; }

        [Column("normalized_email"), EmailAddress]
        public override string NormalizedEmail { get; set; }

        [Column("email_confirmed")] public override bool EmailConfirmed { get; set; }

        [Column("password_hash")] public override string PasswordHash { get; set; }

        [Column("security_stamp")] public override string SecurityStamp { get; set; }

        [Column("concurrency_stamp")] public override string ConcurrencyStamp { get; set; }

        [Column("phone_number")] public override string PhoneNumber { get; set; }

        [Column("phone_number_confirmed")] public override bool PhoneNumberConfirmed { get; set; }

        [Column("two_factor_enabled")] public override bool TwoFactorEnabled { get; set; }

        [Column("lockout_end")] public override DateTimeOffset? LockoutEnd { get; set; }

        [Column("lockout_enabled")] public override bool LockoutEnabled { get; set; }

        [Column("access_failed_count")] public override int AccessFailedCount { get; set; }

        [Column("is_deleted")] public bool IsDeleted { get; set; } = false;

        [Column("has_reset_password")] public bool HasResetPassword { get; set; } = false;

        [Column("tenant_id")]
        public Guid TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [BindNever]
        // [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [BindNever]
        // [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<UserClaim> Claims { get; set; }
        public List<UserLogin> Logins { get; set; }
        public List<UserToken> Tokens { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
