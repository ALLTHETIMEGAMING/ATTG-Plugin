using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;


namespace ATTG3
{
	class EventMainCommand : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		public EventMainCommand(ATTG3Plugin plugin) => this.plugin = plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "";
		public static readonly string[] CA = new string[] { "EVENT", "AGEVENT" };

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.Eventrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}
			else if (plugin.RoundStarted == false && plugin.Event == false)
			{
				string args2 = null;
				if (args.Count() > 0)
				{
					args2 = args[0].ToLower();
				}
				if (args2 == "infect")
				{

					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#FF0000>Infection Event Starting</Color></SIZE>", false);
					plugin.INFECT = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Infect", "Event Name");
					return new string[] { "Infect Event started" };
				}
				else if (args2 == "scpmtf")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=95><color=#0080FF>MTF</Color> VS <color=#ff0000>SCP</Color> Event Starting</SIZE>", false);
					plugin.MTFSCP = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "MTF VS SCP", "Event Name");
					return new string[] { "MTF VS SCP Event started" };
				}
				else if (args2 == "lurk")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#3e4144>BLACKOUT</Color> Event Starting</SIZE>", false);
					plugin.Lerk = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "lurk", "Event Name");
					return new string[] { "Lerk Event started" };
				}
				else if (args2 == "sgod")
				{
					plugin.O79Event = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "sgod", "Event Name");
					return new string[] { "079 Event started" };
				}
				else if (args2 == "infectcon")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>Infection Containment Event Starting</Color></SIZE>", false);
					plugin.Infectcontain = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Infectcon", "Event Name");
					return new string[] { "Infect Containment Event started" };
				}
				else if (args2 == "fflight")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>FF Event Starting</Color></SIZE>", false);
					FFLight.FFLightEvent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "FF Light", "Event Name");
					return new string[] { "FF Event started" };
				}
				else if (args2 == "holdout")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>Hold Out Event Starting</Color></SIZE>", false);
					plugin.HoldOutEvent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Hold Out", "Event Name");
					return new string[] { "Hold Out Event started" };
				}
				else if (args2 == "breach")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>Breach Event Starting</Color></SIZE>", false);
					Breach.Breachevent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Breach", "Event Name");
					return new string[] { "Breach Event started" };
				}
				else if (args2 == "vip")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FFD700>VIP Event Starting</Color></SIZE>", false);
					plugin.VIP = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "VIP", "Event Name");
					return new[] { "VIP Event Started" };
				}
				else if (args2 == "gg")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=50><color=#FFD700>Arms Race Event Starting</Color></SIZE>\n<color=#FF0000>NOTE: THIS EVENT IS NOT DONE</color>", false);
					GunGame.GunGameBool = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Arms Race", "Event Name");
					return new[] { "Arms Race Event Started" };
				}
				else if (args2 == "hide")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=50><color=#FFD700>Hide and Seek Event Starting</Color></SIZE>\n<color=#FF0000>NOTE: THIS EVENT IS NOT DONE</color>", false);
					HideandSeek.HideandSeekevent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Hide and Seek", "Event Name");
					return new[] { "Hide and Seek Event Started" };
				}
				else if (args2 == "team")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=50><color=#FFD700>TEAM Event Starting</Color></SIZE>\n<color=#FF0000>NOTE: THIS EVENT IS NOT DONE</color>", false);
					Team.Teamevent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Team", "Event Name");
					return new[] { "Team Event Started" };
				}
				else if (args2 == "jug")
				{
					if (args.Length > 1)
					{
						Player myPlayer = GetPlayerFromString.GetPlayer(args[1]);
						if (myPlayer != null)
							Jug.VIPplayer = myPlayer;
					}
					else
					{

					}
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FFD700>Juggernaut Event Starting</Color></SIZE>", false);
					plugin.Jugevent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "JUG Event", "Event Name");
					return new[] { "JUG Event Started" };
				}
				else if (args2 == "question")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FFD700>Question Event Starting</Color></SIZE>", false);
					plugin.QEvent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Qustion Event", "Event Name");
					return new[] { "Question Event Started" };
				}
				else if (args2 == "mtfci")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#0080FF>MTF</Color> VS <color=#0B7A00>CI</Color> Event Starting</SIZE>", false);
					plugin.MTFCI = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "MTF VS CI Event", "Event Name");
					return new string[] { "MTF VS CI Event started" };
				}
				else if (args2 == "cap")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75>Control Point Event Starting</SIZE>", false);
					Cap.Holdevent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Cap", "Event Name");
					return new string[] { "Control Point Event started" };
				}
				else if (args2 == "hold")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75>Capure The Flag Event Starting</SIZE>", false);
					Hold.Holdevent = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Hold", "Event Name");
					return new string[] { "Control Point Event started" };
				}
				else if (args2 == "173army")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100>173 Army Event Starting</SIZE>", false);
					Army173.Army173event = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "173Army", "Event Name");
					return new string[] { "173 Army Event started" };
				}
				else if (args2 == "049army")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100>049 Army Event Starting</SIZE>", false);
					Army049.Army049event = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "049Army", "Event Name");
					return new string[] { "049 Army Event started" };
				}
				else if (args2 == "939army")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100>939 Army Event Starting</SIZE>", false);
					Army939.Army939event = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "939 Army", "Event Name");
					return new string[] { "939 Army Event started" };
				}
				else if (args2 == "hostage")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100> Hostage Event Starting</SIZE>", false);
					Hostagevent.Hostage = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Hostage", "Event Name");
					return new string[] { "Hostage Event started" };
				}
				else if (args2 == "tdm")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100> TEAM DEATH MATCH Event Starting</SIZE>", false);
					TDM.Event = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "TDM", "Event Name");
					return new string[] { "TDM Event started" };
				}
				else if (args2 == "murder")
				{
					PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100> <color=#c50000>Murder</color> Event Starting</SIZE>", false);
					Mystery.Event = true;
					plugin.Event = true;
					plugin.Discord.CalldiscordString("Event Started", "Murder", "Event Name");
					return new string[] { "Murder Event started" };
				}

				else
				{
					return new[]
						 {
							"-----CONFIRMED WORKING-----",
							CA.First() + " 173ARMY" + " ",
							CA.First() + " 049ARMY" + " ",
							CA.First() + " 939ARMY" + " ",
							CA.First() + " MTFCI" + " ",
							CA.First() + " BREACH" + " ",
							CA.First() + " LURK" + " ",
							CA.First() + " HIDE" + " ",
							CA.First() + " QUESTION" + " ",
							CA.First() + " SCPMTF" + " ",
							"-----EVENTS IN TESTING-----",
							CA.First() + " TDM" + " ",
							CA.First() + " TEAM" + " ",
							CA.First() + " GG" + " ",
							CA.First() + " MURDER" + " ",
							CA.First() + " VIP" + " ",
							"-----UNKNOWN IF WORKING-----",
							CA.First() + " INFECT" + "",
							CA.First() + " SGOD" + " ",
							CA.First() + " INFECTCON" + " ",
							CA.First() + " FFLIGHT" + " ",
							CA.First() + " HOLDOUT" + " ",
							CA.First() + " JUG" + " ",
							CA.First() + " HOSTAGE" + " ",
						};
				}
			}
			else
			{

				return new[]
						 {
							"-----CONFIRMED WORKING-----",
							CA.First() + " 173ARMY" + " ",
							CA.First() + " 049ARMY" + " ",
							CA.First() + " 939ARMY" + " ",
							CA.First() + " MTFCI" + " ",
							CA.First() + " BREACH" + " ",
							CA.First() + " LURK" + " ",
							CA.First() + " HIDE" + " ",
							CA.First() + " QUESTION" + " ",
							CA.First() + " SCPMTF" + " ",
							"-----EVENTS IN TESTING-----",
							CA.First() + " TDM" + " ",
							CA.First() + " TEAM" + " ",
							CA.First() + " GG" + " ",
							CA.First() + " MURDER" + " ",
							CA.First() + " VIP" + " ",
							"-----UNKNOWN IF WORKING-----",
							CA.First() + " INFECT" + "",
							CA.First() + " SGOD" + " ",
							CA.First() + " INFECTCON" + " ",
							CA.First() + " FFLIGHT" + " ",
							CA.First() + " HOLDOUT" + " ",
							CA.First() + " JUG" + " ",
							CA.First() + " HOSTAGE" + " ",
						};
			}
		}
	}
}
