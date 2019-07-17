using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
	internal class INFECTCon : IEventHandlerRoundStart,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSummonVehicle,
		IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerPlayerHurt,
		IEventHandlerSetRole, IEventHandlerMedkitUse, IEventHandlerPlayerTriggerTesla, IEventHandlerDoorAccess
	{
		private readonly ATTG3Plugin plugin;
		public INFECTCon(ATTG3Plugin plugin) => this.plugin = plugin;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.Infectcontain)
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
				}
				Timing.RunCoroutine(Events.SpawnDelayEvcent("infectcon"));
				foreach (Smod2.API.Item item in PluginManager.Manager.Server.Map.GetItems(Smod2.API.ItemType.WEAPON_MANAGER_TABLET, true))
				{
					Vector itemspawn = item.GetPosition();
					PluginManager.Manager.Server.Map.SpawnItem(Smod2.API.ItemType.MEDKIT, itemspawn, null);
				}
			}
		}
		public void OnPlayerJoin(Smod2.Events.PlayerJoinEvent ev)
		{
			if (plugin.Infectcontain && plugin.RoundStarted)
			{
				ev.Player.ChangeRole(Role.SCP_049_2, true, true, true, true);
				ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_049), true);
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.Infectcontain)
			{
				ev.Allow = false;
				ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
			}
		}
		public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
		{
			if (plugin.Infectcontain)
			{
				if (ev.Killer.TeamRole.Role == Role.SCP_049_2 || ev.DamageTypeVar == DamageType.SCP_049_2)
				{
					if (EventLStorageList.InfecPlayer.Contains(ev.Player.SteamId) == true)
					{
						EventLStorageList.InfecPlayer.Remove(ev.Player.SteamId);
					}
					ev.Player.PersonalBroadcast(10, "You will respawn in 30 seconds", false);
					ev.SpawnRagdoll = false;
					Timing.RunCoroutine(Events.RespawnSpawn(ev.Player, "infectcon"));
				}
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.Infectcontain)
			{
				plugin.Infectcontain = false;
				EventLStorageList.InfecPlayer.Clear();
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.Infectcontain)
			{
				ev.AllowSummon = false;
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (plugin.Infectcontain)
			{
				ev.Triggerable = false;
			}
		}
		public void OnCheckEscape(Smod2.Events.PlayerCheckEscapeEvent ev)
		{
			if (plugin.Infectcontain)
			{
				if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP)
				{
					ev.AllowEscape = true;
					ev.ChangeRole = Role.SCP_049_2;
				}
			}
		}
		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (plugin.Infectcontain)
			{

			}
		}
		public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
		{
			if (plugin.Infectcontain)
			{
				if (ev.Attacker.TeamRole.Team == Smod2.API.Team.SCP && ev.Player.TeamRole.Role != Role.TUTORIAL)
				{
					if (EventLStorageList.InfecPlayer.Contains(ev.Player.SteamId) == false)
					{
						EventLStorageList.InfecPlayer.Add(ev.Player.SteamId);
						Timing.RunCoroutine(Events.Playerhit(ev.Player));
					}
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (plugin.Infectcontain)
			{
				if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId))
				{
					Timing.RunCoroutine(Events.CustomitemDoor(ev.Door, ev.Player.GetCurrentItem().ItemType, ev.Player));
				}
			}
		}
		public void OnMedkitUse(Smod2.Events.PlayerMedkitUseEvent ev)
		{
			if (plugin.Infectcontain)
			{
				if (EventLStorageList.InfecPlayer.Contains(ev.Player.SteamId) == true)
				{
					EventLStorageList.InfecPlayer.Remove(ev.Player.SteamId);
				}
			}
		}
	}
}


