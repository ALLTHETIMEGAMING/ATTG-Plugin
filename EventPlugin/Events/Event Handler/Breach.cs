using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace ATTG3
{
    internal class Breach : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
        IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerLure,
        IEventHandlerGeneratorInsertTablet, IEventHandlerUpdate, IEventHandlerDoorAccess, IEventHandlerCheckRoundEnd, IEventHandlerPlayerDie

    {
        public static bool Nuke;
        public static int gen;
        public static bool Breachevent;
        public static List<string> FreeSCPS = new List<string>();
        public static List<string> MTF = new List<string>();
        public static List<string> CI = new List<string>();
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
                    else if (door.Position.y <= -730 && door.Position.y >= 740)
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
                }
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    if (player.TeamRole.Team == Smod2.API.Team.SCP || player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD)
                    {
                        player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
                        player.PersonalBroadcast(10, "Turn on the generators to free the scps", false);
                        CI.Add(player.SteamId);
                    }
                    else if (player.TeamRole.Team == Smod2.API.Team.CLASSD || player.TeamRole.Team == Smod2.API.Team.SCIENTIST)
                    {
                        player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
                        player.PersonalBroadcast(10, "Stop CI from turning on the generators", false);
                        MTF.Add(player.SteamId);
                    }
                }
            }
        }
        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (Breachevent)
            {
                if (ev.Server.Round.Duration >= 900)
                {
                    ev.Status = ROUND_END_STATUS.MTF_VICTORY;
                    ev.Round.Stats.ScientistsEscaped = 6;
                }
                else if (FreeSCPS.Count == 6)
                {
                    ev.Status = ROUND_END_STATUS.CI_VICTORY;
                    ev.Round.Stats.ClassDEscaped = 6;
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
                }
                else if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
                {
                    ev.Items.Remove(ItemType.DISARMER);
                    ev.Items.Remove(ItemType.WEAPON_MANAGER_TABLET);
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
                            player.PersonalBroadcast(10, "CI:FREE THE SCPS. GO TO THE SCPS CONTAINMENT AND OPEN THE DOOR.", false);
                        }
                        else if (player.TeamRole.Role == Role.NTF_COMMANDER)
                        {
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
                    ev.Allow = true;
                }
            }
        }
        public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
        {
            if (Breachevent)
            {
                ev.SpawnChaos = true;
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
            if (Breachevent && GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time != 0)
            {
                GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time = 0;
            }
        }
        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (Breachevent && gen == 5)
            {
                if (ev.Door.Name == "106_SECONDARY" && !FreeSCPS.Contains("106"))
                {
                    ev.Door.Locked = true;
                    ev.Door.Open = true;
                    FreeSCPS.Add("106");
                    PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>SCP-106 ESCAPED</Color></SIZE>", false);
                }
                else if (ev.Door.Name == "106_PRIMARY" && !FreeSCPS.Contains("106"))
                {
                    ev.Door.Locked = true;
                    ev.Door.Open = true;
                    FreeSCPS.Add("106");
                    PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>SCP-106 ESCAPED</Color></SIZE>", false);
                }
                else if (ev.Door.Position.y <= -730 && !FreeSCPS.Contains("049") && ev.Door.Name != "049_ARMORY" && ev.Door.Position.y >= 740)
                {
                    ev.Door.Locked = true;
                    ev.Door.Open = true;
                    FreeSCPS.Add("049");
                    PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>SCP-049 ESCAPED</Color></SIZE>", false);
                }
                else if (ev.Door.Name == "096" && !FreeSCPS.Contains("096"))
                {
                    ev.Door.Locked = true;
                    ev.Door.Open = true;
                    FreeSCPS.Add("096");
                    PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>SCP-096 ESCAPED</Color></SIZE>", false);
                }
                else if (ev.Door.Name == "173" && !FreeSCPS.Contains("173"))
                {
                    ev.Door.Locked = true;
                    ev.Door.Open = true;
                    FreeSCPS.Add("173");
                    PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>SCP-173 ESCAPED</Color></SIZE>", false);
                }
                else if (ev.Door.Position.y < -1010 && !FreeSCPS.Contains("939"))
                {
                    ev.Door.Locked = true;
                    ev.Door.Open = true;
                    FreeSCPS.Add("939");
                    PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>SCP-939 ESCAPED</Color></SIZE>", false);
                }
            }
        }
        public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
        {
            if (Breachevent)
            {
                ev.SpawnRagdoll = false;
                Timing.RunCoroutine(Events.BREACHRESPAWN(ev.Player, ev.Killer));
            }
        }
    }
}
