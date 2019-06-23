using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
    class Sniper : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        public Sniper(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Changes EL2 Gun";
        public static readonly string[] CA = new string[] { "snipe", "agsnipe" };
		public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
            else
            {
                if (Server.GetPlayers().Count<1)
                    return new string[] { "The server is empty!" };
                Player caller = (sender is Player send) ? send : null;
                if (args.Length>0)
                {
                    Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                    if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                    if (myPlayer.TeamRole.Role!=Role.SPECTATOR && myPlayer.TeamRole.Team != Smod2.API.Team.SCP)
                    {
                        
                        GameObject sniper = (GameObject)myPlayer.GetGameObject();
						Inventory sniperinv = sniper.GetComponent<Inventory>();
                        WeaponManager manager = sniper.GetComponent<WeaponManager>();
						int i = WeaponManagerIndex(manager, 20);
						sniperinv.AddNewItem(20, manager.weapons[i].maxAmmo, 4,3,1);
                        sniperinv.AddNewItem(20, manager.weapons[i].maxAmmo, 1, 4, 1);

                        //sniperinv.AddNewItem(20, -1f, 4, 3, 1);
                        //SetPickup(num, -4.65664672E+11f, component.get_transform().get_position(), component.get_transform().get_rotation(), 0, 0, 0).GetComponent<Pickup>();
                        /*Inventory.SyncItemInfo syncItem = sniperinv.items[20];
						syncItem.modSight = 4;
						syncItem.modBarrel = 3;
						syncItem.modOther = 1;*/


                        return new string[] { myPlayer.Name+" Sniper Added" };
                    }
                    else
                        return new string[] { myPlayer.Name+" is dead or is a SCP" };
                }
                else
                {
                    return new string[] { "SNIPE: " + GetUsage() };
                }
            }

        }
		public static int WeaponManagerIndex(WeaponManager manager, int item)
		{
			// Get weapon index in WeaponManager
			int weapon = -1;
			for (int i = 0; i < manager.weapons.Length; i++)
			{
				if (manager.weapons[i].inventoryID == item)
				{
					weapon = i;
				}
			}

			return weapon;
		}
	}
}
