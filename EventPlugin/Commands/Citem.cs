using Smod2.Commands;

namespace ATTG3
{
    class Citem : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public Citem(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Enables or Disables custom items";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGCITEM";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            /*if (!(sender is Server) &&
				sender is Player player &&
				!plugin.Customitemrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			if (plugin.Citems)
			{
                plugin.Citems = false;
            }
            else 
            {
                plugin.Citems = true;
            }*/
            return new[]
            {
                $"COMMAND DISABLED"
            };

        }
    }
}
