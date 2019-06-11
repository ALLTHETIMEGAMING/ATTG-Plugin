using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace ATTG3
{
	internal class Question : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerSummonVehicle, IEventHandlerPlayerHurt,IEventHandlerSetRole
    {
		private readonly ATTG3Plugin plugin;
		public Question(ATTG3Plugin plugin) => this.plugin = plugin;
		public int Level0check;
        public Dictionary<string, int> Level = new Dictionary<string, int>();

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.QEvent)
			{
				plugin.Info("Question Round Started");
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					Level.Add(player.SteamId, 1);
					player.ChangeRole(Role.CLASSD, true, true, false, true);
				}
			}
		}
        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            if (plugin.QEvent)
            {
				if (ev.Player.TeamRole.Role == Role.CLASSD && ev.Player.SteamId == "76561198141700494")
                {
                    ev.Player.ChangeRole(Role.TUTORIAL,true,false,false,true);
					plugin.Info("Spawned Server Owner");
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
					int Indicheck;
					if (Level.TryGetValue(ev.Player.SteamId, out Indicheck))
					{
						if (ev.DamageType == DamageType.COM15)
						{
							ev.Damage = 0;
							Level[ev.Player.SteamId]++;
							Level.TryGetValue(ev.Player.SteamId, out int CurrentCount);
							ev.Player.SetRank("green", "Level: " + CurrentCount.ToString(), null);
						}
						else if (ev.DamageType == DamageType.USP)
						{
							ev.Damage = 0;
							Level.TryGetValue(ev.Player.SteamId, out Level0check);
							if (Level0check != 1)
							{
								ev.Damage = 0;
								Level[ev.Player.SteamId]--;
								Level.TryGetValue(ev.Player.SteamId, out int CurrentCount);
								ev.Player.SetRank("green", "Level: " + CurrentCount.ToString(), null);
							}
						}
					}
					else
					{
						Level.Add(ev.Player.SteamId, 0);
					}
				}
			}
		}
	}
}


