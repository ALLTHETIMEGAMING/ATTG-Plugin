using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using scp4aiur;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATTG3
{
	internal class Question : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerSpawn, IEventHandlerSummonVehicle, IEventHandlerPlayerHurt
	{
		public static Dictionary<Player, int> Level;
		private readonly ATTG3Plugin plugin;
		public Question(ATTG3Plugin plugin) => this.plugin = plugin;
		public int CurrentCount;
		public int UpdatedCount;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.Questionevent)
			{
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					Level.Add(player, 1);
					player.ChangeRole(Role.CLASSD, true, true, false, true);
				}
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.Questionevent)
			{
				plugin.Questionevent = false;
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.Questionevent)
			{
				ev.Player.ChangeRole(Role.CLASSD, true, true, false, true);
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.Questionevent)
			{
				ev.IsCI = false;
				ev.AllowSummon = false;
			}
		}
		public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
		{
			if (plugin.Questionevent)
			{
				if (ev.Attacker.SteamId == "76561198126860363")
				{
					if (ev.DamageType == DamageType.COM15)
					{
						Level.TryGetValue(ev.Player, out CurrentCount);
						Level[ev.Player] = CurrentCount + 1;
						UpdatedCount = CurrentCount + 1;
						ev.Player.AddHealth((int)ev.Damage);
						ev.Player.SetRank("green", UpdatedCount.ToString(), null);
					}
					else if (ev.DamageType == DamageType.USP)
					{
						Level.TryGetValue(ev.Player, out CurrentCount);
						Level[ev.Player] = CurrentCount - 1;
						ev.Player.AddHealth((int)ev.Damage);
						UpdatedCount = CurrentCount - 1;
						ev.Player.SetRank("green", UpdatedCount.ToString(), null);
					}
				}
			}
		}
	}
}


