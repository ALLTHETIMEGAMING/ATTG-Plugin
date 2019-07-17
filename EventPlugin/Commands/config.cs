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
            
            return "Disables 106 Containment";
        }
        public string GetUsage()
        {
            
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
            if (Config1 == false)
            {
                Config1 = true;
                Events.MapSpawnVec();
            }
			else
            {
                Config1 = false;
            }
            return new[]
            {
                $"Debug test {(Config1 ? "Actavated" : "Deactavated")}"
            };
        }
    }
}
