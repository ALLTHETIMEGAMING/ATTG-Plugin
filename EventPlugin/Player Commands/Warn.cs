﻿using Smod2;
using Smod2.API;
using Smod2.Commands;

namespace ATTG3
{
	class Warn : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public Warn(ATTG3Plugin plugin) => this.plugin = plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => " (Player Name) (Reason)";
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (Server.GetPlayers().Count < 1)
				return new string[] { "The server is empty!" };
			Player caller = (sender is Player send) ? send : null;
			if (args.Length > 0)
			{
				string args2;
				int count;
				Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
				if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
				/*foreach (string text in args)
                {
                    if (args[0] != text)
                    {
                        args2 + text;
                    }
                }*/
				myPlayer.PersonalBroadcast(10, "WARNING: " + myPlayer.Name + " " + Events.StringArrayToString(args, 1), false);
				plugin.Discord.CalldiscordString("Warned Player", myPlayer.Name + " " + Events.StringArrayToString(args, 1), "Reason/Player Name");

				return new string[] { "Warned Player: " + myPlayer.Name + " " + Events.StringArrayToString(args, 1) };
			}
			else
			{
				return new string[] { "AGWARN: " + GetUsage() };
			}
		}
	}
}
