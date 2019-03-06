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
	class CIMTFC : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public CIMTFC(ATTG3Plugin plugin)
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
			if (args.Length > 0)
			{
				switch (args[0].ToLower())
				{
					case "help":
						return new string[]
						{
							"AGCIMTF Command List \n"+
							"AGCIMTF enable - Enables the CI VS MTF gamemode. \n"+
							"AGCIMTF disable - Disables the CI VS MTF gamemode. \n"
						};
					case "enable":
						ATTG3Plugin.Functions.EnableGamemode();
						return new string[]
						{
							"CIMTF will be enabled for the next round!"
						};
					case "disable":
						ATTG3Plugin.Functions.DisableGamemode();
						return new string[]
						{
							"CIMTF gamemode now disabled."
						};
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
