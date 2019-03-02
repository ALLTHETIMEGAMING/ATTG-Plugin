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
    public class ATTG3 : CustomWeapon
    {
        private const int WorldMask = 1207976449;
        private const int PlayerMask = 1208246273;
        private const float GlitchLength = 1.9f;

        private bool overCharged;
        
        public override int MagazineCapacity => ATTG3Plugin.GrenadeMagazine;
        public override float FireRate => ATTG3Plugin.GrenadeFireRate;


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
                .Where(y => Vector3.Distance(y.GetComponent<PlyMovementSync>().position, hit) < ATTG3Plugin.GrenadeOverChargeRadius &&
                            weps.GetShootPermission(y.GetComponent<CharacterClassManager>())))
            {
                player.GetComponent<PlayerStats>().HurtPlayer(new PlayerStats.HitInfo(
					ATTG3Plugin.GrenadeOverChargeDamage,
                    PlayerObject.GetComponent<NicknameSync>().myNick + " (" +
                    PlayerObject.GetComponent<CharacterClassManager>().SteamId + ")",
                    DamageTypes.Tesla,
                    PlayerObject.GetComponent<QueryProcessor>().PlayerId
                ), player);

                if (ATTG3Plugin.GrenadeOverCharageNukeEffect)
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
                damage = ATTG3Plugin.GrenadeTagDamage;

                if (hitbox != null)
                {
                    Timing2.In(x =>
                    {
                        OverchargeDetonate(weps, target.GetComponent<PlyMovementSync>().position);
                        if (weps.GetShootPermission(target.GetComponent<CharacterClassManager>()))
                        {
                            float glitchTime = x;
                            for (int i = 0; i < ATTG3Plugin.GrenadeTagGlitches; i++)
                            {
                                Timing2.In(y => TargetShake(target), glitchTime += GlitchLength);
                            }
                        }
                    }, ATTG3Plugin.GrenadeTagTime);
                }
                else if (Physics.Raycast(ray, out RaycastHit hit, 500f, WorldMask))
                {
                    Timing2.In(x =>
                    {
                        OverchargeDetonate(weps, hit.point);
                    }, DetonateFlash(hit.point));
                }
            }
            else
            {
                switch (hitbox?.id.ToUpper())
                {
                    case "HEAD":
                        damage = ATTG3Plugin.GrenadeHeadDamage;
                        break;

                    case "LEG":
                        damage = ATTG3Plugin.GrenadeLegDamage;
                        break;

                    case "SCP106":
                        damage = ATTG3Plugin.GrenadeScp106Damage;
                        break;

                    default:
                        damage = ATTG3Plugin.GrenadeBodyDamage;
                        break;
                }
            }

            
            int shots = Barrel == 1 ? ATTG3Plugin.GrenadeSuppressedKrakatoa : ATTG3Plugin.GrenadeKrakatoa;
            for (int i = 0; i < shots; i++)
            {
                weps.CallRpcConfirmShot(false, weps.curWeapon);
            }
        }

        private float DetonateFlash(Vector3 pos)
        {
            const int id = 0;
            Vector3 dir = Vector3.zero;
            const float throwForce = 1;

            GrenadeManager component1 = PlayerObject.GetComponent<GrenadeManager>();
            Grenade component2 = Object.Instantiate(component1.availableGrenades[id].grenadeInstance).GetComponent<Grenade>();
            component2.id = PlayerObject.GetComponent<QueryProcessor>().PlayerId + ":" + (component1.smThrowInteger + 4096);
            GrenadeManager.grenadesOnScene.Add(component2);
            component2.SyncMovement(component1.availableGrenades[id].GetStartPos(PlayerObject), (PlayerObject.GetComponent<Scp049PlayerScript>().plyCam.transform.forward + Vector3.up / 4f).normalized * throwForce, Quaternion.Euler(component1.availableGrenades[id].startRotation), component1.availableGrenades[id].angularVelocity);
            component1.CallRpcThrowGrenade(id, PlayerObject.GetComponent<QueryProcessor>().PlayerId, component1.smThrowInteger++ + 4096, dir, true, pos, false, 0);
            
            return component1.availableGrenades[id].timeUnitilDetonation;
        }
    }
}
