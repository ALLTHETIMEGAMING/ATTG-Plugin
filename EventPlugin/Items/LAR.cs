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

namespace ATTG3
{

	public class LAR : CustomItem
    {
        private readonly ATTG3Plugin plugin;

        public static SmodPlayer SmodPlayer { get; private set; }
        public override bool OnPickup()
		{
            if (plugin.Citems)
            {
                PluginManager.Manager.Server.Map.Broadcast(10, "A SCP HAS SPAWNED", false);
                SmodPlayer smodPlayer = new SmodPlayer(PlayerObject);
                smodPlayer.ChangeRole(Smod2.API.Role.SCP_106, false, false);
                smodPlayer.SetHealth(500);
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



