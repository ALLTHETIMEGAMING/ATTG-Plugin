using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using Smod2;
using MEC;
using Smod2.EventHandlers;
using Smod2.Events;
namespace ATTG3
{
	class Overcharge : ICommandHandler
	{
		public float converted;
		private readonly ATTG3Plugin plugin;
		public Overcharge(ATTG3Plugin plugin)
		{
			
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			
			return "";
		}
		public string GetUsage()
		{
			
			return "HCZ Overcharge";
		}
        public static readonly string[] CA = new string[] { "AGLIGHTS", "LIGHTS" };
        public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server)&&
				sender is Player player&&
				!plugin.AdminRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			plugin.Lights=!plugin.Lights;
			if (plugin.Lights)
			{
				foreach (Player player2 in PluginManager.Manager.Server.GetPlayers())
				{
					if (!player2.HasItem(ItemType.FLASHLIGHT))
					{
						player2.GiveItem(ItemType.FLASHLIGHT);
					}
				}
					Timing.RunCoroutine(Events.LightsOut());
			}
			return new[]
			{
				$"Lights {(plugin.Lights ? "Deactavated" : "Actavated")}."
			};
		}
	}
}
