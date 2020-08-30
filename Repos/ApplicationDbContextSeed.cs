using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Zorn.Models;

namespace zergtool
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            await CreateRoles(dbContext);

            if (roleManager.Roles.Count() != 4)
            {
                //Seed Roles
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Leader.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Officer.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Member.ToString()));
            }

            //Seed Default User
            var defaultUser = new ApplicationUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }

        public static async Task CreateRoles(ApplicationDbContext dbContext)
        {
            var existingRoles = dbContext.CtaRoles.ToList();

            await CreateIfNotExists(dbContext, existingRoles, new Role
            {
                Id = Guid.NewGuid(),
                Title = "Camlann",
                Category = "Tank",
                InternalName = "T8_asd",
                IconUrl = ""
            });

            await CreateIfNotExists(dbContext, existingRoles, new Role
            {
                Id = Guid.NewGuid(),
                Title = "Grovekeeper",
                Category = "Tank",
                InternalName = "T8_asd",
                IconUrl = ""
            });

            await dbContext.SaveChangesAsync();
        }

        private static async Task CreateIfNotExists(ApplicationDbContext dbContext, List<Role> existingRoles, Role role)
        {
            if (existingRoles.Any(x => x.InternalName == role.InternalName))
            {
                return;
            }

            await dbContext.CtaRoles.AddAsync(role);

            return;
        }
    }
}