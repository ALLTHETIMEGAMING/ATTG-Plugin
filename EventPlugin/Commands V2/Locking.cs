using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class Locking : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Locking(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
        //Variables Below
        public static readonly string[] CA = new string[] { "MM", "AGMM" };
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
            if (args.Length>0)
            {
                myPlayer=GetPlayerFromString.GetPlayer(args[0]);
                if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
                if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
                {
                    string args2 = args[0].ToLower();
                    if (args2=="reset")
                    {
						if (Vars.Lock.ContainsKey(myPlayer.SteamId))
						{
							Vars.Lock.Remove(myPlayer.SteamId);
						}
						if (Vars.Elock.ContainsKey(myPlayer.SteamId))
						{
							Vars.Elock.Remove(myPlayer.SteamId);
						}
							
						return new string[] { "Elevator locking and door locking Deactivated." };
                    }
                    else if (args2=="lock")
                    {
						if (Vars.Lock.ContainsKey(myPlayer.SteamId))
						{
							Vars.Lock[myPlayer.SteamId]=true;
						}
						else
						{
							Vars.Lock.Add(myPlayer.SteamId, true);
						}
                        return new string[] { myPlayer.Name+" Door Lock Actavated" };
                    }
                    else if (args2=="unlock")
                    {
						if (Vars.Lock.ContainsKey(myPlayer.SteamId))
						{
							Vars.Lock[myPlayer.SteamId]=false;
						}
						else
						{
							Vars.Lock.Add(myPlayer.SteamId, false);
						}
						return new string[] { myPlayer.Name+" Door Unlock Actavated" };
                    }
                    else if (args2=="elock")
                    {
						if (Vars.Elock.ContainsKey(myPlayer.SteamId))
						{
							Vars.Elock[myPlayer.SteamId]=true;
						}
						else
						{
							Vars.Elock.Add(myPlayer.SteamId, true);
						}
						return new string[] { myPlayer.Name+" Elevator locking Actavated" };
                    }
                    else if (args2=="eunlock")
                    {
						if (Vars.Elock.ContainsKey(myPlayer.SteamId))
						{
							Vars.Elock[myPlayer.SteamId]=false;
						}
						else
						{
							Vars.Elock.Add(myPlayer.SteamId, false);
						}
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
            else
            {
                return new[]
                        {
                        CA.First() + "Help" + " Shows this",
                        CA.First() + "Reset" + " Deactivates everything",
                        CA.First() +  " Player Name" + "Lock"  + " Lets player lock doors.",
                        CA.First() +  " Player Name" + "Unlock"  + " Lets player unlock doors.",
                        CA.First() + " Player Name" + "Elock" + " Lets player lock elevators.",
                        CA.First() + " Player Name" + "Eunlock"  + " Lets player unlock elevators.",
                        };
            }
        }
    }
}
