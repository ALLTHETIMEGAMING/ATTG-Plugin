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
		IEventHandlerGeneratorInsertTablet, IEventHandlerCheckRoundEnd, IEventHandlerSummonVehicle, IEventHandlerDecideTeamRespawnQueue, IEventHandlerPlayerTriggerTesla, IEventHandlerDoorAccess
	{




		private readonly ATTG3Plugin plugin;
		public lerk(ATTG3Plugin plugin) => this.plugin = plugin;

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.Lerk)
			{


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

				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team != Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.CLASSD, true, true, true, true);
						player.PersonalBroadcast(10, "ESCAPE", false);
						new Task(async () =>
						{
							await Task.Delay(500);
							player.GiveItem(ItemType.FLASHLIGHT);
							player.GiveItem(ItemType.JANITOR_KEYCARD);
						}).Start();
					}
					if (player.TeamRole.Role == Smod2.API.Role.SCP_173)
					{
						player.ChangeRole(Role.SCP_939_53, true, true, true, true);
					}
					if (player.TeamRole.Role == Smod2.API.Role.SCP_049)
					{
						player.ChangeRole(Role.SCP_939_53, true, true, true, true);
					}
					if (player.TeamRole.Team == Smod2.API.Team.SCP)
					{
						player.PersonalBroadcast(10, "STOP CLASS-D FROM ESCAPING", false);
					}
				}
				Timing.Run(TimingDelay(20));

				foreach (Generator079 gen in Generator079.generators)
				{
					gen.NetworkremainingPowerup = (gen.startDuration = 30f);
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (plugin.Lerk)
			{
				if (ev.Player.TeamRole.Role == Smod2.API.Role.CLASSD)
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
			if (plugin.Lerk)
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
				if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP)
				{
					if (ev.Player.TeamRole.Role == Smod2.API.Role.CHAOS_INSURGENCY)
					{
						new Task(async () =>
						{
							await Task.Delay(500);
							ev.Player.GiveItem(ItemType.FLASHLIGHT);
						}).Start();
						ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD);
					}
				}
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
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team == Smod2.API.Team.SCP)
					{
						player.PersonalBroadcast(10, "The generator in " + ev.Generator.Room.RoomType.ToString() + " is being Activated", false);
					}
				}
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
				ev.IsCI = true;
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
		private IEnumerable<float> TimingDelay(float time)
		{
			while (plugin.Lerk)
			{
				Generator079.generators[0].CallRpcOvercharge();
				foreach (Room room in PluginManager.Manager.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Where(x => x.ZoneType == ZoneType.LCZ).ToArray())

				{
					room.FlickerLights();
				}

				yield return 20;
			}
		}
	}
}


