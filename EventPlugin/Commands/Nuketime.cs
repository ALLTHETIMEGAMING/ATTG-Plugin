using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using Smod2;
using MEC;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;
namespace ATTG3
{
	class Nuketime : ICommandHandler
	{

		private readonly ATTG3Plugin plugin;
		public Nuketime(ATTG3Plugin plugin)
		{
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			return "";
		}
		public string GetUsage()
		{
			return "";
		}
        public static readonly string[] CA = new string[] { "AGLIGHTS", "LIGHTS" };
        public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server)&&
				sender is Player player&&
				!plugin.Allrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
            AlphaWarheadController host = AlphaWarheadController.host;
            host.NetworktimeToDetonation = 600;
            return new[]
			{
				"Nuke set to 600"
			};
		}
	}
}
