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
    public class LD : CustomItem
    {
        float delay = 60;
        private readonly ATTG3Plugin plugin;

        public static SmodPlayer SmodPlayer { get; private set; }

        private static IEnumerable<float> TimingDelay(float time)
        {
            float delay = 60;
            PluginManager.Manager.Server.Map.Broadcast(10, "ALL DOORS ARE NOW LOCKED CLOSED FOR 1 MIN", false);
            foreach (Smod2.API.Door Door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
            {
                if (Door.Open == true)
                {
                    Door.Open = false;
                }

                if (Door.Locked == false)
                {
                    Door.Locked = true;
                }
            }
            yield return delay;
            PluginManager.Manager.Server.Map.Broadcast(10, "ALL DOORS ARE NOW UNLOCKED", false);
            foreach (Smod2.API.Door Door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
            {
                if (Door.Open == false)
                {
                    Door.Open = true;
                }

                if (Door.Locked == true)
                {
                    Door.Locked = false;
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