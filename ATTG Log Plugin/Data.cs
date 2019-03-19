using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;

namespace ATTG_Test
{
	class Data : ICommandHandler
	{
		private readonly ATTGLogPlugin plugin;
		
		public Data(ATTGLogPlugin plugin)
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
				!plugin.AdminRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			plugin.ServerData = !plugin.ServerData;
				return new[]
			{
				$"Camera Pos Grabing is {(plugin.ServerData ? "on" : "off")}."
			};
			
			
		}
	}
}
