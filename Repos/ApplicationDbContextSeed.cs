using System;
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
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Leader.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Officer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Member.ToString()));

            //Seed Default User
            var defaultUser = new ApplicationUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }

            await dbContext.CtaRoles.AddAsync(new Role {
                Id = Guid.NewGuid(),
                Title = "Camlann",
                Category = "Tank",
                InternalName = "T8_asd",
                IconUrl  = ""
            });

            await dbContext.CtaRoles.AddAsync(new Role {
                Id = Guid.NewGuid(),
                Title = "Grovekeeper",
                Category = "Tank",
                InternalName = "T8_asd",
                IconUrl  = ""
            });
        }
    }
}