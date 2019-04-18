using scp4aiur;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ATTG3
{
    class Speed : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public Player myPlayer;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public float converted;
        public Scp939PlayerScript PlayerScript { get; private set; }
        public Speed(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Makes a player that is SCP-939 fast";
        public static readonly string[] CA = new string[] { "SPEED", "AGSPEED" };
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
                return new string[] { "The server is empty!" };
            Player caller = (sender is Player send) ? send : null;
            if (args.Length>0)
            {
                myPlayer=GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                if (myPlayer.TeamRole.Role==Role.SCP_939_53||myPlayer.TeamRole.Role==Role.SCP_939_89)
                {
                    string args2 = args[1].ToLower();
                    if (args2=="reset")
                    {
                        plugin.Running939P=false;
                        return new string[] { myPlayer+" speed reset" };
                    }
                    else if (args2=="set")
                    {
                        converted=float.Parse(args[2]);
                        plugin.Running939P=true;
                        Timing.Run(TimingDelay(0.1f));
                        return new string[] { myPlayer+"given Super speed! at "+converted+" Speed" };
                    }
                    else
                    {
                        return new[]
                        {
                        CA.First() + "Help" + "Shows this",
                        CA.First() + "Reset" + "Resets all 939s speed.",
                        CA.First() + "Set" + "Sets all 939s speed.",
                        };
                    }
                }
                else
                {
                    return new string[] { myPlayer.Name+" is not scp 939" };
                }
            }
            else
            {
                return new[]
                {
                CA.First() + "Help" + "Shows this",
                CA.First() + "Reset" + "Resets all 939s speed.",
                CA.First() + "Set" + "Sets all 939s speed.",
                };
            }
        }
        private IEnumerable<float> TimingDelay(float time)
        {
            while (plugin.Running939P)
            {
                GameObject playerObj = (GameObject)myPlayer.GetGameObject();
                PlayerScript=playerObj.GetComponent<Scp939PlayerScript>();
                PlayerScript.NetworkspeedMultiplier=converted;
                yield return 1f;
            }
        }
    }
}
