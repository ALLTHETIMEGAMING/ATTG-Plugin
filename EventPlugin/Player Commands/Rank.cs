using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
	class Rank : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		public Rank(ATTG3Plugin plugin) => this.plugin = plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "Sets a players Text Role";
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.AdminRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			if (Server.GetPlayers().Count < 1)
				return new string[] { "The server is empty!" };
			Player caller = (sender is Player send) ? send : null;
			if (args.Length > 2)
			{

				Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
				if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
				myPlayer.SetRank(args[1], Events.StringArrayToString(args, 2), null);
				return new string[] { myPlayer.Name + " Role Set" };
				/*else if (args[1].ToLower() == "save")
                {
                    if (args.Length > 4)
                    {
                        Events.Setfilenerf(args[2].ToLower() + ":" + args[3].ToLower() + ":" + Events.StringArrayToString(args, 4) + Environment.NewLine);
                        return new string[] { args[2].ToLower() + ":" + args[3].ToLower() + ":" + Events.StringArrayToString(args, 4) + " Role Saved" };
                    }
                    else
                    {
                        return new string[] { "(Role Prefix) (Role Color) (Role Text)" };
                    }
                }*/
			}
			else
			{
				return new string[] { "Agrank: " + GetUsage() };
			}
		}
	}
}
