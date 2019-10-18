using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;

namespace ATTG3
{
	class Trails : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public Trails(ATTG3Plugin plugin) => this.plugin = plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "";
		//Variables Below
		public static List<string> Playersspawn = new List<string>();
		public static readonly string[] CA = new string[] { "AGFAKE", "FAKE" };
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
				Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
				if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
				if (myPlayer.TeamRole.Role != Role.SPECTATOR)
				{
					if (Playersspawn.Contains(myPlayer.SteamId))
					{
						Playersspawn.Remove(myPlayer.SteamId);
					}

					Playersspawn.Add(myPlayer.SteamId);
					MEC.Timing.RunCoroutine(Events.Trail(myPlayer));
					return new string[] { myPlayer.Name + " Added to list" };
				}
				else
					return new string[] { myPlayer.Name + " is dead!" };
			}
			else
			{
				Playersspawn.Clear();

				return new string[] { "Removed All" };
			}
		}
	}
}
