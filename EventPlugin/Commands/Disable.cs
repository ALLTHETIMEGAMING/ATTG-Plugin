using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
	class Disable : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public Disable(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "Disable ATTG Command Plugin";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "Disable ATTG Command Plugin";
		}
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server)&&
				sender is Player player&&
				!plugin.Disablerank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			else
			{
				plugin.Info(sender+" ran the "+GetUsage()+" command!");
				this.plugin.PluginManager.DisablePlugin(this.plugin);
			}
			return new[]
			{
				$"ATTG COMMAND PLUGIN DISABLED"
			};
		}
	}
}
