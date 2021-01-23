using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopper.Mvc.Entities;

namespace Shopper.Database.ModelBuilders
{
    public class SkuSellingPriceBuilder : IEntityTypeConfiguration<SkuSellingPrice>
    {
        public void Configure(EntityTypeBuilder<SkuSellingPrice> builder)
        {
            builder.HasKey(option => new { option.PriceTypeId, option.SkuId});
        }
    }
}
