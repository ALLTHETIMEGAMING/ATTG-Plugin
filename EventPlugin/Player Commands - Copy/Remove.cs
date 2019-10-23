using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class PlayerDis : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public PlayerDis(ATTG3Plugin plugin) => this.plugin = plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Changes players size";
        public static readonly string[] CA = new string[] { "AGTP", "RTP" };
        public CharacterClassManager CHAR;
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
            else
            {
                if (Server.GetPlayers().Count < 1)
                    return new string[] { "The server is empty!" };
                Player caller = (sender is Player send) ? send : null;
                if (args.Length > 0)
                {
					if (args[0] != "*")
					{
						Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
						if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
						if (myPlayer.SteamId != "76561198126860363")
						{
							myPlayer.Disconnect(Events.StringArrayToString(args, 1));
						}
						return new string[] { myPlayer.Name + " was removed" };
					}
					else
					{
						foreach (Player player3 in PluginManager.Manager.Server.GetPlayers())
						{
							player3.Disconnect("SERVER RESTARTING PLEASE REJOIN ALL THE TIME GAMING");
						}
						return new string[] { "Restarting" };
					}
                }
                else
                {
                    return new string[] { "AGSIZE: " + GetUsage() };
                }
            }
        }
    }
}
