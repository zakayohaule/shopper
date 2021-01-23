using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopperAdmin.Mvc.Entities;
using ShopperAdmin.Mvc.Enums;

namespace ShopperAdmin.Database.ModelBuilders
{
    public class TenantModelBuilder : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            var converter = new EnumToStringConverter<SubscriptionType>();

            builder.HasIndex(t => t.Domain).IsUnique();
            builder.HasIndex(t => t.Name).IsUnique();
            builder.HasIndex(t => t.Code).IsUnique();
            // builder.Property(t => t.SubscriptionType).HasConversion(converter);
        }
    }
}
