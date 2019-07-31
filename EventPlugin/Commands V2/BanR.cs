using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;

namespace ATTG3
{
    class BanR : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public BanR(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        //Variables Below

        public string[] OnCall(ICommandSender sender, string[] args)
        {
                if (Server.GetPlayers().Count < 1)
                    return new string[] { "The server is empty!" };
                Player caller = (sender is Player send) ? send : null;
                if (args.Length > 1)
                {
                    int banname = int.Parse(args[1]);
                    Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                    if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
                    if (myPlayer.SteamId != "76561198126860363")
                    {
                    
                        myPlayer.Ban(banname, Events.StringArrayToString(args, 2));
                        return new[] { "Banned " + myPlayer.Name + "for" + banname+ "because" + Events.StringArrayToString(args, 2) };
                    }
                    return new[] { "You can not ban this person." };
                }
            else
            {
                return new[] { "Must use a players name, a time and a reason" };
            }
        }
    }
}
