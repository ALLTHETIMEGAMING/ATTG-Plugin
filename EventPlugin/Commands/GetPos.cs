using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
namespace ATTG3
{
    class GetPos : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public GetPos(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
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
        public static readonly string[] CA = new string[] { "AGC106", "C106" };

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (sender is Player player&&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            };
            if (sender is Player player1)
            {
                string pos = player1.GetPosition().ToString() + Environment.NewLine;
                Events.Setfile(pos);
            }
            return new[]
            {
                $"Position Added to file."
            };
        }
    }
}
