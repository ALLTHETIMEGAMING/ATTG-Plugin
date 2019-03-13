using scp4aiur;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
namespace ATTG3
{
    class Shake : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private object player2;
        public Player myPlayer;
        private bool running;

        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;

        public Scp939PlayerScript PlayerScript { get; private set; }

        public Shake(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "Shakes Map";
        public string GetUsage() => "AGSHAKE";

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
                Timing.Run(TimingDelay(0.1f));
            }
            return new[]
            {
                $"Map Shake is now {(running ? "on" : "off")}."
            };
        }
        private IEnumerable<float> TimingDelay(float time)
        {
            while (running)
            {
                plugin.Server.Map.Shake();

                yield return 3f;
            }
        }
    }
}
