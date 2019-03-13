using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
    internal class EventHandler : IEventHandlerRoundStart, IEventHandlerWarheadStopCountdown
    {
        private readonly ATTG3Plugin plugin;
        public EventHandler(ATTG3Plugin plugin) => this.plugin=plugin;
        public void OnRoundStart(RoundStartEvent ev)
        {
            if (plugin.Disable)
            {
                this.plugin.pluginManager.DisablePlugin(this.plugin);
            }
            plugin.Running939=false;
            plugin.Voteopen=false;
            plugin.Yes=0;
            plugin.No=0;
        }
        public void OnStopCountdown(WarheadStopEvent ev)
        {
            foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
            {
                if (door.Name=="CHECKPOINT_ENT")
                {
                    door.Open=true;
                    door.Locked=false;
                }
            }
        }
    }
}


