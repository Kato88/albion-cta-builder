using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Zorn.Models;
using System.Threading.Tasks;
using System;

namespace zergtool
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Role> CtaRoles { get; set; }
        public DbSet<CallToArms> Ctas { get; set; }

        public DbSet<QueuePlayer> QueuePlayers { get; set; }

        public DbSet<QueueRole> QueueRoles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<CallToArms>(entity => {
                entity.HasMany(x => x.Admins).WithOne(x => x.CallToArms).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(x => x.Parties).WithOne(x => x.Cta).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(x => x.Queue).WithOne(x => x.Cta).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<QueuePlayer>(entity =>
            {
                entity.HasMany(x => x.Roles).WithOne(x => x.QueuePlayer).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<QueueRole>().ToTable("QueueRole");
        }
    }
}