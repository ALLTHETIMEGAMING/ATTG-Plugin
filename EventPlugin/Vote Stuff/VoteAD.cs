using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.Commands;
using Smod2.API;
using scp4aiur;

namespace ATTG3
{
    class VoteAD : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public float Desplay = 0f;
        public VoteAD(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "tlesla";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "tlesla";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server) &&
                sender is Player player &&
                !plugin.ValidLightsOutRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }


			//IF VOTE IS OPEN
			if (plugin.Voteopen == true)
            {
                running = false;
				//SET TO VOTE IS CLOSED
				plugin.Voteopen = false;


			}

			//IF VOTE IS CLOSED
			else if (plugin.Voteopen == false)
            {
                running = true;
				//SET TO VOTE IS OPEN
				plugin.Voteopen = true;
			}
            return new[]
            {
                $"Vote is now  {(running ? "ACTAVATED" : "DEACTAVATED")}."
            };

        }
        
    }
}
