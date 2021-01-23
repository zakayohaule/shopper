using System.ComponentModel.DataAnnotations.Schema;
using ShopperAdmin.Mvc.Entities.BaseEntities;

namespace ShopperAdmin.Mvc.Entities
{
    [Table("databases")]
    public class Database : BaseEntity<short>
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("connection_string")]
        public string ConnectionString { get; set; }
    }
}
