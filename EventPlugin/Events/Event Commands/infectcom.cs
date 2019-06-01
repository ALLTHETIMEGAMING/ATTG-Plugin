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
    class Infectcom : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Infectcom(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        //Variables Below


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

            if (plugin.RoundStarted == false)
            {
                plugin.INFECT=true;
                return new string[] { "Infect Event started" };
            }
            else
            {
                return new string[] { "Event must be started before the round starts." };
            }
        }
    }
}
