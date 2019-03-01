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

	public class NUKE : CustomItem
	{
        private readonly ATTG3Plugin plugin;

        public static SmodPlayer SmodPlayer { get; private set; }
        public override bool OnPickup()
        {

            if (plugin.Citems == true)
            {
                SmodPlayer smodPlayer = new SmodPlayer(PlayerObject);
                smodPlayer.SetHealth(50);
                return true;
            }
            else
            {
                SmodPlayer = new SmodPlayer(PlayerObject);
                SmodPlayer.PersonalBroadcast(10, "Item is not actavated", false);
                return true;
            }
            // PluginManager.Manager.Server.Map.GetDoors().First(x => x.Name == "ESCAPE"

        }
        public override void OnMedkitUse()
        {
            if (plugin.Citems == true)
            {
                Vector doorpos2 = PluginManager.Manager.Server.Map.GetDoors().First(x => x.Name == "914").Position;
                Vector doorpos3 = new Vector(doorpos2.x + 2, doorpos2.y + 2, doorpos2.z + 2);
                SmodPlayer smodPlayer = new SmodPlayer(PlayerObject);
                smodPlayer.Teleport(doorpos3);
            }
        }


        public override bool OnDeathDrop(GameObject attacker, DamageType damage)
		{

			return false;
		}


	}
}
