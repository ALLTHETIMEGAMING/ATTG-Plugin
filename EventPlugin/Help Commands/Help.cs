using Smod2.Commands;
namespace ATTG3
{
    class Help : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public Help(ATTG3Plugin plugin)
        {
            this.plugin=plugin;
        }

        public string GetCommandDescription()
        {
            return "Prints Help command";
        }

        public string GetUsage()
        {
            return "AGHELP";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            return new[]
            {
                "ATTG COMMAND PLUGIN COMMANDS",
                "-------------COMMANDS-------------",
                "(AGMAP) All Map Commands",
                "(AGLIGHT) (#) Turns on / Off all lights in LCZ/HCZ",
                "(AG079) ALL SCP-079 Commands",
                "(AGSPEED) (Player) (#) Sets 939 player movement speed",
                "(AGSPEEDA) (#) Sets all 939s speed", 
                "(AGVOTE) All Voting commands",
				"-------------HELP COMMANDS-------------",
				"(AGHELP) This command",
				"(AGIH) Prints All Items in SCP:SL",
				"(AGTH) Prints All Teams in SCP:SL",
				"-------------OTHER COMMANDS-------------",
                "(AGAMMO) (Players name) Gives player 100,000 Ammo",
                "(AGLOCK) (Players name) Lets Player Lock Doors",
                "(AGULOCK) (Players name) Lets Player Unlock Doors",
                "(AGSGOD) Starts SCP God Event",
                "(AGFAKE) (Player Name) Fakes a players death",
                "(AGWORK) (Player Name) Spawns a Work Station under player",
                "(AGPLAY) (Player Name) Spawns Unknown",
                "(AGBLAST) Closes the Nuke blast doors",
                "----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
