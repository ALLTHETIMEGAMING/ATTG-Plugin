using scp4aiur;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ATTG3
{
    internal class O79Handler : IEventHandlerRoundStart, IEventHandlerGeneratorFinish, IEventHandlerTeamRespawn
    {

        int gen;
        private readonly ATTG3Plugin plugin;
        public O79Handler(ATTG3Plugin plugin) => this.plugin=plugin;
        public void OnRoundStart(RoundStartEvent ev)
        {
			if (plugin.O79Event)
			{
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
                    if (player.TeamRole.Team==Smod2.API.Team.SCP)
                    {
                        player.SetGodmode(true);
                    }
                    if (player.TeamRole.Team!=Smod2.API.Team.SCP && player.TeamRole.Team!=Smod2.API.Team.NINETAILFOX)
                    {
                        player.ChangeRole(Role.NTF_LIEUTENANT, false, false, true, true);

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
                    foreach(Player player in PluginManager.Manager.Server.GetPlayers())
                    {
                        if (player.TeamRole.Team==Smod2.API.Team.SCP)
                        {
                            player.SetGodmode(false);
                        }

                    }
                }

            }
        }
        public void OnTeamRespawn(Smod2.EventSystem.Events.TeamRespawnEvent ev)
        {
            if (plugin.O79Event)
            {
                ev.SpawnChaos=false;
            }
        }
    }
}


