using Smod2.Commands;
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
				"(AGLIGHT) Turns on / Off all lights in LCZ/HCZ",
                "-------------SCP COMMANDS-------------",
                "(AGP079) Opens SCP-079 Generators",
                "(AGE079) Ejects all tablets from generators",
                "(AGL079) Levels up 079 and gives AP",
				"(AG079T) (#) Sets SCP-079 Generato startup time",
				"-------------VOTE COMMANDS-------------",
                "(AGVOTET) Toggles Voting",
                "(AGVOTEC) Clears Vote",
                "(AGVOTES) Displays Vote",
                "(AGVOTEBC) Displays how to vote",
                "-------------OTHER COMMANDS-------------",
                "(AGHELP) This command",
                "(AGAMMO) (Players name) Gives plaer 100,000",
                "(AGUP) (Players name) Forces player up",
                "(AGDISABLE) Disables ATTG Command Plugin",
                "(AGSHAKE) Shakes all players screens",
				"(AGLIGHTS) Turns off all lights in HCZ / LCZ",
                "----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
