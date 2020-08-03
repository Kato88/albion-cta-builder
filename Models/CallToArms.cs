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
                Category = "Tank",
                Title = "Camlann"
            });

            Roles.Add(new Role {
                Category = "Tank",
                Title = "Grovekeeper",
            });

            Roles.Add(new Role{
                Category = "Tank",
                Title = "Soulscythe"
            });

            Roles.Add(new Role {
                Category = "Tank",
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
                Category = "Melee",
                Title = "Clarent Blade"
            });

            Roles.Add(new Role{
                Category = "Melee",
                Title = "Halberd"
            });

            Roles.Add(new Role{
                Category = "Melee",
                Title = "Gala"
            });

            Roles.Add(new Role{
                Category = "Melee",
                Title = "Bloodletter"
            });

            Roles.Add(new Role{
                Category = "Melee",
                Title = "Other"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Brimstone"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Siegebow"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Wailing Bow"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Permafrost"
            });

            Roles.Add(new Role{
                Category = "Ranged",
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
                Title = "Enigmatic"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Icicle"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Damnation"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Other"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Command Mammoth"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Beetle"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Ballista"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Basilisk"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Eagle"
            });
        }
    }
}