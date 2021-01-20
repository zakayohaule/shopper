using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Mvc.Entities.Identity;

namespace Shared.Database.ModelBuilders.IdentityBuilders
{
    public class RoleClaimBuilder : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("role_claims");
            builder.HasAlternateKey(claim => new {claim.RoleId, claim.ClaimValue});
        }
    }
}