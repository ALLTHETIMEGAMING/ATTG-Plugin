using Smod2.API;
using Smod2.Commands;
using System.Linq;

namespace ATTG3
{
	class ELEL : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		private bool running;
		public ELEL(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin=plugin;
		}
		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "tlesla";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "tlesla";
		}
        public static readonly string[] CA = new string[] { "AGEL", "EL" };
        public static string[] LAE = new string[] { "lae", "lockallelevators" };
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
			running=!running;
			if (running)
			{
				foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
				{
					if (Elevator.Locked==false)
					{
						Elevator.Locked=true;
					}
				}
			}
			if (!running)
			{
				foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
				{
					if (Elevator.Locked==true)
					{
						Elevator.Locked=false;
					}
				}
			}

            if (args.Length>0)
            {
                string args2 = args[0].ToLower();
                if (args2=="lae")
                {

                    return new string[] { " " };
                }
                else if (args2=="close")
                {

                    return new string[] { " " };
                }
                else if (args2=="open")
                {

                    return new string[] { " " };
                }
                else if (args2=="level")
                {
                   
                    return new string[] { " " };
                }
                else if (args2=="time")
                {
                    
                }
                else if (args2=="eject")
                {
                   
                    return new[]{ " " };
                }
                else
                {
                    return new[]
                    {
                        CA.First() + " Help" + " Shows this",
                        CA.First() + " Lock"  + " Lets player lock doors.",
                        CA.First() + " Open"  + " Opens all of 079s generators",
                        CA.First() + " Close" + " Closes all of 079s generators",
                        CA.First() + " Level"  + " Makes 079 Level 5",
                        CA.First() + " Eject"  + " Ejects all Tablets",
                        CA.First() + " Time" + " Number" + " Sets starting time for generators.",
                    };
                }
            }
            else
            {
                return new[]
                    {
                        CA.First() + " Help" + " Shows this",
                        CA.First() + " Lock"  + " Lets player lock doors.",
                        CA.First() + " Open"  + " Opens all of 079s generators",
                        CA.First() + " Close" + " Closes all of 079s generators",
                        CA.First() + " Level"  + " Makes 079 Level 5",
                        CA.First() + " Eject"  + " Ejects all Tablets",
                        CA.First() + " Time" + " Number" + " Sets starting time for generators.",
                    };
            }
            return new[]
			{
				$"all Elevators are now {(running ? "Locked" : "Unlocked")}."
			};
		}
	}
}
