using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
	class door : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public door(ATTG3Plugin plugin) => this.plugin=plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "";
        //Variables Below
        GameObject door2;
		int count;
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server)&&
				sender is Player player&&
				!plugin.Allrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			if (args.Length>0)
			{
				Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
				if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
				if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
				{
					foreach (Door door in Object.FindObjectsOfType<Door>())
					{
						count++;
						if (count==1)
						{
							GameObject player1 = (GameObject)myPlayer.GetGameObject();
                            door.localPos = player1.transform.position;
                            door.transform.SetPositionAndRotation(player1.transform.position, Quaternion.identity);
                            door.UpdatePos();
                        }
					}
					count=0;
				}
			}
			return new string[] { "Door Moved" };
		}
	}
}
