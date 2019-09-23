using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
namespace ATTG3
{
    class Watch : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public Watch(ATTG3Plugin plugin){this.plugin=plugin; }
        public string GetCommandDescription()
        {
            return "";
        }
        public string GetUsage()
        {
            return "Disables 106 Containment";
        }
        public static readonly string[] CA = new string[] { "Watch", "AGWATCH" };

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
            if (args.Length > 0)
            {
                Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
                if (myPlayer.SteamId != "76561198126860363")
                {
                    Events.Watchlist(myPlayer);
                    return new string[] { myPlayer.Name + " Added to watch list" };
                }
                else
                    return new string[] { "You can not add staff members" };
            }
            return new[]{"Added player to list"};
        }
    }
}
