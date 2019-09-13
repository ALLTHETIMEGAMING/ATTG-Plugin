using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ATTG3
{
	internal class Hold : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerSummonVehicle,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerPlayerDropItem,
		IEventHandlerGeneratorInsertTablet, IEventHandlerUpdate, IEventHandlerCheckRoundEnd, IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerElevatorUse, IEventHandlerPlayerTriggerTesla
	{
		public static bool Nuke;
		public static int gen;
		public static bool Holdevent;
		public static List<string> FreeSCPS = new List<string>();
		public static int MTF;
		public static int CI;
		public static bool S300;
		public static bool S600;
		public static bool S900;
		public static bool S1200;
		public static Dictionary<string, string> GenTeam = new Dictionary<string, string>();
		//bool spawn;
		private readonly ATTG3Plugin plugin;
		public Hold(ATTG3Plugin plugin) => this.plugin = plugin;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (Holdevent)
			{
				Nuke = false;
				gen = 0;
				foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
				{
					if (door.Name == "106_SECONDARY")
					{
						door.Locked = true;
					}
					else if (door.Name == "NUKE_SURFACE")
					{
						door.Locked = true;
					}
					else if (door.Name == "106_PRIMARY")
					{
						door.Locked = true;
					}
					else if (door.Name == "GATE_B")
					{
						door.Locked = true;
					}
					else if (door.Name == "INTERCOM")
					{
						door.Locked = true;
					}
					else if (door.Name == "SURFACE_GATE")
					{
						door.Locked = true;
					}
					else if (door.Name == "173")
					{
						door.Locked = true;
					}
					else if (door.Name == "096")
					{
						door.Locked = true;
					}
					else if (door.Name == "914")
					{
						door.Locked = true;
					}
					else if (door.Name == "ESCAPE")
					{
						door.Locked = true;
					}
					else if (door.Name == "LCZ_ARMORY")
					{
						door.Locked = true;
					}
				}
				foreach (Generator079 gen in Generator079.generators)
				{
					gen.NetworkremainingPowerup = (gen.startDuration = 120f);
				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team == Smod2.API.Team.SCP || player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD)
					{
						player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
					}
					else if (player.TeamRole.Team == Smod2.API.Team.CLASSD || player.TeamRole.Team == Smod2.API.Team.SCIENTIST)
					{
						player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
					}
				}
				PluginManager.Manager.Server.Map.ClearBroadcasts();
				PluginManager.Manager.Server.Map.Broadcast(10, "TURN ON MORE GENERATORS THEN THE OTHER TEAM", false);
			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (Holdevent)
			{
				if (ev.Server.Round.Duration >= 1200)
				{
					ev.Status = ROUND_END_STATUS.NO_VICTORY;
				}
				else if (ev.Server.NumPlayers == 0)
				{
					ev.Status = ROUND_END_STATUS.FORCE_END;
				}
				else if (gen == 5)
				{
					if (CI > MTF)
					{
						ev.Status = ROUND_END_STATUS.CI_VICTORY;
						ev.Round.Stats.ClassDEscaped = 30;
					}
					else
					{
						ev.Status = ROUND_END_STATUS.MTF_VICTORY;
						ev.Round.Stats.ScientistsEscaped = 30;
					}
				}
				else
				{
					ev.Status = ROUND_END_STATUS.ON_GOING;
				}
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (Holdevent)
			{
				if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
				{
					ev.Items.Add(ItemType.WEAPON_MANAGER_TABLET);
					ev.Items.Add(ItemType.P90);
					ev.Items.Remove(ItemType.LOGICER);
					ev.Items.Add(ItemType.E11_STANDARD_RIFLE);
				}
				else if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
				{
					ev.Items.Add(ItemType.P90);
					ev.Items.Remove(ItemType.DISARMER);
					ev.Items.Remove(ItemType.FRAG_GRENADE);
				}
			}
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (Holdevent)
			{
				gen++;
				if (GenTeam[ev.Generator.Room.RoomType.ToString()] == Smod2.API.Team.CHAOS_INSURGENCY.ToString())
				{
					CI++;
					PluginManager.Manager.Server.Map.Broadcast(5, "CI HAVE TURNED ON A GENERATOR", false);
				}
				else if (GenTeam[ev.Generator.Room.RoomType.ToString()] == Smod2.API.Team.NINETAILFOX.ToString()){
					MTF++;
					PluginManager.Manager.Server.Map.Broadcast(5, "MTF HAVE TURNED ON A GENERATOR", false);
				}
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (Holdevent)
			{
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated at the moment", false);
			}
		}
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (Holdevent)
			{
				if (GenTeam[ev.Generator.Room.RoomType.ToString()] == Smod2.API.Team.NINETAILFOX.ToString() && ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					ev.Player.PersonalBroadcast(5, "THIS GENERATOR WAS ACTIVATED BY YOUR TEAM", false);
					ev.Allow = false;
				}
				else if (GenTeam[ev.Generator.Room.RoomType.ToString()] == Smod2.API.Team.CHAOS_INSURGENCY.ToString() && ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
				{
					ev.Player.PersonalBroadcast(5, "THIS GENERATOR WAS ACTIVATED BY YOUR TEAM", false);
					ev.Allow = false;
				}
				else if (GenTeam.TryGetValue(ev.Generator.Room.RoomType.ToString(), out var team))
				{
					GenTeam.Remove(ev.Generator.Room.RoomType.ToString());
				}
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (Holdevent)
			{
				gen = 0;
				Holdevent = false;
				CI = 0;
				MTF = 0;
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (Holdevent)
			{
				if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
				{
					ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCIENTIST);
				}
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
				{
					ev.SpawnPos = new Vector(0, 1001, 0);
				}
			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (Holdevent)
			{
				ev.Generator.TimeLeft = 120;
				if (GenTeam.TryGetValue(ev.Generator.Room.RoomType.ToString(), out var team))
				{
					GenTeam.Remove(ev.Generator.Room.RoomType.ToString());
					GenTeam[ev.Generator.Room.RoomType.ToString()] = ev.Player.TeamRole.Team.ToString();
				}
				else
				{
					GenTeam.Add(ev.Generator.Room.RoomType.ToString(), ev.Player.TeamRole.Team.ToString());
				}
				ev.Allow = true;

				PluginManager.Manager.Server.Map.ClearBroadcasts();
				PluginManager.Manager.Server.Map.Broadcast(10, "The Generator in " + ev.Generator.Room.RoomType.ToString() + " is being Activated", false);
			}
		}
		public void OnElevatorUse(PlayerElevatorUseEvent ev)
		{
			if (Holdevent)
			{
				if ((ev.Elevator.ElevatorType != ElevatorType.GateA && ev.Elevator.ElevatorType != ElevatorType.WarheadRoom) && ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
				{
					ev.AllowUse = false;
					ev.Player.PersonalBroadcast(10, "This Elevator is disabled for your team", false);
				}
				else if ((ev.Elevator.ElevatorType != ElevatorType.WarheadRoom && ev.Elevator.ElevatorType != ElevatorType.LiftB && ev.Elevator.ElevatorType != ElevatorType.LiftA) && ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					ev.AllowUse = false;
					ev.Player.PersonalBroadcast(10, "This Elevator is disabled for your team", false);
				}
				if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
				{
					ev.Elevator.MovingSpeed = 7f;
				}
				if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
				{
					ev.Elevator.MovingSpeed = 1f;
				}
			}
		}
		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (Holdevent)
			{
				Nuke = true;
			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (Holdevent)
			{
				Nuke = false;
			}
		}
		public void OnUpdate(Smod2.Events.UpdateEvent ev)
		{
			if (Holdevent)
			{
				if (GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time != 0)
				{
					GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time = 0;
				}
				if (PluginManager.Manager.Server.Round.Duration == 600 && !S600)
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "10 MIN REMAIN", false);
					S600 = true;
				}
				else if (PluginManager.Manager.Server.Round.Duration == 300 && !S300)
				{
					S300 = true;
					PluginManager.Manager.Server.Map.Broadcast(10, "15 MIN REMAIN", false);
				}
				else if (PluginManager.Manager.Server.Round.Duration == 900 && !S900)
				{
					S900 = true;
					PluginManager.Manager.Server.Map.Broadcast(10, "5 MIN REMAIN", false);
				}
				else if (PluginManager.Manager.Server.Round.Duration == 1200 && !S1200)
				{
					S1200 = true;
					//PluginManager.Manager.Server.Map.Broadcast(10, "5 MIN REMAIN", false);
				}
			}
		}
		public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
		{
			if (Holdevent)
			{
				Timing.RunCoroutine(Events.HOLDRESPAWN(ev.Player, ev.Killer));
				//Events.IsEvan("Breach", MTF, CI, Role.NTF_COMMANDER, Role.CHAOS_INSURGENCY);
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (Holdevent)
			{
				ev.AllowSummon = false;
			}
		}
		public void OnPlayerJoin(Smod2.Events.PlayerJoinEvent ev)
		{
			if (Holdevent)
			{
				if (plugin.RoundStarted)
				{
					if ((PluginManager.Manager.Server.NumPlayers % 2) == 0)
					{
						if (Events.TUTCOUNT(Role.NTF_COMMANDER) > Events.TUTCOUNT(Role.CHAOS_INSURGENCY))
						{
							ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
						}
						else if (Events.TUTCOUNT(Role.CHAOS_INSURGENCY) > Events.TUTCOUNT(Role.NTF_COMMANDER))
						{
							ev.Player.ChangeRole(Role.NTF_COMMANDER);
						}
					}
					else
					{
						ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
					}
				}
			}
		}
		public void OnPlayerDropItem(Smod2.Events.PlayerDropItemEvent ev)
		{
			if (Holdevent)
			{
				if (ev.Item.ItemType == ItemType.RADIO || ev.Item.ItemType == ItemType.LOGICER)
				{
					ev.Allow = false;
				}
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (Holdevent)
			{
				ev.Triggerable = false;
			}
		}
	}
}
