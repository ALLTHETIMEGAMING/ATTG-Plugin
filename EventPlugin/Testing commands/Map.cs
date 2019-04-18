using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class MAP : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public MAP(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        //Variables Below
        public static readonly string[] CA = new string[] { "MAP", "AGMAP" };
        public Player myPlayer;

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
            if (args.Length>1)
            {
                myPlayer=GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
                {
                    string args2 = args[1].ToLower();
                    if (args2=="reset")
                    {
                        plugin.ULockdownact=false;
                        plugin.PlayerUD=null;
                        plugin.Lockdownact=false;
                        plugin.PlayerLD=null;
                        plugin.ELockdownact=false;
                        plugin.EPlayerLD=null;
                        plugin.EULockdownact=false;
                        plugin.EPlayerUD=null;
                        return new string[] { " " };
                    }
                    else if (args2=="lock")
                    {
                        plugin.Lockdownact=true;
                        plugin.ULockdownact=false;
                        plugin.PlayerUD=null;
                        plugin.PlayerLD=myPlayer.SteamId;
                        return new string[] { myPlayer.Name+" Door Lock Actavated" };
                    }
                    else if (args2=="unlock")
                    {
                        plugin.ULockdownact=true;
                        plugin.Lockdownact=false;
                        plugin.PlayerLD=null;
                        plugin.PlayerUD=myPlayer.SteamId;
                        return new string[] { myPlayer.Name+" Door Unlock Actavated" };
                    }
                    else if (args2=="elock")
                    {
                        plugin.ELockdownact=true;
                        plugin.EULockdownact=false;
                        plugin.EPlayerUD=null;
                        plugin.EPlayerUD=myPlayer.SteamId;
                        return new string[] { myPlayer.Name+" Elevator locking Actavated" };
                    }
                    else if (args2=="eunlock")
                    {
                        plugin.EULockdownact=true;
                        plugin.ELockdownact=false;
                        plugin.EPlayerLD=null;
                        plugin.EPlayerUD=myPlayer.SteamId;
                        return new string[] { myPlayer.Name+" Elevator Unlocking Actavated" };
                    }
                    
                    else
                    {
                        return new[]
                        {
                        CA.First() + "Help" + " Shows this",
                        CA.First() +  " Player Name" + "Reset" + " Deactivates everything.",
                        CA.First() +  " Player Name" + "Lock"  + " Lets player lock doors.",
                        CA.First() +  " Player Name" + "Unlock"  + " Lets player unlock doors.",
                        CA.First() + " Player Name" + "Elock" + " Lets player lock elevators.",
                        CA.First() + " Player Name" + "Eunlock"  + " Lets player unlock elevators.",
                        };
                    }
                }
                else
                {
                    return new string[] { myPlayer.Name+" is dead." };
                }
            }
            //if (args.Length==1){ }
            else
            {
                return new[]
                        {
                        CA.First() + "Help" + " Shows this",
                        CA.First() + "Reset" + " Deactivates everything for all players.",
                        CA.First() +  " Player Name" + "Lock"  + " Lets player lock doors.",
                        CA.First() +  " Player Name" + "Unlock"  + " Lets player unlock doors.",
                        CA.First() + " Player Name" + "Elock" + " Lets player lock elevators.",
                        CA.First() + " Player Name" + "Eunlock"  + " Lets player unlock elevators.",
                        };
            }
        }
    }
}
