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
                "-------------SCP COMMANDS-------------",
                "(AG106D) Toggles recontaining SCP-106",
                "(AGS079) SCP-079s generators time to 10 sec",
                "(AGP079) Opens SCP-079 Generators",
                "(AGE079) Ejects all tablets from generators",
                "(AGL079) Levels up 079 and gives AP",
                "-------------VOTE COMMANDS-------------",
                "(VOTET) Toggles Voting",
                "(VOTEC) Clears Vote",
                "(VOTES) Displays Vote",
                "(VOTEBC) Displays how to vote",
                "-------------OTHER COMMANDS-------------",
                "(AGHELP) This command",
                "(AGAMMO) (Players name)",
                "(AGUP) (Players name)",
                "(AGDISABLE) Disables Event Plugin",
                "(AGSHAKE) Shakes all players screens",
                "----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
