using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.ModelBuilders.IdentityBuilders
{
    public class UserRolesBuilder : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_role");
        }
    }
}
