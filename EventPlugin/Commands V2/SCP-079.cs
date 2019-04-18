using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
    class L079 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public L079(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "All SCP-079 Commands";
        public static readonly string[] CA = new string[] { "AG079", "079" };
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
                string args2 = args[1].ToLower();
                if (args2=="reset")
                {
                    
                    return new string[] { " " };
                }
                else if (args2=="elock")
                {

                    return new string[] { " " };
                }
                else if (args2=="close")
                {
                    foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
                    {
                        
                        if (Generator.Open==true)
                        {
                            Generator.Open=false;
                        }
                    }

                    return new string[] { " " };
                }
                else if (args2=="open")
                {
                    foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
                    {
                        Generator.Unlock();
                        if (Generator.Open==false)
                        {
                            Generator.Open=true;
                        }
                    }
                    return new string[] { " " };
                }
                else if (args2=="level")
                {
                    foreach (Player myPlayer in PluginManager.Manager.Server.GetPlayers())
                    {
                        myPlayer.Scp079Data.ShowLevelUp(5);
                        myPlayer.Scp079Data.APPerSecond=1000;
                        myPlayer.Scp079Data.MaxAP=100000;
                        myPlayer.Scp079Data.Level=4;
                        
                    }
                    return new string[] { "ALL SCP-079s are now Level 5" };
                }

                else
                {
                    return new[]
                    {
                        CA.First() + "Help" + " Shows this",
                        CA.First() + "Reset" + " Deactivates everything.",
                        CA.First() + "Lock"  + " Lets player lock doors.",
                        CA.First() + "Open"  + " Opens all of 079s generators",
                        CA.First() + "Close" + " Closes all of 079s generators",
                        CA.First() + "Level"  + " Makes 079 Level 5",
                        };
                }
                
            }
            else
            {
                return new string[] { "AGL079" + GetUsage() };
            }
        }
    }
}
