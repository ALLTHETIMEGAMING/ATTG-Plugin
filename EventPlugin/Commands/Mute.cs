using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
namespace ATTG3
{
	class Mute : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public Mute(ATTG3Plugin plugin) { this.plugin = plugin; }
		public string GetCommandDescription() { return ""; }
		public string GetUsage() { return ""; }
		public static readonly string[] CA = new string[] { "MUTEA", "AGMUTE" };
		public static bool Muted = false;
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
			if (!Muted)
			{
				foreach (Player players in PluginManager.Manager.Server.GetPlayers())
				{
					if (players.SteamId != "76561198126860363")
					{
						players.Muted = true;
					}
				}
			}
			else
			{
				foreach (Player players in PluginManager.Manager.Server.GetPlayers())
				{
					players.Muted = false;
				}
			}
			Muted = !Muted;
			return new[] { "Mute All: " + Muted };
		}
	}
}
