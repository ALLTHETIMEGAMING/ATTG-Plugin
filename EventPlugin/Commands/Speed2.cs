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
    class Speed2 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
		private bool running;
		private float Desplay2 = 5f;

		Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;

		public Scp939PlayerScript PlayerScript { get; private set; }

		public Speed2(ATTG3Plugin plugin) => this.plugin = plugin;
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

			running = !running;
					if (running)
					{
						Timing.Run(TimingDelay(0.1f));
					}
					

						return new string[] { " All 939s have been given Super speed! at " + Desplay2 + "Speed" };
                
            
			
		}
		private IEnumerable<float> TimingDelay(float time)
		{
			while (running)
			{
				foreach (Smod2.API.Player myPlayer in Server.GetPlayers())
				{
					if (myPlayer.TeamRole.Role == Role.SCP_939_53 || myPlayer.TeamRole.Role == Role.SCP_939_89)
					{
						GameObject playerObj = (GameObject)myPlayer.GetGameObject();
						PlayerScript = playerObj.GetComponent<Scp939PlayerScript>();
						PlayerScript.NetworkspeedMultiplier = Desplay2;
					}
				}
				yield return 0.5f;
			}
		}
	}
}
