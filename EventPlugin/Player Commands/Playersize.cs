using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
    class playersize : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public playersize(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Changes players size";
        public static readonly string[] CA = new string[] { "AGTP", "RTP" };
		public CharacterClassManager CHAR;
		public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.Allrank.Contains(player.GetRankName()))
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
                        /*int role = (int)myPlayer.TeamRole.Role;
						Class @class = CHAR.klasy[(role >= 0) ? role : role];
						GameObject playerobj = (GameObject)myPlayer.GetGameObject();
						var playercom = playerobj.GetComponent<CharacterClassManager>();
						playercom.transform.localScale = @class.model_offset.scale*4;
                        */
                        GameObject playerobj = (GameObject)myPlayer.GetGameObject();
                        NetworkServer.UnSpawn(playerobj);
                        var playerobj1 = playerobj;
                        playerobj1.transform.localScale = new Vector3(50, 50, 50);
                        NetworkServer.UnSpawn(playerobj1);

                        return new string[] { myPlayer.Name+" size changed" };
                    }
                    else
                        return new string[] { myPlayer.Name+" is dead!" };
                }
                else
                {
                    return new string[] { "AGSIZE: " + GetUsage() };
                }
            }
        }
    }
}
