﻿using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using scp4aiur;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ATTG3
{
	internal class EventPlayerItems
	{
		private readonly ATTG3Plugin plugin;
		public EventPlayerItems(ATTG3Plugin plugin) => this.plugin = plugin;
        public static Dictionary<string, string> Itemset = new Dictionary<string, string>();
        public static List<string> InfecPlayer = new List<string>();
        public static List<string> MapPosEvents = new List<string>();
        public static List<Vector> LCZPOS = new List<Vector>();
        public static List<Vector> HCZPOS = new List<Vector>();
        public static List<Vector> ECZPOS = new List<Vector>();
    }
}