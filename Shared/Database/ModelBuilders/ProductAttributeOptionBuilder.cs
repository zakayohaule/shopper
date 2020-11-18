using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Mvc.Entities;

namespace Shared.Database.ModelBuilders
{
    public class ProductAttributeOptionBuilder : IEntityTypeConfiguration<ProductAttributeOption>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeOption> builder)
        {
            builder.HasKey(option => new { option.ProductId, option.AttributeOptionId});
        }
    }
}
