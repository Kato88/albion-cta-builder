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
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<CallToArms>().ToTable("Cta");
        }
    }
}