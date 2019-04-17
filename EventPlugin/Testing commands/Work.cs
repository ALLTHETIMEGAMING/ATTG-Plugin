﻿using Smod2;
using Smod2.API;
using Smod2.Commands;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Smod2.EventHandlers;

namespace ATTG3
{
    class Work : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Work(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        List<GameObject> wipe = new List<GameObject>();
        int Count;
        public static readonly string[] CA = new string[] { "AGWORK", "WORK" };
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
			if (Server.GetPlayers().Count<1)
			{
				return new string[] { "The server is empty!" };
			}
			Player caller = (sender is Player send) ? send : null;
            if (args.Length>0)
            {
                Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
                {

                    Vector pos = myPlayer.GetPosition();
                    GameObject val = GameObject.Instantiate(GameObject.FindObjectOfType<WorkStation>().gameObject);
                        val.GetComponent<WorkStation>().Networkposition=new Offset {position=new Vector3(pos.x, pos.y, pos.z),rotation=Vector3.zero,scale=Vector3.one};
                        NetworkServer.Spawn(val);
                        wipe.Add(val);
                        return new string[] { "Workstation Spawned" };
                    
                }
                else
                    return new string[] { myPlayer.Name+" is dead!" };
            }
            else
            {
                
                foreach (GameObject game in wipe)
                {
                    Count++;
                    NetworkServer.Destroy(game);
                }


                return new string[] { Count + " Work Stations Wiped" };
            }
        }
    }
}
