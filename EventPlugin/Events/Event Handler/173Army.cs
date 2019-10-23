using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace ATTG3
{
    internal class Army173 : IEventHandlerRoundStart,
        IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSummonVehicle,
        IEventHandlerPlayerTriggerTesla, IEventHandlerPlayerDie, IEventHandlerPlayerJoin, IEventHandlerGeneratorEjectTablet,
		IEventHandlerDoorAccess, IEventHandlerSetRole, IEventHandlerSpawn, IEventHandlerGeneratorInsertTablet, IEventHandlerGeneratorFinish
	{
        private readonly ATTG3Plugin plugin;
        public Army173(ATTG3Plugin plugin) => this.plugin = plugin;
        public static bool Army173event;
		public static int gen;
		public static Dictionary<string, float> GenTime = new Dictionary<string, float>();

		public void OnRoundStart(RoundStartEvent ev)
        {
            if (Army173event)
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
                Timing.RunCoroutine(Events.SpawnDelayEvcent("173Army"));
                foreach (Smod2.API.Item item in PluginManager.Manager.Server.Map.GetItems(Smod2.API.ItemType.WEAPON_MANAGER_TABLET, true))
                {
                    Vector itemspawn = item.GetPosition();
                    PluginManager.Manager.Server.Map.SpawnItem(Smod2.API.ItemType.MICROHID, itemspawn, null);
                }
            }
        }
        public void OnSpawn(PlayerSpawnEvent ev)
        {
            if (Army173event)
            {
            }
        }
        public void OnPlayerJoin(Smod2.Events.PlayerJoinEvent ev)
        {
            if (Army173event && plugin.RoundStarted)
            {
				Timing.RunCoroutine(Events.Armydelay(ev.Player, "173army"));
			}
        }
		public void OnGeneratorFinish(GeneratorFinishEvent ev)
		{
			if (Army173event)
			{
				gen++;
				if (gen == 5)
				{
					foreach (Player player in PluginManager.Manager.Server.GetPlayers())
					{
						if (player.TeamRole.Team == Smod2.API.Team.SCP)
						{
							player.ChangeRole(Role.SPECTATOR);
						}
					}
				}
			}
		}
		public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
		{
			if (Army173event)
			{
				plugin.Info(ev.Generator.TimeLeft.ToString() + "for room" + ev.Generator.Room.ToString());
				if ((ev.Player.TeamRole.Role == Role.NTF_COMMANDER || ev.Player.TeamRole.Role == Role.TUTORIAL) && ev.Player.HasItem(ItemType.WEAPON_MANAGER_TABLET))
				{
					ev.Allow = true;
					if (GenTime.TryGetValue(ev.Generator.Room.RoomType.ToString(), out float Indicheck))
					{
						ev.Generator.TimeLeft = GenTime[ev.Generator.Room.RoomType.ToString()];
					}
					PluginManager.Manager.Server.Map.ClearBroadcasts();
					PluginManager.Manager.Server.Map.Broadcast(10, "The Generator in " + ev.Generator.Room.RoomType.ToString() + " is being Activated", false);
				}
				else
				{
					ev.Allow = false;
				}
			}
		}
		public void OnGeneratorEjectTablet(Smod2.Events.PlayerGeneratorEjectTabletEvent ev)
		{
			if (Army173event)
			{
				plugin.Info(ev.Generator.TimeLeft.ToString() + "for room" + ev.Generator.Room.ToString());
				if (ev.Player.TeamRole.Role != Role.CHAOS_INSURGENCY)
				{
					if (GenTime.TryGetValue(ev.Generator.Room.RoomType.ToString(), out float Indicheck))
					{
						GenTime[ev.Generator.Room.RoomType.ToString()] = ev.Generator.TimeLeft;
					}
					else
					{
						GenTime.Add(ev.Generator.Room.RoomType.ToString(), ev.Generator.TimeLeft);
					}
					ev.Allow = true;
					ev.SpawnTablet = true;
				}
				else
				{
					ev.Allow = false;
				}
			}
		}
		public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
        {
            if (Army173event)
            {
                ev.Allow = false;
                ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
            }
        }
        public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
        {
            if (Army173event)
            {
				if (ev.Killer.TeamRole.Team == Smod2.API.Team.SCP)
				{
					Timing.RunCoroutine(Events.Armydelay(ev.Player, "173army"));
                }
            }
        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (Army173event)
            {
                Army173event = false;
				gen = 0;
				GenTime.Clear();
            }
        }
        public void OnSummonVehicle(SummonVehicleEvent ev)
        {
            if (Army173event)
            {
                ev.IsCI = false;
            }
        }
        public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
        {
            if (Army173event)
            {
                ev.Triggerable = false;
            }
        }
        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            if (Army173event)
            {

            }
        }
        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (Army173event)
            {
                if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId))
                {
                    Timing.RunCoroutine(Events.CustomitemDoor(ev.Door, ev.Player.GetCurrentItem().ItemType, ev.Player));
                }
            }
        }
    }
}
