using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using scp4aiur;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
	internal class VIPESCAPE : IEventHandlerRoundStart,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSpawn,
		 IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle
	{

		private readonly ATTG3Plugin plugin;
		public VIPESCAPE(ATTG3Plugin plugin) => this.plugin = plugin;
		Player VIPplayer = null;
		bool Vipescape;

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.VIP)
			{
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
				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (VIPplayer == null)
					{
						VIPplayer = player;
						player.ChangeRole(Role.CLASSD, true, true, true, true);
					}
					else  if (player.TeamRole.Team == Smod2.API.Team.SCP || player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD)
					{
						player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
					}
					else if (player != VIPplayer &&(player.TeamRole.Team == Smod2.API.Team.CLASSD || player.TeamRole.Team == Smod2.API.Team.SCIENTIST))
					{
						player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
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
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.VIP)
			{
				plugin.VIP = false;
				VIPplayer = null;
				Vipescape = false;

			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.VIP)
			{
				if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
				{
					new Task(async () =>
					{
						await Task.Delay(500);
						foreach (Smod2.API.Item item in ev.Player.GetInventory())
						{
							item.Remove();
						}
						ev.Player.GiveItem(ItemType.LOGICER);
					}).Start();
					ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD);
					ev.Player.PersonalBroadcast(5, "ESCORT THE CLASS D TO THE EXIT", false);
				}
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					new Task(async () =>
						{
							await Task.Delay(500);
							foreach (Smod2.API.Item item in ev.Player.GetInventory())
							{
								item.Remove();
							}
							ev.Player.GiveItem(ItemType.E11_STANDARD_RIFLE);
						}).Start();
					ev.Player.PersonalBroadcast(5, "Kill The Class-D", false);
				}
			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (plugin.VIP)
			{
				if (VIPplayer.TeamRole.Role == Role.CLASSD && Vipescape == false)
				{
					ev.Status = ROUND_END_STATUS.ON_GOING;
				}
				else if (Vipescape == true)
				{
					ev.Status = ROUND_END_STATUS.CI_VICTORY;
				}
				else if (ev.Round.Stats.NTFAlive == 0)
				{
					ev.Status = ROUND_END_STATUS.CI_VICTORY;
				}
				else if (VIPplayer.TeamRole.Role != Role.CLASSD && Vipescape == false)
				{
					ev.Status = ROUND_END_STATUS.MTF_VICTORY;
				}
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.VIP)
			{
				ev.AllowSummon = false;
			}
		}
	}
}


