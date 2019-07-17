using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine.Networking;

namespace ATTG3
{
    class Gas : ICommandHandler
    {
        public static bool GASLCZ;
        private readonly ATTG3Plugin plugin;
        public Gas(ATTG3Plugin plugin)
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
            GASLCZ = !GASLCZ;
            if (GASLCZ)
            {
                DecontaminationGas.TurnOn();
                NetworkCRC.RegisterBehaviour("DecontaminationLCZ", 0);
            }
            else
            {
                foreach (DecontaminationGas gase in DecontaminationGas.gases)
                {
                    if (gase != null)
                    {
                        gase.gameObject.SetActive(true);
                    }
                }
            }
            return new[]
            {
                $"Gas {(GASLCZ ? "Actavated" : "Deactavated")}."
            };
        }
    }
}
