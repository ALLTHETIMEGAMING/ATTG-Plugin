using Smod2.API;
using Smod2.EventHandlers;
using Smod2.EventSystem.Events;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATTG3
{
    internal class Events : IEventHandlerTeamRespawn, IEventHandlerSetRole, IEventHandlerCheckRoundEnd, IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerPlayerJoin
    {
        private readonly ATTG3Plugin plugin;

        public Events(ATTG3Plugin plugin) => this.plugin = plugin;

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            if (plugin.CIMTF)
            {
                if (!ATTG3Plugin.roundstarted)
                {
                    Server server = plugin.pluginManager.Server;
                    server.Map.ClearBroadcasts();
                    server.Map.Broadcast(25, "<color=00FFFF> CI VS MTF Gamemode</color> is starting..", false);
                }
            }
        }
        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            if (plugin.CIMTF)
            {
                if (ev.Player.TeamRole.Role == Role.CLASSD || ev.Player.TeamRole.Role == Role.SCP_079 ||
                    ev.Player.TeamRole.Role == Role.SCP_939_53 || ev.Player.TeamRole.Role == Role.SCP_939_89)
                {
                    SpawnChaos(ev.Player);
                }
                else if (ev.Player.TeamRole.Role == Role.SCP_096 || ev.Player.TeamRole.Role == Role.SCP_173 ||
                    ev.Player.TeamRole.Role == Role.SCP_049 || ev.Player.TeamRole.Role == Role.FACILITY_GUARD || ev.Player.TeamRole.Role == Role.SCIENTIST)
                {
                    SpawnNTF(ev.Player);
                }

            }

        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            if (plugin.CIMTF)
            {
                ATTG3Plugin.roundstarted = true;
                plugin.Server.Map.DetonateWarhead();
                plugin.pluginManager.Server.Map.ClearBroadcasts();
                plugin.Info("Gangwar Gamemode started!");

                foreach (Player player in ev.Server.GetPlayers())
                {
                    if (player.TeamRole.Role == Role.CLASSD || player.TeamRole.Role == Role.SCP_079 ||
                   player.TeamRole.Role == Role.SCP_939_53 || player.TeamRole.Role == Role.SCP_939_89)
                    {
                        SpawnChaos(player);
                    }
                    else if (player.TeamRole.Role == Role.SCP_096 || player.TeamRole.Role == Role.SCP_173 ||
                        player.TeamRole.Role == Role.SCP_049 || player.TeamRole.Role == Role.FACILITY_GUARD || player.TeamRole.Role == Role.SCIENTIST)
                    {
                        SpawnNTF(player);
                    }
                }
            }
        }

        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (plugin.CIMTF)
            {
                plugin.Info("Round Ended!");
                EndGamemodeRound();
            }
        }
// I HATE EVERYTHIG
        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            if (plugin.CIMTF)
            {
                bool ciAlive = false;
                bool ntfAlive = false;

                foreach (Player player in ev.Server.GetPlayers())
                {
                    if (player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
                    {
                        ciAlive = true; continue;
                    }
                    else if (player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
                    {
                        ntfAlive = true;
                    }
                }
                if (ev.Server.GetPlayers().Count > 1)
                {
                    if (ciAlive && ntfAlive)
                    {
                        ev.Status = ROUND_END_STATUS.ON_GOING;
                        ev.Server.Map.ClearBroadcasts();
                        ev.Server.Map.Broadcast(10, "There are " + plugin.Round.Stats.CiAlive + " Chaos alive, and " + plugin.Round.Stats.NTFAlive + " NTF alive.", false);
                    }
                    else if (ciAlive && ntfAlive == false)
                    {
                        ev.Status = ROUND_END_STATUS.OTHER_VICTORY; EndGamemodeRound();
                    }
                    else if (ciAlive == false && ntfAlive)
                    {
                        ev.Status = ROUND_END_STATUS.MTF_VICTORY; EndGamemodeRound();
                    }
                }
            }
        }

        public void OnTeamRespawn(TeamRespawnEvent ev)
        {
            if (plugin.CIMTF)
            {
                if (plugin.Round.Stats.CiAlive >= plugin.Round.Stats.NTFAlive)
                {
                    ev.SpawnChaos = false;
                }
                else if (plugin.Round.Stats.CiAlive < plugin.Round.Stats.NTFAlive)
                {
                    ev.SpawnChaos = true;
                }
            }
        }

        public void EndGamemodeRound()
        {
            if (plugin.CIMTF)
            {
                plugin.Info("EndgameRound Function.");
                ATTG3Plugin.roundstarted = false;
                plugin.Server.Round.EndRound();
            }
        }

        public void SpawnChaos(Player player)
        {
            player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
            player.SetHealth(plugin.ci_health);
        }

        public void SpawnNTF(Player player)
        {
            player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
            player.SetHealth(plugin.ntf_health);
        }
    }
}
