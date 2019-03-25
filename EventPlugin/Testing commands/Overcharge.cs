﻿using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using Smod2;
using scp4aiur;
using Smod2.EventHandlers;
using Smod2.Events;
namespace ATTG3
{
	class Overcharge : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		bool running;
		public Overcharge(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "HCZ Overcharge";
		}
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server)&&
				sender is Player player&&
				!plugin.SCPrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			running=!running;
			if (running)
			{
				Timing.Run(TimingRunLights(PluginManager.Manager.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Where(x => x.ZoneType==ZoneType.LCZ).ToArray()));
			}
			return new[]
			{
				$"Overcharge Actavated."
			};
		}

		private IEnumerable<float> TimingRunLights(IReadOnlyList<Room> rooms)
		{
			while (running)
			{
				Generator079.generators[0].CallRpcOvercharge();
				foreach (Room room in rooms)

				{
					room.FlickerLights();
				}

				yield return 3f;
			}
		}
	}
}
