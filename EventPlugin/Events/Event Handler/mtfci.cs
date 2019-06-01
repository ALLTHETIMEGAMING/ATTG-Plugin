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
	internal class MTFCI : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerLure,
		IEventHandlerGeneratorInsertTablet, IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerDecideTeamRespawnQueue, IEventHandlerPlayerTriggerTesla,
		IEventHandlerDoorAccess, IEventHandlerPlayerDie
	{



		private readonly ATTG3Plugin plugin;
		public MTFCI(ATTG3Plugin plugin) => this.plugin = plugin;

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
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (plugin.MTFCI)
			{

			}
		}

		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.MTFCI)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP || ev.Player.TeamRole.Team == Smod2.API.Team.CLASSD)
				{
					ev.Player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
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
			}
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (plugin.MTFCI)
			{


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
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (plugin.MTFCI)
			{

			}
		}
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (plugin.MTFCI)
			{

			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.MTFCI)
			{
				plugin.MTFCI = false;
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.MTFCI)
			{
				
			}
		}
		public void OnLure(PlayerLureEvent ev)
		{
			if (plugin.MTFCI)
			{

			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.MTFCI)
			{

			}
		}
		public void OnElevatorUse(PlayerElevatorUseEvent ev)
		{
			if (plugin.MTFCI)
			{
			
			}
		}
		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (plugin.MTFCI)
			{

			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (plugin.MTFCI)
			{

			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (plugin.MTFCI)
			{
				ev.Server.Map.ClearBroadcasts();
				ev.Server.Map.Broadcast(10, "There are " + plugin.Round.Stats.CiAlive + " Chaos alive, and " + plugin.Round.Stats.NTFAlive + " NTF alive.", false);
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.MTFCI)
			{
			}
		}
		public void OnDecideTeamRespawnQueue(DecideRespawnQueueEvent ev)
		{
			if (plugin.MTFCI)
			{
				
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (plugin.MTFCI)
			{
				
			}
		}
		public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
		{
			if (plugin.MTFCI)
			{
				ev.SpawnRagdoll = false;
				
			}
		}
	}
}


