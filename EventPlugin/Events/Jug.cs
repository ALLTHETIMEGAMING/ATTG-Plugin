using Smod2.API;
using Smod2.EventHandlers;
using Smod2.EventSystem.Events;
using System.Collections.Generic;
using Smod2.Events;
using System;
using System.Timers;
using UnityEngine;


namespace ATTG3
{
	internal class EventsHandler : IEventHandlerReload, IEventHandlerSetSCPConfig, IEventHandlerTeamRespawn, IEventHandlerCheckRoundEnd, IEventHandlerRoundStart, IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerRoundEnd, IEventHandlerPlayerHurt, IEventHandlerSetRoleMaxHP, IEventHandlerSetRole,
		IEventHandlerLure, IEventHandlerContain106, IEventHandlerThrowGrenade
	{
		private readonly ATTG3Plugin plugin;

		public EventsHandler(ATTG3Plugin plugin) => this.plugin = plugin;

		public static Timer timer;

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (ATTG3Plugin.enabledjug)
			{
				if (!ATTG3Plugin.roundstartedjug)
				{
					Server server = plugin.pluginManager.Server;
					server.Map.ClearBroadcasts();
					server.Map.Broadcast(25, "<color=#228B22>Juggernaut Gamemode</color> is starting...", false);
				}
			}
		}

