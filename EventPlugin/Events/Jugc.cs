using Smod2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.API;
using scp4aiur;

namespace ATTG3
{
	class Jugc : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public Jugc(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin = plugin;
		}
		public string GetCommandDescription()
		{
			return "";
		}

		public string GetUsage()
		{
			return "Juggernaut Enabled : " + ATTG3Plugin.enabledjug + "\n" +
				"[JUGGERNAUT / JUGG / JUG] HELP \n" +
				"JUGGERNAUT ENABLE \n" +
				"JUGGERNAUT DISABLE \n" +
				"JUGGERNAUT SELECT [PlayerName]";
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

			if (args.Length > 0)
			{
				switch (args[0].ToLower())
				{
					case "help":
						return new string[]
						{
						"Juggernaut Command List \n"+
						"Juggernaut enable - Enables the Juggernaut gamemode. \n"+
						"Juggernaut disable - Disables the Juggernaut gamemode. \n"+
						"Juggernaut select [PlayerName] - Selects the player to be the Juggernaut"
						};
					case "enable":
						ATTG3Plugin.EnableGamemodejug();
						return new string[]
						{
							"Juggernaut will be enabled for the next round!"
						};
					case "disable":
						ATTG3Plugin.DisableGamemodejug();
						return new string[]
						{
							"Juggernaut will be disabled for the next round!"
						};
					case "select":
						if (ATTG3Plugin.enabledjug && args.Length > 1)
						{
							Player myplayer = GetPlayerFromString.GetPlayer(args[1]);
							if (myplayer == null)
							{
								return new string[] { " Couldn't get player: " + args[1] };
							}
							ATTG3Plugin.selectedJuggernaut = myplayer;
							ATTG3Plugin.plugin.Info("" + myplayer.Name + " chosen as the Juggernaut!");
							return new string[] { " Player " + myplayer.Name + " selected as the next Juggernaut!" };
						}
						return new string[] { "A player name must be specified, and the gamemode must be enabled!" };
					default:
						return new string[]
						{
							GetUsage()
						};
				}
			}
			else
			{
				return new string[]
				{
					GetUsage()
				};
			}
		}
	}
}
