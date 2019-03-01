using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.Commands;
using Smod2.API;
using scp4aiur;

namespace ATTG3
{
	class Citem : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		private bool running;
		public Citem(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "tlesla";
		}

		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "tlesla";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.Customitemrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			running = !running;
			if (plugin.Citems == true)
			{
                plugin.Citems = false;
            }
            else if (plugin.Citems == false)
            {
                plugin.Citems = true;
            }
            return new[]
			{
				$"all Tleslas are now {(running ? "on" : "off")}."
			};

		}
	}
}
