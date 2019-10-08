using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
namespace ATTG3
{
    class Incomname : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public Incomname(ATTG3Plugin plugin) { this.plugin = plugin; }
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
            
            if (args.Length > 1)
            {
                string args1 = args[0].ToLower();
                if (args1 == "ready")
                {
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Ready, Events.StringArrayToString(args, 1));
                }
                else if (args1 == "restart")
                {
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Restarting, Events.StringArrayToString(args, 1));
                }
                else if (args1 == "bc")
                {
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Transmitting, Events.StringArrayToString(args, 1));
                }
                else if (args1 == "by")
                {
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Transmitting_Bypass, Events.StringArrayToString(args, 1));
                }
                else if (args1 == "admin")
                {
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Transmitting_Admin, Events.StringArrayToString(args, 1));
                }
                else if (args1 == "muted")
                {
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Muted, Events.StringArrayToString(args, 1));
                }
                else if (args1 == "all")
                {
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Muted, Events.StringArrayToString(args, 1));
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Ready, Events.StringArrayToString(args, 1));
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Restarting, Events.StringArrayToString(args, 1));
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Transmitting, Events.StringArrayToString(args, 1));
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Transmitting_Admin, Events.StringArrayToString(args, 1));
                    PluginManager.Manager.Server.Map.SetIntercomContent(IntercomStatus.Transmitting_Bypass, Events.StringArrayToString(args, 1));
                }
                return new[] { "Set inercom content" };
            }
            return new[] { "more Args needed" };
        }
    }
}
