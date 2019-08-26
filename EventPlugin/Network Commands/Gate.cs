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
            GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in objects)
            {
                if (go.activeInHierarchy && go.GetComponent<NetworkIdentity>() != null)
                {
                    if (go.name.Contains("Gate"))
                    {
                        NetworkServer.UnSpawn(go);
                        GameObject ngo = go;
                        ngo.gameObject.transform.localPosition = new Vector3(pos.x, pos.y, pos.z +2);
                        NetworkServer.Spawn(ngo);
                    }
                }
            }
            return new[] { "Gate Teleported" };
        }
    }
}
