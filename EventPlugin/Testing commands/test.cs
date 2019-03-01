﻿/*using Smod2.Commands;
using Smod2;
using Smod2.API;
using System.Linq;
using System.Collections.Generic;

namespace ATTG3C:\Users\aaron\Desktop\scpsl mods\COIN MOD\References\
{
    class test : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;

        public test(ATTG3Plugin plugin) => this.plugin = plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";

        public string[] OnCall(ICommandSender sender, string[] args)
        {
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.ValidLightsOutRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}

			if (Server.GetPlayers().Count < 1)
                return new string[] { "The server is empty!" };

			Player caller = (sender is Player send) ? send : null;
            

            if (args.Length > 0)
            {
                if (args[0].ToLower() == "all" || args[0].StartsWith("*"))
                {
                   
                    foreach (Player p in Server.GetPlayers())
                    {
						p.SetAmmo(AmmoType.DROPPED_5, 100000);
						p.SetAmmo(AmmoType.DROPPED_7, 100000);
						p.SetAmmo(AmmoType.DROPPED_9, 100000);

					}
                    
                }
                Player myPlayer = ATTG3.GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
                if (myPlayer.TeamRole.Role != Role.SPECTATOR)
                {
					myPlayer.SetAmmo(AmmoType.DROPPED_5, 100000);
					myPlayer.SetAmmo(AmmoType.DROPPED_7, 100000);
					myPlayer.SetAmmo(AmmoType.DROPPED_9, 100000);
					return new string[] { myPlayer.Name + " has been given ammo!" };
                }
                else
                    return new string[] { myPlayer.Name + " is dead!" };
            }
            else
            {
                return new string[] { GetUsage() };
            }
        }
    }
}
*/