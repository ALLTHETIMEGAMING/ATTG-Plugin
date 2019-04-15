using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
    class Fakedea : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Fakedea(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        //Variables Below
        List<GameObject> wipe = new List<GameObject>();
        int Count;
        public static readonly string[] CA = new string[] { "AGFAKE", "FAKE" };
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
                Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
                {
                    GameObject player1 = (GameObject)myPlayer.GetGameObject();
                    int role = (int)myPlayer.TeamRole.Role;
                    Class @class = PlayerManager.localPlayer.GetComponent<CharacterClassManager>().klasy[role];
                    GameObject ragdoll = Object.Instantiate(@class.model_ragdoll, player1.transform.position+@class.ragdoll_offset.position, Quaternion.Euler(player1.transform.rotation.eulerAngles+@class.ragdoll_offset.rotation));
                    NetworkServer.Spawn(ragdoll);
                    ragdoll.GetComponent<Ragdoll>().SetOwner(new Ragdoll.Info(myPlayer.PlayerId.ToString(), myPlayer.Name, new PlayerStats.HitInfo(), role, myPlayer.PlayerId));
                    wipe.Add(ragdoll);
                    return new string[] { myPlayer.Name+" Death Faked" };
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
                return new string[] { Count+" Bodys Wiped" };
            }

        }
    }
}
