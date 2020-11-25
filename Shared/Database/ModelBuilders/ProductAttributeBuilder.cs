using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Mvc.Entities;

namespace Shared.Database.ModelBuilders
{
    public class ProductAttributeBuilder : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasKey(option => new { option.ProductId, option.AttributeId});
        }
    }
}
