using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
	internal class Hostagevent : IEventHandlerRoundStart,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSummonVehicle,
		IEventHandlerPlayerTriggerTesla, IEventHandlerPlayerDie, IEventHandlerSetRole, IEventHandlerSpawn
	{
		private readonly ATTG3Plugin plugin;
		public Hostagevent(ATTG3Plugin plugin) => this.plugin = plugin;
        public static bool Hostage;

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (Hostage)
			{
				foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
				{
					if (door.Name == "106_SECONDARY")
					{
						door.Locked = true;
						door.Open = true;
					}
					else if (door.Name == "106_BOTTOM")
					{
						door.Locked = true;
						door.Open = true;
					}
					else if (door.Name == "106_PRIMARY")
					{
						door.Open = true;
						door.Locked = true;
					}
                    else if (door.Name == "914")
                    {
                        door.Open = false;
                        door.Locked = true;
                    }
                }
                foreach (Player player in PluginManager.Manager.Server.GetPlayers())
                {
                    if (player.TeamRole.Role == Role.CLASSD)
                    {
                        player.ChangeRole(Role.CHAOS_INSURGENCY, true, false, true, true);
                        player.PersonalBroadcast(10, "Stop the MTF from killing the Class-Ds", false);
                    }
                    else if (player.TeamRole.Role == Role.SCIENTIST)
                    {
                        player.ChangeRole(Role.CLASSD);
                        player.PersonalBroadcast(10, "Stop the MTF from killing the Class-Ds", false);
                    }
                    else
                    {
                        player.ChangeRole(Role.NTF_COMMANDER);
                        player.PersonalBroadcast(10, "kill the Class-Ds", false);
                    }
                }
            }
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (Hostage)
			{
                if (ev.Player.TeamRole.Role == Role.CLASSD)
                {
                    ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_173);
                }
                else if (ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
                {
                    ev.SpawnPos = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_173);
                }
            }
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (Hostage)
			{
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
			}
		}
		public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
		{
			if (Hostage)
			{
				
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (Hostage)
			{
                Hostage = false;
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (Hostage)
			{
				ev.AllowSummon = false;
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (Hostage)
            {
				ev.Triggerable = false;
			}
		}
		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (Hostage)
			{

			}
		}
	}
}