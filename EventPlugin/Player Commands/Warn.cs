using Smod2;
using Smod2.API;
using Smod2.Commands;

namespace ATTG3
{
    class Warn : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Warn(ATTG3Plugin plugin) => this.plugin = plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => " (Player Name) (Reason)";
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (Server.GetPlayers().Count < 1)
                return new string[] { "The server is empty!" };
            Player caller = (sender is Player send) ? send : null;
            if (args.Length > 0)
            {
                Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }

                myPlayer.PersonalBroadcast(10, myPlayer.Name + ": " + args[1], false);
                return new string[] { "Warned Player: " + myPlayer.Name + " "+args.ToString() };
            }
            else
            {
                return new string[] { "AGWARN: " + GetUsage() };
            }
        }
    }
}
