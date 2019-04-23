using Smod2.Commands;
using Smod2;
using Smod2.API;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using ItemManager;
using ItemManager.Events;
using RemoteAdmin;
using scp4aiur;
namespace ATTG3
{
    class POSDoor : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
		
		private float Desplay2 = 5f;
        public Door door;
        public Door doorid;
        public GameObject door2;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;

		public Scp939PlayerScript PlayerScript { get; private set; }

		public POSDoor(ATTG3Plugin plugin) => this.plugin = plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server) &&
                sender is Player player &&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }


			float converted = float.Parse(args[0]);
			Desplay2 = converted;

			
                door2 = door.GetComponent<NetworkIdentity>();
            
                door2.SetLocalPos =
            


                return new string[] { "Door Moved" };
            
                
            
			
		}
	}
}
