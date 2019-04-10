using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class L079 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public L079(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Makes 079 Level 5";
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
            if (Server.GetPlayers().Count<1)
                return new string[] { "The server is empty!" };
            Player caller = (sender is Player send) ? send : null;
            if (args.Length>0)
            {
                Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                if (myPlayer.TeamRole.Role==Role.SCP_079)
                {
                    myPlayer.Scp079Data.ShowLevelUp(5);
                    myPlayer.Scp079Data.APPerSecond=1000;
                    myPlayer.Scp079Data.MaxAP=100000;
                    myPlayer.Scp079Data.Level=4;
                    return new string[] { myPlayer.Name+" Leveled up" };
                }
                else
                    return new string[] { myPlayer.Name+" is not SCP 079" };
            }
            else
            {
                return new string[] { "AGL079" + GetUsage() };
            }
        }
    }
}
