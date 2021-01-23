using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopper.Mvc.Entities;

namespace Shopper.Database.ModelBuilders
{
    public class AttributeBuilder : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {

        }
    }
}
