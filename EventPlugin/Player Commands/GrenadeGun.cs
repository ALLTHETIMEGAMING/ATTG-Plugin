using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using System;
namespace ATTG3
{
	class GrenadeGun : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public Player myPlayer;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public Scp939PlayerScript PlayerScript { get; private set; }
		public GrenadeGun(ATTG3Plugin plugin) => this.plugin = plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "Makes a player that is SCP-939 fast";
		public static Dictionary<string,string> GrenadeList = new Dictionary<string,string>();
		public static Dictionary<string, int> shotgun = new Dictionary<string, int>();
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

				if (args2 == "reset")
				{
					if (GrenadeList.ContainsKey(myPlayer.SteamId))
					{
						GrenadeList.Remove(myPlayer.SteamId);
					}
					if (shotgun.ContainsKey(myPlayer.SteamId))
					{
						shotgun.Remove(myPlayer.SteamId);
					}
					return new string[] { myPlayer.Name + " Removed from List" };
				}
				else if (args2 == "set")
				{
					if (GrenadeList.ContainsKey(myPlayer.SteamId))
					{
						GrenadeList.Remove(myPlayer.SteamId);
					}
					if (shotgun.ContainsKey(myPlayer.SteamId))
					{
						shotgun.Remove(myPlayer.SteamId);
					}
					string args3 = args[2].ToLower();
					if (args3 == "body")
					{
						GrenadeList.Add(myPlayer.SteamId.ToString(), "body");
						return new string[] { myPlayer.Name + "Added to List" };
					}
					else if (args3 == "grenade")
					{
						GrenadeList.Add(myPlayer.SteamId.ToString(), "grenade");
						return new string[] { myPlayer.Name + "Added to List" };
					}
					else if (args3 == "usp")
					{
						GrenadeList.Add(myPlayer.SteamId.ToString(), "usp");
						return new string[] { myPlayer.Name + "Added to List" };
					}
					else if (args3 == "null")
					{
						GrenadeList.Add(myPlayer.SteamId.ToString(), "null");
						return new string[] { myPlayer.Name + "Added to List" };
					}
					else if (args3 == "shot")
					{
						if (args.Length > 2)
						{
							int result = Int32.Parse(args[3]);
							shotgun.Add(myPlayer.SteamId.ToString(), result);
						}
						else
						{
							shotgun.Add(myPlayer.SteamId.ToString(), 6);
						}
						return new string[] { myPlayer.Name + "Added to List" };
					}
					else
					{
						return new[]
						{
							"",
						CA.First() + " Help" + " Shows this",
						CA.First() + " Reset" + ".",
						CA.First() + " Set" + " Sets.",
						};
					}
				}
				else
				{
					return new[]
					{
							"",
						CA.First() + " Help" + " Shows this",
						CA.First() + " Reset" + ".",
						CA.First() + " Set" + " Sets.",
						};
				}

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
