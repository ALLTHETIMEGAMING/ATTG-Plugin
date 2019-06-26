using Smod2.API;
using Smod2.Commands;
using System;
using UnityEngine;
namespace ATTG3
{
    class MapList : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public MapList(ATTG3Plugin plugin)
        {
            this.plugin=plugin;
        }
        public string GetCommandDescription()
        {
            return "Disables 106 Containment";
        }
        public string GetUsage()
        {
            return "Disables 106 Containment";
        }
        public static readonly string[] CA = new string[] { "Mapadd", "addmap" };
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
                string seed = num.ToString()+ Environment.NewLine;
                if (!(ATTG3Plugin.Maplist.Contains(num.ToString())))
                {
                    Events.CheckMap(seed);
                    return new[]
                    {
                    "Map added to List",
                    };
                }
                else
                    return new[] { "Error Code 1 Map is in file" };
            }
            else
                return new[] { "Error Code 2 player not found" };
        }
    }
}
