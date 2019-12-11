using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
    class PlayerClone : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public PlayerClone(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "Clones players";
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
                        int role = (int)myPlayer.TeamRole.Role;
						Class @class = CHAR.klasy[(role >= 0) ? role : role];
						GameObject playerobj = (GameObject)myPlayer.GetGameObject();
						GameObject val = Object.Instantiate<GameObject>(@class.model_player);
						val.transform.SetParent(playerobj.transform);
						val.transform.localPosition = @class.model_offset.position;
						val.transform.localRotation = Quaternion.Euler(@class.model_offset.rotation);
						val.transform.localScale = @class.model_offset.scale;
						CHAR.myModel = val;
						NetworkServer.Spawn(CHAR.myModel);
						return new string[] { myPlayer.Name+" Cloned" };
                    }
                    else
                        return new string[] { myPlayer.Name+" is dead!" };
                }
                else
                {
                    return new string[] { "AGClone: " + GetUsage() };
                }
            }
        }
    }
}
