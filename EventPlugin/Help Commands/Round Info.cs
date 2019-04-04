using Smod2.Commands;
namespace ATTG3
{
    class RS : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public RS(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGRS";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            return new[]
            {
                
                "-------------Round Stats-------------",
				
				"----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
