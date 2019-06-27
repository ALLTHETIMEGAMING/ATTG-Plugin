using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using Smod2;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using UnityEngine;
using System.IO;
using System.Threading;
using System;

namespace ATTG3
{
    class DebugATTG : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public static bool Config1;
        public DebugATTG(ATTG3Plugin plugin)
        {
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Disables 106 Containment";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "Disables 106 Containment";
        }
        public static readonly string[] CA = new string[] { "ATTGDEBUG", "DEBUG" };

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server) &&
                sender is Player player &&
                !plugin.Allrank.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
            PluginManager.Manager.Server.Map.Broadcast(10, "<size=75><color=red>WARNING DEBUG MODE ACTIVATED</color></size>", false);
            if (args.Length > 0)
            {
                string args2 = args[0].ToLower();
                if (args2 == "1")
                {
                    foreach (string test1 in ATTG3Plugin.Banmemes)
                    {
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING CODE 1\n" + test1, false);
                    }
                }
                else if (args2 == "2")
                {
                    foreach (Vector test1 in ATTG3Plugin.MapCusSpawn)
                    {
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING CODE 2\n" + test1.ToString(), false);
                    }
                }
                else if (args2 == "3")
                {
                    foreach (string test1 in ATTG3Plugin.Maplist)
                    {
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING CODE 3\n" + test1.ToString(), false);
                    }
                }
                else if (args2 == "4")
                {
                    foreach (Vector3 test1 in ATTG3Plugin.TPRooms)
                    {
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING CODE 4\n" + test1.ToString(), false);
                    }
                }
            }
            PluginManager.Manager.Server.Map.Broadcast(10, "<size=90><color=red>DEBUG MODE DEACTIVATED</color></size>", false);
            return new[]
            {
                $"Debug test"
            };
        }
    }
}
