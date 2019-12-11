using Smod2.API;
using Smod2.Commands;
using System;
using System.Linq;
namespace ATTG3
{
	class Nuketime : ICommandHandler
	{

		private readonly ATTG3Plugin plugin;
		public Nuketime(ATTG3Plugin plugin)
		{
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
		public static readonly string[] CA = new string[] { "AGLIGHTS", "LIGHTS" };
		public static bool Customnuketime = false;
		public static int Customtime = 120;
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.Allrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			if (args.Length > 0)
			{
				string args1 = args[0].ToLower();
				int Time = Int32.Parse(args1);
				AlphaWarheadController.host.NetworktimeToDetonation = Time;
				Customtime = Time;
				Customnuketime = true;
				return new[]
			{
				$"Nuke set to " + Time
			};
			}
			else
			{
				Customnuketime = false;
				return new[]
			{
				$"Nuke reset"
			};
			}

		}
	}
}
