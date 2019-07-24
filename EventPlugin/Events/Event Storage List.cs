using Smod2.API;
using System.Collections.Generic;

namespace ATTG3
{
    public class EventLStorageList
    {
        private readonly ATTG3Plugin plugin;
        public EventLStorageList(ATTG3Plugin plugin) => this.plugin = plugin;
        public static Dictionary<string, int> PlayerkillsHoldOut = new Dictionary<string, int>();
        public static Dictionary<string, int> PlayerKillGunGame = new Dictionary<string, int>();
        public static Dictionary<string, string> Itemset = new Dictionary<string, string>();
        public static List<string> InfecPlayer = new List<string>();
        public static List<string> MapPosEvents = new List<string>();
        public static List<Vector> LCZPOS = new List<Vector>();
        public static List<Vector> HCZPOS = new List<Vector>();
        public static List<Vector> ECZPOS = new List<Vector>();
        public static List<Vector> GunGameSpawns = new List<Vector>();
        public static Vector Shop1;

    }
}
