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
	internal class INFECTCon : IEventHandlerRoundStart,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSummonVehicle,
		IEventHandlerPlayerTriggerTesla, IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerCheckEscape, IEventHandlerPlayerHurt
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
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team != Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.CLASSD, true, true, true, true);
						player.PersonalBroadcast(10, "ESCAPE SCP-049-2", false);
						new Task(async () =>
						{
							await Task.Delay(500);
							foreach (Smod2.API.Item item in player.GetInventory())
							{
								item.Remove();
							}
							player.GiveItem(ItemType.JANITOR_KEYCARD);
						}).Start();
						player.AddHealth(100);
					}
					if (player.TeamRole.Team == Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.SCP_049_2, true, true, true, true);
						player.PersonalBroadcast(10, "STOP CLASS-D FROM ESCAPING", false);
						player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_049), true);
						player.AddHealth(400);
					}
				}
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.Infectcontain)
			{
				
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
				ev.Player.PersonalBroadcast(10, "You will respawn in 30 seconds", false);
				ev.SpawnRagdoll = false;
				new Task(async () =>
				{
					await Task.Delay(30000);
					ev.Player.ChangeRole(Role.NTF_COMMANDER, true, true, true, true);
				}).Start();
				
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.Infectcontain)
			{
				plugin.Infectcontain = false;
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
				if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					ev.Triggerable = false;
				}
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
		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.Infectcontain)
			{
				
			}
		}
		public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
		{
			if (plugin.Infectcontain)
			{
				if (ev.Attacker.TeamRole.Team == Smod2.API.Team.SCP)
				{
					ev.Player.ChangeRole(Role.SCP_049_2, true, false, false, true);
				}
			}
		}
	}
}


