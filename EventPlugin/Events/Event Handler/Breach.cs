using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ATTG3
{
    internal class Breach : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerSummonVehicle, IEventHandlerPlayerHurt, IEventHandlerCheckEscape, IEventHandler106Teleport,
        IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerLure, IEventHandlerSetRoleMaxHP,
        IEventHandlerGeneratorInsertTablet, IEventHandlerUpdate, IEventHandlerDoorAccess, IEventHandlerCheckRoundEnd, IEventHandlerPlayerDie, IEventHandlerPocketDimensionEnter, IEventHandlerPlayerJoin
    {
        public static bool Nuke;
        public static int gen;
        public static bool Breachevent;
        public static List<string> FreeSCPS = new List<string>();
        public static int MTF;
        public static int CI;
        public static Dictionary<string, float> GenTime = new Dictionary<string, float>();
        //bool spawn;
        private readonly ATTG3Plugin plugin;
        public Breach(ATTG3Plugin plugin) => this.plugin = plugin;
        public void OnRoundStart(RoundStartEvent ev)
        {
            if (Breachevent)
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
                    else if (door.Position.y <= -730 && door.Position.y >= -740 && door.Name != "049_ARMORY")
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
                    else if (door.Position.y < -1010)
                    {
                        door.Locked = true;
                    }
                    else if (door.Name == "ESCAPE")
                    {
                        door.Locked = true;
                    }
                }
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    if (player.TeamRole.Team == Smod2.API.Team.SCP || player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD)
                    {
                        player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
                        player.PersonalClearBroadcasts();
                        player.PersonalBroadcast(10, "Turn on the generators to free the SCPs", false);
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
            if (Breachevent)
            {
                if (ev.Server.Round.Duration >= 1500)
                {
                    ev.Status = ROUND_END_STATUS.MTF_VICTORY;
                    ev.Round.Stats.ScientistsEscaped = 5;
                }
                else if (FreeSCPS.Count == 5)
                {
                    ev.Status = ROUND_END_STATUS.CI_VICTORY;
                    ev.Round.Stats.ClassDEscaped = 6;
                }
                else if (ev.Server.NumPlayers == 0)
                {
                    ev.Status = ROUND_END_STATUS.FORCE_END;
                }
                else
                {
                    ev.Status = ROUND_END_STATUS.ON_GOING;
                }
            }
        }
        public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
        {
            if (Breachevent)
            {
                if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
                {
                    if (gen != 5)
                    {
                        ev.Items.Add(ItemType.WEAPON_MANAGER_TABLET);
                    }
                    ev.Items.Add(ItemType.FRAG_GRENADE);
                    ev.Items.Add(ItemType.E11_STANDARD_RIFLE);
                }
                else if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
                {
                    ev.Items.Remove(ItemType.DISARMER);
                    ev.Items.Remove(ItemType.WEAPON_MANAGER_TABLET);

                }
                else if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
                {
                    ev.Player.PersonalBroadcast(10, "ESCAPE. YOU CAN NOT ATTACK MTF OR CI", false);
                }
            }
        }
        public void OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            if (Breachevent)
            {
                gen++;
                if (gen == 5)
                {
                    foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                    {
                        if (door.Name == "106_SECONDARY")
                        {
                            door.Locked = false;
                        }
                        else if (door.Name == "106_PRIMARY")
                        {
                            door.Locked = false;
                        }
                        else if (door.Position.y <= -730 && door.Position.y >= 740)
                        {
                            door.Locked = false;
                        }
                        else if (door.Name == "173")
                        {
                            door.Locked = false;
                        }
                        else if (door.Name == "096")
                        {
                            door.Locked = false;
                        }
                        else if (door.Position.y < -1010)
                        {
                            door.Locked = false;
                        }
                    }
                    foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                    {
                        if (player.TeamRole.Role == Role.CHAOS_INSURGENCY)
                        {
                            player.PersonalClearBroadcasts();
                            player.PersonalBroadcast(10, "CI:FREE THE SCPS. GO TO THE SCPS CONTAINMENT AND OPEN THE DOOR.", false);
                        }
                        else if (player.TeamRole.Role == Role.NTF_COMMANDER)
                        {
                            player.PersonalClearBroadcasts();
                            player.PersonalBroadcast(10, "MTF:STOP CI FROM FREEING THE SCPS. GUARD THE SCPs CONTAINMENT CELLS", false);
                        }
                    }
                }
            }
        }
        public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
        {
            if (Breachevent)
            {
                ev.Allow = false;
                ev.Player.PersonalBroadcast(10, "Nuke cannot be activated at the moment", false);
            }
        }
        public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
        {
            if (Breachevent)
            {
                if (ev.Player.TeamRole.Role != Role.CHAOS_INSURGENCY)
                {
                    if (GenTime.ContainsKey(ev.Generator.Room.ToString()))
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
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (Breachevent)
            {
                gen = 0;
                Breachevent = false;
                FreeSCPS.Clear();
            }
        }
        public void OnSpawn(PlayerSpawnEvent ev)
        {
            if (Breachevent)
            {
                if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
                {
                    ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD);
                }
                else if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
                {

                }
            }
        }
        public void OnLure(PlayerLureEvent ev)
        {
            if (Breachevent)
            {

            }
        }
        public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
        {
            if (Breachevent)
            {
                if (GenTime.ContainsKey(ev.Generator.Room.ToString()))
                {
                    ev.Generator.TimeLeft = GenTime[ev.Generator.Room.ToString()];
                }
                if ((ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY || ev.Player.TeamRole.Role == Role.TUTORIAL) && ev.Player.HasItem(ItemType.WEAPON_MANAGER_TABLET))
                {
                    ev.Allow = true;
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
            if (Breachevent)
            {

            }
        }
        public void OnStartCountdown(WarheadStartEvent ev)
        {
            if (Breachevent)
            {
                Nuke = true;
            }
        }
        public void OnStopCountdown(WarheadStopEvent ev)
        {
            if (Breachevent)
            {
                Nuke = false;
            }
        }
        public void OnUpdate(Smod2.Events.UpdateEvent ev)
        {
            if (Breachevent)
            {
                if (GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time != 0)
                {
                    GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time = 0;
                }
                /*if(PluginManager.Manager.Server.Round.Duration == 300)
                {
                    PluginManager.Manager.Server.Map.Broadcast(10, "15 MIN REMAIN", false);
                }
                else if (PluginManager.Manager.Server.Round.Duration == 600)
                {
                    PluginManager.Manager.Server.Map.Broadcast(10, "10 MIN REMAIN", false);
                }
                else if (PluginManager.Manager.Server.Round.Duration == 900)
                {
                    PluginManager.Manager.Server.Map.Broadcast(10, "5 MIN REMAIN", false);
                }*/
            }
        }
        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (Breachevent && gen == 5)
            {
                if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
                {
                    if (ev.Door.Name == "106_SECONDARY" && Events.TUTCOUNT(Role.SCP_106) == 0 && !FreeSCPS.Contains("SCP_106"))
                    {
                        ev.Door.Locked = true;
                        ev.Door.Open = true;
                        PluginManager.Manager.Server.Map.ClearBroadcasts();
                        PluginManager.Manager.Server.Map.Broadcast(10, "SCP-106 Extraction Started", false);
                        Events.BreachSCP(ev.Player, "106");
                    }
                    else if (ev.Door.Name == "106_PRIMARY" && Events.TUTCOUNT(Role.SCP_106) == 0 && !FreeSCPS.Contains("SCP_106"))
                    {
                        ev.Door.Locked = true;
                        ev.Door.Open = true;
                        PluginManager.Manager.Server.Map.ClearBroadcasts();
                        PluginManager.Manager.Server.Map.Broadcast(10, "SCP-106 Extraction Started", false);
                        Events.BreachSCP(ev.Player, "106");
                    }
                    else if (ev.Door.Position.y <= -730 && Events.TUTCOUNT(Role.SCP_049) == 0 && ev.Door.Name != "049_ARMORY" && ev.Door.Position.y >= -740 && !FreeSCPS.Contains("SCP_049"))
                    {
                        ev.Door.Locked = true;
                        ev.Door.Open = true;
                        PluginManager.Manager.Server.Map.ClearBroadcasts();
                        PluginManager.Manager.Server.Map.Broadcast(10, "SCP-049 Extraction Started", false);
                        Events.BreachSCP(ev.Player, "049");
                    }
                    else if (ev.Door.Name == "096" && Events.TUTCOUNT(Role.SCP_096) == 0 && !FreeSCPS.Contains("SCP_096"))
                    {
                        ev.Door.Locked = true;
                        ev.Door.Open = true;
                        PluginManager.Manager.Server.Map.ClearBroadcasts();
                        PluginManager.Manager.Server.Map.Broadcast(10, "SCP-096 Extraction Started", false);
                        Events.BreachSCP(ev.Player, "096");
                    }
                    else if (ev.Door.Name == "173" && Events.TUTCOUNT(Role.SCP_173) == 0 && !FreeSCPS.Contains("SCP_173"))
                    {
                        ev.Door.Locked = true;
                        ev.Door.Open = true;
                        PluginManager.Manager.Server.Map.ClearBroadcasts();
                        PluginManager.Manager.Server.Map.Broadcast(10, "SCP-173 Extraction Started", false);
                        Events.BreachSCP(ev.Player, "173");

                    }
                    else if (ev.Door.Position.y < -1010 && Events.TUTCOUNT(Role.SCP_939_53) == 0 && !FreeSCPS.Contains("SCP_939_59"))
                    {
                        ev.Door.Locked = true;
                        ev.Door.Open = true;
                        PluginManager.Manager.Server.Map.ClearBroadcasts();
                        PluginManager.Manager.Server.Map.Broadcast(10, "SCP-939 Extraction Started", false);
                        Events.BreachSCP(ev.Player, "939");
                    }
                }
                else if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP && ev.Door.Name == "ESCAPE")
                {
                    PluginManager.Manager.Server.Map.ClearBroadcasts();
                    PluginManager.Manager.Server.Map.Broadcast(10, ev.Player.TeamRole.Role.ToString() + " Has Escaped", false);
                    FreeSCPS.Add(ev.Player.TeamRole.Role.ToString());
                    ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
                }
                else
                {
                    if (ev.Door.Name == "106_SECONDARY")
                    {
                        ev.Allow = false;
                    }
                    else if (ev.Door.Name == "106_PRIMARY")
                    {
                        ev.Allow = false;
                    }
                    else if (ev.Door.Position.y <= -730 && ev.Door.Name != "049_ARMORY" && ev.Door.Position.y >= -740)
                    {
                        ev.Allow = false;
                    }
                    else if (ev.Door.Name == "096")
                    {
                        ev.Allow = false;
                    }
                    else if (ev.Door.Name == "173")
                    {
                        ev.Allow = false;
                    }
                    else if (ev.Door.Position.y < -1010)
                    {
                        ev.Allow = false;
                    }
                }
            }
        }
        public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
        {
            if (Breachevent)
            {
                ev.SpawnRagdoll = false;
                Timing.RunCoroutine(Events.BREACHRESPAWN(ev.Player, ev.Killer));
                //Events.IsEvan("Breach", MTF, CI, Role.NTF_COMMANDER, Role.CHAOS_INSURGENCY);
            }
        }
        public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
        {
            if (Breachevent)
            {
                if (ev.Attacker.TeamRole.Team == Smod2.API.Team.SCP)
                {
                    ev.Damage = 0;
                }
            }
        }
        public void OnSummonVehicle(SummonVehicleEvent ev)
        {
            if (Breachevent)
            {
                ev.AllowSummon = false;
            }
        }
        public void OnPocketDimensionEnter(Smod2.Events.PlayerPocketDimensionEnterEvent ev)
        {
            if (Breachevent)
            {
                ev.TargetPosition = ev.LastPosition;
            }
        }
        public void OnCheckEscape(Smod2.Events.PlayerCheckEscapeEvent ev)
        {
            if (Breachevent)
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
            if (Breachevent)
            {
                if (plugin.RoundStarted)
                {
                    if ((PluginManager.Manager.Server.NumPlayers % 2) == 0)
                    {
                        if (Events.TUTCOUNT(Role.NTF_COMMANDER) > Events.TUTCOUNT(Role.CHAOS_INSURGENCY))
                        {
                            ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
                            CI++;
                        }
                        else if (Events.TUTCOUNT(Role.CHAOS_INSURGENCY) > Events.TUTCOUNT(Role.NTF_COMMANDER))
                        {
                            ev.Player.ChangeRole(Role.NTF_COMMANDER);
                            MTF++;
                        }
                    }
                    else
                    {
                        ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
                    }
                }
            }
        }
        public void OnSetRoleMaxHP(Smod2.EventSystem.Events.SetRoleMaxHPEvent ev)
        {
            if (Breachevent)
            {
                if (ev.Role == Role.CHAOS_INSURGENCY)
                {
                    ev.MaxHP = 250;
                }
            }
        }
        public void OnGeneratorUnlock(Smod2.Events.PlayerGeneratorUnlockEvent ev)
        {
            if (Breachevent)
            {
                if (ev.Player.TeamRole.Role != Role.CHAOS_INSURGENCY)
                {
                    ev.Allow = false;
                }
            }
        }
        public void On106Teleport(Smod2.Events.Player106TeleportEvent ev)
        {
            if (Breachevent)
            {
                ev.Position = ev.Player.GetPosition();
            }
        }
    }
}
