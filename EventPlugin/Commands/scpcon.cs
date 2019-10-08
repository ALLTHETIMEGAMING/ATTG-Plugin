using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
namespace ATTG3
{
    class SCPCON : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public SCPCON(ATTG3Plugin plugin) { this.plugin = plugin; }
        public string GetCommandDescription() { return ""; }
        public string GetUsage() { return ""; }
        public static readonly string[] CA = new string[] { "TEMP", "" };
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
                PluginManager.Manager.Server.Map.AnnounceScpKill(Events.StringArrayToString(args, 0));
                return new[] { "Played" };
            }
            return new[] { "more Args needed" };
        }
    }
}
