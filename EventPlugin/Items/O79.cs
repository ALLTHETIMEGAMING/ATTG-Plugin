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
	public class O79 : CustomItem
	{
        private readonly ATTG3Plugin plugin;
        public static SmodPlayer SmodPlayer { get; private set; }
        public override bool OnPickup()
		{
            if (plugin.Citems == true)
            {
                foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
                {
                    Generator.Unlock();
                    Generator.Open = true;
                    Generator.TimeLeft = 10;
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
		public override bool OnDeathDrop(GameObject attacker, DamageType damage)
		{
			return false;
		}
	}
}
