using ItemManager;
using ServerMod2.API;
using Smod2;
using Smod2.API;
using UnityEngine;

namespace ATTG4
{

    public class O49 : CustomItem
    {
        private readonly ATTG4Plugin plugin;

        public static SmodPlayer SmodPlayer { get; private set; }
        public override bool OnPickup()
        {
            PluginManager.Manager.Server.Map.Broadcast(10, "A SCP HAS SPAWNED", false);
            SmodPlayer smodPlayer = new SmodPlayer(PlayerObject);
            smodPlayer.ChangeRole(Smod2.API.Role.SCP_049, false, false);
            smodPlayer.SetHealth(500);

            SmodPlayer=new SmodPlayer(PlayerObject);
            SmodPlayer.PersonalBroadcast(10, "Item is not actavated", false);

            return true;
        }

        public override bool OnDeathDrop(GameObject attacker, DamageType damage)
        {

            return false;
        }


    }
}

