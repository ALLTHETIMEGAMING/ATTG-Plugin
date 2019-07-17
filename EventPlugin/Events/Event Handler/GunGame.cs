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
         IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerPlayerDie, IEventHandlerSpawn
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
                    else if (door.Name == "GATE_A")
                    {
                        door.Locked = true;
                    }
                    else if (door.Name == "GATE_B")
                    {
                        door.Locked = true;
                    }
                }
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    if (player.TeamRole.Role != Role.CLASSD)
                    {
                        player.ChangeRole(Role.CLASSD, true, true, true, true);
                    }
                }
                foreach (Pickup pickup in Object.FindObjectsOfType<Pickup>())
                {
                    NetworkServer.Destroy(pickup.gameObject);
                }
                Events.AllSpawns();
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    ((UnityEngine.GameObject)player.GetGameObject()).GetComponent<WeaponManager>().NetworkfriendlyFire = true;
                }
                PluginManager.Manager.Server.Map.Broadcast(10, "BE THE 1ST PERSON TO GET 10 KILLS TO WIN.\n<color=#FFD700>NOTE THIS EVENT IS NOT DONE</color>", false);
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
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    ((UnityEngine.GameObject)player.GetGameObject()).GetComponent<WeaponManager>().NetworkfriendlyFire = false;
                }
                MostKills = 0;
            }
        }
        public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
        {
            if (GunGameBool)
            {
                ev.Items.Clear();
                if (ev.Player.TeamRole.Team != Smod2.API.Team.CLASSD)
                {
                    ev.Player.ChangeRole(Role.CLASSD);
                }
                Timing.RunCoroutine(Events.GunGamItems(ev.Player));
                Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
                Timing.RunCoroutine(Events.GGTeleport(ev.Player));
                ev.Items.Add(ItemType.MTF_COMMANDER_KEYCARD);
            }
        }
        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (GunGameBool)
            {
                if (!EventLStorageList.PlayerKillGunGame.ContainsValue(10))
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
                    Timing.RunCoroutine(Events.GGRespawn(ev.Player));
                    Timing.RunCoroutine(Events.UpdateItems(ev.Killer));
                }
            }
        }
        public void OnSpawn(Smod2.Events.PlayerSpawnEvent ev)
        {
            if (GunGameBool)
            {
                int RandomInt = new System.Random().Next(EventLStorageList.GunGameSpawns.Count);
                Vector spawnpos = EventLStorageList.GunGameSpawns[RandomInt];
                ev.SpawnPos = spawnpos;
                Timing.RunCoroutine(Events.GunGamItems(ev.Player));
            }
        }
    }
}


