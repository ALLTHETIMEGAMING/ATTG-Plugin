using Smod2.API;
using Smod2.Commands;
using Smod2.Events;
using System.Collections.Generic;
using Smod2.EventHandlers;
using MEC;

namespace ATTG3
{
    class Vote : IEventHandlerCallCommand
    {
        private readonly ATTG3Plugin plugin;
        public Vote(ATTG3Plugin plugin) => this.plugin=plugin;

        public static List<Player> Voted = new List<Player>();
        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            string command = ev.Command.ToLower();
            if (command.StartsWith("yes"))
            {
                if (plugin.Voteopen&&Voted.Contains(ev.Player)==false)
                {
                    Voted.Add(ev.Player);
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
                if (plugin.Voteopen&&Voted.Contains(ev.Player)==false)
                {
                    Voted.Add(ev.Player);
                    plugin.No++;
                    ev.ReturnMessage="Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage="Voteing is not open";
                }
                return;
            }
            else if (command.StartsWith("invintory1"))
            {
                // This is a Test This will be unlocked to all players if it works.
                if (ev.Player.SteamId == "76561198141700494")
                {
                    Events.Inventoryset(ev.Player, 1);
                }
            }
        }
    }
}
