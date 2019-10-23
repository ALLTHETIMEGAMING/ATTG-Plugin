using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
namespace ATTG3
{
	class Rain : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public Player myPlayer;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public Scp939PlayerScript PlayerScript { get; private set; }
		public Rain(ATTG3Plugin plugin) => this.plugin = plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "Makes a player that is SCP-939 fast";
		public static readonly string[] CA = new string[] { "AGGRENADE" };
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.Allrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			if (Server.GetPlayers().Count < 1)
				return new string[] { "The server is empty!" };
			Player caller = (sender is Player send) ? send : null;
			if (args.Length > 0)
			{
				myPlayer = GetPlayerFromString.GetPlayer(args[0]);
				if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }

				string args2 = args[1].ToLower();
				if (args2 == "body")
				{
					MEC.Timing.RunCoroutine(Events.Rain(myPlayer, "body"));
				}
				else if (args2 == "grenade")
				{
					MEC.Timing.RunCoroutine(Events.Rain(myPlayer, "grenade"));
				}
				return new string[] { myPlayer.Name + "Added to List" };
			}
			else
			{
				return new[]
				{
				CA.First() + "Help" + " Shows this",
				CA.First() + "Reset",
				CA.First() + "Set",
				};
			}
		}
	}
}
