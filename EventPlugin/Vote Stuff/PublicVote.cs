using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
    class Vote : IEventHandlerCallCommand
    {
        private readonly ATTG3Plugin plugin;
        public Vote(ATTG3Plugin plugin) => this.plugin=plugin;
        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            string command = ev.Command.ToLower();
            if (command.StartsWith("yes"))
            {
                if (plugin.Voteopen)
                {
                    plugin.Yes++;
                    ev.ReturnMessage="Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage="Voteing is not open";
                }
                return;
            }
            else if (command.StartsWith("no"))
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
