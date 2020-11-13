using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Mvc.Entities.BaseEntities
{
    public class SoftDeleteBaseEntity<T> : BaseEntity<T>
    {
        [Column("is_deleted")] 
        public bool IsDeleted { get; set; } = false;
    }
}