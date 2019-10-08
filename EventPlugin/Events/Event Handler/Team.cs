using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace ATTG3
{
	internal class Team : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerSummonVehicle, IEventHandlerPlayerHurt, IEventHandlerSetRole, IEventHandlerCheckRoundEnd
	{
		private readonly ATTG3Plugin plugin;
		public Team(ATTG3Plugin plugin) => this.plugin = plugin;
		public static List<string> TEAM1 = new List<string>();
		public static List<string> TEAM2 = new List<string>();
		public static bool Teamevent;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (Teamevent)
			{
				plugin.Info("Question Round Started");
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					player.ChangeRole(Role.CLASSD, true, true, false, true);
				}
			}
		}
		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (Teamevent)
			{
				ev.Items.Clear();
				if (ev.Player.TeamRole.Role == Role.TUTORIAL)
				{
					ev.Items.Add(ItemType.COM15);
					ev.Items.Add(ItemType.USP);
					ev.Items.Add(ItemType.P90);
				}
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (Teamevent)
			{
				Teamevent = false;
				TEAM2.Clear();
				TEAM1.Clear();
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (Teamevent)
			{
				ev.IsCI = false;
				ev.AllowSummon = false;
			}
		}
		public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
		{
			if (Teamevent)
			{
				if (ev.Attacker.TeamRole.Role == Role.TUTORIAL)
				{
					if (ev.DamageType == DamageType.COM15)
					{
						ev.Damage = 0;
						if (TEAM1.Contains(ev.Player.SteamId))
						{
							TEAM1.Remove(ev.Player.SteamId);
						}
						if (!(TEAM2.Contains(ev.Player.SteamId)))
						{
							TEAM2.Add(ev.Player.SteamId);
							ev.Player.SetRank("cyan", "TEAM: 2", null);
						}
						
					}
					else if (ev.DamageType == DamageType.USP)
					{
						ev.Damage = 0;
						if (TEAM2.Contains(ev.Player.SteamId))
						{
							TEAM2.Remove(ev.Player.SteamId);
						}
						if (!(TEAM1.Contains(ev.Player.SteamId)))
						{
							TEAM1.Add(ev.Player.SteamId);
							ev.Player.SetRank("green", "TEAM: 1", null);
						}
						
					}
				}
			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (Teamevent)
			{
				ev.Status = ROUND_END_STATUS.ON_GOING;
			}
		}
	}
}



