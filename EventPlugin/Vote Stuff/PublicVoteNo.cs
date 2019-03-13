using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
    class No : IEventHandlerCallCommand
    {
        private readonly ATTG3Plugin plugin;
        public No(ATTG3Plugin plugin) => this.plugin=plugin;
        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            string command = ev.Command.ToLower();
            if (command.StartsWith("no"))
            {
                if (plugin.Voteopen)
                {
                    plugin.No++;
                    ev.ReturnMessage="Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage="Voteing is not open";
                }
                return;
            }
        }
    }
}
