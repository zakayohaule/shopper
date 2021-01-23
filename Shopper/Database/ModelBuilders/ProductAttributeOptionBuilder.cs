using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopper.Mvc.Entities;

namespace Shopper.Database.ModelBuilders
{
    public class ProductAttributeOptionBuilder : IEntityTypeConfiguration<ProductAttributeOption>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeOption> builder)
        {
            builder.HasKey(option => new { option.ProductId, option.AttributeOptionId});
        }
    }
}
