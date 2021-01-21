using System;
using System.ComponentModel.DataAnnotations.Schema;
using ShopperAdmin.Mvc.Entities.BaseEntities;
using ShopperAdmin.Mvc.Enums;

namespace ShopperAdmin.Mvc.Entities
{
    [Table("tenant_subscriptions")]
    public class TenantSubscription : BaseEntity<ulong>
    {
        [Column("tenant_id")]
        public Guid TenantId { get; set; }

        [Column("subscription_date")]
        public DateTime SubscriptionDate { get; set; }

        [Column("subscription_type")]
        public SubscriptionType SubscriptionType { get; set; }

        [Column("subscription_end_date")]
        public DateTime SubscriptionEndDate { get; set; }

        [Column("is_current")]
        public bool IsCurrent { get; set; } = true;

        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }
    }
}
