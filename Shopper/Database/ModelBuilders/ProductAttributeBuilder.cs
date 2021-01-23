using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopper.Mvc.Entities;

namespace Shopper.Database.ModelBuilders
{
    public class ProductAttributeBuilder : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasKey(option => new { option.ProductId, option.AttributeId});
        }
    }
}
