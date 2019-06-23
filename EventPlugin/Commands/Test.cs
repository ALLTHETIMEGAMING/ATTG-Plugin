using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
namespace ATTG3
{
    class Test : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public Test(ATTG3Plugin plugin){ this.plugin=plugin;}
        public string GetCommandDescription()
        {
            return "Testing Command";
        }
        public string GetUsage()
        {
            return "Testing Command";
        }
        public static readonly string[] CA = new string[] { "AGTEST", "TEST" };

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
			ConfigFile.ReloadGameConfig();
			ServerConsole.EnterCommand("config r");
			plugin.Info("Config Reload started");
			return new[]
            {
                $"Command Started"
            };
        }
    }
}
