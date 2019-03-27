using System.Linq;
using Smod2.EventHandlers;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
	class Test : NetworkManager, ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public Test(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return " ";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return " ";
		}
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
			Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
			if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; ; }
			if (args.Length>1)
			{

				Vector pos = myPlayer.GetPosition();
				Vector3 Spawnpoint = new Vector3(pos.x, pos.y, pos.z);
				GameObject Player = playerPrefab;
				Vector3 Spawn = transform.TransformPoint(Spawnpoint);
				GameObject Test = Instantiate(Player, Spawn.transform.TransformPoint);
				
				NetworkServer.Spawn(Test);
				



			}
				
			return new[]
			{
				$"Object Created"
			};
		}
	}
}
