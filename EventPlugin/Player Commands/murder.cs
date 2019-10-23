using Smod2;
using Smod2.API;
using Smod2.Commands;
using System;
using System.IO;
using System.Linq;
using MEC;

namespace ATTG3
{
    class Murder : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Murder(ATTG3Plugin plugin) => this.plugin=plugin;
        public string GetCommandDescription() => "";
        public string GetUsage() => " adds player to list";
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.Allrank.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"Command PRILIST does not exist "
                };
            }
            if (Server.GetPlayers().Count<1)
                return new string[] { "The server is empty!" };
            Player caller = (sender is Player send) ? send : null;
			if (Mystery.Event)
			{
				if (args.Length > 0)
				{
					Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
					if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
					switch (args[1].ToLower())
					{
						case "det":
							{
								Timing.RunCoroutine(Events.SpawnDet(myPlayer));

								return new string[] { "Player " + myPlayer.Name + " spawned as a Detective." };
							}
						case "murd":
							{
								Timing.RunCoroutine(Events.SpawnMurd(myPlayer));

								return new string[] { "Player " + myPlayer.Name + " spawned as a Murderer." };
							}
						case "civ":
							{
								Timing.RunCoroutine(Events.SpawnCiv(myPlayer));

								return new string[] { "Player " + myPlayer.Name + " spawned as a Civilian." };
							}
						default:
							return new string[] { "Invalid role selected." };
					}
				}
				else
				{
					return new string[] { "Murder: " + GetUsage() };
				}
				}
			else
			{
				return new string[] { "Murder: " + GetUsage() };
			}
        }
    }
}
