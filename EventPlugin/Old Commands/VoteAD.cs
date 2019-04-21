using Smod2.API;
using Smod2.Commands;
using System.Linq;

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
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Enables or Disables voteing";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGVOTET";
        }
        public static readonly string[] CA = new string[] { "AGVOTET", "VOTET" };
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.Voterank.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
            else
            {
                if (plugin.Voteopen)
                {
                    running=false;
                    plugin.Voteopen=false;
                }
                else if (!plugin.Voteopen)
                {
                    running=true;
                    plugin.Voteopen=true;
                }
                return new[]
                {
                $"Vote is now  {(running ? "ACTAVATED" : "DEACTAVATED")}."
            };
            }
        }
    }
}
