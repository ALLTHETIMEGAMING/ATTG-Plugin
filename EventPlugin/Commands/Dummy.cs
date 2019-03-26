using System.Linq;
using Smod2.EventHandlers;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using UnityEngine;
using UnityEngine.Networking;
using Object = System.Object;

namespace ATTG3
{
	class Test : NetworkBehaviour, ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public GameObject local;
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
				GameObject player2 = (GameObject)myPlayer.GetGameObject();
				GameObject Test = Instantiate(player2);
				PlayerList.AddPlayer(Test);




			}
				
			return new[]
			{
				$""
			};
		}
	}
}
