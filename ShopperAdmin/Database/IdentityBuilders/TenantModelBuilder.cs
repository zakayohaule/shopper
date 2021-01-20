using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Mvc.Entities;
using Shared.Mvc.Enums;

namespace Shared.Database.ModelBuilders.IdentityBuilders
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
