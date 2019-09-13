using Smod2.API;
using Smod2.Commands;
using System.Linq;
namespace ATTG3
{
    class BreachCamp : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public BreachCamp(ATTG3Plugin plugin)
        {
            this.plugin = plugin;
        }
        public string GetCommandDescription()
        {
            return "";
        }
        public string GetUsage()
        {
            return "";
        }
        public static readonly string[] CA = new string[] { "CAMP", "AGCAMP" };
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server) &&
                sender is Player player &&
                !plugin.Eventrank.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
            if (Breach.Breachevent || Hold.Holdevent || Cap.Holdevent)
            {
                Events.MTFEnter();
                return new[] { "Warning all Campers" };
            }
            else
                return new[] { "Event not running" };
        }
    }
}
