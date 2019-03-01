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
	class TleslR : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		private bool running;
		public float Desplay = 0f;
		public TleslR(ATTG3Plugin plugin)
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
				!plugin.ValidLightsOutRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			
			running = !running;
			if (running)
			{
				
				foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
				{
					TeslaGate.TriggerDistance = 0;
				}
			}
			if (!running)
			{
				foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
				{
					TeslaGate.TriggerDistance = 5.5f;
				}
			}
			return new[]
			{
				$"all Tleslas are now {(running ? "DEACTAVATED" : "ACTAVATED")}."
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
