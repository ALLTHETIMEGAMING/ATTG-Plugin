﻿using Smod2.Commands;
using Smod2.API;

namespace ATTG3
{
    class JuggernautCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "";
        }

        public string GetUsage()
        {
            return "Juggernaut Enabled : " + ATTG3Plugin.enabled2 + "\n" +
                "[JUGGERNAUT / JUGG / JUG] HELP \n" +
                "JUGGERNAUT ENABLE \n" +
                "JUGGERNAUT DISABLE \n" +
                "JUGGERNAUT SELECT [PlayerName]";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower() == "help")
                {
                    return new string[] {
                        "Juggernaut Command List \n" +
                        "juggernaut enable - Enable Juggernaut for the next round. \n" +
                        "juggernaut disable - Disable Juggernaut this and following rounds. \n" +
                        "juggernaut select [PlayerName] - Select player as next juggernaut if possible."
                    };
                }
                else if (args[0].ToLower() == "enable") { ATTG3Plugin.JugEnableGamemode(); return new string[] { "Juggernaut will be enabled next round!" }; }
                else if (args[0].ToLower() == "disable") { ATTG3Plugin.JugDisableGamemode(); return new string[] { "Juggernaut is now disabled." }; }
                else if (args[0].ToLower() == "select")
                {
                    if (args.Length > 1)
                    {
                        if (ATTG3Plugin.enabled2)
                        {
                            Player player = GetPlayerFromString.GetPlayer(args[1]);
                            if (player == null) { return new string[] { "Couldn't get player: " + args[1] }; }
                            Jug.selectedJuggernaut = player;
                            ATTG3Plugin.plugin.Info("" + player.Name + "Chosen as the Juggernaut");
                            return new string[] { "Player selected as the next Juggernaut : " + player.Name };
                        }
                        else
                            return new string[] { "Juggernaut is not enabled for next round!" };
                    }
                    else
                        return new string[] { "JUGGERNAUT SELECT [PlayerName]" };
                }
                else
                    return new string[] { GetUsage() };
            }
            else
                return new string[] { GetUsage() };
        }
    }
}
