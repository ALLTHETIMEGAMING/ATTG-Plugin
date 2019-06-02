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
	internal class INFECT : IEventHandlerRoundStart,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSummonVehicle,
		IEventHandlerPlayerTriggerTesla, IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerCheckEscape, IEventHandlerPlayerHurt
	{



		private readonly ATTG3Plugin plugin;
		public INFECT(ATTG3Plugin plugin) => this.plugin = plugin;

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.INFECT)
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
				
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.INFECT)
			{
				if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP)
				{
					ev.Player.ChangeRole(Role.CLASSD, true, true, true, true);
					ev.Player.PersonalBroadcast(10, "ESCAPE SCP-049-2", false);
					new Task(async () =>
						{
						await Task.Delay(500);
						foreach (Smod2.API.Item item in ev.Player.GetInventory())
						{
							item.Remove();
						}
						ev.Player.GiveItem(ItemType.JANITOR_KEYCARD);
					}).Start();
					ev.Player.AddHealth(100);
				}
				if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
				{
					ev.Player.ChangeRole(Role.SCP_049_2, true, true, true, true);
					ev.Player.PersonalBroadcast(10, "STOP CLASS-D FROM ESCAPING", false);
					ev.Player.AddHealth(400);
				}
			}
		}
		public void OnPlayerJoin(Smod2.Events.PlayerJoinEvent ev)
		{
			if (plugin.INFECT)
			{
				ev.Player.ChangeRole(Role.SCP_049_2, true, true, true, true);
				ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_049), true);
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
		public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
		{
			if (plugin.INFECT)
			{
				ev.Player.PersonalBroadcast(10, "You will respawn in 30 seconds", false);
				ev.SpawnRagdoll = false;
				new Task(async () =>
				{
					await Task.Delay(30000);
					ev.Player.ChangeRole(Role.SCP_049_2, true, true, true, true);
					ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_049), true);
				}).Start();
				
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.INFECT)
			{
				plugin.INFECT = false;
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.INFECT)
			{
				ev.AllowSummon = false;
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
		public void OnCheckEscape(Smod2.Events.PlayerCheckEscapeEvent ev)
		{
			if (plugin.INFECT)
			{
				if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP)
				{
					ev.AllowEscape = true;
					ev.ChangeRole = Role.SCP_049_2;
				}
			}
		}
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.O79Event)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.SCP)
				{
					ev.Player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_049), true);
				}
			}
		}
		public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
		{
			if (plugin.O79Event)
			{
				if (ev.Attacker.TeamRole.Team == Smod2.API.Team.SCP)
				{
					ev.Player.ChangeRole(Role.SCP_049_2, true, false, false, true);
				}
			}
		}
	}
}


