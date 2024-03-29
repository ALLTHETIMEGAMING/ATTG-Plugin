﻿using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;

namespace ATTG3
{
    class TleslR : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public float Desplay = 0f;
        public TleslR(ATTG3Plugin plugin)
        {
            this.plugin=plugin;
        }

        public string GetCommandDescription()
        {
            return "Actavates or Deactavates all Tesla gates";
        }

        public string GetUsage()
        {
            return "";
        }
        public static readonly string[] CA = new string[] { "AGTR", "TR" };
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

                foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
                {
                    TeslaGate.TriggerDistance=0;
                }
            }
            if (!running)
            {
                foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
                {
                    TeslaGate.TriggerDistance=5.5f;
                }
            }
            return new[]
            {
                $"all Tleslas are now {(running ? "DEACTAVATED" : "ACTAVATED")}."
            };

        }
    }
}
