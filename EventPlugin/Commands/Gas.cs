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
	class Gas : ICommandHandler
	{
        public static bool GASLCZ;
		private readonly ATTG3Plugin plugin;
		public Gas(ATTG3Plugin plugin)
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
            GASLCZ = !GASLCZ;
            if (GASLCZ)
            {
                DecontaminationGas.TurnOn();
            }
            else
            {
                foreach (DecontaminationGas gase in DecontaminationGas.gases)
                {
                    if (gase != null)
                    {
                        gase.gameObject.SetActive(false);
                    }
                }
            }
			return new[]
			{
				$"Gas {(GASLCZ ? "Actavated" : "Deactavated")}."
			};
		}
	}
}
