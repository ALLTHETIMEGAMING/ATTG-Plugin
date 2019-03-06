using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.Commands;
using Smod2.API;
using scp4aiur;
using Smod2;
using ServerMod2.API;
using ItemManager;
using UnityEngine;
using System.Threading;
using System.Collections;
namespace ATTG3
{
	class T079 : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public GameObject  Generator;
		public T079(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "Ejects all Tablets";
		}

		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "Ejects all Tablets";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.SCPrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			Generator079
			float num2 = NetworkremainingPowerup = (startDuration = ConfigFile.ServerConfig.GetFloat("generator_duration", startDuration));
			Generator079.NetworkremainingPowerup

			return new[]
			{
				$"Generator tablets ejected"
			};

		}
	}
}
