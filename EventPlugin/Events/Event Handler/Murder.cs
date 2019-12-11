using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ATTG3
{
	public class Mystery : IEventHandlerSummonVehicle, IEventHandlerCheckRoundEnd, IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerPlayerDie, IEventHandlerPlayerTriggerTesla, IEventHandlerPlayerPickupItem
	{
		private readonly ATTG3Plugin plugin;
		public Mystery(ATTG3Plugin plugin) => this.plugin = plugin;
		public static bool Event = false;
		public System.Random Gen = new System.Random();
		public static Dictionary<string, bool> Murd = new Dictionary<string, bool>();

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (Event)
			{
				plugin.Info("Murder Event started");
				int murderers = 0;
				int decti = 0;
				foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
				{
					if (door.Name == "NUKE_SURFACE")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "SURFACE_GATE")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "GATE_A")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "GATE_B")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "914")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "CHECKPOINT_ENT")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "CHECKPOINT_LCZ_A")
					{
						door.Locked = true;
						door.Open = true;
					}
					else if (door.Name == "CHECKPOINT_LCZ_B")
					{
						door.Locked = true;
						door.Open = true;
					}
					else if (door.Name == "106_PRIMARY")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "106_BOTTOM")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "INTERCOM")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "106_SECONDARY")
					{
						door.Locked = true;
						door.Open = false;
					}
					door.DontOpenOnWarhead = true;
					door.BlockAfterWarheadDetonation = false;
				}
				List<Player> players = ev.Server.GetPlayers();

				foreach (GameObject player in PlayerManager.singleton.players) player.GetComponent<WeaponManager>().NetworkfriendlyFire = true;
				foreach (Smod2.API.Item item in PluginManager.Manager.Server.Map.GetItems(Smod2.API.ItemType.USP, true))
				{
					item.Remove();
				}
				foreach (Smod2.API.Item item in PluginManager.Manager.Server.Map.GetItems(Smod2.API.ItemType.P90, true))
				{
					Vector itemspawn = item.GetPosition();
					PluginManager.Manager.Server.Map.SpawnItem(Smod2.API.ItemType.COM15, itemspawn, null);
					item.Remove();
				}
				foreach (Smod2.API.Item item in PluginManager.Manager.Server.Map.GetItems(Smod2.API.ItemType.E11_STANDARD_RIFLE, true))
				{
					Vector itemspawn = item.GetPosition();
					PluginManager.Manager.Server.Map.SpawnItem(Smod2.API.ItemType.COM15, itemspawn, null);
					item.Remove();
				}
				if (ev.Server.NumPlayers >= 10)
				{
					murderers = 2;
					decti = 3;
				}
				else if (ev.Server.NumPlayers >= 20)
				{
					murderers = 3;
					decti = 4;
				}
				else
				{
					murderers = 1;
					decti = 2;
				}
				for (int i = 0; i < murderers; i++)
				{
					if (players.Count == 0) break;
					int random = Gen.Next(players.Count);
					Player player = players[random];
					players.Remove(player);
					plugin.Info("Spawning Murder " + player.Name.ToString());
					Timing.RunCoroutine(Events.SpawnMurd(player));
				}
				for (int i = 0; i < decti; i++)
				{
					if (players.Count == 0) break;
					int random = Gen.Next(players.Count);
					Player player = players[random];
					players.Remove(player);
					plugin.Info("Spawning Dective");
					Timing.RunCoroutine(Events.SpawnDet(player));
				}

				foreach (Player player in players) Timing.RunCoroutine(Events.SpawnCiv(player));
			}
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (Event)
			{
				Murd.Clear();
				Event = false;
				plugin.Info("Round Ended!");
			}
		}

		public void OnPlayerDie(PlayerDeathEvent ev)
		{
			if (!Event) return;

			switch (ev.Player.TeamRole.Role)
			{
				case Role.CLASSD when Murd.ContainsKey(ev.Player.SteamId):
					plugin.Server.Map.ClearBroadcasts();
					plugin.Server.Map.Broadcast(15, "A murderer, " + ev.Player.Name + " has been eliminated by " + ev.Killer.Name + "!", false);
					Murd.Remove(ev.Player.SteamId);
					foreach (Smod2.API.Item item in ev.Player.GetInventory())
					{
						item.Remove();
					}
					break;
				case Role.CLASSD:
					{
						plugin.Server.Map.ClearBroadcasts();
						plugin.Server.Map.Broadcast(25, "There are now " + (plugin.Server.Round.Stats.ClassDAlive - 1) + " Civilians alive.", false);
						if (!Murd.ContainsKey(ev.Killer.SteamId) && ev.Killer != null)
						{
							ev.Killer.ChangeRole(Role.SPECTATOR);
							ev.Killer.PersonalClearBroadcasts();
							ev.Killer.PersonalBroadcast(10, "<color=#c50000>You killed an innocent person!", false);
						}

						break;
					}
				case Role.SCIENTIST:
					{
						plugin.Server.Map.ClearBroadcasts();
						plugin.Server.Map.Broadcast(15, "A detective, " + ev.Player.Name + " has been killed!", false);
						if (!Murd.ContainsKey(ev.Killer.SteamId) && ev.Killer != null)
						{
							ev.Killer.Kill();
							ev.Player.PersonalClearBroadcasts();
							ev.Player.PersonalBroadcast(10, "<color=#c50000>You were innocent and killed a Detective!", false);
						}

						break;
					}
			}
		}

		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (!Event) return;
			if (plugin.Server.Round.Duration < 5) return;

			bool civAlive = false;
			bool murdAlive = false;

			foreach (Player player in ev.Server.GetPlayers().Where(ply => ply.TeamRole.Team != Smod2.API.Team.SPECTATOR))
				if (Murd.ContainsKey(player.SteamId))
					murdAlive = true;
				else if (player.TeamRole.Role == Role.CLASSD && !Murd.ContainsKey(player.SteamId))
					civAlive = true;

			if (murdAlive && civAlive)
				ev.Status = ROUND_END_STATUS.ON_GOING;
			else if (!murdAlive && civAlive)
			{
				ev.Status = ROUND_END_STATUS.MTF_VICTORY;
				PluginManager.Manager.Server.Round.EndRound();
				plugin.Info("All of the murderers are dead!");
			}
			else if (murdAlive && !civAlive)
			{
				ev.Status = ROUND_END_STATUS.SCP_VICTORY;
				PluginManager.Manager.Server.Round.EndRound();
				plugin.Info("All the Civilians are dead!");
			}
		}

		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (Event)
			{
				ev.AllowSummon = false;
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (Event)
			{
				ev.Triggerable = false;
			}
		}
		public void OnPlayerPickupItem(Smod2.Events.PlayerPickupItemEvent ev)
		{
			if (Event)
			{
				if ((ev.Item.ItemType == ItemType.ZONE_MANAGER_KEYCARD || ev.Item.ItemType == ItemType.USP || ev.Item.ItemType == ItemType.RADIO) && !Murd.ContainsKey(ev.Player.SteamId))
				{
					ev.Allow = false;
					ev.Player.PersonalBroadcast(10, "Only Murders can have this item", false);
				}
			}
		}
	}
}
