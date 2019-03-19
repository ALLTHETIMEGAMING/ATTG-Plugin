using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace ATTG3
{
	class Data : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		
		public Data(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin = plugin;
		}
		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "";
		}
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.SCPrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			plugin.ServerData = !plugin.ServerData;
				return new[]
			{
				$"Data Grabing is {(plugin.ServerData ? "on" : "off")}."
			};
			
			
		}
	}
}
