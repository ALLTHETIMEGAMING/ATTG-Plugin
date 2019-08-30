﻿using MEC;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace ATTG3
{
    internal class Army939 : IEventHandlerRoundStart,
        IEventHandlerRoundEnd, IEventHandlerWarheadChangeLever, IEventHandlerSummonVehicle,
        IEventHandlerPlayerTriggerTesla, IEventHandlerPlayerDie, IEventHandlerPlayerJoin,
        IEventHandlerDoorAccess, IEventHandlerSetRole, IEventHandlerSpawn
    {
        private readonly ATTG3Plugin plugin;
        public Army939(ATTG3Plugin plugin) => this.plugin = plugin;
        public static bool Army939event;

        public void OnRoundStart(RoundStartEvent ev)
        {
            if (Army939event)
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
                Timing.RunCoroutine(Events.SpawnDelayEvcent("939Army"));
                foreach (Smod2.API.Item item in PluginManager.Manager.Server.Map.GetItems(Smod2.API.ItemType.WEAPON_MANAGER_TABLET, true))
                {
                    Vector itemspawn = item.GetPosition();
                    PluginManager.Manager.Server.Map.SpawnItem(Smod2.API.ItemType.MICROHID, itemspawn, null);
                }
            }
        }
        public void OnSpawn(PlayerSpawnEvent ev)
        {
            if (Army939event)
            {
            }
        }
        public void OnPlayerJoin(Smod2.Events.PlayerJoinEvent ev)
        {
            if (Army939event && plugin.RoundStarted)
            {
				Timing.RunCoroutine(Events.Armydelay(ev.Player, "Army939"));
			}
        }
        public void OnChangeLever(Smod2.Events.WarheadChangeLeverEvent ev)
        {
            if (Army939event)
            {
                ev.Allow = false;
                ev.Player.PersonalBroadcast(10, "Nuke cannot be activated", false);
            }
        }
        public void OnPlayerDie(Smod2.Events.PlayerDeathEvent ev)
        {
            if (Army939event)
            {
                if (ev.Killer.TeamRole.Role == Role.SCP_173)
                {
					Timing.RunCoroutine(Events.Armydelay(ev.Player, "Army939"));
                }
            }
        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (Army939event)
            {
				Army939event = false;
            }
        }
        public void OnSummonVehicle(SummonVehicleEvent ev)
        {
            if (Army939event)
            {
                ev.AllowSummon = false;
            }
        }
        public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
        {
            if (Army939event)
            {
                ev.Triggerable = false;
            }
        }
        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            if (Army939event)
            {

            }
        }
        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (Army939event)
            {
                if (EventLStorageList.Itemset.ContainsKey(ev.Player.SteamId))
                {
                    Timing.RunCoroutine(Events.CustomitemDoor(ev.Door, ev.Player.GetCurrentItem().ItemType, ev.Player));
                }
            }
        }
    }
}
