using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
	internal class O79Handler : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet
	{

		bool nuke;
		int gen;
		private readonly ATTG3Plugin plugin;
		public O79Handler(ATTG3Plugin plugin) => this.plugin=plugin;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.O79Event)
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
					if (player.TeamRole.Role==Role.SCP_079)
					{
						player.Scp079Data.Level=4;
					}
					else if (player.TeamRole.Team==Smod2.API.Team.SCP)
					{
						player.SetGodmode(true);
					}
					else if (player.TeamRole.Team!=Smod2.API.Team.SCP&&player.TeamRole.Team!=Smod2.API.Team.NINETAILFOX)
					{
						player.ChangeRole(Role.NTF_LIEUTENANT, true, false, true, true);
					}
				}
			}
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (plugin.O79Event)
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
				}

			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.O79Event&&nuke)
			{
				ev.Allow=false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated at the moment", false);
			}
		}
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (plugin.O79Event)
			{
				ev.Allow=false;
			}
		}
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (plugin.O79Event)
			{
				ev.SpawnChaos=false;
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			gen=0;
			plugin.O79Event=false;
		}
	}
}


