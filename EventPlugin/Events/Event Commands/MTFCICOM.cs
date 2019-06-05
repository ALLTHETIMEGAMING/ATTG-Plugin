using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;


namespace ATTG3
{
    class MTFCICOM : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public MTFCICOM(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";

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
				// #0080FF Is Blue
				// #0B7A00 Is Green
				PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#0080FF>MTF</Color> VS <color=#0B7A00>CI</Color> Event Starting</SIZE>", false);
				plugin.MTFCI = true;
                return new string[] { "MTF VS CI Event started" };
            }
            else
            {
                return new string[] { "Event must be started before the round starts." };
            }
        }
    }
}