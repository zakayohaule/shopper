using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.ModelBuilders.IdentityBuilders
{
    public class PermissionBuilder : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasIndex(p => p.DisplayName).IsUnique();
        }
    }
}
