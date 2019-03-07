using Smod2.Commands;
using Smod2;
using Smod2.API;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using Smod2.EventHandlers;
using Smod2.EventSystem.Events;
using ServerMod2.API;
using ItemManager;
using UnityEngine;

namespace ATTG3
{
    class Up : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;

        public Up(ATTG3Plugin plugin) => this.plugin = plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Forces player up";

        public string[] OnCall(ICommandSender sender, string[] args)
        {
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.AdminRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			else
			{
				if (Server.GetPlayers().Count < 1)
					return new string[] { "The server is empty!" };

				Player caller = (sender is Player send) ? send : null;


				if (args.Length > 0)
				{

					Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
					if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
					if (myPlayer.TeamRole.Role != Role.SPECTATOR)
					{
						float x = myPlayer.GetPosition().x;
						float y = myPlayer.GetPosition().y;
						float z = myPlayer.GetPosition().z;
						float y2 = y + 40;
						Vector pos = new Vector(x, y2, z);

						myPlayer.Teleport(pos, false);
						return new string[] { myPlayer.Name + " has been moved up" };
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
}
