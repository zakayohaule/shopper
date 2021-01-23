using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopper.Mvc.Entities.Identity;

namespace Shopper.Database.ModelBuilders.IdentityBuilders
{
    public class UserClaimBuilder : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("user_claims");
        }
    }
}
