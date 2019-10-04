using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace ATTG3
{
    class PlayerConsole : IEventHandlerCallCommand
    {
        private readonly ATTG3Plugin plugin;
        public PlayerConsole(ATTG3Plugin plugin) => this.plugin = plugin;
        public static Dictionary<string, bool> StaffCall = new Dictionary<string, bool>();
        public static Dictionary<string, string> Voted = new Dictionary<string, string>();
        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            string command = ev.Command.ToLower();
            if (command.StartsWith("yes"))
            {
                if (plugin.Voteopen && Voted.ContainsKey(ev.Player.SteamId) == false)
                {
                    Voted.Add(ev.Player.SteamId, "yes");
                    plugin.Yes++;
                    ev.ReturnMessage = "Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage = "Voting is not open";
                }
                return;
            }
            else if (command.StartsWith("no"))
            {
                if (plugin.Voteopen && Voted.ContainsKey(ev.Player.SteamId) == false)
                {
                    Voted.Add(ev.Player.SteamId, "no");
                    plugin.No++;
                    ev.ReturnMessage = "Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage = "Voting is not open";
                }
                return;
            }
            else if (command.StartsWith("10lock"))
            {
                plugin.Info("10lock Command Called");
                if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId) == true)
                {
                    EventLStorageList.Itemset[ev.Player.SteamId] = "10lock";
                    ev.ReturnMessage = "Event Item Set to 10 lock (Code 1)";
                }
                else if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId) == false)
                {
                    EventLStorageList.Itemset.Add(ev.Player.SteamId, "10lock");
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
                if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId) == true)
                {
                    EventLStorageList.Itemset[ev.Player.SteamId] = "destroy";
                    ev.ReturnMessage = "Event Item Set to destroy (Code 1)";
                }
                else if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId) == false)
                {
                    EventLStorageList.Itemset.Add(ev.Player.SteamId, "destroy");
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
                if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId) == true)
                {
                    EventLStorageList.Itemset[ev.Player.SteamId] = "30lock";
                    ev.ReturnMessage = "Event Item Set to 30 Lock (Code 1)";
                }
                else if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId) == false)
                {
                    EventLStorageList.Itemset.Add(ev.Player.SteamId, "30lock");
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
            else if (ev.Player.TeamRole.Role == Role.SCP_079)
            {
                var SCP079 = ev.Player.Scp079Data;
                if (command.StartsWith("kill"))
                {
                    ev.Player.ChangeRole(Role.SPECTATOR);
                    ev.ReturnMessage = "Killed Player";
                }
                else if (command.StartsWith("tesla"))
                {
                    if (SCP079.Level >= 2 && SCP079.AP >= 100)
                    {
                        foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
                        {
                            TeslaGate.Activate(true);
                        }
                        SCP079.AP -= 100;
                        ev.ReturnMessage = "Spamed all Teslas";
                    }
                    else
                    {
                        ev.ReturnMessage = "You are not level 3 or you do not have 100 power ";
                    }
                }
                else if (command.StartsWith("door") && ev.Player.SteamId == "76561198126860363")
                {
                    if (SCP079.Level >= 4 && SCP079.AP >= 200)
                    {
                        foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                        {
                            if (door.Name == "")
                            {
                                door.Open = false;
                            }
                        }
                        SCP079.AP -= 200;
                        ev.ReturnMessage = "Closed all Doors";
                    }
                    else
                    {
                        ev.ReturnMessage = "You are not level 5 or you do not have 200 power ";
                    }
                }
                else if (command.StartsWith("cassie") && ev.Player.SteamId == "76561198126860363")
                {
                    if (SCP079.Level >= 4 && SCP079.AP >= 200)
                    {
                        //string args2 = ev.Command[1].ToString().ToLower();
                        PluginManager.Manager.Server.Map.AnnounceCustomMessage(ev.Command);
                        SCP079.AP -= 200;
                        ev.ReturnMessage = "Cassie Called";
                    }
                    else
                    {
                        ev.ReturnMessage = "You are not level 5 or you do not have 200 power ";
                    }
                }
                else if (command.StartsWith("locate") && ev.Player.SteamId == "76561198126860363")
                {
                    if (SCP079.Level >= 4 && SCP079.AP >= 200)
                    {
                        Events.Locate();
                        SCP079.AP -= 200;
                        ev.ReturnMessage = "Locating Players";
                    }
                    else
                    {
                        ev.ReturnMessage = "You are not level 5 or you do not have 200 power ";
                    }
                }
            }
            else if (command.StartsWith("calladmin"))
            {
                if (StaffCall.ContainsKey(ev.Player.SteamId) == false)
                {
                    StaffCall.Add(ev.Player.SteamId, false);

                    ev.ReturnMessage = "Calling Staff";
                }
                else
                {
                    ev.ReturnMessage = "You have aleady called Staff";
                }
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
