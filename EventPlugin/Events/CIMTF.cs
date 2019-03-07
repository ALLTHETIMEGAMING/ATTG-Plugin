using System.Linq;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.EventSystem.Events;
using Smod2.Events;
using System.Collections.Generic;
using scp4aiur;

namespace ATTG3
{
	internal class CIMTF : IEventHandlerTeamRespawn, IEventHandlerSetRole, IEventHandlerCheckRoundEnd, IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerPlayerJoin, IEventHandlerWaitingForPlayers
	{
		private readonly ATTG3Plugin plugin;

		public CIMTF(ATTG3Plugin plugin) => this.plugin = plugin;

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (ATTG3Plugin.enabledcimtf)
			{
				if (!ATTG3Plugin.roundstartedcimtf)
				{
					Server server = plugin.pluginManager.Server;
					server.Map.ClearBroadcasts();
					server.Map.Broadcast(25, "<color=00FFFF> CI VS MTF Gamemode</color> is starting..", false);
				}
			}
		}
		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (ATTG3Plugin.enabledcimtf || ATTG3Plugin.roundstartedcimtf)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP || ev.Player.TeamRole.Team == Smod2.API.Team.CLASSD)
				{
					Timing.Run(ATTG3Plugin.SpawnChaos(ev.Player, 0));
				}
				else if (ev.Player.TeamRole.Role == Role.FACILITY_GUARD || ev.Player.TeamRole.Team == Smod2.API.Team.SCIENTIST)
				{
					Timing.Run(ATTG3Plugin.SpawnNTF(ev.Player, 0));
				}
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.SPECTATOR)
				{
					ev.Player.PersonalBroadcast(25, "You are dead, but don't worry, you will respawn soon!", false);
				}
			}
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			
		}

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (ATTG3Plugin.enabledcimtf)
			{
				ATTG3Plugin.roundstartedcimtf = true;
				plugin.Server.Map.StartWarhead();
				plugin.pluginManager.Server.Map.ClearBroadcasts();
				plugin.Info("CIMTF Gamemode started!");
				List<Player> players = ev.Server.GetPlayers();

				for (int i = 0; i < (players.Count / 2); i++)
				{
					int random = new System.Random().Next(players.Count);
					Player randomplayer = players[random];
					players.Remove(randomplayer);
					Timing.Run(ATTG3Plugin.SpawnNTF(randomplayer, 0));
				}
				foreach (Player player in players)
				{
					Timing.Run(ATTG3Plugin.SpawnChaos(player, 0));
				}
			}
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (ATTG3Plugin.enabledcimtf || ATTG3Plugin.roundstartedcimtf)
			{
				plugin.Info("Round Ended!");
				ATTG3Plugin.EndGamemodeRound();
			}
		}

		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (ATTG3Plugin.enabledcimtf || ATTG3Plugin.roundstartedcimtf)
			{
				bool ciAlive = false;
				bool ntfAlive = false;

				foreach (Player player in ev.Server.GetPlayers())
				{
					if (player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
					{
						ciAlive = true; continue;
					}
					else if (player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
					{
						ntfAlive = true;
					}
				}
				if (ev.Server.GetPlayers().Count > 1)
				{
					if (ciAlive && ntfAlive)
					{
						ev.Status = ROUND_END_STATUS.ON_GOING;
						ev.Server.Map.ClearBroadcasts();
						ev.Server.Map.Broadcast(10, "There are " + plugin.Round.Stats.CiAlive + " Chaos alive, and " + plugin.Round.Stats.NTFAlive + " NTF alive.", false);
					}
					else if (ciAlive && ntfAlive == false)
					{
						ev.Status = ROUND_END_STATUS.OTHER_VICTORY; ATTG3Plugin.EndGamemodeRound();
					}
					else if (ciAlive == false && ntfAlive)
					{
						ev.Status = ROUND_END_STATUS.MTF_VICTORY; ATTG3Plugin.EndGamemodeRound();
					}
				}
			}
		}

		public void OnTeamRespawn(TeamRespawnEvent ev)
		{
			if (ATTG3Plugin.enabledcimtf || ATTG3Plugin.roundstartedcimtf)
			{
				if (plugin.Round.Stats.CiAlive >= plugin.Round.Stats.NTFAlive)
				{
					ev.SpawnChaos = false;
				}
				else if (plugin.Round.Stats.CiAlive < plugin.Round.Stats.NTFAlive)
				{
					ev.SpawnChaos = true;
				}
			}
		}
	}
}
