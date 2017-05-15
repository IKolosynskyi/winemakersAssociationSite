using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WinemakersAssociation.Data.Entities;

namespace WinemakersAssociation.Persistence.Common
{
    public class MainContext : IdentityDbContext<UserEntity, UserRoleEntity, long>
    {
        public MainContext(DbContextOptions options) : base(options)
        {
        }

        public new DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<PageContentEntity> PageContents { get; set; }

        public virtual DbSet<PageEntity> Pages { get; set; }

        public new virtual DbSet<UserRoleEntity> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageEntity>().HasMany(e => e.Contents).WithOne(e => e.Page);

            base.OnModelCreating(modelBuilder);
        }

    }
}