﻿using scp4aiur;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;

namespace ATTG3
{
    class Gthrow : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Player Gplayer;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Gthrow(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Throws Grenades";
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
            else
            {
                if (Server.GetPlayers().Count<1)
                    return new string[] { "The server is empty!" };
                Player caller = (sender is Player send) ? send : null;
                if (args.Length>0)
                {

                    Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                    if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                    if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
                    {
                        plugin.GenLock=!plugin.GenLock;
                        if (plugin.GenLock)
                        {
                            Timing.Run(TimingDelay(0.1f));
                            Gplayer=myPlayer;
                            return new string[] { myPlayer.Name+" Grenade Throw Actavated" };
                        }
                        else
                        {
                            return new string[] { myPlayer.Name+" Grenade Throw Deactavated" };
                        }
                    }
                    else
                        return new string[] { myPlayer.Name+" is dead!" };
                }
                else
                {
                    return new string[] { "AGGT: "+GetUsage() };
                }
            }
        }
        private IEnumerable<float> TimingDelay(float time)
        {
            while (plugin.GenLock)
            {
                Vector Grot = Gplayer.GetRotation();
                Vector Gpos = Gplayer.GetPosition();
                Gplayer.ThrowGrenade(ItemType.FRAG_GRENADE, false, Grot, false, Gpos, false, 10f, false);
                yield return 3f;
            }
        }
    }
}
