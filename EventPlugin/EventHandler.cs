using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;
using MEC;
using System.IO;
using System.Collections.Generic;
using Smod2.Attributes;
using Smod2.Config;
using System.Linq;
using System.Threading;
using System;
using Smod2.Commands;
using UnityEngine.Networking;

namespace ATTG3
{
	internal class EventHandler : IEventHandlerWarheadStopCountdown,
		IEventHandlerDoorAccess, IEventHandlerGeneratorUnlock, IEventHandlerPlayerHurt,
        IEventHandlerSetRole, IEventHandlerBan, IEventHandlerGeneratorInsertTablet,
		IEventHandlerWarheadKeycardAccess, IEventHandlerElevatorUse, IEventHandlerRoundEnd, IEventHandlerWaitingForPlayers, IEventHandlerNicknameSet, IEventHandlerRoundStart,
		IEventHandlerTeamRespawn, IEventHandlerSpawn, IEventHandlerSetConfig, IEventHandlerShoot, IEventHandlerPlayerJoin, IEventHandlerPocketDimensionEnter, IEventHandlerPlayerDie
	{
		private readonly ATTG3Plugin plugin;
		public EventHandler(ATTG3Plugin plugin) => this.plugin = plugin;
		public Scp096PlayerScript PlayerScript { get; private set; }
		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			if (plugin.Disable)
			{
				this.plugin.PluginManager.DisablePlugin(this.plugin);
			}
			foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
			{
				Elevator.MovingSpeed = plugin.Elevatord;
			}
            Events.RestartRound();
        }
		public void OnRoundStart(RoundStartEvent ev)
		{
			plugin.RoundStarted = true;
		}
		public void OnNicknameSet(Smod2.Events.PlayerNicknameSetEvent ev)
		{
			if (ev.Nickname.StartsWith("@"))
			{
				ev.Player.Ban(99999999, "If you have @ This is a Perm Ban");
				plugin.Info("Banned: " + ev.Nickname);
			}
			else if (ev.Player.SteamId == "76561198069087428")
			{
				ev.Nickname = "Gren";
			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			foreach (Smod2.API.Door door in PluginManager.Manager.Server.Map.GetDoors())
			{
				if (door.Name == "CHECKPOINT_ENT")
				{
					door.Open = true;
					door.Locked = false;
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			Player player = ev.Player;
			if (plugin.O96Door)
			{
				if (ev.Player.TeamRole.Role == Role.SCP_096)
				{
					GameObject Obj = (GameObject)ev.Player.GetGameObject();
					PlayerScript = Obj.GetComponent<Scp096PlayerScript>();
					if (PlayerScript.Networkenraged == Scp096PlayerScript.RageState.Enraged && ev.Door.Locked == false)
					{
						ev.Door.Open = true;
					}
				}
			}
            if (ev.Player.TeamRole.Role == Role.TUTORIAL)
            {
                ev.Allow = true;
            }
            if (plugin.SCPPRO && !plugin.Event && ev.Player.TeamRole.Role != Role.TUTORIAL)
            {
                if (PluginManager.Manager.Server.Round.Duration < 180)
                {
                    if (ev.Door.Name == "106_PRIMARY")
                    {
                        ev.Allow = false;
                        ev.Door.Open = false;
                        ev.Player.PersonalBroadcast(10, "You can not open this door yet", false);
                    }
                    else if (ev.Door.Name == "106_SECONDARY")
                    {
                        ev.Allow = false;
                        ev.Door.Open = false;
                        ev.Player.PersonalBroadcast(10, "You can not open this door yet", false);
                    }
                }
            }
			
			if (plugin.NoCHand == true)
			{
				if (ev.Door.Permission == "CONT_LVL_3" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD) ||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "CONT_LVL_2" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD) ||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD) || player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD) ||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD) || player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "CONT_LVL_1" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD) ||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD) || player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD) ||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD) || player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD) ||
						player.HasItem(ItemType.GUARD_KEYCARD) || player.HasItem(ItemType.JANITOR_KEYCARD) ||
						player.HasItem(ItemType.SCIENTIST_KEYCARD) || player.HasItem(ItemType.ZONE_MANAGER_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "CHCKPOINT_ACC" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD) ||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD) || player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD) ||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD) || player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD) ||
						player.HasItem(ItemType.GUARD_KEYCARD) || player.HasItem(ItemType.ZONE_MANAGER_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "ARMORY_LVL_1" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD) || player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD) ||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD) || player.HasItem(ItemType.GUARD_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "ARMORY_LVL_2" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD) || player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD) ||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "ARMORY_LVL_3" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "ARMORY_LVL_1" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD) || player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD) ||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD) || player.HasItem(ItemType.GUARD_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "EXIT_ACC" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD) || player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD))
					{
						ev.Allow = true;
					}
				}
				else if (ev.Door.Permission == "INCOM_ACC" && ev.Door.Locked == false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD))
					{
						ev.Allow = true;
					}
				}
			}
			if (ev.Door.Name == "NUKE_SURFACE")
			{
				if (ev.Allow == true)
				{
					ev.Door.Locked = true;
					ev.Door.Open = true;
				}
			}
		}
		public void OnGeneratorUnlock(PlayerGeneratorUnlockEvent ev)
		{
			Player player = ev.Player;
			Generator gen = ev.Generator;
			if (plugin.GenLock)
			{
				ev.Allow = false;
			}
			if (plugin.GenHand && !plugin.GenLock)
			{
				if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE) ||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD))
				{
					gen.Unlock();
				}
			}
		}
		public void OnSetRole(PlayerSetRoleEvent ev)
		{
            GameObject val = GameObject.Find("Host");
            int num = -1;
            if (val != null)
            {
                num = val.GetComponent<RandomSeedSync>().seed;
            }
            if (plugin.Lights)
			{
				ev.Player.GiveItem(ItemType.FLASHLIGHT);
			}
            if (plugin.TestingSpawn)
            {
                int RandomInt = new System.Random().Next(ATTG3Plugin.Maplist.Count);
            }
            if (plugin.AdminRanks.Contains(ev.Player.GetRankName()) && Setup.Setupbool && ev.Player.TeamRole.Role == Role.TUTORIAL)
            {
                ev.Player.PersonalBroadcast(10, "Map Seed is:" + num, false);
            }
            /*if (PluginManager.Manager.Server.Round.Stats.ClassDAlive == 0)
            {
                ev.Items.Remove(ItemType.DISARMER);
            }*/
		}
		public void OnBan(BanEvent ev)
		{
			if (ev.Player.SteamId == "76561198126860363")
			{
				ev.AllowBan = false;
				PluginManager.Manager.Server.Map.ClearBroadcasts();
				Player steamadmin = ev.Admin;
				if (ev.Admin.SteamId != "76561198126860363")
				{
					steamadmin.Ban(1);
					PluginManager.Manager.Server.Map.ClearBroadcasts();
					PluginManager.Manager.Server.Map.Broadcast(10, ev.Admin.Name + " Was uno reverse carded", false);
				}
			}
			else if (ev.Admin.SteamId.Equals("76561198126860363"))
			{
                /*
                PluginManager.Manager.Server.Map.ClearBroadcasts();
                int RandomInt = new System.Random().Next(ATTG3Plugin.Banmemes.Count);
				PluginManager.Manager.Server.Map.Broadcast(10, ev.Player.Name.ToUpper().ToString() + " "+ ATTG3Plugin.Banmemes[RandomInt], false);
                */
                ev.Reason = "";
			}
		}
		public void OnWarheadKeycardAccess(Smod2.Events.WarheadKeycardAccessEvent ev)
		{
			Player player = ev.Player;
			if (player.HasItem(ItemType.O5_LEVEL_KEYCARD) || player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD) ||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD))
			{
				ev.Allow = true;
			}
			if (player.GetBypassMode() == true)
			{
				ev.Allow = true;
			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.GenLock)
			{
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Generators are Locked", false);
				ev.RemoveTablet = true;
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (ev.Player.SteamId == "76561198126860363")
			{
                Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
                if (Config.Config1 && ATTG3Plugin.MapCusSpawn.Count > 0)
                {
                    System.Random rand = new System.Random();
                    List<Vector> values = Enumerable.ToList(ATTG3Plugin.MapCusSpawn.Keys);
                    int size = ATTG3Plugin.MapCusSpawn.Count;
                    ev.SpawnPos = values[rand.Next(size)];
                }
			}
		}
		public void OnElevatorUse(Smod2.Events.PlayerElevatorUseEvent ev)
		{
			/*if (Vars.Elock.TryGetValue(ev.Player.SteamId, out bool elock)&&elock==true)
			{
				ev.Elevator.Locked=true;

			}
			else if (Vars.Elock.TryGetValue(ev.Player.SteamId, out elock)&&elock==false)
			{
				ev.Elevator.Locked=false;
			{*/
		}
		public void OnRoundEnd(Smod2.Events.RoundEndEvent ev)
		{
			plugin.RoundStarted = false;
            ConfigFile.ReloadGameConfig();
            ATTG3Plugin.MapCusSpawn.Clear();
        }
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (ev.SpawnChaos == true && plugin.Event == false && ev.PlayerList.Count> 0)
			{
				PluginManager.Manager.Server.Map.AnnounceCustomMessage("UNAUTHORIZED PERSONNEL SPOTTED AT GATE A");
			}
		}
        public void OnSetConfig(Smod2.Events.SetConfigEvent ev)
        {
			/*
            if (ATTG3Plugin.Maplist.Count > 0)
            {
                if (ev.Key == "map_seed")
                {
                    var Mapfile = File.ReadAllLines(ATTG3Plugin.Mapseeds);
                    ATTG3Plugin.Maplist = new List<string>(Mapfile);
                    int RandomInt = new System.Random().Next(ATTG3Plugin.Maplist.Count);
                    int mapseed = Int32.Parse(ATTG3Plugin.Maplist[RandomInt].ToString());
                    plugin.Info(mapseed.ToString());
                    ev.Value = mapseed;
                }
            }
			*/
        }
        public void OnShoot(Smod2.Events.PlayerShootEvent ev)
        {
            if (SSAM.SSAMBOT&&ev.Player.SteamId == "76561198126860363")
            {
                Events.SSAIMBOT(ev.Player);
                ev.ShouldSpawnHitmarker = true;
            }
			else if (GrenadeGun.GrenadeList.ContainsKey(ev.Player.SteamId.ToString()))
			{
				GameObject player = (GameObject)ev.Player.GetGameObject();
				WeaponManager playerWM = player.GetComponent<WeaponManager>();
				Ray ray = new Ray(playerWM.camera.transform.position + playerWM.camera.transform.forward, playerWM.camera.transform.forward);
				if (ev.Target != null)
				{
					if (GrenadeGun.GrenadeList[ev.Player.SteamId.ToString()] == "grenade")
					{
						int kill = 0;
						ev.Player.ThrowGrenade(GrenadeType.FRAG_GRENADE, false, new Vector(0f, 0f, 0f), true, ev.TargetPosition, true, 0f, false);
						ev.Player.ThrowGrenade(GrenadeType.FLASHBANG, false, new Vector(0f, 0f, 0f), true, ev.TargetPosition, true, 0f, false);
						while (kill != 10)
						{
							Vector target = new Vector(ev.TargetPosition.x + UnityEngine.Random.Range(-5f, 5f), ev.TargetPosition.y + UnityEngine.Random.Range(0f, 5f), ev.TargetPosition.z + UnityEngine.Random.Range(-5f, 5f));
							ev.Player.ThrowGrenade(GrenadeType.FRAG_GRENADE, false, new Vector(0f, 0f, 0f), true, target, true, 0f, false);
							target = new Vector(ev.TargetPosition.x + UnityEngine.Random.Range(-5f, 5f), ev.TargetPosition.y + UnityEngine.Random.Range(0f, 5f), ev.TargetPosition.z + UnityEngine.Random.Range(-5f, 5f));
							ev.Player.ThrowGrenade(GrenadeType.FLASHBANG, false, new Vector(0f, 0f, 0f), true, target, true, 0f, false);
							kill++;
						}
					}
					else if (GrenadeGun.GrenadeList[ev.Player.SteamId.ToString()] == "body")
					{
						int kill = 0;
						while (kill != 10)
						{
							GameObject player1 = (GameObject)ev.Target.GetGameObject();
							int role = (int)ev.Target.TeamRole.Role;
							Vector3 target = new Vector3(ev.TargetPosition.x + UnityEngine.Random.Range(-5f, 5f), ev.TargetPosition.y + UnityEngine.Random.Range(0f, 5f), ev.TargetPosition.z + UnityEngine.Random.Range(-5f, 5f));
							Class @class = PlayerManager.localPlayer.GetComponent<CharacterClassManager>().klasy[role];
							GameObject ragdoll = UnityEngine.Object.Instantiate(@class.model_ragdoll, target + @class.ragdoll_offset.position, Quaternion.Euler(player1.transform.rotation.eulerAngles + @class.ragdoll_offset.rotation));
							NetworkServer.Spawn(ragdoll);
							ragdoll.GetComponent<Ragdoll>().SetOwner(new Ragdoll.Info(ev.Player.PlayerId.ToString(), ev.Target.Name, new PlayerStats.HitInfo(), role, ev.Target.PlayerId));
							Fakedea.wipe.Add(ragdoll);
							kill++;
						}
					}
				}
				else if (Physics.Raycast(ray, out RaycastHit raycastHit, 150f))
				{
					if (GrenadeGun.GrenadeList[ev.Player.SteamId.ToString()] == "grenade")
					{
						Vector3 destination = raycastHit.point + raycastHit.normal * 1f;
						Vector vector3pos = new Vector(destination.x, destination.y, destination.z);
						ev.Player.ThrowGrenade(GrenadeType.FRAG_GRENADE, false, new Vector(0f, 0f, 0f), true, vector3pos, true, 0f, false);
					}
				}
			}
        }
        public void OnPlayerJoin(Smod2.Events.PlayerJoinEvent ev)
        {

        }
		public void OnPocketDimensionEnter(Smod2.Events.PlayerPocketDimensionEnterEvent ev)
		{
			if (ev.Player.TeamRole.Role == Role.TUTORIAL)
			{
				ev.TargetPosition = ev.LastPosition;
			}
		}
        public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
        {
            if (Feed.Feedbool)
            {
                Timing.RunCoroutine(Events.FEED(ev.Player));
            }
			if (ev.Player.SteamId == "76561198126860363")
			{
				ev.Player.SetAmmo(AmmoType.DROPPED_5, 0);
				ev.Player.SetAmmo(AmmoType.DROPPED_7, 0);
				ev.Player.SetAmmo(AmmoType.DROPPED_9, 0);
			}

		}
        public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
        {
            if (ev.Attacker.SteamId == "76561198126860363")
            {
                if (ev.DamageType != DamageType.TESLA && ev.Attacker.TeamRole.Team != Smod2.API.Team.SCP && ev.DamageType != DamageType.POCKET && ev.Player.TeamRole.Team != Smod2.API.Team.SCP && ev.DamageType != DamageType.FLYING)
                {
                    ev.Damage = 50;
                }
                else if (ev.Player.TeamRole.Role == Smod2.API.Role.SCP_106)
                {
                    ev.Damage = 4;
                }
            }
        }

	}
}
