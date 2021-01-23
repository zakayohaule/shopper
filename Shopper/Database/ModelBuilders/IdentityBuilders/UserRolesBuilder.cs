using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopper.Mvc.Entities.Identity;

namespace Shopper.Database.ModelBuilders.IdentityBuilders
{
    public class UserRolesBuilder : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_role");
        }
    }
}
