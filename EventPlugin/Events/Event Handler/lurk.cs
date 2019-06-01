using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using scp4aiur;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATTG3
{
	internal class lerk : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerLure,
		IEventHandlerGeneratorInsertTablet, IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerDecideTeamRespawnQueue, IEventHandlerPlayerTriggerTesla
	{

		bool nuke;
		int gen;
		int C106;
		bool Nuke;

		private readonly ATTG3Plugin plugin;
		public lerk(ATTG3Plugin plugin) => this.plugin = plugin;

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.Lerk)
			{

				nuke = false;
				gen = 0;
				foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
				{
					
					if (door.Name == "ESCAPE")
					{
						door.Locked = true;
					}
					else if (door.Name == "CHECKPOINT_ENT")
					{
						door.Locked = true;
					}
					plugin.Lights = true;
				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team != Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.CLASSD, true, true, true, true);
						new Task(async () =>
						{
							await Task.Delay(500);
							player.GiveItem(ItemType.FLASHLIGHT);
						}).Start();
						player.PersonalBroadcast(10, "ESCAPE", false);
					}
					if (player.TeamRole.Team == Smod2.API.Team.SCP)
					{
						player.PersonalBroadcast(10, "STOP CLASS-D FROM ESCAPING", false);
						
					}
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (plugin.Lerk)
			{
				if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP)
				{
					if (ev.Door.Name == "CHECKPOINT_ENT")
					{
						float x = 187;
						float y = 994;
						float z = -30;
						Vector teleport = new Vector(x, y, z);
						ev.Player.Teleport(teleport, true);
						ev.Allow = false;
						ev.Door.Open = false;
					}
				}
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.Lerk)
			{

			}
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (plugin.Lerk)
			{


			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.Lerk && !nuke)
			{
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
			}
		}
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (plugin.Lerk)
			{

			}
		}
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (plugin.Lerk)
			{

			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.Lerk)
			{
				plugin.Lerk = false;
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.Lerk)
			{
				ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD);
			}
		}
		public void OnLure(PlayerLureEvent ev)
		{
			if (plugin.Lerk)
			{

			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.Lerk)
			{

			}
		}
		public void OnElevatorUse(PlayerElevatorUseEvent ev)
		{
			if (plugin.Lerk)
			{
				if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP)
				{
					foreach (Player player in PluginManager.Manager.Server.GetPlayers())
					{
						if (player.TeamRole.Team == Smod2.API.Team.SCP)
						{
							player.PersonalBroadcast(10, "A CLASS-D has used a elevator", false);
						}
					}
				}
			}
		}
		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (plugin.Lerk)
			{

			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (plugin.Lerk)
			{

			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{

		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.Lerk)
			{
				ev.AllowSummon = false;
			}
		}
		public void OnDecideTeamRespawnQueue(DecideRespawnQueueEvent ev)
		{
			if (plugin.Lerk)
			{
				
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (plugin.Lerk)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
				{
					ev.Triggerable = false;
				}
			}
		}
	}
}


