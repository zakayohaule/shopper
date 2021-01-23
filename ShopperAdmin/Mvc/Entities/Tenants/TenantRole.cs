using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopperAdmin.Mvc.Entities.Tenants
{
    [Table("roles")]
    public class TenantRole
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("tenant_id")]
        public Guid TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("normalized_name")]
        public string NormalizedName { get; set; }

        [Column("display_name")]
        public string DisplayName { get; set; }
    }
}
