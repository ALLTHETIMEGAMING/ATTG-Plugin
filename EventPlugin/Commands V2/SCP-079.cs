using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using scp4aiur;

namespace ATTG3
{
	class SCP079 : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public SCP079(ATTG3Plugin plugin) => this.plugin=plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "All SCP-079 Commands";
		public static readonly string[] CA = new string[] { "AG079", "079" };
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
			
			if (args.Length>0)
			{
				string args2 = args[0].ToLower();
				if (args2=="elock")
				{
					plugin.GenLock=!plugin.GenLock;

					return new[]
					{
				$"Generators {(plugin.GenLock ? "Unlocked" : "Locked")}."
					};
				}
				else if (args2=="close")
				{
					foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
					{
						if (Generator.Open==true)
						{
							Generator.Open=false;
						}
					}
					return new string[] { "Generator Doors closed." };
				}
				else if (args2=="open")
				{
					foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
					{
						Generator.Unlock();
						if (Generator.Open==false)
						{
							Generator.Open=true;
						}
					}
					return new string[] { "Generator Doors opened." };
				}
				else if (args2=="level")
				{
					foreach (Player myPlayer in PluginManager.Manager.Server.GetPlayers())
					{
						if (myPlayer.TeamRole.Role==Role.SCP_079)
						{
							myPlayer.Scp079Data.ShowLevelUp(5);
							myPlayer.Scp079Data.Level=4;
						}
					}
					return new string[] { "ALL SCP-079s are now Level 5" };
				}
				else if (args2=="time")
				{
					if (args.Length>=1)
					{
						float converted = float.Parse(args[1]);

						foreach (Generator079 gen in Generator079.generators)
						{
							gen.NetworkremainingPowerup=(gen.startDuration=converted);
						}

						return new[]
						{
						$"Generator Time set to {converted}"
					};

					}
					else
					{
						return new string[] { "You need to type a number." };
					}
				}
				else if (args2=="eject")
				{
					foreach (Smod2.API.Generator Generator in Smod2.PluginManager.Manager.Server.Map.GetGenerators())
					{
						Generator.HasTablet=false;
						Generator.Open=false;
					}
					return new[]
					{
					$"Generator tablets ejected"
					};
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

		}
	}
}
