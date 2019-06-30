using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
	internal class Jug : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSetRole,
		 IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerThrowGrenade
	{

		private readonly ATTG3Plugin plugin;
		public Jug(ATTG3Plugin plugin) => this.plugin = plugin;
		public static Player VIPplayer = null;

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.Jugevent)
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
					else if (door.Name == "ESCAPE")
					{
						door.Locked = true;
					}
					else if (door.Name == "914")
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
					else
					{
						player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
					}
				}
				foreach (Pickup pickup in Object.FindObjectsOfType<Pickup>())
				{
					NetworkServer.Destroy(pickup.gameObject);
				}
				//Object 106con = Object.FindObjectOfType<LureSubjectContainer>();
				
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.Jugevent)
			{
				//Stops the nuke lever
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.Jugevent)
			{
				// Stops the event
				plugin.VIP = false;
				VIPplayer = null;
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.Jugevent)
			{
				// Give all players ammo
				Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
				ev.Items.Clear();
				if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					//Gives MTF items
					ev.Items.Add(ItemType.E11_STANDARD_RIFLE);
					ev.Items.Add(ItemType.MTF_COMMANDER_KEYCARD);
					ev.Items.Add(ItemType.FRAG_GRENADE);
					ev.Items.Add(ItemType.RADIO);
					ev.Items.Add(ItemType.MEDKIT);
					ev.Player.SetHealth(150);
					ev.Player.PersonalBroadcast(5, "KILL THE JUGGERNAUT", false);
				}
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.CLASSD)
				{
					//Gives CI items
					ev.Items.Add(ItemType.LOGICER);
					ev.Items.Add(ItemType.E11_STANDARD_RIFLE);
					ev.Items.Add(ItemType.CHAOS_INSURGENCY_DEVICE);
					ev.Items.Add(ItemType.FRAG_GRENADE);
					ev.Player.PersonalBroadcast(5, "Survive 25 min", false);
					ev.Player.SetHealth(10000);
				}
			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (plugin.Jugevent)
			{
				if (VIPplayer.TeamRole.Role == Role.SPECTATOR)
				{
					ev.Status = ROUND_END_STATUS.FORCE_END;
				}
				else if (PluginManager.Manager.Server.Round.Duration >= 1500)
				{
					ev.Status = ROUND_END_STATUS.CI_VICTORY;
				}
				else
				{
					ev.Status = ROUND_END_STATUS.ON_GOING;
				}
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.Jugevent)
			{
				ev.AllowSummon = true;
				ev.IsCI = false;
			}
		}
		public void OnThrowGrenade(Smod2.Events.PlayerThrowGrenadeEvent ev)
		{
			if (plugin.Jugevent)
			{
				if (ev.Player == VIPplayer)
				{
					plugin.Info("JUGGERNAUT Through a Grenade");
					ev.RemoveItem = false;
				}
			}
		}
	}
}


