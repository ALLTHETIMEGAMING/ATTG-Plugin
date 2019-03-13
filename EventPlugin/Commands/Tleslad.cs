using scp4aiur;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;

namespace ATTG3
{
    class Tleslad : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public Tleslad(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "";
        }

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
            else
            {
                running=!running;
                if (running)
                {
                    Timing.Run(TimingDelay(0.1f));
                }
                return new[]
                {
                $"all Tleslas are now {(running ? "on" : "off")}."
                };
            }
        }
        private IEnumerable<float> TimingDelay(float time)
        {
            while (running)
            {
                foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
                {
                    TeslaGate.Activate(true);
                }

                yield return 0.5f;
            }
        }
    }
}
