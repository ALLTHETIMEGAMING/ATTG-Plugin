using scp4aiur;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Smod2.EventHandlers;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using UnityEngine;
using RemoteAdmin;

namespace ATTG3
{
	class Test : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public static GameObject local;
		public Test(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return " ";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return " ";
		}
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server)&&
				sender is Player player&&
				!plugin.AdminRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			PlayerList.AddPlayer(local);
			return new[]
			{
				$""
			};
		}
	}
}
