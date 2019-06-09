using Smod2;
using Smod2.API;
using Smod2.Commands;

using System.Linq;


namespace ATTG3
{
    class EventMainCommand : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public EventMainCommand(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => "";
		//Variables Below
		public static readonly string[] CA = new string[] { "AGEVENT", "EVENT" };

		public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
			if (plugin.RoundStarted == false && plugin.Event == false)
			{


				if (args.Length > 0)
				{
					string args2 = args[0].ToLower();
					if (args2 == "infect")
					{
						
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#FF0000>Infection Event Starting</Color></SIZE>", false);
						plugin.INFECT = true;
						plugin.Event = true;
						return new string[] { "Infect Event started" };
					}
					else if (args2 == "scpmtf")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#0080FF>MTF</Color> VS <color=#ff0000>SCP</Color> Event Starting</SIZE>", false);
						plugin.MTFSCP = true;
						plugin.Event = true;
						return new string[] { "MTF VS SCP Event started" };
					}
					else if (args2 == "lurk")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#3e4144>BLACKOUT</Color> Event Starting</SIZE>", false);
						plugin.Lerk = true;
						plugin.Event = true;
						return new string[] { "Lerk Event started" };
					}
					else if (args2 == "sgod")
					{
						plugin.O79Event = true;
						plugin.Event = true;
						return new string[] { "079 Event started" };
					}
					else if (args2 == "infectcon")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>Infection Containment Event Starting</Color></SIZE>", false);
						plugin.Infectcontain = true;
						plugin.Event = true;
						return new string[] { "Infect Containment Event started" };
					}
					else if (args2 == "vip")
					{

						return new[] { " " };
					}
					else if (args2 == "mtfci")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#0080FF>MTF</Color> VS <color=#0B7A00>CI</Color> Event Starting</SIZE>", false);
						plugin.MTFCI = true;
						plugin.Event = true;
						return new string[] { "MTF VS CI Event started" };
					}
					else
					{
						return new[]
							 {
					 CA.First() + " INFECT" + "",
					 CA.First() + " SCPMTF" + " ",
					 CA.First() + " LURK" + " ",
					 CA.First() + " SGOD" + " ",
					 CA.First() + " INFECTCON" + " ",
					 CA.First() + " VIP" + " ",
					 CA.First() + " MTFCI" + " ",
					};
					}
				}
				else
				{
					return new[]
						 {
					 CA.First() + " INFECT" + "",
					 CA.First() + " SCPMTF" + " ",
					 CA.First() + " LURK" + " ",
					 CA.First() + " SGOD" + " ",
					 CA.First() + " INFECTCON" + " ",
					 CA.First() + " VIP" + " ",
					 CA.First() + " MTFCI" + " ",
					};
				}
			}
            else
            {
                return new string[] { "Events must be started before the round starts." };
            }
        }
    }
}
