using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
    internal class GunGame : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSetRole,
         IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerPlayerDie, IEventHandlerSpawn, IEventHandlerPlayerTriggerTesla,
        IEventHandlerPlayerHurt, IEventHandlerPlayerDropItem
    {

        private readonly ATTG3Plugin plugin;
        public GunGame(ATTG3Plugin plugin) => this.plugin = plugin;
        public static bool GunGameBool;
        int MostKills = 0;
        public void OnRoundStart(RoundStartEvent ev)
        {
            if (GunGameBool)
            {
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
                    else if (door.Name == "914")
                    {
                        door.Locked = true;
                    }
                    else if (door.Name == "ESCAPE")
                    {
                        door.Locked = true;
                    }
                    else if (door.Name == "INTERCOM")
                    {
                        door.Locked = true;
                    }
                    else if (door.Name == "GATE_A")
                    {
                        door.Locked = true;
                        door.Open = true;
                    }
					else if (door.Name == "GATE_B")
					{
						door.Locked = true;
						door.Open = true;
					}
					else if (door.Name == "CHECKPOINT_ENT")
					{
						door.Locked = true;
						door.Open = false;
					}
					foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
                    {
                        if (Elevator.ElevatorType == ElevatorType.GateA || Elevator.ElevatorType == ElevatorType.GateB)
                        {
                            Elevator.Locked = true;
                        }
                    }
                }
                foreach (Pickup pickup in Object.FindObjectsOfType<Pickup>())
                {
                    NetworkServer.Destroy(pickup.gameObject);
                }
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    ((UnityEngine.GameObject)player.GetGameObject()).GetComponent<WeaponManager>().NetworkfriendlyFire = true;
                }
                PluginManager.Manager.Server.Map.Broadcast(10, "BE THE 1ST PERSON TO GET 20 KILLS TO WIN.\n<color=#FFD700>NOTE THIS EVENT IS NOT DONE</color>", false);
            }
        }
        public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
        {
            if (GunGameBool)
            {
                //Stops the nuke lever
                ev.Allow = false;
                ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
            }
        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (GunGameBool)
            {
                // Stops the event
                GunGameBool = false;
                EventLStorageList.PlayerKillGunGame.Clear();
                EventLStorageList.GunGameSpawns.Clear();
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    ((UnityEngine.GameObject)player.GetGameObject()).GetComponent<WeaponManager>().NetworkfriendlyFire = false;
                }
                MostKills = 0;
                ev.Round.Stats.ClassDEscaped = ev.Round.Stats.ClassDStart;
            }
        }
        public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
        {
            if (GunGameBool)
            {
                ev.Items.Clear();
                if (ev.Player.TeamRole.Role != Smod2.API.Role.CLASSD)
                {
                    ev.Player.ChangeRole(Role.CLASSD);
                }
                else if (ev.Player.TeamRole.Role == Role.CLASSD)
                {
                    Timing.RunCoroutine(Events.GunGamItems(ev.Player));
                }
            }
        }
        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (GunGameBool)
            {
                if (!EventLStorageList.PlayerKillGunGame.ContainsValue(20))
                {
                    ev.Status = ROUND_END_STATUS.ON_GOING;
                }
            }
        }
        public void OnSummonVehicle(SummonVehicleEvent ev)
        {
            if (GunGameBool)
            {
                ev.AllowSummon = false;
            }
        }
        public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
        {
            if (GunGameBool)
            {

                // Checking if player is in list
                if (ev.Killer != null && ev.Killer.SteamId != ev.Player.SteamId && ev.Killer.TeamRole.Role != Role.TUTORIAL)
                {
                    if (!EventLStorageList.PlayerKillGunGame.ContainsKey(ev.Killer.SteamId))
                    {
                        EventLStorageList.PlayerKillGunGame.Add(ev.Killer.SteamId, 1);
                    }
                    else if (EventLStorageList.PlayerKillGunGame.ContainsKey(ev.Killer.SteamId))
                    {
                        EventLStorageList.PlayerKillGunGame[ev.Killer.SteamId]++;
                        if (EventLStorageList.PlayerKillGunGame[ev.Killer.SteamId] > MostKills)
                        {
                            MostKills = EventLStorageList.PlayerKillGunGame[ev.Killer.SteamId];
                            PluginManager.Manager.Server.Map.ClearBroadcasts();
                            PluginManager.Manager.Server.Map.Broadcast(10, ev.Killer.Name + " has the most kills " + MostKills, false);
                        }
                    }
                    ev.Killer.SetRank("Green", EventLStorageList.PlayerKillGunGame[ev.Killer.SteamId] + " Kills", null);
                    
                    Timing.RunCoroutine(Events.UpdateItems(ev.Killer));
                }
                Timing.RunCoroutine(Events.GGRespawn(ev.Player));
                ev.SpawnRagdoll = false;
            }
        }
        public void OnSpawn(PlayerSpawnEvent ev)
        {
            if (GunGameBool)
            {
                if (ev.Player.TeamRole.Role == Role.CLASSD)
                {
                    ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.FACILITY_GUARD);
                }
            }
        }
        public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
        {
            if (GunGameBool)
            {
                ev.Triggerable = false;
            }
        }
        public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
        {
            if (GunGameBool)
            {
                if (ev.DamageType == DamageType.COM15)
                {
                    if (EventLStorageList.PlayerKillGunGame[ev.Attacker.SteamId] > 18)
                    {
                        ev.Damage = 10;
                    }
                }
            }
        }
        public void OnPlayerDropItem(Smod2.Events.PlayerDropItemEvent ev)
        {
            if (GunGameBool)
            {
                ev.Allow = false;
            }
        }
    }
}


