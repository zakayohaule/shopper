using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Mvc.Entities.Identity;

namespace Shared.Database.ModelBuilders.IdentityBuilders
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