using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using MEC;

namespace ATTG3
{
    class GunGameNerf : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public GunGameNerf(ATTG3Plugin plugin) => this.plugin = plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        public static readonly string[] CA = new string[] { "AGR", "" };
        public CharacterClassManager CHAR;
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
                if (Server.GetPlayers().Count < 1)
                    return new string[] { "The server is empty!" };
                Player caller = (sender is Player send) ? send : null;
                if (args.Length > 0)
                {
                    Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
                    if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
                    if (myPlayer.SteamId != "76561198126860363")
                    {
                        if (EventLStorageList.PlayerKillGunGame.ContainsKey(myPlayer.SteamId))
                        {
                            int killcount = int.Parse(args[1]);
                            EventLStorageList.PlayerKillGunGame[myPlayer.SteamId] = killcount;
                            myPlayer.SetRank(null, killcount.ToString() + " kills", null);
                            Timing.RunCoroutine(Events.UpdateItems(myPlayer));
                        }
                    }
                    return new string[] { myPlayer.Name + " kills where set to " + args[1] };
                }
                else
                {
                    return new string[] { "AGSIZE: " + GetUsage() };
                }
            }
        }
    }
}
