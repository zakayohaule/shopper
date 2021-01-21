using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopperAdmin.Database.ModelBuilders
{
    public class DatabaseBuilder : IEntityTypeConfiguration<Mvc.Entities.Database>
    {
        public void Configure(EntityTypeBuilder<Mvc.Entities.Database> builder)
        {
            builder.HasIndex(d => d.ConnectionString).IsUnique();
        }
    }
}
