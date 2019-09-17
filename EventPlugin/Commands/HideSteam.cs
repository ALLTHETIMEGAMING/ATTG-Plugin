using Smod2;
using Smod2.API;
using Smod2.Commands;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Smod2.EventHandlers;

namespace ATTG3
{
	class HideSteam : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public HideSteam(ATTG3Plugin plugin) => this.plugin = plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "";
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
				Player Player1= sender as Player;
				GameObject playerObj = (GameObject)Player1.GetGameObject();
				playerObj.GetComponent<CharacterClassManager>().SetSteamId(myPlayer.SteamId.ToString());
				playerObj.GetComponent<NicknameSync>().NetworkmyNick = myPlayer.Name;
				Player1.SetRank(myPlayer.GetUserGroup().Color.ToString(), myPlayer.GetUserGroup().BadgeText.ToString(), null);
				return new string[] { Player1.Name + " Is now hidden" };
			}
			else
			{
				return new string[] { "AGHIDE: " + GetUsage() };
			}
		}
	}
}
