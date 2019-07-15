using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using MEC;

namespace ATTG3
{
    class MEME : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public MEME(ATTG3Plugin plugin)
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
        public static readonly string[] CA = new string[] { "ATTGDEBUG", "MEME" };
        public static bool MEMETIME;
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
            MEMETIME = !MEMETIME;
            if (MEMETIME)
            {
                Timing.RunCoroutine(Events.MapShake());
                Timing.RunCoroutine(Events.TleslaSpam());
                Timing.RunCoroutine(Events.MEMETIME());
            }

            return new[]
            {
                $"MEMEING Server"
            };
        }
    }
}
