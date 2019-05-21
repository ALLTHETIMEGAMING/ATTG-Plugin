using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
    internal class VicHandler : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
        IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet,
        IEventHandlerUpdate 
    {
        
        bool nuke;
        int gen;
        private readonly ATTG3Plugin plugin;
        public VicHandler(ATTG3Plugin plugin) => this.plugin=plugin;
        public void OnRoundStart(RoundStartEvent ev)
        {
            if (plugin.VicEvent)
            {
                nuke=false;
                gen=0;
                foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                {
                    if (door.Name=="914")
                    {
                        door.Locked=true;
                    }
                }
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    player.ChangeRole(Role.CLASSD, true, true, true, true);
                }
            }
        }
        
        public void OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            if (plugin.VicEvent)
            {
                gen++;
                if (gen==5)
                {
                    
                    nuke=true;
                }

            }
        }
        public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
        {
            if (plugin.VicEvent&&!nuke)
            {
                ev.Allow=false;
                ev.Player.PersonalBroadcast(10, "Nuke cannot be activated at the moment", false);
            }
        }
        public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
        {
            if (plugin.VicEvent)
            {
                ev.Allow=false;
            }
        }
        public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
        {
            if (plugin.VicEvent)
            {
                ev.SpawnChaos=false;
            }
        }
        public void OnUpdate(Smod2.Events.UpdateEvent ev)
        {
            int stats = PluginManager.Manager.Server.Round.Duration;
            if (stats==60)
            {
                


            }

        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            gen=0;
            plugin.O79Event=false;
        }
    }
}


