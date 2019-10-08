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
				"------------- WHITELIST ONLY COMMANDS-------------",
                "(AGMAP) All Map Commands",
                "(AGLIGHT) (#) Turns on / Off all lights in LCZ/HCZ",
                "(AG079) ALL SCP-079 Commands",
                "(AGSPEED) (Player) (#) Sets 939 player movement speed",
                "(AGSPEEDA) (#) Sets all 939s speed", 
                "(AGVOTE) All Voting commands",
				"(AGAMMO) (Players name) Gives player 100,000 Ammo",
				"(EVENT) Shows Event Menu",
				"(AGFAKE) (Player Name) Fakes a players death",
				"(CONFIG) Changes map seed",
				"(AGWORK) (Player Name) Spawns a Work Station under player",
				"(AGBLAST) Closes the Nuke blast doors",
				"(GETPOS) Adds POS",
				"-------------NO RESTRICTION COMMANDS-------------",
				"(AGHELP) This command",
				"(AGIH) Prints All Items in SCP:SL",
				"(AGTH) Prints All Teams in SCP:SL",
				"(AGWARN) (Player Name) (Reason) Warns Player",
				"(HR) Views All Help Requests from Players",
                "----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
