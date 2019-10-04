using Smod2.API;
using Smod2.Commands;
using System.Linq;
namespace ATTG3
{
    class HelpRequest : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public HelpRequest(ATTG3Plugin plugin)
        {
            this.plugin = plugin;
        }
        public string GetCommandDescription()
        {
            return "";
        }
        public string GetUsage()
        {
            return "";
        }
        public static readonly string[] CA = new string[] { "TEMP", "" };

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (PlayerConsole.StaffCall.Count == 0)
            {
                return new[] { "There are no staff requests at the moment" };
            }
            else if (PlayerConsole.StaffCall.Count > 0)
            {
                foreach(Player players in PlayerConsole.StaffCall.Keys)
                {

                }
            }
            
            return new[] { "Template" };
            
        }
    }
}
