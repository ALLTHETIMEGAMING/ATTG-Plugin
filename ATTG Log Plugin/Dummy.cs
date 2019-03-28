using System.Linq;
using Smod2.EventHandlers;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTGL
{
	class Test : NetworkManager, ICommandHandler
	{
		private readonly ATTGLogPlugin plugin;
		public Test(ATTGLogPlugin plugin)
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

				Class @class = klasy[curClass];
				GameObject val = Object.Instantiate<GameObject>(@class.model_ragdoll);
				




			}

			return new[]
			{
				$"Object Created"
			};
		}
	}
}

