using Smod2.API;
using Smod2.Commands;
using Smod2.Events;
using System.Collections.Generic;
using Smod2.EventHandlers;
using MEC;

namespace ATTG3
{
    class PlayerConsole : IEventHandlerCallCommand
    {
        private readonly ATTG3Plugin plugin;
        public PlayerConsole(ATTG3Plugin plugin) => this.plugin=plugin;

        public static List<string> Voted = new List<string>();
        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            string command = ev.Command.ToLower();
            if (command.StartsWith("yes"))
            {
                if (plugin.Voteopen&&Voted.Contains(ev.Player.SteamId)==false)
                {
                    Voted.Add(ev.Player.SteamId);
                    plugin.Yes++;
                    ev.ReturnMessage="Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage="Voting is not open";
                }
                return;
            }
            else if (command.StartsWith("no"))
            {
                if (plugin.Voteopen&&Voted.Contains(ev.Player.SteamId)==false)
                {
                    Voted.Add(ev.Player.SteamId);
                    plugin.No++;
                    ev.ReturnMessage="Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage="Voting is not open";
                }
                return;
            }
            else if (command.StartsWith("10lock"))
            {
                plugin.Info("10lock Command Called");
                if (EventPlayerItems.Itemset.ContainsKey(ev.Player.SteamId) ==true)
                {
                    EventPlayerItems.Itemset[ev.Player.SteamId] = "10lock";
                    ev.ReturnMessage = "Event Item Set to 10 lock (Code 1)";
                }
                else if (EventPlayerItems.Itemset.ContainsKey(ev.Player.SteamId) == false)
                {
                    EventPlayerItems.Itemset.Add(ev.Player.SteamId, "10lock");
                    ev.ReturnMessage = "Event Item Set to 10 Lock (Code 2)";
                }
                else
                {
                    ev.ReturnMessage = "HOW DID YOU DO THIS? (ERROR CODE 3)";
                }
                return;
            }
            else if (command.StartsWith("destroy"))
            {
                plugin.Info("Destroy Command Called");
                if (EventPlayerItems.Itemset.ContainsKey(ev.Player.SteamId) ==true)
                {
                    EventPlayerItems.Itemset[ev.Player.SteamId] = "destroy";
                    ev.ReturnMessage = "Event Item Set to destroy (Code 1)";
                }
                else if (EventPlayerItems.Itemset.ContainsKey(ev.Player.SteamId) == false)
                {
                    EventPlayerItems.Itemset.Add(ev.Player.SteamId, "destroy");
                    ev.ReturnMessage = "Event Item Set to destroy (Code 2)";
                }
                else
                {
                    ev.ReturnMessage = "HOW DID YOU DO THIS? (ERROR CODE 3)";
                }
                return;
            }
            else if (command.StartsWith("30lock"))
            {
                plugin.Info("30lock Command Called");
                if (EventPlayerItems.Itemset.ContainsKey(ev.Player.SteamId) ==true)
                {
                    EventPlayerItems.Itemset[ev.Player.SteamId] = "30lock";
                    ev.ReturnMessage = "Event Item Set to 30 Lock (Code 1)";
                }
                else if (EventPlayerItems.Itemset.ContainsKey(ev.Player.SteamId) == false)
                {
                    EventPlayerItems.Itemset.Add(ev.Player.SteamId, "30lock");
                    ev.ReturnMessage = "Event Item Set to 30 Lock (Code 2)";
                }
                else
                {
                    ev.ReturnMessage = "HOW DID YOU DO THIS? (ERROR CODE 3)";
                }
                return;
            }
            else if (command.StartsWith("serverhelp"))
            {
                ev.ReturnMessage = "/n" +
                    ".no Votes no /n " +
                    ".yes Votes Yes /n" +
                    ".10lock Sets Event key access /n" +
                    ".destroy Sets Event key access /n" +
                    ".30lock Sets Event key access /n";
                return;
            }
            #region Invcode

            /* else if (command.StartsWith("inv1"))
             {
                 // This is a Test This will be unlocked to all players if it works.
                 if (ev.Player.SteamId == "76561198126860363")
                 {
                     Events.Inventoryset(ev.Player, 1);
                     ev.ReturnMessage = "Changed Held item";
                 }
                 else
                 {
                     ev.ReturnMessage = "Command is in testing mode";
                 }
             }
             else if (command.StartsWith("inv2"))
             {
                 // This is a Test This will be unlocked to all players if it works.
                 if (ev.Player.SteamId == "76561198126860363")
                 {
                     Events.Inventoryset(ev.Player, 2);
                     ev.ReturnMessage = "Changed Held item";
                 }
                 else
                 {
                     ev.ReturnMessage = "Command is in testing mode";
                 }
             }
             else if (command.StartsWith("inv3"))
             {
                 // This is a Test This will be unlocked to all players if it works.
                 if (ev.Player.SteamId == "76561198126860363")
                 {
                     Events.Inventoryset(ev.Player, 3);
                     ev.ReturnMessage = "Changed Held item";
                 }
                 else
                 {
                     ev.ReturnMessage = "Command is in testing mode";
                 }
             }
             else if (command.StartsWith("inv4"))
             {
                 // This is a Test This will be unlocked to all players if it works.
                 if (ev.Player.SteamId == "76561198126860363")
                 {
                     Events.Inventoryset(ev.Player, 4);
                     ev.ReturnMessage = "Changed Held item";
                 }
                 else
                 {
                     ev.ReturnMessage = "Command is in testing mode";
                 }
             }
             else if (command.StartsWith("inv5"))
             {
                 // This is a Test This will be unlocked to all players if it works.
                 if (ev.Player.SteamId == "76561198126860363")
                 {
                     Events.Inventoryset(ev.Player, 5);
                     ev.ReturnMessage = "Changed Held item";
                 }
                 else
                 {
                     ev.ReturnMessage = "Command is in testing mode";
                 }
             }
             else if (command.StartsWith("inv6"))
             {
                 // This is a Test This will be unlocked to all players if it works.
                 if (ev.Player.SteamId == "76561198126860363")
                 {
                     Events.Inventoryset(ev.Player, 6);
                     ev.ReturnMessage = "Changed Held item";
                 }
                 else
                 {
                     ev.ReturnMessage = "Command is in testing mode";
                 }
             }
             else if (command.StartsWith("inv7"))
             {
                 // This is a Test This will be unlocked to all players if it works.
                 if (ev.Player.SteamId == "76561198126860363")
                 {
                     Events.Inventoryset(ev.Player, 7);
                     ev.ReturnMessage = "Changed Held item";
                 }
                 else
                 {
                     ev.ReturnMessage = "Command is in testing mode";
                 }
             }
             else if (command.StartsWith("inv8"))
             {
                 // This is a Test This will be unlocked to all players if it works.
                 if (ev.Player.SteamId == "76561198126860363")
                 {
                     Events.Inventoryset(ev.Player, 8);
                     ev.ReturnMessage = "Changed Held item";
                 }
                 else
                 {
                     ev.ReturnMessage = "Command is in testing mode";
                 }
             }*/
            #endregion
        }
    }
}
