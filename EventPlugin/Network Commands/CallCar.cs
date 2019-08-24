using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using System.Reflection;
namespace ATTG3
{
    class CarCall : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public CarCall(ATTG3Plugin plugin)
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
        public static readonly string[] CA = new string[] { "AGCAR", "CAR" };
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
            typeof(MTFRespawn).GetMethod("SummonVan", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(new MTFRespawn(), null);

            return new[] { "Car Called" };
        }
    }

    
}
