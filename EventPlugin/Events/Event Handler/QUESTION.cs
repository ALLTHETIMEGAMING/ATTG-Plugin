using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace ATTG3
{
	internal class Question : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerSummonVehicle, IEventHandlerPlayerHurt
	{
		private readonly ATTG3Plugin plugin;
		public Question(ATTG3Plugin plugin) => this.plugin = plugin;
		public int CurrentCount;
		public int Levelvalue;
        public Dictionary<Player, int> Level { get; set; } = new Dictionary<Player, int>();

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.QEvent)
			{
				plugin.Info("Question Round Started");
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					Level.Add(player, 1);
					player.ChangeRole(Role.CLASSD, true, true, false, true);
				}
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.QEvent)
			{
				plugin.QEvent = false;
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.QEvent)
			{
				ev.IsCI = false;
				ev.AllowSummon = false;
			}
		}
		public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
		{
			if (plugin.QEvent)
			{
				if (ev.Attacker.TeamRole.Role == Role.TUTORIAL)
				{
					if (ev.DamageType == DamageType.COM15)
					{
						ev.Damage = 0;
						Level.TryGetValue(ev.Player, out CurrentCount);
						Level[ev.Player] = CurrentCount + 1;
						Level.TryGetValue(ev.Player, out Levelvalue);
						ev.Player.SetRank("green", "Level: " + Levelvalue.ToString(), null);
						plugin.Info("PLAYER LEVEL UP");
					}
					else if (ev.DamageType == DamageType.USP)
					{
						ev.Damage = 0;
						Level.TryGetValue(ev.Player, out CurrentCount);
						Level[ev.Player] = CurrentCount -1;
						Level.TryGetValue(ev.Player, out Levelvalue);
						ev.Player.SetRank("green", "Level: " + Levelvalue.ToString(), null);
						plugin.Info("PLAYER LEVEL DOWN");
					}
				}
			}
		}
	}
}


