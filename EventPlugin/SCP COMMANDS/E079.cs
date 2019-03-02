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
	class E079 : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public E079(ATTG3Plugin plugin)
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

			
			foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
			{
                Generator.HasTablet = false;
                Generator.Open = false;
				

			}
			return new[]
			{
				$"Generator tablets ejected"
			};

		}
	}
}
