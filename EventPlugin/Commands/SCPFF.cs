using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using Smod2;
using scp4aiur;
using Smod2.EventHandlers;
using Smod2.Events;
namespace ATTG3
{
	class SCPFF : ICommandHandler
	{
		public float converted;
		private readonly ATTG3Plugin plugin;
		public SCPFF(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "HCZ Overcharge";
		}
        public static readonly string[] CA = new string[] { "AGLIGHTS", "LIGHTS" };
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
			foreach (Smod2.API.Player player1 in plugin.Server.GetPlayers())
			{
				if (player1.TeamRole.Team == Smod2.API.Team.SCP)
				{
					((UnityEngine.GameObject)player1.GetGameObject()).GetComponent<WeaponManager>().NetworkfriendlyFire = true;
				}
			}
			return new[]
			{
				$"SCP FF ACTAVATED."
			};
		}

	}
}
