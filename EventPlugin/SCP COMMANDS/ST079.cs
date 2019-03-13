using Smod2.API;
using Smod2.Commands;
using System.Linq;
namespace ATTG3
{
    class ST079 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public ST079(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Command Disabled";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "Command Disabled";
        }
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.SCPrank.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }

            /* = !running;
			foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
			{
				
				Generator.TimeLeft = 10;

			}*/
            return new[]
            {
                $"COMMAND DISABLED"
            };

        }


    }
}
