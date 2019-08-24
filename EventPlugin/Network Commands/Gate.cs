using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
namespace ATTG3
{
    class Gate : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public Gate(ATTG3Plugin plugin)
        {
            this.plugin = plugin;
        }
        public string GetCommandDescription()
        {
            return "";
        }
        public string GetUsage()
        {
            return "";
        }
        public static readonly string[] CA = new string[] { "Gate", "AGate" };
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
            Vector pos = player1.GetPosition();
            GameObject Gate = UnityEngine.Object.FindObjectOfType<TeslaGate>().gameObject;
            NetworkServer.UnSpawn(Gate);
            GameObject Gate1 = Gate;
            Gate1.transform.position = new Vector3(pos.x, pos.y, pos.z+2);
            NetworkServer.Spawn(Gate1);

            return new[] { "Gate Teleported" };
        }
    }
}
