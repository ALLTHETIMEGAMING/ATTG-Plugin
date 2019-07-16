using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using MEC;

namespace ATTG3
{
    class Setup : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public static bool Setupbool;
        public Setup(ATTG3Plugin plugin)
        {
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            return "";
        }
        public string GetUsage()
        {
            return "";
        }
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
            Setupbool = !Setupbool;

            return new[]
            {
                $"Setup Server = " + Setupbool
            };
        }
    }
}
