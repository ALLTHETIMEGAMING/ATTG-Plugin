﻿using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;

namespace ATTG3
{
    class Locker1 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Locker1(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        //Variables Below
        int count;
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
            if (args.Length>0)
            {
                Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
                {

                    
                    foreach (Locker Lock in Object.FindObjectsOfType<Locker>())
                    {
                        count++;
                        if (count==1)
                        {
                            Vector pos = myPlayer.GetPosition();
                            Lock.NetworklocalPos = new Offset
                            {
                                position=new Vector3(pos.x, pos.y, pos.z),
                                rotation=Vector3.zero,
                                scale=Vector3.one
                            };

                            //Lock.NetworklocalPos;
                        }
                    }
                    count=0;
                }
            }




            return new string[] { "Locker Moved" };

        }
    }
}
