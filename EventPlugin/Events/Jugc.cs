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
			return "AGJUG Enabled : " + plugin.Jug + "\n" +
				"[AGJUG / JUGG / JUG] HELP \n" +
				"AGJUG ENABLE \n" +
				"AGJUG DISABLE \n" +
				"AGJUG SELECT [PlayerName]";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{


			if (args.Length > 0)
			{
				switch (args[0].ToLower())
				{
					case "help":
						return new string[]
						{
						"AGJUG Command List \n"+
						"AGJUG enable - Enables the Juggernaut gamemode. \n"+
						"AGJUG disable - Disables the Juggernaut gamemode. \n"+
						"AGJUG select [PlayerName] - Selects the player to be the Juggernaut"
						};
					case "enable":
						plugin.Jug = true;
						return new string[]
						{
							"Juggernaut will be enabled for the next round!"
						};
					case "disable":
						plugin.Jug = false;
						return new string[]
						{
							"Juggernaut will be disabled for the next round!"
						};
					case "select":
						if (plugin.Jug && args.Length > 1)
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
