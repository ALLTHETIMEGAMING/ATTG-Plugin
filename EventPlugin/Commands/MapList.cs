using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;
namespace ATTG3
{
    class MapList : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        private bool running;
        public MapList(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Disables 106 Containment";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "Disables 106 Containment";
        }
        public static readonly string[] CA = new string[] { "AGC106", "C106" };

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (sender is Player player1)
            {
                GameObject val = GameObject.Find("Host");
                int num = -1;
                if (val != null)
                {
                    num = val.GetComponent<RandomSeedSync>().seed;
                }

                string seed = num.ToString() + ":" +player1.Name + Environment.NewLine;
                Events.CheckMap(seed);
            }
            return new[]
            {
                $"Added Map to Check File."
            };
        }
    }
}
