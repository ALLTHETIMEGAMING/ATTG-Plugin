using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using ServerMod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using UnityEngine.Networking;
using Smod2.Attributes;
using ATTG_Command;
namespace ATTG3
{
    class Car : NetworkBehaviour, ICommandHandler
	{
        private readonly ATTG3Plugin plugin;

        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Car(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Forces player up";
		private static int kRpcRpcVan;
		public string[] OnCall(ICommandSender sender, string[] args)
        {
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.Allrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			else
			{
				kRpcRpcVan = -871850524;
				NetworkWriter val = new NetworkWriter();
				val.Write((short)0);
				val.Write((short)2);
				val.WritePackedUInt32((uint)kRpcRpcVan);
				val.Write(plugin.MTFre.GetComponent<NetworkIdentity>().netId);
				this.SendRPCInternal(val, 2, "RpcVan");
			}
                return new string[] { "Car Called" };
                
            }
        }

    }

