using Smod2;
using Smod2.API;
using Smod2.Commands;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Smod2.EventHandlers;

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
        }
    }
}
