using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class VoteC : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public float Desplay = 0f;
        public VoteC(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Clears Vote";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGVOTEC";
        }

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
            plugin.Yes=0;
            plugin.No=0;
            return new[]
            {
                $"Votes are cleared."
            };

        }

    }
}
