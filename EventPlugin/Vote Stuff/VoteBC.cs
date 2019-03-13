using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class VoteBC : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public float Desplay = 0f;
        public VoteBC(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Displays how to vote";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGVOTEBC";
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
            PluginManager.Manager.Server.Map.Broadcast(10, "Want A Event Next round? Push ` and type .Yes or .No to vote", false);
            return new[]
            {
                $"Vote BC Played"
            };
        }
    }
}
