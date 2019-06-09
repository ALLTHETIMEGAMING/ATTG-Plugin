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
	internal class SCPMTF : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn,
		IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerGeneratorEjectTablet, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerLure,
		IEventHandlerGeneratorInsertTablet, IEventHandlerSummonVehicle, IEventHandlerPlayerTriggerTesla, IEventHandlerDoorAccess, IEventHandlerPlayerHurt
	{



		int C106;
		int gen;
		bool nuke;

		private readonly ATTG3Plugin plugin;
		public SCPMTF(ATTG3Plugin plugin) => this.plugin = plugin;

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.MTFSCP)
			{
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.TeamRole.Team != Smod2.API.Team.SCP)
					{
						player.ChangeRole(Role.NTF_LIEUTENANT, true, true, false, true);
						player.PersonalBroadcast(10, "Kill All SCPs", false);
						new Task(async () =>
						{
							await Task.Delay(500);
							player.SetAmmo(AmmoType.DROPPED_5, 10000);
							player.SetAmmo(AmmoType.DROPPED_7, 10000);
							player.SetAmmo(AmmoType.DROPPED_9, 10000);
						}).Start();
					}
					if (player.TeamRole.Team == Smod2.API.Team.SCP)
					{
						player.PersonalBroadcast(10, "Kill All MTF", false);
						if (player.TeamRole.Role == Role.SCP_049)
						{
							player.AddHealth(20000);
						}
						else if (player.TeamRole.Role == Role.SCP_096)
						{
							player.AddHealth(15000);
						}
						else if (player.TeamRole.Role == Role.SCP_106)
						{
							player.AddHealth(17000);
						}
						else if (player.TeamRole.Role == Role.SCP_173)
						{
							player.AddHealth(70000);
						}
						else if (player.TeamRole.Role == Role.SCP_939_53 || player.TeamRole.Role == Role.SCP_939_89)
						{
							player.AddHealth(30000);
						}
						else if (player.TeamRole.Role == Role.SCP_079)
						{
							player.PersonalClearBroadcasts();
							player.ChangeRole(Role.NTF_LIEUTENANT, true, true, true, true);
						}
					}
					
				}
				foreach (Generator079 gen in Generator079.generators)
				{
					gen.NetworkremainingPowerup = (gen.startDuration = 300f);
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			if (plugin.MTFSCP)
			{ 
			}
		}

		public void OnSetRole(Smod2.Events.PlayerSetRoleEvent ev)
		{
			if (plugin.MTFSCP)
			{
				
			}
		}
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (plugin.MTFSCP)
			{
				gen++;
				if (gen == 5)
				{
					nuke = true;
				}
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (!nuke)
				{
					ev.Allow = false;
					ev.Player.PersonalBroadcast(10, "Nuke cannot be activated untill all generators are on", false);
				}
			}
		}
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (ev.Generator.TimeLeft <= 30)
				{
					ev.Allow = false;
					ev.Player.PersonalBroadcast(10, "You can not stop a Generator after the time remaining is less than 30 sec. ", false);
				}
			}
		}
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (plugin.MTFSCP)
			{

			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (plugin.MTFSCP)
			{
				nuke = false;
				gen = 0;
				plugin.MTFSCP = false;
				C106 = 0;
			}
		}
		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
				{
					new Task(async () =>
					{
						await Task.Delay(500);
						ev.Player.SetAmmo(AmmoType.DROPPED_5, 10000);
						ev.Player.SetAmmo(AmmoType.DROPPED_7, 10000);
						ev.Player.SetAmmo(AmmoType.DROPPED_9, 10000);
					}).Start();
					if (ev.Player.TeamRole.Role == Role.NTF_COMMANDER)
					{
						new Task(async () =>
						{
							await Task.Delay(500);
							ev.Player.GiveItem(ItemType.MICROHID);
						}).Start();
					}
					if (gen == 5 && ev.Player.TeamRole.Role != Role.NTF_COMMANDER)
					{
						new Task(async () =>
						{
							await Task.Delay(500);
							ev.Player.GiveItem(ItemType.MICROHID);
						}).Start();
					}
				}
			}
		}
		public void OnLure(PlayerLureEvent ev)
		{
			if (plugin.MTFSCP)
			{
				C106++;
				if (C106 >= 10)
				{
					ev.AllowContain = true;
				}
				else
				{
					ev.AllowContain = false;
					PluginManager.Manager.Server.Map.Broadcast(10, C106 + " OUT OF 10 PEOPLE SACRIFICED TO 106", false);
				}
			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.MTFSCP)
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

		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (plugin.MTFSCP)
			{

			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (plugin.MTFSCP)
			{

			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (plugin.MTFSCP)
			{
				ev.IsCI = false;
			}
		}
		public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
		{
			if (plugin.MTFSCP)
			{

			}
		}
		public void OnPlayerHurt(Smod2.Events.PlayerHurtEvent ev)
		{
			if (plugin.MTFSCP)
			{
				if (ev.Player.GetHealth().ToString() == "1000" || ev.Player.GetHealth().ToString() == "5000" || ev.Player.GetHealth().ToString() == "10000")
				{
					if (ev.Attacker.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
					{
						ev.Attacker.PersonalClearBroadcasts();
						ev.Attacker.PersonalBroadcast(3, ev.Player.TeamRole.Role.ToString() + " has " + ev.Player.GetHealth().ToString() + " HP Remaining", false);
					}
				}
			}
		}
	}
}


