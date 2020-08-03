using System;
using System.Collections.Generic;

namespace Zorn.Models {
    public class CallToArms 
    {
        public Guid Id {get; set;}
        public string Title {get; set;}

        public List<Role> Roles {get; set;}

        public CallToArms()
        {
            Id = Guid.NewGuid();
            Roles = new List<Role>();
            InitRoles();
        }

        private void InitRoles() 
        {
            Roles.Add(new Role {
                Category = "Tanks",
                Title = "Camlann"
            });

            Roles.Add(new Role {
                Category = "Tanks",
                Title = "Grovekepper",
            });

            Roles.Add(new Role{
                Category = "Tanks",
                Title = "Soulscythe"
            });

            Roles.Add(new Role {
                Category = "Tanks",
                Title = "Other",
            });

            Roles.Add(new Role {
                Category = "Heal",
                Title = "Holy"
            });

            Roles.Add(new Role {
                Category = "Heal",
                Title = "Nature",
            });

            Roles.Add(new Role {
                Category = "Heal",
                Title = "Other"
            });

            Roles.Add(new Role {
                Category = "Bruiser",
                Title = "Clerant Blade"
            });

            Roles.Add(new Role{
                Category = "Bruiser",
                Title = "Hellebard"
            });

            Roles.Add(new Role{
                Category = "Bruiser",
                Title = "Gala"
            });

            Roles.Add(new Role{
                Category = "Bruiser",
                Title = "Other"
            });

            Roles.Add(new Role{
                Category = "Clapper",
                Title = "Brimstone"
            });

            Roles.Add(new Role{
                Category = "Clapper",
                Title = "Siegebow"
            });

            Roles.Add(new Role{
                Category = "Clapper",
                Title = "Wailing Bow"
            });

            Roles.Add(new Role{
                Category = "Clapper",
                Title = "Permafrost"
            });

            Roles.Add(new Role{
                Category = "Clapper",
                Title = "Weeping"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Locus"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Occult"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Icycle"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Other"
            });
        }
    }
}