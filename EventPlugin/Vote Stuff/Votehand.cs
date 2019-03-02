using Smod2;
using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace ATTG3
{
	class CommandEvent : IEventHandlerCallCommand
	{
		private readonly ATTG3Plugin plugin;

		public CommandEvent(ATTG3Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnCallCommand(PlayerCallCommandEvent ev)
		{
			string command = ev.Command.ToLower();
			if (!command.StartsWith("no") && !command.StartsWith("yes")) return;
			if (plugin.Voteopen != true)
			{
				ev.ReturnMessage = "Voteing is not open";
				return;
			}
			string[] s = command.Split(' ');
			if (s[0] == "no")
			{
					plugin.No++;
					ev.ReturnMessage = "Vote Submitted";
				return;
			}
			else if (s[0] == "yes")
			{
					plugin.Yes++;
				ev.ReturnMessage = "Vote Submitted";
				return;
			}
		}
	}
}
