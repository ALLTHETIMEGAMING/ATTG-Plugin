using Smod2;
using Smod2.API;
using Smod2.Commands;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Smod2.EventHandlers;

namespace ATTG3
{
    public class Vars
    {
        private readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Vars(ATTG3Plugin plugin) => this.plugin=plugin;
        //Variables Below
        public static Dictionary<string, float> Speed;
        public static Dictionary<string, bool> Lock;
        public static Dictionary<string, bool> Elock;


    }
}
