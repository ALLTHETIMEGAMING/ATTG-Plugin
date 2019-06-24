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
				if (args.Length>0)
				{
					converted=float.Parse(args[0]);
				}
				else
				{
                    
					converted=3f;
				}
				foreach (Player player2 in PluginManager.Manager.Server.GetPlayers())
				{
					if (!player2.HasItem(ItemType.FLASHLIGHT))
					{
						player2.GiveItem(ItemType.FLASHLIGHT);
					}
				}
					Timing.RunCoroutine(TimingDelay(converted));
			}
			return new[]
			{
				$"Lights {(plugin.Lights ? "Deactavated" : "Actavated")}."
			};
		}
        public IEnumerator<float> TimingDelay(float time)
		{
			while (plugin.Lights)
			{
				Generator079.generators[0].CallRpcOvercharge();
				foreach (Room room in PluginManager.Manager.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Where(x => x.ZoneType==ZoneType.LCZ).ToArray())
				{
					room.FlickerLights();
				}
				yield return converted;
			}
		}
	}
}
