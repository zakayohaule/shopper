using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Mvc.Entities;

namespace Shared.Database.ModelBuilders
{
    public class PriceTypeBuilder : IEntityTypeConfiguration<PriceType>
    {
        public void Configure(EntityTypeBuilder<PriceType> builder)
        {
        }
    }
}
