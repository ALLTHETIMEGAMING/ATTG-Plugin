using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.Commands;
using Smod2.API;
using scp4aiur;


namespace ATTG3
{
    class CIMTFC : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public float Desplay = 0f;
        public CIMTFC(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Enable CIMTF Event for the next round.";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "CIMTF";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server) &&
                sender is Player player &&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }



            if (args.Length > 0)
            {
                if (args[0].ToLower() == "help")
                {
                    return new string[] {
                        "CIMTF Command List \n" +
                        "CIMTF enable - Enable CIMTF for the next round. \n" +
                        "CIMTF disable - Disable CIMTF for following rounds. \n"
                    };
                }
                else if (args[0].ToLower() == "enable")
                {
                    plugin.CIMTF = true;
                    return new string[]
                    {
                        "CIMTF will be enabled next round!"
                    };
                }
                else if (args[0].ToLower() == "disable")
                {
                    plugin.CIMTF = false;
                    return new string[]
                    {
                        "CIMTF is now disabled."
                    };
                }
                else
                    return new string[]
                    {
                        GetUsage()
                    };
            }
            else
                return new string[]
                {
                    GetUsage()
                };
        }
    }
        
    }

