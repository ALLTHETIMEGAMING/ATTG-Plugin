﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.Commands;
using Smod2.API;
using scp4aiur;
using Smod2;
using ServerMod2.API;
using ItemManager;
using UnityEngine;
using System.Threading;
using System.Collections;
namespace ATTG3
{
	class C106 : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		private bool running;
		public C106(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "Disables 106 Containment";
		}

		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "Disables 106 Containment";
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

			running = !running;
			if (running)
			{
				Smod2.PluginManager.Manager.Server.Map.FemurBreaker(false);
			}
			if (!running)
			{
				Smod2.PluginManager.Manager.Server.Map.FemurBreaker(true);
			}
			return new[]
			{
				$"all SCP 106 is now {(running ? "Locked" : "Unlocked")}."
			};

		}
		private IEnumerable<float> TimingDelay(float time)
		{
			while (running)
			{
				foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
				{
					TeslaGate.Activate(true);
				}

				yield return 0.5f;
			}
		}
	}
}
