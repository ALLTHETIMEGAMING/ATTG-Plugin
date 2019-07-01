using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using MEC;

namespace ATTG3
{
    internal class FFLight : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
        IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet,
        IEventHandlerSetRole, IEventHandlerSummonVehicle
    {
        public static bool FFLightEvent;
        private readonly ATTG3Plugin plugin;
        public FFLight(ATTG3Plugin plugin) => this.plugin=plugin;
        public void OnRoundStart(RoundStartEvent ev)
        {
            if (FFLightEvent)
            {
                foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                {
                    if (door.Name=="914")
                    {
                        door.Locked=true;
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
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    player.ChangeRole(Role.CLASSD, true, true, true, true);
                }
                PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>You have 60 Seconds to hide</Color></SIZE>", false);
                Events.Delay60();
            }
        }
        
        public void OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            if (FFLightEvent)
            {

            }
        }
        public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
        {
            if (FFLightEvent)
            {
                ev.Allow=false;
                ev.Player.PersonalBroadcast(10, "Nuke cannot be activated at the moment", false);
            }
        }
        public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
        {
            if (FFLightEvent)
            {
                ev.Allow=false;
            }
        }
        public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
        {
            if (FFLightEvent)
            {
                ev.SpawnChaos=false;
            }
        }
        
        public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
        {
            if (FFLightEvent)
            {
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
                ev.AllowSummon = false;
            }
        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (FFLightEvent)
            {
                FFLightEvent = false;
            }
        }
    }
}


