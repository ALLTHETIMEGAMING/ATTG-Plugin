using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine.Networking;

namespace ATTG3
{
    class Shop : ICommandHandler
    {
        public Player myPlayer;
        public static bool Shoptp;
        private readonly ATTG3Plugin plugin;
        public Shop(ATTG3Plugin plugin)
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
            if (args.Length > 0)
            {
                myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
                if (myPlayer.TeamRole.Role != Role.SPECTATOR)
                {
                    myPlayer.Teleport(EventLStorageList.Shop1);
                }
            }
            return new[]
            {
                $"Player Teleported."
            };
        }
    }
}
