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

            await CreateIfNotExists(dbContext, existingRoles, new List<Role>()
            {
                // swords
                new Role { Title = "Broadsword", Category  = "Melee", InternalName = "T8_MAIN_SWORD", IconUrl = "/img/roles/T8_MAIN_SWORD.png" },
                new Role { Title = "Claymore", Category  = "Melee", InternalName = "T8_2H_CLAYMORE", IconUrl = "/img/roles/T8_2H_CLAYMORE.png" },
                new Role { Title = "Dual Swords", Category  = "Melee", InternalName = "T8_2H_DUALSWORD", IconUrl = "/img/roles/T8_2H_DUALSWORD.png" },
                new Role { Title = "Clarent Blade", Category  = "Melee", InternalName = "T8_MAIN_SCIMITAR_MORGANA", IconUrl = "/img/roles/T8_MAIN_SCIMITAR_MORGANA.png" },
                new Role { Title = "Carving Sword", Category  = "Melee", InternalName = "T8_2H_CLEAVER_HELL", IconUrl = "/img/roles/T8_2H_CLEAVER_HELL.png" },
                new Role { Title = "Galatine Pair", Category  = "Melee", InternalName = "T8_2H_DUALSCIMITAR_UNDEAD", IconUrl = "/img/roles/T8_2H_DUALSCIMITAR_UNDEAD.png" },
                new Role { Title = "Kingmaker", Category  = "Melee", InternalName = "T8_2H_CLAYMORE_AVALON", IconUrl = "/img/roles/T8_2H_CLAYMORE_AVALON.png" },

                // axes
                new Role { Title = "Battleaxe", Category  = "Melee", InternalName = "T8_MAIN_AXE", IconUrl = "/img/roles/T8_MAIN_AXE.png" },
                new Role { Title = "Greataxe", Category  = "Melee", InternalName = "T8_2H_AXE", IconUrl = "/img/roles/T8_2H_AXE.png" },
                new Role { Title = "Halberd", Category  = "Melee", InternalName = "T8_2H_HALBERD", IconUrl = "/img/roles/T8_2H_HALBERD.png" },
                new Role { Title = "Carrioncaller", Category  = "Melee", InternalName = "T8_2H_HALBERD_MORGANA", IconUrl = "/img/roles/T8_2H_HALBERD_MORGANA.png" },
                new Role { Title = "Infernal Scythe", Category  = "Melee", InternalName = "T8_2H_SCYTHE_HELL", IconUrl = "/img/roles/T8_2H_SCYTHE_HELL.png" },
                new Role { Title = "Bear Paws", Category  = "Melee", InternalName = "T8_2H_DUALAXE_KEEPER", IconUrl = "/img/roles/T8_2H_DUALAXE_KEEPER.png" },

                // maces
                new Role { Title = "Mace", Category  = "Tank", InternalName = "T8_MAIN_MACE", IconUrl = "/img/roles/T8_MAIN_MACE.png" },
                new Role { Title = "Heavy Mace", Category  = "Tank", InternalName = "T8_2H_MACE", IconUrl = "/img/roles/T8_2H_MACE.png" },
                new Role { Title = "Morning Star", Category  = "Tank", InternalName = "T8_2H_FLAIL", IconUrl = "/img/roles/T8_2H_FLAIL.png" },
                new Role { Title = "Bedrock", Category  = "Tank", InternalName = "T8_MAIN_ROCKMACE_KEEPER", IconUrl = "/img/roles/T8_MAIN_ROCKMACE_KEEPER.png" },
                new Role { Title = "Incubus", Category  = "Tank", InternalName = "T8_MAIN_MACE_HELL", IconUrl = "/img/roles/T8_MAIN_MACE_HELL.png" },
                new Role { Title = "Camlann", Category  = "Tank", InternalName = "T8_2H_MACE_MORGANA", IconUrl = "/img/roles/T8_2H_MACE_MORGANA.png" },

                // hammers
                new Role { Title = "Hammer", Category  = "Tank", InternalName = "T8_MAIN_HAMMER", IconUrl = "/img/roles/T8_MAIN_HAMMER.png" },
                new Role { Title = "Polehammer", Category  = "Tank", InternalName = "T8_2H_POLEHAMMER", IconUrl = "/img/roles/T8_2H_POLEHAMMER.png" },
                new Role { Title = "Great Hammer", Category  = "Tank", InternalName = "T8_2H_HAMMER", IconUrl = "/img/roles/T8_2H_HAMMER.png" },
                new Role { Title = "Tombhammer", Category  = "Tank", InternalName = "T8_2H_HAMMER_UNDEAD", IconUrl = "/img/roles/T8_2H_HAMMER_UNDEAD.png" },
                new Role { Title = "Forge Hammers", Category  = "Tank", InternalName = "T8_2H_DUALHAMMER_HELL", IconUrl = "/img/roles/T8_2H_DUALHAMMER_HELL.png" },
                new Role { Title = "Grovekeeper", Category  = "Tank", InternalName = "T8_2H_RAM_KEEPER", IconUrl = "/img/roles/T8_2H_RAM_KEEPER.png" },

                // crossbows
                new Role { Title = "Crossbow", Category  = "Ranged", InternalName = "T8_2H_CROSSBOW", IconUrl = "/img/roles/T8_2H_CROSSBOW.png" },
                new Role { Title = "Heavy Crossbow", Category  = "Ranged", InternalName = "T8_2H_CROSSBOWLARGE", IconUrl = "/img/roles/T8_2H_CROSSBOWLARGE.png" },
                new Role { Title = "Light Crossbow", Category  = "Ranged", InternalName = "T8_MAIN_1HCROSSBOW", IconUrl = "/img/roles/T8_MAIN_1HCROSSBOW.png" },
                new Role { Title = "Weeping Repeater", Category  = "Ranged", InternalName = "T8_2H_REPEATINGCROSSBOW_UNDEAD", IconUrl = "/img/roles/T8_2H_REPEATINGCROSSBOW_UNDEAD.png" },
                new Role { Title = "Boltcasters", Category  = "Ranged", InternalName = "T8_2H_DUALCROSSBOW_HELL", IconUrl = "/img/roles/T8_2H_DUALCROSSBOW_HELL.png" },
                new Role { Title = "Siegebow", Category  = "Ranged", InternalName = "T8_2H_CROSSBOWLARGE_MORGANA", IconUrl = "/img/roles/T8_2H_CROSSBOWLARGE_MORGANA.png" },
                new Role { Title = "Energy Shaper", Category  = "Ranged", InternalName = "T8_2H_CROSSBOW_CANNON_AVALON", IconUrl = "/img/roles/T8_2H_CROSSBOW_CANNON_AVALON.png" },

                // bows
                new Role { Title = "Bow", Category  = "Ranged", InternalName = "T8_2H_BOW", IconUrl = "/img/roles/T8_2H_BOW.png" },
                new Role { Title = "Warbow", Category  = "Ranged", InternalName = "T8_2H_WARBOW", IconUrl = "/img/roles/T8_2H_WARBOW.png" },
                new Role { Title = "Longbow", Category  = "Ranged", InternalName = "T8_2H_LONGBOW", IconUrl = "/img/roles/T8_2H_LONGBOW.png" },
                new Role { Title = "Whispering Bow", Category  = "Ranged", InternalName = "T8_2H_LONGBOW_UNDEAD", IconUrl = "/img/roles/T8_2H_LONGBOW_UNDEAD.png" },
                new Role { Title = "Wailing Bow", Category  = "Ranged", InternalName = "T8_2H_BOW_HELL", IconUrl = "/img/roles/T8_2H_BOW_HELL.png" },
                new Role { Title = "Bow of Badon", Category  = "Ranged", InternalName = "T8_2H_BOW_KEEPER", IconUrl = "/img/roles/T8_2H_BOW_KEEPER.png" },
                new Role { Title = "Mistpiercer", Category  = "Ranged", InternalName = "T8_2H_BOW_AVALON", IconUrl = "/img/roles/T8_2H_BOW_AVALON.png" },

                // spears
                new Role { Title = "Spear", Category  = "Melee", InternalName = "T8_MAIN_SPEAR", IconUrl = "/img/roles/T8_MAIN_SPEAR.png" },
                new Role { Title = "Pike", Category  = "Melee", InternalName = "T8_2H_SPEAR", IconUrl = "/img/roles/T8_2H_SPEAR.png" },
                new Role { Title = "Glaive", Category  = "Melee", InternalName = "T8_2H_GLAIVE", IconUrl = "/img/roles/T8_2H_GLAIVE.png" },
                new Role { Title = "Heron Spear", Category  = "Melee", InternalName = "T8_MAIN_SPEAR_KEEPER", IconUrl = "/img/roles/T8_MAIN_SPEAR_KEEPER.png" },
                new Role { Title = "Spirithunter", Category  = "Melee", InternalName = "T8_2H_HARPOON_HELL", IconUrl = "/img/roles/T8_2H_HARPOON_HELL.png" },
                new Role { Title = "Trinity Spear", Category  = "Melee", InternalName = "T8_2H_TRIDENT_UNDEAD", IconUrl = "/img/roles/T8_2H_TRIDENT_UNDEAD.png" },

                // nature
                new Role { Title = "Nature Staff", Category  = "Heal", InternalName = "T8_MAIN_NATURESTAFF", IconUrl = "/img/roles/T8_MAIN_NATURESTAFF.png" },
                new Role { Title = "Great Nature Staff", Category  = "Heal", InternalName = "T8_2H_NATURESTAFF", IconUrl = "/img/roles/T8_2H_NATURESTAFF.png" },
                new Role { Title = "Wild Staff", Category  = "Heal", InternalName = "T8_2H_WILDSTAFF", IconUrl = "/img/roles/T8_2H_WILDSTAFF.png" },
                new Role { Title = "Druidic Staff", Category  = "Heal", InternalName = "T8_MAIN_NATURESTAFF_KEEPER", IconUrl = "/img/roles/T8_MAIN_NATURESTAFF_KEEPER.png" },
                new Role { Title = "Blight Staff", Category  = "Heal", InternalName = "T8_2H_NATURESTAFF_HELL", IconUrl = "/img/roles/T8_2H_NATURESTAFF_HELL.png" },
                new Role { Title = "Rampant Staff", Category  = "Heal", InternalName = "T8_2H_NATURESTAFF_KEEPER", IconUrl = "/img/roles/T8_2H_NATURESTAFF_KEEPER.png" },

                // daggers
                new Role { Title = "Dagger", Category  = "Melee", InternalName = "T8_MAIN_DAGGER", IconUrl = "/img/roles/T8_MAIN_DAGGER.png" },
                new Role { Title = "Dagger Pair", Category  = "Melee", InternalName = "T8_2H_DAGGERPAIR", IconUrl = "/img/roles/T8_2H_DAGGERPAIR.png" },
                new Role { Title = "Claws", Category  = "Melee", InternalName = "T8_2H_CLAWPAIR", IconUrl = "/img/roles/T8_2H_CLAWPAIR.png" },
                new Role { Title = "Bloodletter", Category  = "Melee", InternalName = "T8_MAIN_RAPIER_MORGANA", IconUrl = "/img/roles/T8_MAIN_RAPIER_MORGANA.png" },
                new Role { Title = "Black Hands", Category  = "Melee", InternalName = "T8_2H_IRONGAUNTLETS_HELL", IconUrl = "/img/roles/T8_2H_IRONGAUNTLETS_HELL.png" },
                new Role { Title = "Deathgivers", Category  = "Melee", InternalName = "T8_2H_DUALSICKLE_UNDEAD", IconUrl = "/img/roles/T8_2H_DUALSICKLE_UNDEAD.png" },

                // quarter staff
                new Role { Title = "Quarterstaff", Category  = "Tank", InternalName = "T8_2H_QUARTERSTAFF", IconUrl = "/img/roles/T8_2H_QUARTERSTAFF.png" },
                new Role { Title = "Iron-clad Staff", Category  = "Tank", InternalName = "T8_2H_IRONCLADEDSTAFF", IconUrl = "/img/roles/T8_2H_IRONCLADEDSTAFF.png" },
                new Role { Title = "Double Bladed Staff", Category  = "Tank", InternalName = "T8_2H_DOUBLEBLADEDSTAFF", IconUrl = "/img/roles/T8_2H_DOUBLEBLADEDSTAFF.png" },
                new Role { Title = "Black Monk Stave", Category  = "Tank", InternalName = "T8_2H_COMBATSTAFF_MORGANA", IconUrl = "/img/roles/T8_2H_COMBATSTAFF_MORGANA.png" },
                new Role { Title = "Soulscythe", Category  = "Tank", InternalName = "T8_2H_TWINSCYTHE_HELL", IconUrl = "/img/roles/T8_2H_TWINSCYTHE_HELL.png" },
                new Role { Title = "Staff of Balance", Category  = "Tank", InternalName = "T8_2H_ROCKSTAFF_KEEPER", IconUrl = "/img/roles/T8_2H_ROCKSTAFF_KEEPER.png" },

                // fire staves
                new Role { Title = "Fire Staff", Category  = "Ranged", InternalName = "T8_MAIN_FIRESTAFF", IconUrl = "/img/roles/T8_MAIN_FIRESTAFF.png" },
                new Role { Title = "Great Fire Staff", Category  = "Ranged", InternalName = "T8_2H_FIRESTAFF", IconUrl = "/img/roles/T8_2H_FIRESTAFF.png" },
                new Role { Title = "Infernal Staff", Category  = "Ranged", InternalName = "T8_2H_INFERNOSTAFF", IconUrl = "/img/roles/T8_2H_INFERNOSTAFF.png" },
                new Role { Title = "Wildfire Staff", Category  = "Ranged", InternalName = "T8_MAIN_FIRESTAFF_KEEPER", IconUrl = "/img/roles/T8_MAIN_FIRESTAFF_KEEPER.png" },
                new Role { Title = "Brimstone Staff", Category  = "Ranged", InternalName = "T8_2H_FIRESTAFF_HELL", IconUrl = "/img/roles/T8_2H_FIRESTAFF_HELL.png" },
                new Role { Title = "Blazing Staff", Category  = "Ranged", InternalName = "T8_2H_INFERNOSTAFF_MORGANA", IconUrl = "/img/roles/T8_2H_INFERNOSTAFF_MORGANA.png" },
                new Role { Title = "Dawnsong", Category  = "Ranged", InternalName = "T8_2H_FIRE_RINGPAIR_AVALON", IconUrl = "/img/roles/T8_2H_FIRE_RINGPAIR_AVALON.png" },

                // holy staves
                new Role { Title = "Holy Staff", Category  = "Heal", InternalName = "T8_MAIN_HOLYSTAFF", IconUrl = "/img/roles/T8_MAIN_HOLYSTAFF.png" },
                new Role { Title = "Great Holy Staff", Category  = "Heal", InternalName = "T8_2H_HOLYSTAFF", IconUrl = "/img/roles/T8_2H_HOLYSTAFF.png" },
                new Role { Title = "Divine Staff", Category  = "Heal", InternalName = "T8_2H_DIVINESTAFF", IconUrl = "/img/roles/T8_2H_DIVINESTAFF.png" },
                new Role { Title = "Lifetouch Staff", Category  = "Heal", InternalName = "T8_MAIN_HOLYSTAFF_MORGANA", IconUrl = "/img/roles/T8_MAIN_HOLYSTAFF_MORGANA.png" },
                new Role { Title = "Fallen Staff", Category  = "Heal", InternalName = "T8_2H_HOLYSTAFF_HELL", IconUrl = "/img/roles/T8_2H_HOLYSTAFF_HELL.png" },
                new Role { Title = "Redemption Staff", Category  = "Heal", InternalName = "T8_2H_HOLYSTAFF_UNDEAD", IconUrl = "/img/roles/T8_2H_HOLYSTAFF_UNDEAD.png" },

                // arcane
                new Role { Title = "Arcane Staff", Category  = "Support", InternalName = "T8_MAIN_ARCANESTAFF", IconUrl = "/img/roles/T8_MAIN_ARCANESTAFF.png" },
                new Role { Title = "Great Arcane Staff", Category  = "Support", InternalName = "T8_2H_ARCANESTAFF", IconUrl = "/img/roles/T8_2H_ARCANESTAFF.png" },
                new Role { Title = "Enigmatic Staff", Category  = "Support", InternalName = "T8_2H_ENIGMATICSTAFF", IconUrl = "/img/roles/T8_2H_ENIGMATICSTAFF.png" },
                new Role { Title = "Witchwork Staff", Category  = "Support", InternalName = "T8_MAIN_ARCANESTAFF_UNDEAD", IconUrl = "/img/roles/T8_MAIN_ARCANESTAFF_UNDEAD.png" },
                new Role { Title = "Occult Staff", Category  = "Support", InternalName = "T8_2H_ARCANESTAFF_HELL", IconUrl = "/img/roles/T8_2H_ARCANESTAFF_HELL.png" },
                new Role { Title = "Malevolent Locus", Category  = "Support", InternalName = "T8_2H_ENIGMATICORB_MORGANA", IconUrl = "/img/roles/T8_2H_ENIGMATICORB_MORGANA.png" },

                // frost staves
                new Role { Title = "Frost Staff", Category  = "Ranged", InternalName = "T8_MAIN_FROSTSTAFF", IconUrl = "/img/roles/T8_MAIN_FROSTSTAFF.png" },
                new Role { Title = "Great Frost Staff", Category  = "Ranged", InternalName = "T8_2H_FROSTSTAFF", IconUrl = "/img/roles/T8_2H_FROSTSTAFF.png" },
                new Role { Title = "Glacial Staff", Category  = "Ranged", InternalName = "T8_2H_GLACIALSTAFF", IconUrl = "/img/roles/T8_2H_GLACIALSTAFF.png" },
                new Role { Title = "Hoarfrost Staff", Category  = "Ranged", InternalName = "T8_MAIN_FROSTSTAFF_KEEPER", IconUrl = "/img/roles/T8_MAIN_FROSTSTAFF_KEEPER.png" },
                new Role { Title = "Icicle Staff", Category  = "Support", InternalName = "T8_2H_ICEGAUNTLETS_HELL", IconUrl = "/img/roles/T8_2H_ICEGAUNTLETS_HELL.png" },
                new Role { Title = "Permafrost Prism", Category  = "Ranged", InternalName = "T8_2H_ICECRYSTAL_UNDEAD", IconUrl = "/img/roles/T8_2H_ICECRYSTAL_UNDEAD.png" },

                // cursed staves
                new Role { Title = "Cursed Staff", Category  = "Ranged", InternalName = "T8_MAIN_CURSEDSTAFF", IconUrl = "/img/roles/T8_MAIN_CURSEDSTAFF.png" },
                new Role { Title = "Great Cursed Staff", Category  = "Ranged", InternalName = "T8_2H_CURSEDSTAFF", IconUrl = "/img/roles/T8_2H_CURSEDSTAFF.png" },
                new Role { Title = "Demonic Staff", Category  = "Ranged", InternalName = "T8_2H_DEMONICSTAFF", IconUrl = "/img/roles/T8_2H_DEMONICSTAFF.png" },
                new Role { Title = "Lifecurse Staff", Category  = "Ranged", InternalName = "T8_MAIN_CURSEDSTAFF_UNDEAD", IconUrl = "/img/roles/T8_MAIN_CURSEDSTAFF_UNDEAD.png" },
                new Role { Title = "Cursed Skull", Category  = "Ranged", InternalName = "T8_2H_SKULLORB_HELL", IconUrl = "/img/roles/T8_2H_SKULLORB_HELL.png" },
                new Role { Title = "Damnation Skull", Category  = "Support", InternalName = "T8_2H_CURSEDSTAFF_MORGANA", IconUrl = "/img/roles/T8_2H_CURSEDSTAFF_MORGANA.png" },

                // mounts
                new Role { Title = "Command Mammoth", Category  = "Battlemount", InternalName = "T8_MOUNT_MAMMOTH_BATTLE", IconUrl = "/img/roles/T8_MOUNT_MAMMOTH_BATTLE.png" },
                new Role { Title = "Beetle", Category  = "Battlemount", InternalName = "UNIQUE_MOUNT_BEETLE_CRYSTAL", IconUrl = "/img/roles/UNIQUE_MOUNT_BEETLE_CRYSTAL.png" },
                new Role { Title = "Eagle", Category  = "Battlemount", InternalName = "UNIQUE_MOUNT_ARMORED_EAGLE_CRYSTAL", IconUrl = "/img/roles/UNIQUE_MOUNT_ARMORED_EAGLE_CRYSTAL.png" },
                new Role { Title = "Chariot", Category  = "Battlemount", InternalName = "UNIQUE_MOUNT_TOWER_CHARIOT_CRYSTAL", IconUrl = "/img/roles/UNIQUE_MOUNT_TOWER_CHARIOT_CRYSTAL.png" },
                new Role { Title = "Venom Basilisk", Category  = "Battlemount", InternalName = "T7_MOUNT_ARMORED_SWAMPDRAGON_BATTLE", IconUrl = "/img/roles/T7_MOUNT_ARMORED_SWAMPDRAGON_BATTLE.png" },
                new Role { Title = "Flame Basilisk", Category  = "Battlemount", InternalName = "T7_MOUNT_SWAMPDRAGON_BATTLE", IconUrl = "/img/roles/T7_MOUNT_SWAMPDRAGON_BATTLE.png" },
                new Role { Title = "Siege Ballista", Category  = "Battlemount", InternalName = "T6_MOUNT_SIEGE_BALLISTA", IconUrl = "/img/roles/T6_MOUNT_SIEGE_BALLISTA.png" },

            });

            await dbContext.SaveChangesAsync();
        }

        private static async Task CreateIfNotExists(ApplicationDbContext dbContext, List<Role> existingRoles, List<Role> roles)
        {
            foreach (var role in roles)
            {
                if (!existingRoles.Any(x => x.InternalName == role.InternalName))
                {
                    role.Id = Guid.NewGuid();
                    await dbContext.CtaRoles.AddAsync(role);
                }
            }

            return;
        }
    }
}