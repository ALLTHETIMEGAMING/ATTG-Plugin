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
	public class CD : CustomItem
	{
        private readonly ATTG3Plugin plugin;

        public static SmodPlayer SmodPlayer { get; private set; }
        public override bool OnPickup()
		{
            if (plugin.Citems == true)
            {

                foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                {
                    if (door.Open == true)
                    {
                        door.Open = false;
                    }
                }
                return true;
            }
            else
            {
                SmodPlayer = new SmodPlayer(PlayerObject);
                SmodPlayer.PersonalBroadcast(10, "Item is not actavated", false);
                return true;
            }
        }

		public override bool OnDrop()
		{

            if (plugin.Citems == true)
            {

                foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                {
                    if (door.Open == false)
                    {
                        door.Open = true;
                    }
                }
            }
			return true;
		}

		public override bool OnDeathDrop(GameObject attacker, DamageType damage)
		{

			return false;
		}

	}
}



