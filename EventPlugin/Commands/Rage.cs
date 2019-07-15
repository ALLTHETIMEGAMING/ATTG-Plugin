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
	class Rage : ICommandHandler
	{
        public static bool SSAMBOT = false;
		private readonly ATTG3Plugin plugin;
		public Rage(ATTG3Plugin plugin)
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
				!plugin.Allrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
            foreach (Player target in plugin.Server.GetPlayers())
            {
                if (target.TeamRole.Role == Role.SCP_096)
                {
                    GameObject SCP = (GameObject)target.GetGameObject();

                    SCP.GetComponent<Scp096PlayerScript>().ragemultiplier_coodownduration = 0.5f;
                    SCP.GetComponent<Scp096PlayerScript>().IncreaseRage(100f);
                    SCP.GetComponent<Scp096PlayerScript>().enraged = Scp096PlayerScript.RageState.Panic;

                   
                }
            }
            return new[] { $"SCP-096 has been placed into a state of panic" };
        }
	}
}
