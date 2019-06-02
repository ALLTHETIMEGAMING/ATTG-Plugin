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

namespace ATTG3
{
	internal class MTFCI : IEventHandlerRoundStart,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSetRole, IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle
	{



		private readonly ATTG3Plugin plugin;
		public MTFCI(ATTG3Plugin plugin) => this.plugin = plugin;
		public int MTFKill = 0;
		public int CIKills = 0;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.MTFCI)
			{

				foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
				{
					
					if (door.Name == "ESCAPE")
					{
						door.Locked = true;
					}
					else if (door.Name == "SURFACE_GATE")
					{
						door.Locked = true;
						door.Open = true;
					}

				}
				foreach (BlastDoor blast in Object.FindObjectsOfType<BlastDoor>())
				{
					blast.NetworkisClosed = true;
				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					
				}
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.MTFCI)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP || ev.Player.TeamRole.Team == Smod2.API.Team.CLASSD)
				{
					ev.Player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
					new Task(async () =>
					{
						await Task.Delay(500);
						ev.Player.GiveItem(ItemType.MEDKIT);
					}).Start();
				}
				else if (ev.Player.TeamRole.Role == Smod2.API.Role.FACILITY_GUARD || ev.Player.TeamRole.Team == Smod2.API.Team.SCIENTIST)
				{
					ev.Player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
					new Task(async () =>
					{
						await Task.Delay(500);
						foreach (Smod2.API.Item item in ev.Player.GetInventory())
						{
							if (item.ItemType == ItemType.DISARMER)
							{
								item.Remove();
							}
						}
					}).Start();
				}
				else if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX) {
					new Task(async () =>
					{
						await Task.Delay(500);
						foreach (Smod2.API.Item item in ev.Player.GetInventory())
						{
							if (item.ItemType == ItemType.DISARMER)
							{
								item.Remove();
							}
						}
					}).Start();
				}
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.MTFCI)
			{ 
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
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
				if (MTFKill != 50 && CIKills != 50)
				{
					if (ATTG3Plugin.TUTCOUNT(Role.TUTORIAL) > 0)
					{
						ev.Server.Map.ClearBroadcasts();
						ev.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>TIME OUT. DO NOT SHOOT</Color></SIZE>", false);
						foreach (Player player in PluginManager.Manager.Server.GetPlayers())
						{
							player.SetCurrentItem(ItemType.NULL);
						}
					}
					else
					{
						ev.Server.Map.ClearBroadcasts();
						PluginManager.Manager.Server.Map.Broadcast(10, "<color=#0080FF>MTF Has " + MTFKill + " Kills out of 50</Color> <color=#0B7A00>CI Has " + CIKills + " Kills out of 50</Color>", false);
						ev.Status = ROUND_END_STATUS.ON_GOING;
					}
				}
				else
				{
					if (MTFKill == 50)
					{
						ev.Server.Map.ClearBroadcasts();
						ev.Server.Map.Broadcast(10, "<SIZE=75><color=#0080FF>MTF WIN</Color></SIZE>", false);
						ev.Status = ROUND_END_STATUS.MTF_VICTORY;
					}
					else if(CIKills == 50)
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
				if (ev.Killer.TeamRole.Role == Smod2.API.Role.NTF_COMMANDER)
				{
					MTFKill++;
					ev.Player.PersonalBroadcast(10, "You will respawn in 10 seconds", false);
					
					new Task(async () =>
					{
						await Task.Delay(10000);
						ev.Player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
						ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CHAOS_INSURGENCY), true);
					}).Start();
					
				}
				else if (ev.Killer.TeamRole.Role == Smod2.API.Role.CHAOS_INSURGENCY)
				{
					CIKills++;
					ev.Player.PersonalBroadcast(10, "You will respawn in 10 seconds", false);
					new Task(async () =>
					{
						await Task.Delay(10000);
						ev.Player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
						ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.NTF_COMMANDER), true);
					}).Start();
				}
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.INFECT)
			{
				ev.AllowSummon = false;
			}
		}
	}
}


