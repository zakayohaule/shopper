using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Mvc.Entities;

namespace Shared.Database.ModelBuilders
{
    public class AttributeBuilder : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {

        }
    }
}
