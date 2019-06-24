using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
namespace ATTG3
{
    class Config : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public static bool Config1;
        public Config(ATTG3Plugin plugin)
        {
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Disables 106 Containment";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "Disables 106 Containment";
        }
        public static readonly string[] CA = new string[] { "Config", "Configtest" };

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.Allrank.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
			Config1 = true;
			ConfigFile.ReloadGameConfig();
            return new[]
            {
                $"Map Changed."
            };
        }
    }
}
