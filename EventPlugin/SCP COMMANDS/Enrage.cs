using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.Commands;
using Smod2.API;
using scp4aiur;
using Smod2;
using ServerMod2.API;
using ItemManager;
using UnityEngine;
using System.Threading;
using System.Collections;
namespace ATTG3
{
	class Enrage : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		private object player2;
		private bool running;
		public Player myPlayer;

		public Scp096PlayerScript PlayerScript { get; private set; }
		public Enrage(ATTG3Plugin plugin)
		{
			//Constructor passing plugin refrence to this class
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "Ejects all Tablets";
		}

		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "Ejects all Tablets";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.SCPrank.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You  do not have permissions to that command."
				};
			}

			Player caller = (sender is Player send) ? send : null;


			if (args.Length > 0)
			{

				myPlayer = GetPlayerFromString.GetPlayer(args[0]);

				if (myPlayer == null) { return new string[] { "Couldn't get player: " + args[0] }; }
				if (myPlayer.TeamRole.Role == Role.SCP_096)
				{
					running = !running;
					if (running)
					{
						Timing.Run(TimingDelay(0.1f));
					}


					return new string[] { myPlayer.Name + " has been enraged" };
				}
				else
					return new string[] { myPlayer.Name + " is not scp 096" };
			}
			else
			{

				return new string[] { "ERROR can not find player" };
			}
		}
		private IEnumerable<float> TimingDelay(float time)
		{
			while (running)
			{
				GameObject playerObj = (GameObject)myPlayer.GetGameObject();
				PlayerScript = playerObj.GetComponent<Scp096PlayerScript>();
				PlayerScript.Networkenraged = PlayerScript.enraged;
				yield return 0.5f;
			}
		}
	}
}
