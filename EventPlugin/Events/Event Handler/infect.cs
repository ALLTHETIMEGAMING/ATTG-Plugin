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
	internal class INFECT : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerLure,
		IEventHandlerGeneratorInsertTablet, IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerDecideTeamRespawnQueue, IEventHandlerPlayerTriggerTesla,
		IEventHandlerDoorAccess, IEventHandlerPlayerDie, IEventHandlerPlayerJoin
	{



		private readonly ATTG3Plugin plugin;
		public INFECT(ATTG3Plugin plugin) => this.plugin = plugin;

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.INFECT)
			{
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team != Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.CLASSD, true, true, true, true);
						player.PersonalBroadcast(10, "ESCAPE SCP-049-2", false);
						new Task(async () =>
						{
							await Task.Delay(500);
							player.GiveItem(ItemType.JANITOR_KEYCARD);
						}).Start();
					}
					if (player.TeamRole.Team == Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.SCP_049_2, true, true, true, true);
						player.PersonalBroadcast(10, "STOP CLASS-D FROM ESCAPING", false);
					}
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (plugin.INFECT)
			{

			}
		}

		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.INFECT)
			{
				
			}
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (plugin.INFECT)
			{


			}
		}
		public void OnPlayerJoin(Smod2.Events.PlayerJoinEvent ev)
		{
			if (plugin.INFECT)
			{
				ev.Player.ChangeRole(Role.SCP_049_2, true, true, false, true);
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.INFECT)
			{ 
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
			}
		}
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (plugin.INFECT)
			{

			}
		}
		public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
		{
			if (plugin.INFECT)
			{
				ev.SpawnRagdoll = false;
				ev.Player.ChangeRole(Role.SCP_049_2, true, true, false, true);
			}
		}
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (plugin.INFECT)
			{

			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.INFECT)
			{
				plugin.INFECT = false;
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.INFECT)
			{
				
			}
		}
		public void OnLure(PlayerLureEvent ev)
		{
			if (plugin.INFECT)
			{

			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.INFECT)
			{

			}
		}
		public void OnElevatorUse(PlayerElevatorUseEvent ev)
		{
			if (plugin.INFECT)
			{
			
			}
		}
		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (plugin.INFECT)
			{

			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (plugin.INFECT)
			{

			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{

		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.INFECT)
			{
				ev.AllowSummon = false;
			}
		}
		public void OnDecideTeamRespawnQueue(DecideRespawnQueueEvent ev)
		{
			if (plugin.INFECT)
			{
				
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (plugin.INFECT)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
				{
					ev.Triggerable = false;
				}
			}
		}
	}
}


