using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Shared.Mvc.Entities.Identity
{
    [Table("user_tokens")]
    public class UserToken : IdentityUserToken<long>
    {
        [Column("user_id")] public override long UserId { get; set; }

        [Column("login_provider")] public override string LoginProvider { get; set; }

        [Column("name")] public override string Name { get; set; }

        [Column("value")] public override string Value { get; set; }


        [ForeignKey(nameof(UserId))] public AppUser User { get; set; }
    }
}