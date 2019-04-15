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
    class Blast : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Blast(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        //Variables Below
        bool Running;

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
            Running=!Running;
            if (Running)
            {
                foreach (BlastDoor blast in Object.FindObjectsOfType<BlastDoor>())
                {
                    blast.NetworkisClosed=true;
                }
            }
            if (!Running)
            {
                foreach (BlastDoor blast in Object.FindObjectsOfType<BlastDoor>())
                {
                    blast.NetworkisClosed=false;
                }
            }

            return new[]
            {
                $"Blast Doors {(Running ? "Off" : "On")}."
            };

        }
    }
}
