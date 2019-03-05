using Smod2.Commands;
using Smod2;
using Smod2.API;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using Smod2.EventHandlers;
using Smod2.EventSystem.Events;
using ServerMod2.API;
using ItemManager;
using UnityEngine;
using System.Collections;


namespace ATTG3
{
    class O79T : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;

        public static List<Generator079> generators;
        public O79T(ATTG3Plugin plugin) => this.plugin = plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server) &&
                sender is Player player &&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }

            foreach (Generator079 Gen in generators)
            Gen.NetworkremainingPowerup = 10f;


            return new[]
            {
                $"Timer Set to 10 sec"
            };

        }
    }
}
