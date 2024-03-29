﻿using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System;
using System.Linq;
namespace ATTG3
{
	class GunSound : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public Player myPlayer;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public GunSound(ATTG3Plugin plugin) => this.plugin = plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "Makes a player that is SCP-939 fast";
		public static Dictionary<string,string> Gunsoundlist = new Dictionary<string,string>();
        public static Dictionary<string, int> Gunsoundslist = new Dictionary<string, int>();
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
                    Gunsoundlist.Remove(myPlayer.SteamId.ToString());
					return new string[] { myPlayer.Name + " Removed from List" };
				}
				else if (args2 == "set")
				{
					if (Gunsoundlist.ContainsKey(myPlayer.SteamId))
					{
                        Gunsoundlist.Remove(myPlayer.SteamId);
					}
					string args3 = args[2].ToLower();
					if (args3 == "usp")
					{
                        Gunsoundlist.Add(myPlayer.SteamId.ToString(), "usp");
						return new string[] { myPlayer.Name + "Added to List" };
					}
					else if (args3 == "null")
					{
                        Gunsoundlist.Add(myPlayer.SteamId.ToString(), "null");
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
                else if (args2 == "sound")
                {
                    if (Gunsoundslist.ContainsKey(myPlayer.SteamId))
					{
                        Gunsoundslist.Remove(myPlayer.SteamId);
					}
                    string args3 = args[2].ToLower();
                    int soundnum = Int32.Parse(args3);
					if (soundnum > 0)
					{
						Gunsoundslist.Add(myPlayer.SteamId.ToString(), soundnum);
						return new string[] { myPlayer.Name + "Added to List" };
					}
					else
					{
						if (Gunsoundslist.ContainsKey(myPlayer.SteamId))
						{
							Gunsoundslist.Remove(myPlayer.SteamId);
						}
						return new string[] { myPlayer.Name + "Removed from List" };
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
