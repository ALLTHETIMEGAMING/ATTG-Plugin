using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
	internal class ClassD : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerLure,
		IEventHandlerGeneratorInsertTablet
	{

		bool nuke;
		int gen;
		int C106;
		bool Nuke;
		private readonly ATTG3Plugin plugin;
		public ClassD(ATTG3Plugin plugin) => this.plugin=plugin;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.ClassD)
			{
				nuke=false;
				gen=0;
				foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
				{
					if (door.Name=="106_SECONDARY")
					{
						door.Locked=true;
					}
					else if (door.Name=="NUKE_SURFACE")
					{
						door.Locked=true;
					}
					else if (door.Name=="106_PRIMARY")
					{
						door.Locked=true;
					}
					else if (door.Name=="HID")
					{
						door.Locked=true;
					}
				}
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team!=Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.FACILITY_GUARD, true, false, true, true);
						player.SetAmmo(AmmoType.DROPPED_5, 10000);
						player.SetAmmo(AmmoType.DROPPED_7, 10000);
						player.SetAmmo(AmmoType.DROPPED_9, 10000);
						player.PersonalBroadcast(10, "You are a MTF. Your goal is to turn on the Generators.", false);
					}

					if (player.TeamRole.Team==Smod2.API.Team.SCP)
					{
						player.PersonalBroadcast(10, "You are a SCP. Your goal is to Guard the Generators.", false);
						player.AddHealth(10000);

					}
				}
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.ClassD)
			{
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Role==Role.SCP_079&&gen!=5)
					{
						player.Scp079Data.Level=4;
					}
					if (player.TeamRole.Team==Smod2.API.Team.SCP&&gen!=5&&player.TeamRole.Role!=Role.SCP_049_2)
					{
						player.SetGodmode(true);
					}
				}
			}
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (plugin.ClassD)
			{
				gen++;
				if (gen==5)
				{
					foreach (Player player in PluginManager.Manager.Server.GetPlayers())
					{
						if (player.TeamRole.Team==Smod2.API.Team.SCP)
						{
							player.SetGodmode(false);
						}

					}
					foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
					{
						if (door.Name=="106_SECONDARY")
						{
							door.Locked=false;
						}
						else if (door.Name=="NUKE_SURFACE")
						{
							door.Locked=false;
						}
						else if (door.Name=="106_PRIMARY")
						{
							door.Locked=false;
						}
						else if (door.Name=="HID")
						{
							door.Locked=false;
							door.Open=true;
						}
					}
					nuke=true;
				}

			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.ClassD && !nuke)
			{
				ev.Allow=false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated at the moment", false);
			}
		}
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (plugin.ClassD)
			{
				if (ev.Generator.TimeLeft<=30)
				{
					ev.Player.PersonalBroadcast(10, "You can not stop a Generator after the time remaining is less than 30 sec. ", false);
					ev.Allow=false;
				}
			}
		}
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (plugin.ClassD)
			{
				ev.SpawnChaos=false;
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.ClassD)
			{
				gen=0;
				plugin.O79Event=false;
				C106=0;
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.ClassD)
			{
				if (gen==5)
				{
					ev.Player.GiveItem(ItemType.MICROHID);
				}
			}
		}
		public void OnLure(PlayerLureEvent ev)
		{
			if (plugin.ClassD)
			{ 
				C106++;
				if (C106==10)
				{
				ev.AllowContain=true;

				}
				else
				{
					ev.AllowContain=false;
					PluginManager.Manager.Server.Map.Broadcast(10, C106+" OUT OF 10 PEOPLE SACRIFICED TO 106", false);
				}
			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.ClassD)
			{

				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team==Smod2.API.Team.SCP)
					{
						player.PersonalBroadcast(10, "The generator in "+ev.Generator.Room+" is being Activated", false);
					}
				}
			}
		}
		public void OnElevatorUse(PlayerElevatorUseEvent ev)
		{
			if (plugin.ClassD)
			{

				Smod2.API.ElevatorType AccessedEl = ev.Elevator.ElevatorType;
				if (AccessedEl==ElevatorType.GateB||AccessedEl==ElevatorType.GateA&&Nuke!=true)
				{
					ev.AllowUse=false;
				}
			}
		}
		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (plugin.ClassD)
			{
				Nuke=true;
			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (plugin.ClassD)
			{
				Nuke=false;
			}
		}
	}
}


