﻿using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ATTG3
{
    internal class Cap : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerSummonVehicle, IEventHandlerCheckEscape, IEventHandlerGeneratorUnlock,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerLure, IEventHandlerPlayerDropItem,
		IEventHandlerGeneratorInsertTablet, IEventHandlerUpdate, IEventHandlerCheckRoundEnd, IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerElevatorUse, IEventHandlerDoorAccess
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
		public static Dictionary<string, float> GenTime = new Dictionary<string, float>();
        //bool spawn;
        private readonly ATTG3Plugin plugin;
        public Cap(ATTG3Plugin plugin) => this.plugin = plugin;
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
				}
                foreach (Generator079 gen in Generator079.generators)
                {
                    gen.NetworkremainingPowerup = (gen.startDuration = 180f);
                }
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    if (player.TeamRole.Team == Smod2.API.Team.SCP || player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD)
                    {
                        player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
                        player.PersonalClearBroadcasts();
                        player.PersonalBroadcast(10, "Turn on the generators to win", false);
                        CI++;
                    }
                    else if (player.TeamRole.Team == Smod2.API.Team.CLASSD || player.TeamRole.Team == Smod2.API.Team.SCIENTIST)
                    {
                        player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
                        player.PersonalClearBroadcasts();
                        player.PersonalBroadcast(10, "Stop CI from turning on the generators", false);
                        MTF++;
                    }
                }
			}
        }
        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (Holdevent)
            {
                if (ev.Server.Round.Duration >= 1500)
                {
                    ev.Status = ROUND_END_STATUS.MTF_VICTORY;
                    ev.Round.Stats.ScientistsEscaped = 5;
                }
                else if (ev.Server.NumPlayers == 0)
                {
                    ev.Status = ROUND_END_STATUS.FORCE_END;
                }
				else if (gen == 5)
				{
					ev.Status = ROUND_END_STATUS.CI_VICTORY;
					ev.Round.Stats.ClassDEscaped = 5;
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
					ev.Items.Add(ItemType.FLASHBANG);
					ev.Items.Add(ItemType.E11_STANDARD_RIFLE);
					ev.Items.Remove(ItemType.CHAOS_INSURGENCY_DEVICE);
				}
				else if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
                {
					ev.Items.Add(ItemType.P90);
					ev.Items.Remove(ItemType.DISARMER);
					ev.Items.Remove(ItemType.MTF_COMMANDER_KEYCARD);
					ev.Items.Remove(ItemType.WEAPON_MANAGER_TABLET);
					ev.Items.Remove(ItemType.FRAG_GRENADE);
				}
            }
        }
        public void OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            if (Holdevent)
            {
                gen++;
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
				plugin.Info(ev.Generator.TimeLeft.ToString() + " for room " + ev.Generator.Room.RoomType.ToString());
				if (ev.Player.TeamRole.Role != Role.CHAOS_INSURGENCY)
                {
                    if (GenTime.TryGetValue(ev.Generator.Room.RoomType.ToString(), out float Indicheck))
                    {
                        GenTime[ev.Generator.Room.RoomType.ToString()] = ev.Generator.TimeLeft;
                    }
                    else
                    {
                        GenTime.Add(ev.Generator.Room.RoomType.ToString(), ev.Generator.TimeLeft);
                    }
					if (ev.Allow == true)
					{
						PluginManager.Manager.Server.Map.ClearBroadcasts();
						PluginManager.Manager.Server.Map.Broadcast(10, "The Generator in " + ev.Generator.Room.RoomType.ToString() + " Activation Stopped", false);
						ev.SpawnTablet = false;
					}
                }
                else
                {
                    ev.Allow = false;
                }
            }
        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (Holdevent)
            {
                gen = 0;
				Holdevent = false;
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
					ev.Player.SetHealth(250);
				}
            }
        }
        public void OnLure(PlayerLureEvent ev)
        {
            if (Holdevent)
            {

            }
        }
        public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
        {
            if (Holdevent)
            {
				
                if ((ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY || ev.Player.TeamRole.Role == Role.TUTORIAL) && ev.Player.HasItem(ItemType.WEAPON_MANAGER_TABLET))
                {
					plugin.Info(ev.Generator.TimeLeft.ToString() + " for room " + ev.Generator.Room.RoomType.ToString());
					ev.Allow = true;
                    if (GenTime.TryGetValue(ev.Generator.Room.RoomType.ToString(), out float Indicheck))
                    {
                        ev.Generator.TimeLeft = GenTime[ev.Generator.Room.RoomType.ToString()];
                    }
                    PluginManager.Manager.Server.Map.ClearBroadcasts();
                    PluginManager.Manager.Server.Map.Broadcast(10, "The Generator in " + ev.Generator.Room.RoomType.ToString() + " is being Activated", false);
                }
                else
                {
                    ev.Allow = false;
                }
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
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX && ev.Elevator.ElevatorType == ElevatorType.SCP049Chamber)
				{
					ev.AllowUse = false;
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
				if(PluginManager.Manager.Server.Round.Duration == 900 && !S600)
                {
                    PluginManager.Manager.Server.Map.Broadcast(10, "10 MIN REMAIN", false);
					S600 = true;
                }
                else if (PluginManager.Manager.Server.Round.Duration == 600 && !S300)
                {
					S300 = true;
                    PluginManager.Manager.Server.Map.Broadcast(10, "15 MIN REMAIN", false);
                }
                else if (PluginManager.Manager.Server.Round.Duration == 1200 && !S900)
                {
					S900 = true;
                    PluginManager.Manager.Server.Map.Broadcast(10, "5 MIN REMAIN", false);
                }
				else if (PluginManager.Manager.Server.Round.Duration == 1500 && !S1200)
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
                Timing.RunCoroutine(Events.BREACHRESPAWN(ev.Player, ev.Killer));
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
        public void OnCheckEscape(Smod2.Events.PlayerCheckEscapeEvent ev)
        {
            if (Holdevent)
            {
                if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
                {
                    ev.AllowEscape = true;
                    ev.ChangeRole = Role.CHAOS_INSURGENCY;
                    PluginManager.Manager.Server.Map.ClearBroadcasts();
                    PluginManager.Manager.Server.Map.Broadcast(10, ev.Player.TeamRole.Role.ToString() + " Has Escaped", false);
                    FreeSCPS.Add(ev.Player.TeamRole.Role.ToString());
                }
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
        public void OnGeneratorUnlock(Smod2.Events.PlayerGeneratorUnlockEvent ev)
        {
            if (Holdevent)
            {
                if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
                {
                    ev.Allow = true;
                }
                else
                {
                    ev.Allow = false;
                }
            }
        }
		public void OnPlayerDropItem(Smod2.Events.PlayerDropItemEvent ev)
		{
			if (Holdevent)
			{
				if (ev.Item.ItemType == ItemType.RADIO || ev.Item.ItemType == ItemType.LOGICER || ev.Item.ItemType == ItemType.FLASHBANG)
				{
					ev.Allow = false;
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (Holdevent)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					if (ev.Door.Locked == false && ev.Door.Name != "CHECKPOINT_ENT")
					{
						ev.Allow = true;
					}
				}
				else if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY && ev.Door.Locked == false)
				{
					ev.Allow = true;
				}
			}
		}
	}
}
