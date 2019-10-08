using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;

namespace ATTG3
{
    internal class TDM : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerSetRole, IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerShoot,
        IEventHandlerPlayerDie, IEventHandlerSpawn, IEventHandlerPlayerDropItem, IEventHandlerDoorAccess, IEventHandlerPlayerJoin, IEventHandlerElevatorUse, IEventHandlerUpdate, IEventHandlerPlayerTriggerTesla
    {


        Server Server => PluginManager.Manager.Server;
        private readonly ATTG3Plugin plugin;
        public TDM(ATTG3Plugin plugin) => this.plugin = plugin;
        public static int MTFKill = 0;
        public static int CIKills = 0;
        public static bool Event;
        public void OnRoundStart(RoundStartEvent ev)
        {
            if (Event)
            {
                foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                {
                    if (door.Name == "SURFACE_GATE")
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
                    }
                    else if (door.Name == "CHECKPOINT_LCZ_A")
                    {
                        door.Locked = true;
                        door.Open = true;
                    }
                    else if (door.Name == "LCZ_ARMORY")
                    {
                        door.Locked = true;
                    }
                    else if (door.Name == "CHECKPOINT_LCZ_B")
                    {
                        door.Locked = true;
                        door.Open = true;
                    }
                    else if (door.Name == "INTERCOM")
                    {
                        door.Locked = true;
                    }
                    else if (door.Name == "173")
                    {
                        door.Locked = true;
                    }
                }
                foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
                {
                    if (Elevator.ElevatorType == ElevatorType.LiftA || Elevator.ElevatorType == ElevatorType.LiftB)
                    {
                        Elevator.Use();
                        Elevator.Locked = true;
                    }
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
                PluginManager.Manager.Server.Map.Broadcast(10, "BE THE TEAM WITH THE MOST KILLS AT THE END OF THE ROUND", false);
            }
        }
        public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
        {
            if (Event)
            {
                ev.Items.Clear();
                ev.Items.Add(ItemType.E11_STANDARD_RIFLE);
                ev.Items.Add(ItemType.P90);
                ev.Items.Add(ItemType.COM15);
                ev.Items.Add(ItemType.MEDKIT);
            }
        }
        public void OnSpawn(Smod2.Events.PlayerSpawnEvent ev)
        {
            if (Event)
            {
                if (ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
                {
                    ev.SpawnPos = ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.FACILITY_GUARD);
                }
                else if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
                {
                    ev.SpawnPos = ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCIENTIST);
                }
                Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
            }
        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (Event)
            {
                MTFKill = 0;
                CIKills = 0;
                Event = false;
            }
        }
        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (Event)
            {
                if (ev.Server.Round.Duration >= 900)
                {
                    if (MTFKill > CIKills)
                    {
                        ev.Status = ROUND_END_STATUS.MTF_VICTORY;
                        ev.Server.Round.Stats.ScientistsEscaped = 30;
                    }
                    else if (MTFKill < CIKills)
                    {
                        ev.Status = ROUND_END_STATUS.CI_VICTORY;
                        ev.Server.Round.Stats.ClassDEscaped = 30;
                        foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                        {
                            if (player.TeamRole.Role != Role.CHAOS_INSURGENCY)
                            {
                                player.ChangeRole(Role.SPECTATOR);
                            }
                        }
                    }
                    else
                    {
                        ev.Status = ROUND_END_STATUS.NO_VICTORY;
                    }
                }
                else if (PluginManager.Manager.Server.GetPlayers().Count <= 1)
                {
                    ev.Server.Map.ClearBroadcasts();
                    ev.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>NO PLAYERS. ENDING ROUND</Color></SIZE>", false);
                    ev.Status = ROUND_END_STATUS.NO_VICTORY;
                }
            }
        }
        public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
        {
            if (Event)
            {
                ev.SpawnRagdoll = false;
                Timing.RunCoroutine(Events.TDMRespawn(ev.Player, ev.Killer));
            }
        }
        public void OnSummonVehicle(SummonVehicleEvent ev)
        {
            if (Event)
            {
                ev.AllowSummon = false;
            }
        }
        public void OnPlayerDropItem(PlayerDropItemEvent ev)
        {
            if (Event)
            {
                ev.Allow = false;
            }
        }
        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (Event)
            {
                if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
                {
                    if (ev.Door.Locked == false && ev.Door.Name != "CHECKPOINT_ENT")
                    {
                        ev.Allow = true;
                    }
                    if (ev.Door.Name == "CHECKPOINT_ENT")
                    {
                        ev.Player.Kill();
                        ev.Player.PersonalBroadcast(10, "YOU TRIED TO ENTER A RESTRICTED ZONE", false);
                    }
                }
                else if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY && ev.Door.Locked == false)
                {
                    ev.Allow = true;
                }
            }
        }
        public void OnPlayerJoin(Smod2.Events.PlayerJoinEvent ev)
        {
            if (Event)
            {
                if (plugin.RoundStarted)
                {
                    if (Events.TUTCOUNT(Role.NTF_COMMANDER) > Events.TUTCOUNT(Role.CHAOS_INSURGENCY))
                    {
                        ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
                    }
                    else if (Events.TUTCOUNT(Role.CHAOS_INSURGENCY) > Events.TUTCOUNT(Role.NTF_COMMANDER))
                    {
                        ev.Player.ChangeRole(Role.NTF_COMMANDER);
                    }
                    else
                    {
                        ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
                    }
                }
            }
        }
        public void OnElevatorUse(PlayerElevatorUseEvent ev)
        {
            if (Event)
            {
                if ((ev.Elevator.ElevatorType != ElevatorType.GateA && ev.Elevator.ElevatorType != ElevatorType.WarheadRoom && ev.Elevator.ElevatorType != ElevatorType.SCP049Chamber) && ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
                {
                    ev.AllowUse = false;
                }
                else if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
                {
                    if (ev.Elevator.ElevatorType == ElevatorType.LiftA || ev.Elevator.ElevatorType == ElevatorType.LiftB)
                    {
                        Vector pos = ev.Elevator.GetPositions()[1];
                        ev.Player.Teleport(pos, true);
                    }
                }
            }
        }
        public void OnUpdate(Smod2.Events.UpdateEvent ev)
        {
            if (Event)
            {
                if (GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time != 0)
                {
                    GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time = 0;
                }
            }
        }
        public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
        {
            if (Event)
            {
                ev.Triggerable = false;
            }
        }
        public void OnShoot(Smod2.Events.PlayerShootEvent ev)
        {
            if (Event)
            {
                if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
                {
                    Events.MTFEnterTDM(ev.Player);
                    Events.MTFEnterPlayer(ev.Player);
                }
            }
        }
    }
}


