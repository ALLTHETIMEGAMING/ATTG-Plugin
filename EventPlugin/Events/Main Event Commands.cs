﻿using Smod2;
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
                !plugin.Allrank.Contains(player.GetRankName()))
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
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=95><color=#0080FF>MTF</Color> VS <color=#ff0000>SCP</Color> Event Starting</SIZE>", false);
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
					else if (args2 == "fflight")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>FF Event Starting</Color></SIZE>", false);
						FFLight.FFLightEvent = true;
						plugin.Event = true;
						return new string[] { "FF Event started" };
					}
					else if (args2 == "holdout")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>Hold Out Event Starting</Color></SIZE>", false);
						plugin.HoldOutEvent = true;
						plugin.Event = true;
						return new string[] { "Hold Out Event started" };
					}
					else if (args2 == "breach")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FF0000>Breach Event Starting</Color></SIZE>", false);
						Breach.Breachevent = true;
						plugin.Event = true;
						return new string[] { "Breach Event started" };
					}
					else if (args2 == "vip")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FFD700>VIP Event Starting</Color></SIZE>", false);
						plugin.VIP = true;
						plugin.Event = true;
						return new[] { "VIP Event Started" };
					}
					else if (args2 == "gg")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=50><color=#FFD700>Arms Race Event Starting</Color></SIZE>\n<color=#FF0000>NOTE: THIS EVENT IS NOT DONE</color>", false);
						GunGame.GunGameBool = true;
						plugin.Event = true;
						Events.AllSpawns();
						return new[] { "Arms Race Event Started" };
					}
					else if (args2 == "hide")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=50><color=#FFD700>Hide and Seek Event Starting</Color></SIZE>\n<color=#FF0000>NOTE: THIS EVENT IS NOT DONE</color>", false);
						HideandSeek.HideandSeekevent = true;
						plugin.Event = true;
						Events.AllSpawns();
						return new[] { "Hide and Seek Event Started" };
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
						return new[] { "JUG Event Started" };
					}
					else if (args2 == "question")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=75><color=#FFD700>Question Event Starting</Color></SIZE>", false);
						plugin.QEvent = true;
						plugin.Event = true;
						plugin.Info("Question EVENT ACTAVATED");
						plugin.Info("Question" + plugin.QEvent.ToString());
						return new[] { "Question Event Started" };
					}
					else if (args2 == "mtfci")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100><color=#0080FF>MTF</Color> VS <color=#0B7A00>CI</Color> Event Starting</SIZE>", false);
						plugin.MTFCI = true;
						plugin.Event = true;
						return new string[] { "MTF VS CI Event started" };
					}
					else if (args2 == "173army")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100>173 Army Event Starting</SIZE>", false);
						Army173.Army173event = true;
						plugin.Event = true;
						return new string[] { "173 Army Event started" };
					}
					else if (args2 == "holdout")
					{
						PluginManager.Manager.Server.Map.Broadcast(10, "<SIZE=100>173 Army Event Starting</SIZE>", false);
						Army173.Army173event = true;
						plugin.Event = true;
						return new string[] { "173 Army Event started" };
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
					 CA.First() + " FFLIGHT" + " ",
					 CA.First() + " HOLDOUT" + " ",
					 CA.First() + " JUG" + " ",
					 CA.First() + " MTFCI" + " ",
					 CA.First() + " QUESTION" + " ",
					 CA.First() + " HIDE" + " ",
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
					 CA.First() + " FFLIGHT" + " ",
					 CA.First() + " HOLDOUT" + " ",
					 CA.First() + " JUG" + " ",
					 CA.First() + " MTFCI" + " ",
					 CA.First() + " QUESTION" + " ",
					 CA.First() + " HIDE" + " ",
					};
				}
			}
			else
			{

				return new[]
				{
					"Events must be started before the round starts.",
					 CA.First() + " INFECT" + "",
					 CA.First() + " SCPMTF" + " ",
					 CA.First() + " LURK" + " ",
					 CA.First() + " SGOD" + " ",
					 CA.First() + " INFECTCON" + " ",
					 CA.First() + " VIP" + " ",
					 CA.First() + " FFLIGHT" + " ",
					 CA.First() + " HOLDOUT" + " ",
					 CA.First() + " JUG" + " ",
					 CA.First() + " MTFCI" + " ",
					 CA.First() + " QUESTION" + " ",
					 CA.First() + " HIDE" + " ",
				};
			}
        }
    }
}
