using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace ATTG3
{
	class GenTime : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;

		public static TextMeshProUGUI Sign2 = new TextMeshProUGUI();

		public GenTime(ATTG3Plugin plugin)
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
			return "Sets All of SCP-079s Generators max time";
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
			if (args.Length > 0)
			{
				float converted = float.Parse(args[0]);

				foreach (Generator079 gen in Generator079.generators)
				{
					gen.NetworkremainingPowerup = (gen.startDuration = converted);
				}

				return new[]
				{
				$"Generator Time set to {converted}"
			};
			}
			else
			{
				return new string[] { "AG079T: " + GetUsage() };
			}
		}
	}
}
