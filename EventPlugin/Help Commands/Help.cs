﻿using Smod2.Commands;
namespace ATTG3
{
    class Help : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public Help(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Prints Help command";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGHELP";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            return new[]
            {
                "ATTG COMMAND PLUGIN COMMANDS",
                "-------------MAP COMMANDS-------------",
                "(AGTL) Turns on all Tleslas",
                "(AGEL) Toggles Elevator locking",
                "(AGTLR) Toggles Tlesla Gates",
                "(AGELR) (#) changes Elevator speed",
				"(AGLIGHT) (#) Turns on / Off all lights in LCZ/HCZ",
                "-------------SCP COMMANDS-------------",
                "(AGP079) Opens SCP-079 Generators",
                "(AGE079) Ejects all tablets from generators",
                "(AGL079) Levels up 079 and gives AP",
                "(AG079T) (#) Sets SCP-079 generators startup time",
                "(AGSPEED) (Player) (#) Sets 939 player movement speed",
                "(AGSPEEDA) (#) Sets all 939s speed", 
				"-------------VOTE COMMANDS-------------",
                "(AGVOTET) Toggles Voting",
                "(AGVOTEC) Clears Vote",
                "(AGVOTES) Displays Vote",
                "(AGVOTEBC) Displays how to vote",
				"-------------HELP COMMANDS-------------",
				"(AGHELP) This command",
				"(AGIH) Prints All Items in SCP:SL",
				"(AGTH) Prints All Teams in SCP:SL",
				"-------------OTHER COMMANDS-------------",
                "(AGAMMO) (Players name) Gives player 100,000 Ammo",
                "(AGLOCK) (Players name) Lets Player Lock Doors",
                "(AGULOCK) (Players name) Lets Player Unlock Doors",
                "(AGDISABLE) Disables ATTG Command Plugin",
                "(AGSHAKE) Shakes all players screens",
				"(AGLIGHTS) Turns off all lights in HCZ / LCZ",
				"(AGGEND) Disables all Generators",
                "(AG079EVENT) Starts 079 Event",
                "(AGFAKE) (Player Name) Fakes a players death",
                "(AGWORK) (Player Name) Spawns a Work Station under player",
                "(AGBLAST) Closes the Nuke blast doors",
                "----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
