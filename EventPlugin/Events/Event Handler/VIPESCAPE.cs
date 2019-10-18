using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
	internal class VIPESCAPE : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSetRole,
		 IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerSpawn, IEventHandlerPlayerDie, IEventHandlerCheckEscape, IEventHandlerPlayerTriggerTesla
	{

		private readonly ATTG3Plugin plugin;
		public VIPESCAPE(ATTG3Plugin plugin) => this.plugin = plugin;
		public static Player VIPplayer = null;
		public static bool Vipescape = false;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.VIP)
			{
				VIPplayer = null;
				foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
				{
					if (door.Name == "106_SECONDARY")
					{
						door.Locked = true;
					}
					else if (door.Name == "NUKE_SURFACE")
					{
						door.Locked = true;
					}
					else if (door.Name == "106_PRIMARY")
					{
						door.Locked = true;
					}
					else if (door.Name == "HID")
					{
						door.Locked = true;
					}
					else if (door.Name == "INTERCOM")
					{
						door.Locked = true;
					}
				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (VIPplayer == null)
					{
						VIPplayer = player;
						player.ChangeRole(Role.CLASSD, true, true, true, true);
						plugin.Info(VIPplayer.ToString());
						MEC.Timing.RunCoroutine(Events.SetHP(player, 400));
					}
					else if (player.TeamRole.Team == Smod2.API.Team.SCIENTIST || player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD)
					{
						player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
						player.PersonalBroadcast(5, "ESCORT THE CLASS D TO THE EXIT", false);
					}
					else if (player != VIPplayer && (player.TeamRole.Team == Smod2.API.Team.CLASSD || player.TeamRole.Team == Smod2.API.Team.SCP))
					{
						player.ChangeRole(Role.NTF_LIEUTENANT, true, true, false, true);
						player.PersonalBroadcast(5, "Kill The Class-D", false);
					}
				}
				foreach (Pickup pickup in Object.FindObjectsOfType<Pickup>())
				{
					NetworkServer.Destroy(pickup.gameObject);
				}
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.VIP)
			{
				//Stops the nuke lever
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.VIP)
			{
				// Stops the event
				plugin.VIP = false;
				VIPplayer = null;
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.VIP)
			{
				// Give all players ammo
				ev.Items.Clear();
				ev.Items.Add(ItemType.E11_STANDARD_RIFLE);
				ev.Items.Add(ItemType.P90);
				ev.Items.Add(ItemType.COM15);
				ev.Items.Add(ItemType.MEDKIT);
				ev.Items.Add(ItemType.CHAOS_INSURGENCY_DEVICE);
			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (plugin.VIP)
			{
				if (Vipescape == true)
				{
					ev.Status = ROUND_END_STATUS.SCP_CI_VICTORY;
				}
				else if (Events.TUTCOUNT(Role.NTF_LIEUTENANT) == 0)
				{
					ev.Status = ROUND_END_STATUS.FORCE_END;
				}
				else if (VIPplayer == null)
				{
					ev.Status = ROUND_END_STATUS.MTF_VICTORY;
				}
				else
				{
					ev.Status = ROUND_END_STATUS.ON_GOING;
				}
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.VIP)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
				{
					ev.SpawnPos = ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD);
				}
				Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
				Timing.RunCoroutine(Events.Setplayerattach(ev.Player, ItemType.E11_STANDARD_RIFLE, WeaponBarrel.MUZZLE_BREAK,
				WeaponOther.AMMO_COUNTER, WeaponSight.SNIPER_SCOPE));
				Timing.RunCoroutine(Events.Setplayerattach(ev.Player, ItemType.P90, WeaponBarrel.MUZZLE_BREAK,
				WeaponOther.AMMO_COUNTER, WeaponSight.HOLO_SIGHT));
				Timing.RunCoroutine(Events.Setplayerattach(ev.Player, ItemType.COM15, WeaponBarrel.NONE,
				WeaponOther.FLASHLIGHT, WeaponSight.NONE));
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.VIP)
			{
				ev.AllowSummon = false;
			}
		}
		public void OnPlayerDie(PlayerDeathEvent ev)
		{
			if (plugin.VIP)
			{
				if (ev.Player.SteamId == VIPplayer.SteamId)
					VIPplayer = null;
			}
		}
		public void OnCheckEscape(PlayerCheckEscapeEvent ev)
		{
			if (plugin.VIP)
			{
				if (ev.Player.SteamId == VIPplayer.SteamId)
					Vipescape = true;
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (plugin.VIP)
			{
				ev.Triggerable = false;
			}
		}
	}
}


