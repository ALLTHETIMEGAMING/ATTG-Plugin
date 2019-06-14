using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using MEC;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ATTG3
{
	internal class MTFCI : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerSetRole, IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle,
		IEventHandlerPlayerDie, IEventHandlerSpawn
	{



		private readonly ATTG3Plugin plugin;
		public MTFCI(ATTG3Plugin plugin) => this.plugin = plugin;
		public static int MTFKill = 0;
		public static int CIKills = 0;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.MTFCI)
			{
				foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
				{
					if (door.Name == "SURFACE_GATE")
					{
						door.Locked = true;
						door.Open = true;
					}
				}
				foreach (BlastDoor blast in Object.FindObjectsOfType<BlastDoor>())
				{
					blast.NetworkisClosed = true;
				}

				foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
				{
					Elevator.Locked = true;
				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team == Smod2.API.Team.SCP || player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD)
					{
						player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
					}
					else if (player.TeamRole.Team == Smod2.API.Team.CLASSD || player.TeamRole.Team == Smod2.API.Team.SCIENTIST)
					{
						player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
					}
				}
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.MTFCI)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
				{
					ev.Items.Clear();
					ev.Items.Add(Events.Invrandgive());
					ev.Items.Add(ItemType.COM15);
				}
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					ev.Items.Clear();
					ev.Items.Add(Events.Invrandgive());
					ev.Items.Add(ItemType.COM15);
				}
			}
		}
		public void OnSpawn(Smod2.Events.PlayerSpawnEvent ev)
		{
			if (plugin.MTFCI)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
				{
					ev.SpawnPos = new Vector(0, 1001, 0);
					Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
				}
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					ev.SpawnPos = new Vector(170, 984, 36);
					Timing.RunCoroutine(Events.GiveAmmo(ev.Player));
				}
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.MTFCI)
			{
				MTFKill = 0;
				CIKills = 0;
				plugin.MTFCI = false;
				
			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (plugin.MTFCI)
			{
				if (MTFKill < 25 && CIKills < 25)
				{
					if (PluginManager.Manager.Server.GetPlayers().Count <= 1)
					{	ev.Server.Map.ClearBroadcasts();
						ev.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>NO PLAYERS. ENDING ROUND</Color></SIZE>", false);
						ev.Status = ROUND_END_STATUS.NO_VICTORY;
					}
					else
					{
						ev.Status = ROUND_END_STATUS.ON_GOING;
					}
				}
				else
				{
					if (MTFKill >= 25)
					{
						ev.Server.Map.ClearBroadcasts();
						ev.Server.Map.Broadcast(10, "<SIZE=75><color=#0080FF>MTF WIN</Color></SIZE>", false);
						ev.Status = ROUND_END_STATUS.MTF_VICTORY;
					}
					else if (CIKills >= 25)
					{
						ev.Server.Map.ClearBroadcasts();
						ev.Server.Map.Broadcast(10, "<SIZE=75><color=#0B7A00>CI WIN</Color></SIZE>", false);
						ev.Status = ROUND_END_STATUS.CI_VICTORY;
					}
					else
					{
						ev.Server.Map.ClearBroadcasts();
						ev.Server.Map.Broadcast(10, "<SIZE=75><color=#FFFF00>STALEMATE</Color></SIZE>", false);
						ev.Status = ROUND_END_STATUS.NO_VICTORY;
					}
				}
			}
		}
		public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
		{
			if (plugin.MTFCI)
			{
				ev.SpawnRagdoll = false;
				Timing.RunCoroutine(Events.MTFCIRESPAWN(ev.Player));
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.MTFCI)
			{
				ev.AllowSummon = false;
			}
		}
	}
}


