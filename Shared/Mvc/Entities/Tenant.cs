using System;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Mvc.Entities.BaseEntities;
using Shared.Mvc.Enums;

namespace Shared.Mvc.Entities
{
    [Table("tenants")]
    public class Tenant : NoTenantBaseEntity<Guid>
    {
        [Column("name")] public string Name { get; set; }

        [Column("code")] public string Code { get; set; }

        [Column("email")] public string Email { get; set; }

        [Column("address")] public string Address { get; set; }

        [Column("phone_number_1")] public string PhoneNumber1 { get; set; }

        [Column("phone_number_2")] public string PhoneNumber2 { get; set; }

        [Column("description")] public string Description { get; set; }

        [Column("domain")] public string Domain { get; set; }

        [Column("connection_string")] public string ConnectionString { get; set; }

        [Column("logo_path")] public string LogoPath { get; set; }

        [Column("subscription_type")] public SubscriptionType SubscriptionType { get; set; }

        [Column("valid_to")] public DateTime ValidTo { get; set; } = DateTime.Now.AddYears(1);

        public bool Active { get; set; } = true;
    }
}
