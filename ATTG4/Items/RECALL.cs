using ItemManager;
using scp4aiur;
using ServerMod2.API;
using Smod2.API;
using System.Collections.Generic;
using UnityEngine;


namespace ATTG4
{
    public class RECALL : CustomItem
    {
        readonly float delay = 60;
        private readonly ATTG4Plugin plugin;

        public static SmodPlayer SmodPlayer { get; private set; }
        public override bool OnPickup()
        {
            SmodPlayer=new SmodPlayer(PlayerObject);
            SmodPlayer.PersonalBroadcast(10, "RECALLED ACTIVATING IN 1 MIN ", false);
            Timing.Run(TimingDelay(delay));
            return true;
        }
        public override bool OnDeathDrop(GameObject attacker, DamageType damage)
        {
            return false;
        }
        private static IEnumerable<float> TimingDelay(float time)
        {
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
