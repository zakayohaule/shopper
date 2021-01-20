using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopperAdmin.Mvc.Entities.Identity;

namespace ShopperAdmin.Database.ModelBuilders.IdentityBuilders
{
    public class UserTokenBuilder : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("user_tokens");
        }
    }
}
