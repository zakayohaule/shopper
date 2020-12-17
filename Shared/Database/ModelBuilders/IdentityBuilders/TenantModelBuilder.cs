using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Mvc.Entities;

namespace Shared.Database.ModelBuilders.IdentityBuilders
{
    public class TenantModelBuilder : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasIndex(t => t.Domain).IsUnique();
            builder.HasIndex(t => t.Name).IsUnique();
        }
    }
}
