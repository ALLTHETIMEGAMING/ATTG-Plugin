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
using MEC;

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
            return "Disables 106 Containment";
        }
        public string GetUsage()
        { 
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
            PluginManager.Manager.Server.Map.Broadcast(10, "DEBUG MODE ACTIVATED", false);
            if (args.Length > 0)
            {
                string args2 = args[0].ToLower();
                if (args2 == "1")
                {
                    int MLC = ATTG3Plugin.Banmemes.Count;
                    int MLCC = 0;
                    foreach (string test1 in ATTG3Plugin.Banmemes)
                    {
                        MLCC++;
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING CODE 1 " + "("+ MLCC + " / "+MLC+")"+"\n" + test1, false);
                    }
                }
                else if (args2 == "2")
                {
                    int MLC = ATTG3Plugin.Maplist.Count;
                    int MLCC = 0;
                    foreach (string test1 in ATTG3Plugin.Maplist)
                    {
                        MLCC++;
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING CODE 2 " + "("+ MLCC + " / "+MLC+")"+"\n" + test1.ToString(), false);
                    }
                }
                else if (args2 == "3")
                {
                    int MLC = ATTG3Plugin.EventSpawn.Count();
                    int MLCC = 0;
                    foreach (string test1 in ATTG3Plugin.Maplist)
                    {
                        MLCC++;
                        PluginManager.Manager.Server.Map.ClearBroadcasts();
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING CODE 3 " + "("+ MLCC + "/"+MLC+")"+"\n" + test1.ToString(), false);
                    }
                }
                else if (args2 == "4")
                {
                    int MLC = ATTG3Plugin.TPRooms.Count;
                    int MLCC = 0;
                    foreach (Vector3 test1 in ATTG3Plugin.TPRooms)
                    {
                        MLCC++;
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING CODE 4 "+ "("+ MLCC + " / "+MLC+")"+"\n" + test1.ToString(), false);
                    }
                }
                else if (args2 == "ann")
                {
                    var ins = new DecontaminationLCZ();

                    foreach (DecontaminationLCZ.Announcement test in ins.announcements)
                    {
                        PluginManager.Manager.Server.Map.Broadcast(2, "DEBUGING ANN Code 1 " + test.ToString(), false);
                    }
                }
                else if (args2 == "full")
                {
                    Timing.RunCoroutine(Events.Fulldebug());

                }
            }
            PluginManager.Manager.Server.Map.Broadcast(10, "DEBUG MODE DEACTIVATED", false);
            return new[]
            {
                $"Debuging Server"
            };
        }
    }
}
