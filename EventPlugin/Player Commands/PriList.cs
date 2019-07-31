using Smod2;
using Smod2.API;
using Smod2.Commands;
using System;
using System.IO;
using System.Linq;

namespace ATTG3
{
    class PriList : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public PriList(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => " adds player to list";
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.Allrank.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"Command PRILIST does not exist "
                };
            }
            if (Server.GetPlayers().Count<1)
                return new string[] { "The server is empty!" };
            Player caller = (sender is Player send) ? send : null;
            if (args.Length>0)
            {
                Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                Events.Setfilepri(myPlayer.ToString() + Environment.NewLine);
                    return new string[] { myPlayer.Name+" added to file" };
            }
            else
            {
                return new string[] { "PRILIST: " + GetUsage() };
            }
        }
    }
}
