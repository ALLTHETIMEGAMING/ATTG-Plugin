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
    class Speed : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
		private object player2;
		public Player myPlayer;
		private bool running;

		Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;

		public Scp939PlayerScript PlayerScript { get; private set; }

		public Speed(ATTG3Plugin plugin) => this.plugin = plugin;
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

            if (Server.GetPlayers().Count < 1)
                return new string[] { "The server is empty!" };

            Player caller = (sender is Player send) ? send : null;


            if (args.Length > 0)
            {
                
                myPlayer = GetPlayerFromString.GetPlayer(args[0]);

				if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
                if (myPlayer.TeamRole.Role == Role.SCP_939_53 || myPlayer.TeamRole.Role == Role.SCP_939_89)
                {
					running = !running;
					if (running)
					{
						Timing.Run(TimingDelay(0.1f));
					}
					

						return new string[] { myPlayer.Name + " has been given Super speed!" };
                }
                else
                    return new string[] { myPlayer.Name + " is not scp 939" };
            }
            else
            {
                return new string[] { GetUsage() };
            }
			
		}
		private IEnumerable<float> TimingDelay(float time)
		{
			while (running)
			{
				GameObject playerObj = (GameObject)myPlayer.GetGameObject();
				PlayerScript = playerObj.GetComponent<Scp939PlayerScript>();
				PlayerScript.NetworkspeedMultiplier = 5;

				yield return 0.5f;
			}
		}
	}
}
