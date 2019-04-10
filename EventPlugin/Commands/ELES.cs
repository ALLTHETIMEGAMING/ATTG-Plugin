using Smod2.API;
using Smod2.Commands;
using System;
using System.Linq;

namespace ATTG3
{
	class ELELS : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public ELELS(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "Sets Elevators Speed";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "AGELS";
		}
		public string[] OnCall(ICommandSender sender, String[] args)
		{
			float Desplay2 = 0f;
			if (!(sender is Server)&&
				sender is Player player&&
				!plugin.AdminRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			if (args.Length>0)
			{
				foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
				{
					float converted = float.Parse(args[0]);
					Elevator.MovingSpeed=converted;
					Desplay2=converted;
				}
			}
			else
			{
				foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
				{
					Elevator.MovingSpeed=5;
					Desplay2=5;
				}
			}
			return new[]
			{
				$"Elevator Speed set to {(Desplay2)}"
			};
		}
	}
}
