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
    public class RECALL : CustomItem
    {
        //List<int>;
        //private readonly ATTG3Plugin plugin;

        float delay = 60;
        private readonly ATTG3Plugin plugin;

        public static SmodPlayer SmodPlayer { get; private set; }


        public override bool OnPickup()
        {
            if (plugin.Citems == true)
            {
                SmodPlayer = new SmodPlayer(PlayerObject);
                SmodPlayer.PersonalBroadcast(10, "RECALLED ACTIVATING IN 1 MIN ", false);
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
        private static IEnumerable<float> TimingDelay(float time)
        {
            // Why wont this work?
            //Severity	Code	Description	Project	File	Line	Suppression State
            //Error CS0120  An object reference is required for the non-static field, method, or property 'CustomItem.PlayerObject' ATTG3    47  Active
            

            float delay = 60;
            int HP = SmodPlayer.GetHealth();
            
            Vector pos = SmodPlayer.GetPosition();

            yield return delay;
            SmodPlayer.SetHealth(HP);
            SmodPlayer.Teleport((pos), true);
			SmodPlayer.PersonalBroadcast(10, "RECALLED ACTIVATED ", false);

		}
    }
}
