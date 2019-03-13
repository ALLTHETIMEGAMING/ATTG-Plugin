using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class VoteA : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public float Desplay = 0f;
        public VoteA(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            return "Shows Vote";
        }
        public string GetUsage()
        {
            return "AGVOTES";
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
            return new[]
            {
                $"Vote is Yes:{(plugin.Yes)} No {(plugin.No)}."
            };

        }

    }
}
