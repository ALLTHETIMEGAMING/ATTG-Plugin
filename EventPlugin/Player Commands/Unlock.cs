﻿using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class Unlock : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Unlock(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Lets player Unlock doors";
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
                        plugin.ULockdownact=!plugin.ULockdownact;
                        if (plugin.ULockdownact)
                        {
                            plugin.PlayerUD=myPlayer.Name;
                            return new string[] { myPlayer.Name+" Unlockdown Actavated" };
                        }
                        else
                        {
                            return new string[] { myPlayer.Name+" Unlockdown Deactavated" };
                        }
                    }
                    else
                        return new string[] { myPlayer.Name+" is dead!" };
                }
                else
                {
                    return new string[] { "AGULOCK: "+GetUsage() };
                }
            }
        }
    }
}
