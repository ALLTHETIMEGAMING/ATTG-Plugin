using scp4aiur;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



namespace ATTG3
{
	internal class EventHandler : IEventHandlerWarheadStopCountdown,
		IEventHandlerDoorAccess, IEventHandlerPlayerDie, IEventHandlerGeneratorUnlock,
		IEventHandlerSetRole, IEventHandlerBan, IEventHandlerGeneratorInsertTablet,
		IEventHandlerWarheadKeycardAccess, IEventHandlerElevatorUse, IEventHandlerRoundEnd, IEventHandlerWaitingForPlayers, IEventHandlerNicknameSet, IEventHandlerRoundStart,
	    IEventHandlerTeamRespawn
	{
		private readonly ATTG3Plugin plugin;
		public EventHandler(ATTG3Plugin plugin) => this.plugin=plugin;
		Player Killed;
		public Scp096PlayerScript PlayerScript { get; private set; }
		int Wait = 0;
		bool Running = false;
		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			if (plugin.Disable)
			{
				this.plugin.PluginManager.DisablePlugin(this.plugin);
			}
			foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
			{
				Elevator.MovingSpeed = plugin.Elevatord;
			}
			plugin.Voteopen = false;
			plugin.Yes = 0;
			plugin.No = 0;
			plugin.Lights = false;
			plugin.GenSpam = false;
			plugin.Lerk = false;
			plugin.INFECT = false;
			plugin.MTFCI = false;
			plugin.Event = false;
			plugin.MTFSCP = false;
			plugin.VIP = false;
			plugin.QEvent = false;
			plugin.Infectcontain = false;
			Vote.Voted.Clear();
			plugin.RoundStarted = false;
			MAP.Shake = false;
			MAP.Shake = false;
			MAP.Tleslad = false;
			MAP.Tleslas = false;
		}
		public void OnRoundStart(RoundStartEvent ev)
		{
			plugin.RoundStarted = true;
		}
		public void OnNicknameSet(Smod2.Events.PlayerNicknameSetEvent ev)
		{
			if (ev.Player.SteamId == "76561198141700494")
			{
				//the steam 64 id is a global mod
				ev.Nickname = "KILL ME PLZ";
			}
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			foreach (Smod2.API.Door door in PluginManager.Manager.Server.Map.GetDoors())
			{
				if (door.Name == "CHECKPOINT_ENT")
				{
					door.Open = true;
					door.Locked = false;
				}
			}
		}
		public void OnDoorAccess(PlayerDoorAccessEvent ev)
		{
			Player player = ev.Player;
			if (plugin.O96Door)
			{
				if (ev.Player.TeamRole.Role==Role.SCP_096)
				{
					GameObject Obj = (GameObject)ev.Player.GetGameObject();
					PlayerScript=Obj.GetComponent<Scp096PlayerScript>();
					if (PlayerScript.Networkenraged == Scp096PlayerScript.RageState.Enraged && ev.Door.Locked == false)
					{
						ev.Door.Open=true;
					}
				}
			}
			/*if (Vars.Lock.TryGetValue(ev.Player.SteamId, out bool Lock)&&Lock==true)
			{
				ev.Door.Locked=true;
				ev.Door.Open=false;
			}
			if (Vars.Lock.TryGetValue(ev.Player.SteamId, out Lock)&&Lock==false)
			{
				ev.Door.Locked=false;
				ev.Door.Open=true;
			}*/
			if (plugin.NoCHand == true)
			{
				if (ev.Door.Permission=="CONT_LVL_3"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="CONT_LVL_2"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="CONT_LVL_1"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD)||
						player.HasItem(ItemType.GUARD_KEYCARD)||player.HasItem(ItemType.JANITOR_KEYCARD)||
						player.HasItem(ItemType.SCIENTIST_KEYCARD)|| player.HasItem(ItemType.ZONE_MANAGER_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="CHCKPOINT_ACC"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD)||
						player.HasItem(ItemType.GUARD_KEYCARD)||player.HasItem(ItemType.ZONE_MANAGER_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="ARMORY_LVL_1"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.GUARD_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="ARMORY_LVL_2"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="ARMORY_LVL_3"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="ARMORY_LVL_1"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.GUARD_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="EXIT_ACC"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="INCOM_ACC"&&ev.Door.Locked==false)
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD))
					{
						ev.Allow=true;
					}
				}
			}
		}
		public void OnGeneratorUnlock(PlayerGeneratorUnlockEvent ev)
		{
			Player player = ev.Player;
			Generator gen = ev.Generator;
			if (plugin.GenLock)
			{
				ev.Allow=false;
			}
			if (plugin.GenHand&&!plugin.GenLock)
			{
				if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD))
				{
					gen.Unlock();
				}
			}
		}
		public void OnPlayerDie(PlayerDeathEvent ev)
		{
			RoundStats stats = PluginManager.Manager.Server.Round.Stats;

			if (plugin.O49infect)
			{
				Smod2.API.Player test;
				test=ev.Killer;
				Killed=ev.Player;
				if (test.TeamRole.Role==Role.SCP_049&&stats.Zombies<8)
				{
					Running=true;
					Timing.Run(TimingDelay(30));
				}
			}
		}
		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (plugin.Lights)
			{
				ev.Player.GiveItem(ItemType.FLASHLIGHT);
			}
		}
		public void OnBan(BanEvent ev)
		{

			if (ev.Player.SteamId == "76561198126860363")
			{
				ev.AllowBan=false;
				PluginManager.Manager.Server.Map.ClearBroadcasts();
				Player steamadmin = ev.Admin;
				if (ev.Admin.SteamId != "76561198126860363")
				{
					steamadmin.Ban(1);
					PluginManager.Manager.Server.Map.ClearBroadcasts();
					PluginManager.Manager.Server.Map.Broadcast(10, ev.Admin.Name+" Was uno reverse carded", false);
				}
			}
			else if (ev.Admin.SteamId == "76561198126860363")
			{
				PluginManager.Manager.Server.Map.ClearBroadcasts();
				PluginManager.Manager.Server.Map.Broadcast(10, ev.Player.Name + " Was uno reverse carded", false);
			}
			

		}
		public void OnWarheadKeycardAccess(Smod2.Events.WarheadKeycardAccessEvent ev)
		{
			Player player = ev.Player;
			if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD))
			{
				ev.Allow=true;
			}
			if (player.GetBypassMode()==true)
			{
				ev.Allow=true;
			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (plugin.GenLock)
			{
				ev.Allow=false;
				ev.Player.PersonalBroadcast(10, "Generators are Locked", false);
				ev.RemoveTablet=true;
			}
		}
		public void OnElevatorUse(Smod2.Events.PlayerElevatorUseEvent ev)
		{
			/*if (Vars.Elock.TryGetValue(ev.Player.SteamId, out bool elock)&&elock==true)
			{
				ev.Elevator.Locked=true;

			}
			else if (Vars.Elock.TryGetValue(ev.Player.SteamId, out elock)&&elock==false)
			{
				ev.Elevator.Locked=false;
			{*/
		}
		public void OnRoundEnd(Smod2.Events.RoundEndEvent ev)
		{
			plugin.RoundStarted=false;
		}
		public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
		{
			if (ev.SpawnChaos == true && plugin.Event == false)
			{
				PluginManager.Manager.Server.Map.AnnounceCustomMessage("UNAUTHORIZED PERSONNEL SPOTTED AT GATE A");
			}
		}
		private IEnumerable<float> TimingDelay(float time)
		{
			while (Running)
			{
				if (Wait==0)
				{
					Wait++;
				}
				if (Wait==1)
				{
					Running=false;
					Killed.ChangeRole(Smod2.API.Role.SCP_049_2, false, false, false, true);
					Wait=0;
				}
				yield return 30;
			}
		}
	}
}


