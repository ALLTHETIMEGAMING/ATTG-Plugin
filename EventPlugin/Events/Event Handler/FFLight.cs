using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using MEC;

namespace ATTG3
{
    internal class FFLight : IEventHandlerRoundStart,
        IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet,
        IEventHandlerSetRole, IEventHandlerSummonVehicle
    {
        public static bool FFLightEvent;
        bool CustomSpawn;
        private readonly ATTG3Plugin plugin;
        public FFLight(ATTG3Plugin plugin) => this.plugin=plugin;
        public void OnRoundStart(RoundStartEvent ev)
        {
            if (FFLightEvent)
            {
                // Starts Event
                foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                {
                    if (door.Name == "914")
                    {
                        door.Locked = true;
                    }
                    if (door.Name == "CHECKPOINT_LCZ_A")
                    {
                        door.Locked = true;
                    }
                    if (door.Name == "CHECKPOINT_LCZ_B")
                    {
                        door.Locked = true;
                    }
                }
                plugin.Info(EventLStorageList.LCZPOS.Count + "LCZ Spawn POS at Start");
                // Changes Players Role
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    player.ChangeRole(Role.CLASSD, true, true, true, true);
                }
                if (EventLStorageList.LCZPOS.Count > 0)
                {
                    plugin.Info("Custom Spawn is True");
                    CustomSpawn = true;
                }
                else
                {
                    plugin.Info("Custom Spawn is False");
                    PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>You have 60 Seconds to hide</Color></SIZE>", false);
                }
                Timing.RunCoroutine(Events.Delay60());
                
            }
        }
        public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
        {
            if (FFLightEvent)
            {
                // Stops Nuke
                ev.Allow=false;
                ev.Player.PersonalBroadcast(10, "Nuke cannot be activated at the moment", false);
            }
        }
        public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
        {
            if (FFLightEvent)
            {
                // Stops Generator eject
                ev.Allow=false;
            }
        }
        public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
        {
            if (FFLightEvent)
            {
                // Sets Spawn Items
                ev.Items.Clear();
                Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
                ev.Items.Add(ItemType.E11_STANDARD_RIFLE);
                ev.Items.Add(ItemType.MTF_COMMANDER_KEYCARD);
                ev.Items.Add(ItemType.MEDKIT);
            }
        }
        public void OnSummonVehicle(SummonVehicleEvent ev)
        {
            if (FFLightEvent)
            {
                // Stops Respawns
                ev.AllowSummon = false;
            }
        }
        public void OnSpawn(Smod2.Events.PlayerSpawnEvent ev)
        {
            if (FFLightEvent)
            {
                if (EventLStorageList.LCZPOS.Count != 0 && CustomSpawn) {
                    // Spawning Player at custom spawn point
                    int RandomInt = new System.Random().Next(EventLStorageList.LCZPOS.Count);
                    Vector spawnpos = EventLStorageList.LCZPOS[RandomInt];
                    ev.SpawnPos = spawnpos;
                    plugin.Info(spawnpos.ToString());
                    EventLStorageList.LCZPOS.Remove(spawnpos);
                }
                Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
            }
        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (FFLightEvent)
            {
                // turns off event and FF
                FFLightEvent = false;
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    ((UnityEngine.GameObject)player.GetGameObject()).GetComponent<WeaponManager>().NetworkfriendlyFire = false;
                }
            }
        }
    }
}


