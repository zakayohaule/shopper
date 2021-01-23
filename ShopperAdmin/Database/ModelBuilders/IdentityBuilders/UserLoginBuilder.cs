using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.ModelBuilders.IdentityBuilders
{
    public class UserLoginBuilder : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("user_logins");
        }
    }
}
