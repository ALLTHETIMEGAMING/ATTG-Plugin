using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class Feed : ICommandHandler
    {
        public Player myPlayer;
        public static bool Feedbool;
        public static int count;
        private readonly ATTG3Plugin plugin;
        public Feed(ATTG3Plugin plugin)
        {
            this.plugin = plugin;
        }
        public string GetCommandDescription()
        {
            return "";
        }
        public string GetUsage()
        {
            return "";
        }
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
            Feedbool = !Feedbool;
            if (Feedbool)
            {
                foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                {
                    Vector FEEDVEC = Smod2.PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_939_53);
                    if (door.Position.y < -1010)
                    {
                        door.Locked = true;
                        door.Open = false;
                    }
                }
                foreach (Player player1 in PluginManager.Manager.Server.GetPlayers())
                {
                    if (player1.TeamRole.Role == Role.SPECTATOR)
                    {
                        if (count > 2)
                        {
                            player1.ChangeRole(Role.CLASSD);
                            player1.Teleport(Smod2.PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_939_53));
                        }
                        else
                        {
                            player1.ChangeRole(Role.SCP_939_89, true, true, true, true);
                            count++;
                        }
                    }
                }
            }
            else
            {
                foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
                {
                    Vector FEEDVEC = Smod2.PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_939_53);
                    if (door.Position.y < -1010)
                    {
                        door.Locked = false;
                        count = 0;
                    }
                }
            }
            return new[]
            {
                $"Feedint pit {(Feedbool ? "Actavated" : "Deactavated")}"
            };
        }
    }
}