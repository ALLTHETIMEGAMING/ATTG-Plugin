using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2;
using ServerMod2.API;
using ItemManager;
using Smod2.API;
using UnityEngine;
using System.Threading;
using scp4aiur;
using System.Collections;

namespace ATTG3
{
	public class ELE : CustomItem
	{
		float delay = 60;

        private readonly ATTG3Plugin plugin;

        public static SmodPlayer SmodPlayer { get; private set; }
        private static IEnumerable<float> TimingDelay(float time)
		{
			float delay = 60;
			PluginManager.Manager.Server.Map.Broadcast(10, "Elevators are now locked", false);
			foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
			{
				if (Elevator.Locked == false)
				{
					Elevator.Locked = true;
				}
			}
			yield return delay;
			PluginManager.Manager.Server.Map.Broadcast(10, "Elevators are now unlocked", false);
			foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
			{
				if (Elevator.Locked == true)
				{
					Elevator.Locked = false;
				}
			}
		}
		public override bool OnPickup()
		{


            if (plugin.Citems == true)
            {
                Timing.Run(TimingDelay(delay));
                return true;
            }
            else
            {
                SmodPlayer = new SmodPlayer(PlayerObject);
                SmodPlayer.PersonalBroadcast(10, "Item is not actavated", false);
                return true;
            }
		}


		public override bool OnDeathDrop(GameObject attacker, DamageType damage)
		{

			return false;
		}

	}
}
