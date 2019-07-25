using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace ATTG3
{
	internal class HoldOut : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn, IEventHandlerCheckRoundEnd,
        IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerLure,
		IEventHandlerGeneratorInsertTablet, IEventHandlerSummonVehicle, IEventHandlerPlayerTriggerTesla, IEventHandlerDoorAccess, IEventHandlerWarheadDetonate, IEventHandlerPlayerDie
	{
		bool nuke;
        public static int Wave = 1;
        Server Server => PluginManager.Manager.Server;
        public Dictionary<string, int> Time = new Dictionary<string, int>();
		private readonly ATTG3Plugin plugin;
		public HoldOut(ATTG3Plugin plugin) => this.plugin = plugin;
		public Dictionary<string, float> GenTime = new Dictionary<string, float>();
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.HoldOutEvent)
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
                    else if (door.Name == "GATE_B")
                    {
                        door.Locked = true;
                        door.Open = false;
                    }
                    else if (door.Name == "CHECKPOINT_ENT")
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
						door.Open = true;
					}
					else if (door.Name == "106_BOTTOM")
					{
						door.Locked = true;
						door.Open = true;
					}
					else if (door.Name == "106_SECONDARY")
					{
						door.Locked = true;
						door.Open = true;
					}
                    else if (door.Position.y >= 15f && door.Position.y <=  25f)
                    {
                        door.Open = true;
                    }
					door.DontOpenOnWarhead = true;
					door.BlockAfterWarheadDetonation = false;
				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team == Smod2.API.Team.CLASSD)
					{
						player.ChangeRole(Role.SCP_049, true, true, false, true);
					}
                    else if (player.TeamRole.Team == Smod2.API.Team.SCIENTIST)
                    {
                        player.ChangeRole(Role.NTF_LIEUTENANT, true, true, false, true);
                    }
                    else if (player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD)
                    {
                        player.ChangeRole(Role.NTF_LIEUTENANT, true, true, false, true);
                    }
                }
				foreach (Generator079 gen in Generator079.generators)
				{
					gen.NetworkremainingPowerup = (gen.startDuration = 300f);
				}
				foreach (Smod2.API.Item item in PluginManager.Manager.Server.Map.GetItems(Smod2.API.ItemType.MTF_COMMANDER_KEYCARD, true))
				{
					item.Remove();
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
                if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId))
                {
                    Timing.RunCoroutine(Events.CustomitemDoor(ev.Door, ev.Player.GetCurrentItem().ItemType, ev.Player));
                }
            }
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					ev.Player.SetRank("cyan", "TEAM: MTF", null);
					Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
                    ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_173), true);
                    ev.Player.PersonalClearBroadcasts();
                    ev.Player.PersonalBroadcast(10, "SURVIVE 10 MIN", false);
                    ev.Items.Remove(ItemType.DISARMER);
                    ev.Items.Remove(ItemType.MTF_COMMANDER_KEYCARD);
                    ev.Items.Remove(ItemType.MTF_LIEUTENANT_KEYCARD);
                    ev.Items.Remove(ItemType.RADIO);
                }
                else if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
                {
                    if (ev.Player.TeamRole.Role != Role.SCP_049_2)
                    {
                        ev.Player.ChangeRole(Role.SCP_049_2);
                    }
                    ev.Player.SetRank("red", "TEAM: SCP", null);
                    ev.Player.PersonalClearBroadcasts();
                    ev.Player.PersonalBroadcast(10, "KILL ALL MTF", false);
                    ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD));
                }
            }
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
				
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.HoldOutEvent)
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
			if (plugin.HoldOutEvent)
			{
				if (ev.Player.TeamRole.Team != Smod2.API.Team.NINETAILFOX)
				{
					float Indicheck;
					if (GenTime.TryGetValue(ev.Generator.Room.ToString(), out Indicheck))
					{
						GenTime[ev.Generator.Room.ToString()] = ev.Generator.TimeLeft;
					}
					else
					{
						GenTime.Add(ev.Generator.Room.ToString(), ev.Generator.TimeLeft);
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
			if (plugin.HoldOutEvent)
			{
                
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
                var HOLDOUTKILL = EventLStorageList.PlayerkillsHoldOut;
                nuke = false;
				plugin.HoldOutEvent = false;
                Wave = 1;
                HOLDOUTKILL.Clear();
			}
		}
		public void OnLure(PlayerLureEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
				
			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
				
			}
		}
		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
                foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
                {
                    Elevator.MovingSpeed = 5;
                }
            }
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
                ev.Cancel = false;
				
            }
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
				ev.IsCI = false;
                ev.AllowSummon = false;
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (plugin.HoldOutEvent)
			{
				ev.Triggerable = false;
			}
		}
		public void OnDetonate()
		{
			if (plugin.HoldOutEvent)
			{
			}
		}
        /*public void OnGeneratorUnlock(Smod2.Events.PlayerGeneratorUnlockEvent ev)
		{
			if (plugin.HoldOutEvent)
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
			if (plugin.HoldOutEvent)
			{
                var HOLDOUTKILL = EventLStorageList.PlayerkillsHoldOut;
                ev.Player.SetRank("default", "DEAD", null);
                if (ev.Player.TeamRole.Role == Role.SCP_049_2)
                {
                    if (Server.Round.Stats.Zombies <= 4 && Wave != 4)
                    {
                        Timing.RunCoroutine(Events.RespawnSpawn(ev.Player, "holdout"));
                        Wave++;
                        Server.Map.Broadcast(10, "WAVE " + Wave + " STARTING", false);
                    }
                    if (ev.Killer != null)
                    {
                        if (!HOLDOUTKILL.ContainsKey(ev.Killer.SteamId))
                        {
                            HOLDOUTKILL.Add(ev.Killer.SteamId, 1);
                        }
                        else if (HOLDOUTKILL.ContainsKey(ev.Killer.SteamId))
                        {
                            HOLDOUTKILL[ev.Killer.SteamId]++;
                            if ((HOLDOUTKILL[ev.Killer.SteamId] % 3) == 0)
                            {
                                ev.Killer.GiveItem(ItemType.FRAG_GRENADE);
                                ev.Killer.PersonalBroadcast(5, "Grenade Added", false);
                            }
                            else if (HOLDOUTKILL[ev.Killer.SteamId] == 10)
                            {
                                ev.Killer.GiveItem(ItemType.LOGICER);
                                ev.Killer.PersonalBroadcast(5, "LOGICER Added", false);
                                ev.Player.PersonalClearBroadcasts();
                            }
                        }
                    }
                }
			}
		}
        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (plugin.HoldOutEvent)
            {
                // Add SCP-049-2 Attack Waves and heal all players when new wave starts
                if (Wave != 4&& Server.Round.Stats.NTFAlive != 0)
                {
                    ev.Status = ROUND_END_STATUS.ON_GOING;
                }
                else if (Server.Round.Stats.NTFAlive == 0)
                {
                    ev.Status = ROUND_END_STATUS.SCP_VICTORY;
                }
                else if (Wave == 4)
                {
                    ev.Status = ROUND_END_STATUS.MTF_VICTORY;
                }
            }
        }
        public void OnSpawn(Smod2.Events.PlayerSpawnEvent ev)
        {
            if (plugin.HoldOutEvent)
            {
                if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
                {
                    if (ev.Player.TeamRole.Role != Role.SCP_049_2)
                    {
                        ev.Player.ChangeRole(Role.SCP_049_2);
                    }
                    ev.Player.SetRank("red", "TEAM: SCP", null);
                    ev.Player.PersonalClearBroadcasts();
                    ev.Player.PersonalBroadcast(10, "KILL ALL MTF", false);
                    ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD));
                }
            }
        }
    }
}