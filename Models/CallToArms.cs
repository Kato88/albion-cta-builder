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
                Title = "Camlann",
                InternalName = "T8_2H_MACE_MORGANA"
            });

            Roles.Add(new Role {
                Category = "Tank",
                Title = "Grovekeeper",
                InternalName = "T8_2H_RAM_KEEPER"
            });

            Roles.Add(new Role{
                Category = "Tank",
                Title = "Soulscythe",
                InternalName = "T8_2H_TWINSCYTHE_HELL"
            });

            Roles.Add(new Role {
                Category = "Heal",
                Title = "Holy",
                InternalName = "T8_2H_HOLYSTAFF_HELL"
            });

            Roles.Add(new Role {
                Category = "Heal",
                Title = "Nature",
                InternalName = "T8_2H_NATURESTAFF_KEEPER"
            });

            Roles.Add(new Role {
                Category = "Melee",
                Title = "Clarent Blade",
                InternalName = "T8_MAIN_SCIMITAR_MORGANA"
            });

            Roles.Add(new Role{
                Category = "Melee",
                Title = "Halberd",
                InternalName = "T8_2H_HALBERD"
            });

            Roles.Add(new Role{
                Category = "Melee",
                Title = "Gala",
                InternalName = "T8_2H_DUALSCIMITAR_UNDEAD"
            });

            Roles.Add(new Role{
                Category = "Melee",
                Title = "Bloodletter",
                InternalName = "T8_MAIN_RAPIER_MORGANA"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Brimstone",
                InternalName = "T8_2H_FIRESTAFF_HELL"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Siegebow",
                InternalName = "T8_2H_CROSSBOWLARGE_MORGANA"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Wailing Bow",
                InternalName = "T8_2H_BOW_HELL"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Permafrost",
                InternalName = "T8_2H_ICECRYSTAL_UNDEAD"
            });

            Roles.Add(new Role{
                Category = "Ranged",
                Title = "Weeping",
                InternalName = "T8_2H_REPEATINGCROSSBOW_UNDEAD"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Locus",
                InternalName = "T8_2H_ENIGMATICORB_MORGANA"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Occult",
                InternalName = "T8_2H_ARCANESTAFF_HELL"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Enigmatic",
                InternalName = "T8_2H_ENIGMATICSTAFF"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Icicle",
                InternalName = "T8_2H_ICEGAUNTLETS_HELL"
            });

            Roles.Add(new Role{
                Category = "Support",
                Title = "Damnation",
                InternalName = "T8_2H_CURSEDSTAFF_MORGANA"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Command Mammoth",
                InternalName = "T8_MOUNT_MAMMOTH_BATTLE"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Beetle",
                InternalName = "UNIQUE_MOUNT_BEETLE_SILVER"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Ballista",
                InternalName = "T6_MOUNT_SIEGE_BALLISTA"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Basilisk",
                InternalName = "T7_MOUNT_ARMORED_SWAMPDRAGON_BATTLE"
            });

            Roles.Add(new Role {
                Category = "Battlemount",
                Title = "Eagle",
                InternalName = "UNIQUE_MOUNT_ARMORED_EAGLE_CRYSTAL"
            });
        }
    }
}