using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shopper.Mvc.Entities.BaseEntities
{
    public class NoTenantBaseEntity<T>
    {
        [Column("id")]
        [Key]
        public T Id { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [BindNever]
        // [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [BindNever]
        // [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
