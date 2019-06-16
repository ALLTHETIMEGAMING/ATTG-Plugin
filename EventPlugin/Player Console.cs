﻿using Smod2.API;
using Smod2.Commands;
using Smod2.Events;
using System.Collections.Generic;
using Smod2.EventHandlers;
using MEC;

namespace ATTG3
{
    class PlayerConsole : IEventHandlerCallCommand
    {
        private readonly ATTG3Plugin plugin;
        public PlayerConsole(ATTG3Plugin plugin) => this.plugin=plugin;

        public static List<Player> Voted = new List<Player>();
        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            string command = ev.Command.ToLower();
            if (command.StartsWith("yes"))
            {
                if (plugin.Voteopen&&Voted.Contains(ev.Player)==false)
                {
                    Voted.Add(ev.Player);
                    plugin.Yes++;
                    ev.ReturnMessage="Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage="Voteing is not open";
                }
                return;
            }
            else if (command.StartsWith("no"))
            {
                if (plugin.Voteopen&&Voted.Contains(ev.Player)==false)
                {
                    Voted.Add(ev.Player);
                    plugin.No++;
                    ev.ReturnMessage="Vote Submitted";
                }
                else
                {
                    ev.ReturnMessage="Voteing is not open";
                }
                return;
            }
           /* else if (command.StartsWith("inv1"))
            {
				// This is a Test This will be unlocked to all players if it works.
				if (ev.Player.SteamId == "76561198126860363")
				{
					Events.Inventoryset(ev.Player, 1);
					ev.ReturnMessage = "Changed Held item";
				}
				else
				{
					ev.ReturnMessage = "Command is in testing mode";
				}
			}
			else if (command.StartsWith("inv2"))
			{
				// This is a Test This will be unlocked to all players if it works.
				if (ev.Player.SteamId == "76561198126860363")
				{
					Events.Inventoryset(ev.Player, 2);
					ev.ReturnMessage = "Changed Held item";
				}
				else
				{
					ev.ReturnMessage = "Command is in testing mode";
				}
			}
			else if (command.StartsWith("inv3"))
			{
				// This is a Test This will be unlocked to all players if it works.
				if (ev.Player.SteamId == "76561198126860363")
				{
					Events.Inventoryset(ev.Player, 3);
					ev.ReturnMessage = "Changed Held item";
				}
				else
				{
					ev.ReturnMessage = "Command is in testing mode";
				}
			}
			else if (command.StartsWith("inv4"))
			{
				// This is a Test This will be unlocked to all players if it works.
				if (ev.Player.SteamId == "76561198126860363")
				{
					Events.Inventoryset(ev.Player, 4);
					ev.ReturnMessage = "Changed Held item";
				}
				else
				{
					ev.ReturnMessage = "Command is in testing mode";
				}
			}
			else if (command.StartsWith("inv5"))
			{
				// This is a Test This will be unlocked to all players if it works.
				if (ev.Player.SteamId == "76561198126860363")
				{
					Events.Inventoryset(ev.Player, 5);
					ev.ReturnMessage = "Changed Held item";
				}
				else
				{
					ev.ReturnMessage = "Command is in testing mode";
				}
			}
			else if (command.StartsWith("inv6"))
			{
				// This is a Test This will be unlocked to all players if it works.
				if (ev.Player.SteamId == "76561198126860363")
				{
					Events.Inventoryset(ev.Player, 6);
					ev.ReturnMessage = "Changed Held item";
				}
				else
				{
					ev.ReturnMessage = "Command is in testing mode";
				}
			}
			else if (command.StartsWith("inv7"))
			{
				// This is a Test This will be unlocked to all players if it works.
				if (ev.Player.SteamId == "76561198126860363")
				{
					Events.Inventoryset(ev.Player, 7);
					ev.ReturnMessage = "Changed Held item";
				}
				else
				{
					ev.ReturnMessage = "Command is in testing mode";
				}
			}
			else if (command.StartsWith("inv8"))
			{
				// This is a Test This will be unlocked to all players if it works.
				if (ev.Player.SteamId == "76561198126860363")
				{
					Events.Inventoryset(ev.Player, 8);
					ev.ReturnMessage = "Changed Held item";
				}
				else
				{
					ev.ReturnMessage = "Command is in testing mode";
				}
			}*/
		}
    }
}
