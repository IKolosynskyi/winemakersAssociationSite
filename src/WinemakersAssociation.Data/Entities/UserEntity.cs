using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WinemakersAssociation.Data.Entities
{
    [Table("User")]
    public class UserEntity : IdentityUser<long>, IBaseEntity
    {
        [Column("user_id")]
        public override long Id { get; set; }


        public override string PasswordHash { get; set; }

        [NotMapped]
        public override string PhoneNumber { get; set; }

        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }

        [NotMapped]
        public override string ConcurrencyStamp { get; set; }

        [ForeignKey("userrole_id")]
        public UserRoleEntity Role { get; set; }
    }
}