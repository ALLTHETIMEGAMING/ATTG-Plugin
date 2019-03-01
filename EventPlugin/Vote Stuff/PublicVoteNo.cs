using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;

namespace ATTG3
{
    class No : IEventHandlerCallCommand
    {
        private readonly ATTG3Plugin plugin;

        public No(ATTG3Plugin plugin) => this.plugin = plugin;

        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            string command = ev.Command.ToLower();
            if (command.StartsWith("no")) {
				if (plugin.Voteopen == true)
				{
					plugin.No++;
					ev.ReturnMessage = "Vote Submitted";

				}
				else
				{
					ev.ReturnMessage = "Voteing is not open";
				}
				return;
            }
            

            
        }
    }
}
