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

namespace ATTG4
{
	public class ZOM : CustomItem
	{
        private readonly ATTG4Plugin plugin;
        public static SmodPlayer SmodPlayer { get; private set; }
        public override bool OnPickup()
		{
                PluginManager.Manager.Server.Map.Broadcast(10, "A SCP HAS SPAWNED", false);
                SmodPlayer smodPlayer = new SmodPlayer(PlayerObject);
                smodPlayer.ChangeRole(Smod2.API.Role.SCP_049_2, false, false);
                smodPlayer.SetHealth(500);
			return true;
		}
		public override bool OnDeathDrop(GameObject attacker, DamageType damage)
		{
			return false;
		}
	}
}
