using Smod2;
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
            return "All Voting Commands";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGVOTE";
        }
        public static string[] CA = new string[] { "AGVOTE", "VOTE" };
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
			if (args.Length>0)
			{
				string args2 = args[0].ToLower();
				if (args2=="Open")
				{
					plugin.Voteopen=true;

					return new string[] { "Voting Opened." };
				}
				else if (args2=="Close")
				{
					plugin.Voteopen=false;
					return new string[] { "Voting Closed" };
				}
				else if (args2=="BC")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "Push ` and type .Yes or .No to vote", false);
					return new string[] { " " };
				}
				else if (args2=="level")
				{
					return new string[] { "ALL SCP-079s are now Level 5" };
				}
				else if (args2=="clear")
				{
					plugin.Yes=0;
					plugin.No=0;
					PlayerConsole.Voted.Clear();
					return new string[] { "Votes Cleared" };
				}
				else if (args2=="see")
				{
					return new[]
					{
						$"Vote is Yes:{(plugin.Yes)} No {(plugin.No)}."
					};
				}
				else
				{
					return new[]
					{
						CA.First() + " Help" + " Shows this.",
						CA.First() + " Open"  + " Opens voting.",
						CA.First() + " Close" + " Closes voting.",
						CA.First() + " See"  + " Displays Vote.",
						CA.First() + " Clear"  + " Clears votes.",
					};
				}
			}
			else
			{
				return new[]
				{
					CA.First() + " Help" + " Shows this.",
					CA.First() + " Open"  + " Opens voting.",
					CA.First() + " Close" + " Closes voting.",
					CA.First() + " See"  + " Displays Vote.",
					CA.First() + " Clear"  + " Clears votes.",
				};
			}
        }
    }
}
