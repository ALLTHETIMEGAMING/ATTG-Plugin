using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
namespace ATTG3
{
    class C106 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public C106(ATTG3Plugin plugin)
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
        public static readonly string[] CA = new string[] { "AGC106", "C106" };

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
            running=!running;
            if (running)
            {
                Smod2.PluginManager.Manager.Server.Map.FemurBreaker(false);
            }
            if (!running)
            {
                Smod2.PluginManager.Manager.Server.Map.FemurBreaker(true);
            }
            return new[]
            {
                $"all SCP 106 is now {(running ? "Locked" : "Unlocked")}."
            };
        }
    }
}
