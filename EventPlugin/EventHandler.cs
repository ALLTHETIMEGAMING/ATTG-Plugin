using scp4aiur;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using UnityEngine;
using Smod2.Attributes;
using ATTG_Command;
using Smod2.EventSystem.Events;

namespace ATTG3
{
	internal class EventHandler : IEventHandlerRoundStart, IEventHandlerWarheadStopCountdown, IEventHandlerDoorAccess, IEventHandlerPlayerDie, IEventHandlerGeneratorUnlock, IEventHandlerSetRole 
    {
		private readonly ATTG3Plugin plugin;
		public EventHandler(ATTG3Plugin plugin) => this.plugin=plugin;
		Player Killed;
		public Scp096PlayerScript PlayerScript { get; private set; }
		int Wait = 0;
		bool Running = false;
		public void OnRoundStart(RoundStartEvent ev)
		{
			if (plugin.Disable)
			{
				this.plugin.pluginManager.DisablePlugin(this.plugin);
			}

            foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
            {
                Elevator.MovingSpeed=plugin.Elevatord;
            }


            plugin.Running939P=false;
			plugin.Running939=false;
			plugin.Voteopen=false;
			plugin.Yes=0;
			plugin.No=0;
			plugin.Lights=false;
		}
		public void OnStopCountdown(WarheadStopEvent ev)
		{
			foreach (Smod2.API.Door door in PluginManager.Manager.Server.Map.GetDoors())
			{
				if (door.Name=="CHECKPOINT_ENT")
				{
					door.Open=true;
					door.Locked=false;
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
					if (PlayerScript.Networkenraged==Scp096PlayerScript.RageState.Enraged)
					{
						ev.Door.Open=true;
					}
				}
			}
			if (plugin.NoCHand)
			{
				if (ev.Door.Permission=="CONT_LVL_3")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="CONT_LVL_2")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="CONT_LVL_1")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD)||
						player.HasItem(ItemType.GUARD_KEYCARD)||player.HasItem(ItemType.JANITOR_KEYCARD)||
						player.HasItem(ItemType.SCIENTIST_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="CHCKPOINT_ACC")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.FACILITY_MANAGER_KEYCARD)||
						player.HasItem(ItemType.CONTAINMENT_ENGINEER_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.MAJOR_SCIENTIST_KEYCARD)||
						player.HasItem(ItemType.GUARD_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="ARMORY_LVL_1")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.GUARD_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="ARMORY_LVL_2")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="ARMORY_LVL_3")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="ARMORY_LVL_1")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD)||
						player.HasItem(ItemType.SENIOR_GUARD_KEYCARD)||player.HasItem(ItemType.GUARD_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="EXIT_ACC")
				{
					if (player.HasItem(ItemType.O5_LEVEL_KEYCARD)||player.HasItem(ItemType.CHAOS_INSURGENCY_DEVICE)||
						player.HasItem(ItemType.MTF_COMMANDER_KEYCARD)||player.HasItem(ItemType.MTF_LIEUTENANT_KEYCARD))
					{
						ev.Allow=true;
					}
				}
				else if (ev.Door.Permission=="INCOM_ACC")
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
                foreach (Generator079 gen2 in Generator079.generators)
                {
                    gen2.NetworkisDoorUnlocked=false;
                    gen2.NetworkisDoorOpen=false;
                }

            }
			if (plugin.NoCHand && !plugin.GenLock)
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
				if (test.TeamRole.Role==Role.SCP_049 && stats.Zombies < 8)
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


