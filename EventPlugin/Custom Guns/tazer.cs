using System.Linq;
using ItemManager;
using ItemManager.Events;
using RemoteAdmin;
using scp4aiur;
using Smod2.API;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
    public class Taze : CustomWeapon, IDoubleDroppable
	{
        private const int WorldMask = 1207976449;
        private const int PlayerMask = 1208246273;
        private const float GlitchLength = 1.9f;

		private bool overCharged;

		public override int MagazineCapacity => ATTG3Plugin.GrenadeMagazine;
		public override float FireRate => ATTG3Plugin.GrenadeFireRate;

		public float DoubleDropWindow => ATTG3Plugin.GrenadeOverChargeable ? ATTG3Plugin.DoubleDropTime : 0;
		public bool OnDoubleDrop()
		{
			overCharged = !overCharged;



			return false;
		}
		public override void OnInitialize()
        {
            base.OnInitialize();

            Sight = 0;
            Barrel = 1;
            MiscAttachment = 1;
        }

        private static void TargetShake(GameObject target)
        {
            int rpcId = -737840022;

            NetworkWriter writer = new NetworkWriter();
            writer.Write((short)0);
            writer.Write((short)2);
            writer.WritePackedUInt32((uint)rpcId);
            writer.Write(target.GetComponent<NetworkIdentity>().netId);
            writer.FinishMessage();
            target.GetComponent<CharacterClassManager>().connectionToClient.SendWriter(writer, 0);
        }

        private void OverchargeDetonate(WeaponManager weps, Vector3 hit)
        {
            foreach (GameObject player in PlayerManager.singleton.players.Except(new[] { PlayerObject })
                .Where(y => Vector3.Distance(y.GetComponent<PlyMovementSync>().position, hit) < ATTG3Plugin.TAZEOverChargeRadius &&
                            weps.GetShootPermission(y.GetComponent<CharacterClassManager>())))
            {


                if (ATTG3Plugin.TAZEOverCharageNukeEffect)
                {
                    TargetShake(player);
                }
            }
        }

        protected override void OnValidShoot(GameObject target, ref float damage)
        {
            WeaponManager weps = PlayerObject.GetComponent<WeaponManager>();
            Transform cam = PlayerObject.GetComponent<Scp049PlayerScript>().plyCam.transform;

            Ray ray = new Ray(cam.position, cam.forward);
            Physics.Raycast(cam.position + cam.forward, cam.forward, out RaycastHit playerHit, PlayerMask);
            HitboxIdentity hitbox = playerHit.collider?.GetComponent<HitboxIdentity>();

            if (!overCharged)
            {


                if (hitbox != null)
                {
                    Timing2.In(x =>
                    {
                        OverchargeDetonate(weps, target.GetComponent<PlyMovementSync>().position);
                        if (weps.GetShootPermission(target.GetComponent<CharacterClassManager>()))
                        {
                            float glitchTime = x;
                            for (int i = 0; i < ATTG3Plugin.TAZETagGlitches; i++)
                            {
                                Timing2.In(y => TargetShake(target), glitchTime += GlitchLength);
                            }
                        }
                    }, ATTG3Plugin.TAZETagTime);
                }

            }



            int shots = Barrel == 1 ? ATTG3Plugin.TAZESuppressedKrakatoa : ATTG3Plugin.TAZEKrakatoa;
            for (int i = 0; i < shots; i++)
            {
                weps.CallRpcConfirmShot(false, weps.curWeapon);
            }
        }


    }
}
