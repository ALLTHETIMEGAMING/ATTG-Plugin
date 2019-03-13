using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.Commands;
using Smod2.API;
using scp4aiur;

namespace ATTG4
{
	class Disable : ICommandHandler
	{
		private readonly ATTG4Plugin plugin;
		
		public Disable(ATTG4Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
            // This prints when someone types HELP HELLO
            return "Disable ATTG Item Plugin";
        }

		public string GetUsage()
		{
            // This prints when someone types HELP HELLO
            return "Disable ATTG Item Plugin";
        }

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.Disablerank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			else
			{
				plugin.Info(sender + " ran the " + GetUsage() + " command!");
				this.plugin.pluginManager.DisablePlugin(this.plugin);
			}
			return new[]
			{
				$"ATTG ITEM PLUGIN DISABLED"
			};

		}
	}
}
