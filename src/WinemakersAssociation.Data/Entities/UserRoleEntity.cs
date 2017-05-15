using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WinemakersAssociation.Data.Entities
{
    [Table("userrole")]
    public class UserRoleEntity : IdentityRole<long>, IBaseEntity
    {
        [Column("userrole_id")]
        public override long Id { get; set; }

        [Column("name")]
        public override string Name { get; set; }
    }
}