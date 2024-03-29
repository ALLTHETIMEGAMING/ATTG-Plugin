﻿using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace ATTG3
{
	internal class SCPMTFEVENT : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerLure,
		IEventHandlerGeneratorInsertTablet, IEventHandlerSummonVehicle, IEventHandlerPlayerTriggerTesla, IEventHandlerDoorAccess, IEventHandlerWarheadDetonate, IEventHandlerPlayerDie
	{
		int C106;
		public static int gen;
		bool nuke;
		public Dictionary<string, int> Time = new Dictionary<string, int>();
		private readonly ATTG3Plugin plugin;
		public SCPMTFEVENT(ATTG3Plugin plugin) => this.plugin = plugin;
		public Dictionary<string, float> GenTime = new Dictionary<string, float>();
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.MTFSCP)
			{
				ATTG3Plugin.Jan10Lock = true;
				ATTG3Plugin.Jan10Lock = true;
				ATTG3Plugin.JanDestroy = true;
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
					else if (door.Name == "914")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "106_PRIMARY")
					{
						door.Locked = true;
						door.Open = false;
					}
					else if (door.Name == "106_PRIMARY")
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
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team != Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.NTF_LIEUTENANT, true, true, false, true);
					}
				}
				foreach (Generator079 gen in Generator079.generators)
				{
					gen.NetworkremainingPowerup = (gen.startDuration = 120f);
				}
				foreach (Smod2.API.Item item in PluginManager.Manager.Server.Map.GetItems(Smod2.API.ItemType.MTF_COMMANDER_KEYCARD, true))
				{
					item.Remove();
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId))
				{
					// Testing Custom Door events
					plugin.Info("Running SCPMTF Door key test");
					Timing.RunCoroutine(Events.CustomitemDoor(ev.Door, ev.Player.GetCurrentItem().ItemType, ev.Player));
				}
				if (ev.Door.Name == "914" && gen == 0)
				{
					ev.Player.PersonalBroadcast(10, "A generator must be turned on", false);
				}
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					ev.Player.SetRank("cyan", "TEAM: MTF", null);
					Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
					if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
					{
						ev.Items.Add(ItemType.MICROHID);
					}
					if (gen == 5 && ev.Player.TeamRole.Role != Role.NTF_COMMANDER)
					{
						ev.Items.Add(ItemType.MICROHID);
					}
				}
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
				{
					if (ev.Player.TeamRole.Role == Role.SCP_049)
					{
						ev.Player.SetRank("silver", "SCP-049", null);
						ev.Player.AddHealth(20000);
					}
					else if (ev.Player.TeamRole.Role == Role.SCP_096)
					{
						ev.Player.SetRank("silver", "SCP-096", null);
						ev.Player.AddHealth(15000);
					}
					else if (ev.Player.TeamRole.Role == Role.SCP_106)
					{
						ev.Player.SetRank("silver", "SCP-106", null);
						ev.Player.AddHealth(15000);
						ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_049));
					}
					else if (ev.Player.TeamRole.Role == Role.SCP_173)
					{
						ev.Player.SetRank("silver", "SCP-173", null);
						ev.Player.AddHealth(70000);
						ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_049));
					}
					else if (ev.Player.TeamRole.Role == Role.SCP_939_53 || ev.Player.TeamRole.Role == Role.SCP_939_89)
					{
						ev.Player.SetRank("silver", "SCP-939", null);
						ev.Player.AddHealth(30000);
					}
					else if (ev.Player.TeamRole.Role == Role.SCP_079)
					{
						ev.Player.PersonalClearBroadcasts();
						ev.Player.ChangeRole(Role.NTF_LIEUTENANT, true, true, true, true);
					}
				}
				ev.Items.Remove(ItemType.DISARMER);
				//Events.SCPMTF();
			}
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (plugin.MTFSCP)
			{
				gen++;
				if (gen == 1)
				{
					nuke = true;
					foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
					{
						if (door.Name == "914")
						{
							door.Locked = false;
							door.Open = false;
							PluginManager.Manager.Server.Map.ClearBroadcasts();
							PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#0B7A00>914 UNLOCKED</color></SIZE>", false);
						}
					}
				}
				if (gen == 4)
				{
					foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
					{
						if (door.Name == "106_PRIMARY")
						{
							door.Locked = false;
							door.Open = false;
						}
						else if (door.Name == "106_SECONDARY")
						{
							door.Locked = false;
							door.Open = false;
						}
					}
					PluginManager.Manager.Server.Map.ClearBroadcasts();
					PluginManager.Manager.Server.Map.Broadcast(10, "<size=75><color=#0B7A00>106 DOORS UNLOCKED</color></size>", false);
				}
				if (gen == 5)
				{
					nuke = true;
					foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
					{

						if (door.Name == "NUKE_SURFACE")
						{
							door.Locked = false;
							door.Open = false;
						}
						else if (door.Name == "SURFACE_GATE")
						{
							door.Locked = false;
							door.Open = true;
						}
						else if (door.Name == "GATE_A")
						{
							door.Locked = false;
							door.Open = false;
						}
						else if (door.Name == "106_PRIMARY")
						{
							door.Locked = false;
							door.Open = true;
						}
					}
					PluginManager.Manager.Server.Map.ClearBroadcasts();
					PluginManager.Manager.Server.Map.Broadcast(10, "<size=75><color=#0B7A00>NUKE CONTROL ROOM UNLOCKED</color></size>", false);
				}
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (!nuke)
				{
					ev.Allow = false;
					ev.Player.PersonalBroadcast(10, "Nuke cannot be activated untill all generators are on", false);
				}
			}
		}
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (ev.Player.TeamRole.Team != Smod2.API.Team.NINETAILFOX)
				{
					float Indicheck;
					if (GenTime.TryGetValue(ev.Generator.Room.RoomType.ToString(), out Indicheck))
					{
						GenTime[ev.Generator.Room.RoomType.ToString()] = ev.Generator.TimeLeft;
					}
					else
					{
						GenTime.Add(ev.Generator.Room.RoomType.ToString(), ev.Generator.TimeLeft);
					}
					ev.Allow = true;
				}
				else
				{
					ev.Allow = false;
				}
			}
		}
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (plugin.MTFSCP)
			{

			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.MTFSCP)
			{
				nuke = false;
				gen = 0;
				plugin.MTFSCP = false;
				C106 = 0;
			}
		}
		public void OnLure(PlayerLureEvent ev)
		{
			if (plugin.MTFSCP)
			{
				C106++;
				if (C106 >= 10)
				{
					ev.AllowContain = true;
				}
				else
				{
					ev.AllowContain = false;
					PluginManager.Manager.Server.Map.ClearBroadcasts();
					PluginManager.Manager.Server.Map.Broadcast(10, C106 + " OUT OF 10 PEOPLE SACRIFICED TO 106", false);
				}
			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (Events.Gendelaybool)
				{
					ev.Allow = false;
					ev.Player.PersonalBroadcast(10, "Generator Cooldown Active Plz Wait", false);
				}
				else if (ev.Allow == true)
				{
					Timing.RunCoroutine(Events.GenDelay());
					float Indicheck;
					if (GenTime.TryGetValue(ev.Generator.Room.RoomType.ToString(), out Indicheck))
					{
						ev.Generator.TimeLeft = GenTime[ev.Generator.Room.RoomType.ToString()];
					}
					PluginManager.Manager.Server.Map.ClearBroadcasts();
					PluginManager.Manager.Server.Map.Broadcast(10, "The Generator in " + ev.Generator.Room.RoomType.ToString() + " is being Activated", false);
				}
			}
		}
		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (plugin.MTFSCP)
			{
				foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
				{
					Elevator.MovingSpeed = 5;
				}
			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (plugin.MTFSCP)
			{
				ev.Cancel = false;
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.MTFSCP)
			{
				ev.IsCI = false;
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (plugin.MTFSCP)
			{
				ev.Triggerable = false;
			}
		}
		public void OnDetonate()
		{
			if (plugin.MTFSCP)
			{
				gen = 5;
			}
		}
		/*public void OnGeneratorUnlock(Smod2.Events.PlayerGeneratorUnlockEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (genunlocked == true)
				{
					ev.Allow = false;
				}
				else
				{
					ev.Allow = true;
					genunlocked = true;
				}
			}
		}*/
		public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
		{
			if (plugin.MTFSCP)
			{
				ev.Player.SetRank("default", "DEAD", null);
			}
		}
	}
}


