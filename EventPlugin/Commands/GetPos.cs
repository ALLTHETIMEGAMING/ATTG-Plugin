using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;
namespace ATTG3
{
    class GetPos : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public GetPos(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
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
        public static readonly string[] CA = new string[] { "AGC106", "C106" };

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (sender is Player player&&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            };
            if (sender is Player player1)
            {
                if (args.Length > 0)
                {
                    string pos;
                    string args2 = args[0].ToLower();
                    GameObject val = GameObject.Find("Host");
                    int num = -1;
                    if (val != null)
                    {
                        num = val.GetComponent<RandomSeedSync>().seed;
                    }
                    if (args2 == "lcz")
                    {
                        pos = num.ToString() + ":" + "LCZ" + ":" + player1.GetPosition().ToString() + Environment.NewLine;
                        string Mapseeds = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "MapFiles" + Path.DirectorySeparatorChar + num.ToString() + ".txt";
                        string text2 = pos.Replace("(", "");
                        text2 = text2.Replace(" ", "");
                        text2 = text2.Replace(")", "");
                        File.AppendAllText(Mapseeds, text2);
                        plugin.Info("Printing to file " + num);
                    }
                    else if (Server.getc())
                    {
                        pos = num.ToString() + ":" + "HCZ" + ":" + player1.GetPosition().ToString() + Environment.NewLine;
                        string Mapseeds = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "MapFiles" + Path.DirectorySeparatorChar + num.ToString() + ".txt";
                        string text2 = pos.Replace("(", "");
                        text2 = text2.Replace(" ", "");
                        text2 = text2.Replace(")", "");
                        File.AppendAllText(Mapseeds, text2);
                        plugin.Info("Printing to file " + num.ToString());
                    }
                    else if (args2 == "ecz")
                    {
                        pos = num.ToString() + ":"+ "ECZ" + ":"+ player1.GetPosition().ToString() + Environment.NewLine;
                        string Mapseeds = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "MapFiles" + Path.DirectorySeparatorChar + num.ToString() + ".txt";
                        string text2 = pos.Replace("(","");
                        text2 = text2.Replace(" ", "");
                        text2 = text2.Replace(")", "");
                        File.AppendAllText(Mapseeds, text2);
                        plugin.Info("Printing to file " + num);
                    }
                    return new[]
{
                        "Position Added to file."
                    };
                }
                else
                {
                    return new[]
                    {
                        $"must use LCZ/HCZ/ECZ."
                    };
                }
            }
            else
            {
                return new[]
                {
                $"Not a player."
                };
            }
        }
    }
}
