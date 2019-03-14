using scp4aiur;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ATTG3
{
    class Speed2 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private float Desplay2 = 5f;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Scp939PlayerScript PlayerScript { get; private set; }

        public Speed2(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Gives ALL 939s Super Speed";

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
            float converted = float.Parse(args[0]);
            Desplay2=converted;

            plugin.Running939=!plugin.Running939;
            if (plugin.Running939)
            {
                Timing.Run(TimingDelay(0.1f));
                return new string[] { " All 939s have been given Super speed! at "+Desplay2+"Speed" };
            }
            else
            {
                return new string[] { "939 speed reset" };
            }
        }
        private IEnumerable<float> TimingDelay(float time)
        {
            while (plugin.Running939)
            {
                foreach (Smod2.API.Player myPlayer in Server.GetPlayers())
                {
                    if (myPlayer.TeamRole.Role==Role.SCP_939_53||myPlayer.TeamRole.Role==Role.SCP_939_89)
                    {
                        GameObject playerObj = (GameObject)myPlayer.GetGameObject();
                        PlayerScript=playerObj.GetComponent<Scp939PlayerScript>();
                        PlayerScript.NetworkspeedMultiplier=Desplay2;
                    }
                }
                yield return 0.5f;
            }
        }
    }
}
