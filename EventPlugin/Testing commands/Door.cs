using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
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
                    Door[] doors = Object.FindObjectsOfType<Door>();
                    Random rnd = new Random();
                    int index = Random.Range(0, doors.Length);
                    Door door = doors[index];
                    GameObject player1 = (GameObject)myPlayer.GetGameObject();
                    // Test 1
                    door.localPos = player1.transform.position;
                    //door.SetLocalPos();
                    //door.UpdatePos();
                    // Test 2
                    door.transform.SetPositionAndRotation(player1.transform.position, Quaternion.identity);
                    //door.UpdatePos();
                    // Test 3
                    door.transform.position = player1.transform.position;
                    //door.UpdatePos();
                }
			}
			return new string[] { "Door Moved" };
		}
	}
}
