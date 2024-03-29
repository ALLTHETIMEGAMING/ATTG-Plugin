﻿using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
namespace ATTG3
{
    class P079 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public P079(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Opens all of 079s generators";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "";
        }
        public static readonly string[] CA = new string[] { "AG079P", "079P", "GP" };
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
            running=!running;
            if (running)
            {
                foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
                {
                    Generator.Unlock();
                    if (Generator.Open==false)
                    {
                        Generator.Open=true;
                    }
                }
            }
            if (!running)
            {
                foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
                {

                    if (Generator.Open==true)
                    {
                        Generator.Open=false;
                    }
                }
            }
            return new[]
            {
                $"all SCP of 079s generators are now {(running ? "open" : "closed")}."
            };
        }
    }
}
