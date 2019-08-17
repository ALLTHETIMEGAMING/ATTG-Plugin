using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
    class DoorMain : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public DoorMain(ATTG3Plugin plugin) => this.plugin = plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        //Variables Below
        public Door door1;
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
            Player player1 = sender as Player;
            GameObject door = Object.FindObjectOfType<Door>().gameObject;
            Vector pos = player1.GetPosition();
            NetworkServer.UnSpawn(door);
            GameObject door1 = door;
            door1.transform.position = new Vector3(pos.x, pos.y, pos.z);
            NetworkServer.Spawn(door1);
            return new string[] { "Door Moved" };
        }
    }
}