		public void OnSetRoleMaxHP(SetRoleMaxHPEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				if (ev.Role == Role.CHAOS_INSURGENCY)
					ev.MaxHP = ATTG3Plugin.juggernaut_healh;
			}
		}

		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				if (ATTG3Plugin.IsJuggernaut(ev.Player))
				{
					if (ev.TeamRole.Team != Smod2.API.Team.CHAOS_INSURGENCY || ev.TeamRole.Team == Smod2.API.Team.SPECTATOR)
					{
						ATTG3Plugin.ResetJuggernaut(ev.Player);
					}
				}
				else
				{
					// Set NTF Inventory
					plugin.Info("Setting NTF items..");
					List<ItemType> items = new List<ItemType>();
					items.Add(ItemType.E11_STANDARD_RIFLE);
					items.Add(ItemType.MTF_COMMANDER_KEYCARD);
					items.Add(ItemType.FRAG_GRENADE);
					items.Add(ItemType.FLASHBANG);
					items.Add(ItemType.RADIO);
					items.Add(ItemType.MEDKIT);

					if (ATTG3Plugin.JugNTF_Disarmer)

					{
						items.Add(ItemType.DISARMER);
					}
					else
					{

						items.Add(ItemType.FRAG_GRENADE);
					}

					if (ev.TeamRole.Team != Smod2.API.Team.SPECTATOR)
					{
						if (ev.TeamRole.Team != Smod2.API.Team.NINETAILFOX)
						{
							plugin.Info("Spawning " + ev.Player.Name + " as NTF Commander, and setting inventory.");
							ev.Items = items;
							ATTG3Plugin.SpawnAsNTFCommander(ev.Player);
							ev.Player.SetHealth(ATTG3Plugin.jugntf_health);
						}
						else if (ev.TeamRole.Role == Role.FACILITY_GUARD || ev.TeamRole.Role == Role.NTF_LIEUTENANT || ev.TeamRole.Role == Role.NTF_SCIENTIST || ev.TeamRole.Role == Role.NTF_CADET)
							ev.Items = items;
						ATTG3Plugin.SpawnAsNTFCommander(ev.Player);
						ev.Player.SetHealth(ATTG3Plugin.jugntf_health);
					}
				}
			}
		}

		public void OnReload(PlayerReloadEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				if (ev.Player.Name == ATTG3Plugin.juggernaut.Name || ev.Player.SteamId == ATTG3Plugin.juggernaut.SteamId)
				{
					ev.Player.SetAmmo(AmmoType.DROPPED_7, 2000);
				}
			}
		}

		public void OnThrowGrenade(PlayerThrowGrenadeEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				if (ev.Player == ATTG3Plugin.juggernaut)
				{
					if (ATTG3Plugin.jugg_infinite_nades)
					{
						ev.Player.GiveItem(ItemType.FRAG_GRENADE);
					}
				}
			}
		}

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (ATTG3Plugin.enabledjug)
			{
				ATTG3Plugin.roundstartedjug = true;
				plugin.pluginManager.Server.Map.ClearBroadcasts();
				plugin.Info("Juggernaut Gamemode Started!");
				List<Player> players = ev.Server.GetPlayers();

				if (ATTG3Plugin.jugg_killer != null && ATTG3Plugin.jugg_killer is Player)
				{
					ATTG3Plugin.selectedJuggernaut = ATTG3Plugin.jugg_killer;
				}

				if (ATTG3Plugin.selectedJuggernaut == null)
				{
					int chosenJuggernaut = new System.Random().Next(players.Count);

					ATTG3Plugin.juggernaut = players[chosenJuggernaut];

					foreach (Player player in players)
					{
						// Selected random Juggernaut
						if (ATTG3Plugin.IsJuggernaut(player))
						{
							plugin.Info("" + player.Name + " Chosen as the Juggernaut");
							ATTG3Plugin.SpawnAsJuggernaut(player);
						}
						else
						{
							// Spawned as normal NTF Commander
							plugin.Debug("Spawning " + player.Name + "as an NTF Commander");
							ATTG3Plugin.SpawnAsNTFCommander(player);
						}
					}
				}
				else if (ATTG3Plugin.selectedJuggernaut != null && ATTG3Plugin.selectedJuggernaut is Player)
				{
					foreach (Player player in players)
					{
						if (player.SteamId == ATTG3Plugin.selectedJuggernaut.SteamId || player.Name == ATTG3Plugin.selectedJuggernaut.Name)
						{
							plugin.Info("Selected " + ATTG3Plugin.selectedJuggernaut.Name + " as the Juggernaut");
							ATTG3Plugin.SpawnAsJuggernaut(player);
							ATTG3Plugin.selectedJuggernaut = null;
						}
						else
						{
							plugin.Debug("Spawning " + player.Name + "as an NTF Commander");
							ATTG3Plugin.SpawnAsNTFCommander(player);
						}
					}
				}
				for (int i = 0; i < 4; i++)
				{
					foreach (Player player in players)
					{
						player.GiveItem(ItemType.MICROHID);
					}
				}
			}
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
				plugin.Info("Round Ended!");
			ATTG3Plugin.EndGamemodeRound();
		}

		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				bool juggernautAlive = false;
				bool mtfAllive = false;

				foreach (Player player in ev.Server.GetPlayers())
				{
					if (ATTG3Plugin.IsJuggernaut(player) && player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
					{
						juggernautAlive = true; continue;
					}

					else if (player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
					{
						mtfAllive = true;
					}
				}
				if (ev.Server.GetPlayers().Count > 1)
				{
					if (ATTG3Plugin.juggernaut != null && juggernautAlive && mtfAllive)
					{
						ev.Status = ROUND_END_STATUS.ON_GOING;
					}
					else if (ATTG3Plugin.juggernaut != null && juggernautAlive && mtfAllive == false)
					{
						ev.Status = ROUND_END_STATUS.CI_VICTORY; ATTG3Plugin.EndGamemodeRound();
					}
					else if (ATTG3Plugin.juggernaut == null && juggernautAlive == false && mtfAllive)
					{
						ev.Status = ROUND_END_STATUS.MTF_VICTORY; ATTG3Plugin.EndGamemodeRound();
					}
				}
			}
		}

		public void OnPlayerDie(PlayerDeathEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				if (ATTG3Plugin.IsJuggernaut(ev.Player))
				{
					plugin.pluginManager.Server.Map.ClearBroadcasts();
					plugin.pluginManager.Server.Map.Broadcast(20, "<color=#228B22>Juggernaut " + ATTG3Plugin.juggernaut.Name + "</color> has been killed by " + ev.Killer.Name + "!", false);
					ATTG3Plugin.ResetJuggernaut(ev.Player);
					ATTG3Plugin.jugg_killer = ev.Killer;
				}
				else
				{
					plugin.Server.Map.ClearBroadcasts();
					plugin.Server.Map.Broadcast(15, "There are " + (ATTG3Plugin.plugin.pluginManager.Server.Round.Stats.NTFAlive - 1) + " NTF remaining.", false);
				}
			}
		}

		public void OnPlayerHurt(PlayerHurtEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				if (ATTG3Plugin.IsJuggernaut(ev.Player))
				{
					ATTG3Plugin.juggernaut_healh = (ATTG3Plugin.juggernaut_healh > ev.Player.GetHealth()) ? ATTG3Plugin.juggernaut_healh : ev.Player.GetHealth();
					plugin.pluginManager.Server.Map.ClearBroadcasts();
					plugin.pluginManager.Server.Map.Broadcast(3, "<color=#228B22>Juggernaut " + ATTG3Plugin.juggernaut.Name + "</color> HP : <color=#ff0000>" + (Convert.ToInt32(ATTG3Plugin.juggernaut.GetHealth() - ev.Damage)) + "/" + ATTG3Plugin.juggernaut_healh + "</color>", false);
				}
			}
		}

		public void OnLure(PlayerLureEvent ev)
		{
			if (ATTG3Plugin.enabledjug && ATTG3Plugin.roundstartedjug)
			{
				ATTG3Plugin.activator = ev.Player;
			}

		}

		public void OnContain106(PlayerContain106Event ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				if (ATTG3Plugin.juggernaut != null)
				{
					timer = new Timer();
					timer.Interval = 10000;
					timer.Elapsed += OnTimedEvent;
					timer.AutoReset = false;
					timer.Enabled = true;
				}
			}
		}

		public void OnTimedEvent(System.Object source, ElapsedEventArgs e)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				Player juggernautPlayer = ATTG3Plugin.GetJuggernautPlayer();
				if (juggernautPlayer != null && ATTG3Plugin.activator != null)
					ATTG3Plugin.CriticalHitJuggernaut(juggernautPlayer, ATTG3Plugin.activator);
				else if (juggernautPlayer != null)
					ATTG3Plugin.CriticalHitJuggernaut(juggernautPlayer);
			}
		}

		public void OnSetSCPConfig(SetSCPConfigEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
			{
				ev.Ban049 = true;
				ev.Ban079 = true;
				ev.Ban096 = true;
				ev.Ban106 = true;
				ev.Ban173 = true;
				ev.Ban939_53 = true;
				ev.Ban939_89 = true;
			}
		}

		public void OnTeamRespawn(TeamRespawnEvent ev)
		{
			if (ATTG3Plugin.enabledjug || ATTG3Plugin.roundstartedjug)
				ev.SpawnChaos = false;
		}
	}
}
