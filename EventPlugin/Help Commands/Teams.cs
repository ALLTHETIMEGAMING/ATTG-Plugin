using Smod2.Commands;
namespace ATTG3
{
    class Teams : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public Teams(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Prints All Teams in SCP:SL";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGTH";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            return new[]
            {
                "ALL TEAMS IN SCPSL",
                "-------------TEAMS-------------",
				"ID       Name                    Description",
				"0       | SCP                   | All SCPs",
				"1       | NINETAILFOX           | All MTF",
				"2       | CHAOS_INSURGENCY      | Chaos insurgency",
				"3       | SCIENTIST             | Scientiest",
				"4       | CLASSD                | Class-D",
				"5       | SPECTATOR             | Spectators",
				"6       | TUTORIAL              | TUTORIAL",
				"----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
