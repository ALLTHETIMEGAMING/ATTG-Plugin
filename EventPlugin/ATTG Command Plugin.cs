using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ATTG_Web;

namespace ATTG3
{
    [PluginDetails(
        author = "All The Time Gaming",
        description = "COMMAND MOD",
        id = "ATTG.ADMIN.COMMAND",
        name = "ATTG Admin",
        SmodMajor = 3,
        SmodMinor = 5,
        SmodRevision = 0,
        version = "5.0.0"
        )]
    public class ATTG3Plugin : Smod2.Plugin
    {
        public static ATTG3Plugin Instance { get; private set; }
        #region Values
        // Command Perms
        public string[] AdminRanks { get; private set; }
        public string[] Voterank { get; private set; }
        public string[] Allrank { get; private set; }
        public string[] Eventrank { get; private set; }
        // Command Vars
        public bool Voteopen { get; set; }
        public bool SCPPRO { get; set; }
        public bool Disable { get; set; } = false;
        public int Yes { get; set; }
        public int No { get; set; }
        public bool Lights { get; set; }
        public bool GenLock { get; set; }
        public bool O49infect { get; set; }
        public bool O96Door { get; set; }
        public bool NoCHand { get; set; }
        public static int itemid;
        public float Elevatord { get; set; }
        public bool GenSpam { get; set; }
        public bool GenHand { get; set; }
        public bool Running939P { get; set; }
        public bool Running939 { get; set; }
        public bool RoundStarted { get; set; }
        public bool Debuging { get; set; }
        public string[] UNO { get; set; }
        public bool UnoEnabled { get; set; }

        //Events Below
        public bool Event { get; set; }
        public bool Infectcontain { get; set; }
        public bool MTFSCP { get; set; }
        public bool VIP { get; set; }
        public bool O79Event { get; set; }
        public bool ClassD { get; set; }
        public bool Lerk { get; set; }
        public bool VicEvent { get; set; }
        public bool MTFCI { get; set; }
        public bool INFECT { get; set; }
        public bool QEvent { get; set; }
        public bool TestingSpawn { get; set; }
        public bool Jugevent { get; set; }
        public bool HoldOutEvent { get; set; }

        // Custiom Item Bools
        public static bool JanDestroy { get; set; } = true;
        public static bool Jan10Lock { get; set; } = true;
        public static bool Jan30Lock { get; set; } = true;
        #endregion
        public static List<string> Randoimitem = new List<string>();
        public static List<Vector3> TPRooms = new List<Vector3>();
        public static List<Vector3> NoRooms = new List<Vector3>();
        public static List<Vector3> NoRoomTP = new List<Vector3>();
        public static List<string> Maplist = new List<string>();
        public static List<string> Banmemes = new List<string>();
        public static Dictionary<Vector, string> MapCusSpawn = new Dictionary<Vector, string>();
        public static string EventSpawn = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "EventSpawn.txt";
        public static string Mapseeds = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "Mapseeds.txt";
        public static string Prilist = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "PRILIST.txt";
        public static string Nerflist = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "NerfList.txt";
        public static string CustomRank = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "CustomRank.txt";
        public static string WatchList = FileManager.GetAppFolder(shared: true) + "ATTG" + Path.DirectorySeparatorChar + "WatchList.txt";
        public Call_Discord Discord = new Call_Discord(ATTG_Webhook.Instance);
        //End of Events
        public override void Register()
        {
            Instance = this;
            // Configs
            AddConfig(new ConfigSetting("attg_ranks", new[] { "owner", "coowner", "o51", "eventm" }, false, true, ""));
            AddConfig(new ConfigSetting("attg_event_ranks", new[] { "owner", "coowner", "o51", "eventm", "ethic" }, false, true, "Ranks for all Event Commands"));
            AddConfig(new ConfigSetting("attg_vote_ranks", new[] { "owner", "coowner", "admin", "o51", "eventm" }, false, true, "Valid ranks for all voteing Commands"));
            AddConfig(new ConfigSetting("attg_all_ranks", new[] { "owner" }, false, true, "Valid ranks for all Commands"));
            AddConfig(new ConfigSetting("attg_049_infect", false, false, true, "Allows SCP-049 revive instantly"));
            AddConfig(new ConfigSetting("attg_096_door", false, false, true, "Allows SCP-096 able to open all doors when enraged"));
            AddConfig(new ConfigSetting("attg_door_hand", true, false, true, "Allows all players able to open keycard doors with out a keycard in hand"));
            AddConfig(new ConfigSetting("attg_gen_hand", true, false, true, "Allows all players able to open Generators with out a keycard in hand"));
            AddConfig(new ConfigSetting("attg_elevator_speed", 1f, false, true, "Default Elevator speed"));
            AddConfig(new ConfigSetting("attg_scp_pro", true, false, true, "Locks scps doors"));
            AddConfig(new ConfigSetting("attg_uno_user", new[] { "76561198126860363" }, false, true, "User can not be banned"));
            AddConfig(new ConfigSetting("attg_uno", true, false, true, "if uno user can not be banned"));

            ReloadConfig();
            //SCP-079/Genorator Commands
            //this.AddCommands(GenSpam1.CA, new GenSpam1(this));
            this.AddCommands(SCP079.CA, new SCP079(this));
            // lock / TeslaGate Commands 
            this.AddCommands(Locking.CA, new Locking(this));
            this.AddCommands(MAP.CA, new MAP(this));
            //Vote Commands
            this.AddCommands(VoteC.CA, new VoteC(this));
            //Help Commands
            this.AddCommand("AGIH", new ItemH(this));
            this.AddCommand("AGTH", new Teams(this));
            this.AddCommand("AGHELP", new Help(this));
            this.AddCommands(RS.CA, new RS(this));
            //Other
            this.AddCommands(C106.CA, new C106(this));
            this.AddCommands(Up.CA, new Up(this));
            //this.AddCommands(Speed2.CA, new Speed2(this));
            this.AddCommands(Speed.CA, new Speed(this));
            this.AddCommands(Overcharge.CA, new Overcharge(this));
            // EVENTS
            this.AddCommands(EventMainCommand.CA, new EventMainCommand(this));


            //END OF EVENTS
            this.AddCommand("AGFAKE", new Fakedea(this));
            this.AddCommand("AGAMMO", new Ammo(this));
            this.AddCommand("AGBLAST", new Blast(this));
            this.AddCommand("AGLOCKER", new Locker1(this));
            this.AddCommand("AGWORK", new Work(this));
            this.AddCommand("AGGENM", new Genm(this));
            this.AddCommand("AGRANK", new Rank(this));
            this.AddCommand("AGTFF", new TFF(this));
            this.AddCommand("AGTP", new Teleport(this));
            this.AddCommand("AGDEBUG", new DebugATTG(this));
            this.AddCommand("AGDoor", new DoorMain(this));
            this.AddCommand("AGTPR", new TeleportRemove(this));
            this.AddCommand("GETPOS", new GetPos(this));
            this.AddCommand("MAPADD", new MapList(this));
            this.AddCommand("SSAM", new SSAM(this));
            this.AddCommand("Meme", new MEME(this));
            this.AddCommand("AGSIZE", new playersize(this));
            this.AddCommands(Sniper.CA, new Sniper(this));
            this.AddCommands(Config.CA, new Config(this));
            this.AddCommand("AGRAGE", new Rage(this));
            this.AddCommand("GAS", new Gas(this));
            this.AddCommand("AGNUKE", new Nuketime(this));
            this.AddCommand("AGWARN", new Warn(this));
            this.AddCommand("AGSETUP", new Setup(this));
            this.AddCommand("AGREMOVE", new PlayerDis(this));
            this.AddCommand("AGR", new GunGameNerf(this));
            this.AddCommand("SHOP", new Shop(this));
            this.AddCommand("FEED", new Feed(this));
            this.AddCommand("AGBAN", new BanR(this));
            this.AddCommand("PRILIST", new PriList(this));
			this.AddCommand("aghide", new HideSteam(this));
			this.AddCommand("Gun", new GrenadeGun(this));
			this.AddCommand("rain", new Rain(this));
            this.AddCommand("Weapon", new Weppon(this));
			this.AddCommand("Locate", new Locate(this));
            this.AddCommand("Watch", new Watch(this));
            this.AddCommand("intercom", new Incomname(this));
            this.AddCommand("scpcon", new SCPCON(this));

            // Commands added after 8/23/2019
            this.AddCommands(Gate.CA, new Gate(this));
            this.AddCommands(Con106.CA, new Con106(this));
            this.AddCommands(Heli.CA, new Heli(this));
            this.AddCommands(BreachCamp.CA, new BreachCamp(this));
            //Event Handlers
            this.AddEventHandlers(new EventHandler(this), Priority.Highest);
            this.AddEventHandlers(new O79Handler(this), Priority.High);
            this.AddEventHandlers(new Lerk(this), Priority.High);
            this.AddEventHandlers(new MTFCI(this), Priority.High);
            this.AddEventHandlers(new INFECT(this), Priority.High);
            this.AddEventHandlers(new INFECTCon(this), Priority.High);
            this.AddEventHandlers(new SCPMTFEVENT(this), Priority.High);
            this.AddEventHandlers(new VIPESCAPE(this), Priority.High);
            this.AddEventHandlers(new Question(this), Priority.High);
            this.AddEventHandlers(new Jug(this), Priority.High);
            this.AddEventHandlers(new FFLight(this), Priority.High);
            this.AddEventHandlers(new HoldOut(this), Priority.High);
            this.AddEventHandlers(new GunGame(this), Priority.High);
            this.AddEventHandlers(new Breach(this), Priority.High);
			this.AddEventHandlers(new HideandSeek(this), Priority.High);
			this.AddEventHandlers(new Army173(this), Priority.High);
			this.AddEventHandlers(new Army049(this), Priority.High);
			this.AddEventHandlers(new Army939(this), Priority.High);
			this.AddEventHandlers(new TDM(this), Priority.High);
			this.AddEventHandlers(new PlayerConsole(this));
            this.AddEventHandlers(new Hostagevent(this), Priority.High);
			this.AddEventHandlers(new Cap(this), Priority.High);
			this.AddEventHandlers(new Hold(this), Priority.High);
            
		}
        public void ReloadConfig()
        {
            // Command Perms
            AdminRanks = GetConfigList("attg_ranks");
            Voterank = GetConfigList("attg_vote_ranks");
            Eventrank = GetConfigList("attg_event_ranks");
            Allrank = GetConfigList("attg_all_ranks");
            //Dissable Config
            Disable = GetConfigBool("attg_command_disable");
            // Other
            O49infect = GetConfigBool("attg_049_infect");
            O96Door = GetConfigBool("attg_096_door");
            NoCHand = GetConfigBool("attg_door_hand");
            GenHand = GetConfigBool("attg_gen_hand");
            Elevatord = GetConfigFloat("attg_elevator_speed");
            UnoEnabled = GetConfigBool("attg_uno");
            SCPPRO = GetConfigBool("attg_scp_pro");
            UNO = GetConfigList("attg_uno_user");
        }
        public override void OnEnable()
        {
            Info("ATTG Command Plugin enabled.");
            Randoimitem.Add("sniper");
            Randoimitem.Add("heavy");
            Randoimitem.Add("grenade");
            /*if (!File.Exists(EventSpawn))
            {
                using (new StreamWriter(File.Create(EventSpawn))) { }
            }*/
            if (!File.Exists(Mapseeds))
            {
                using (new StreamWriter(File.Create(Mapseeds))) { }
            }
            if (!File.Exists(Prilist))
            {
                using (new StreamWriter(File.Create(Prilist))) { }
            }
            if (!File.Exists(Nerflist))
            {
                using (new StreamWriter(File.Create(Nerflist))) { }
            }
            if (!File.Exists(CustomRank))
            {
                using (new StreamWriter(File.Create(CustomRank))) { }
            }
            if (!File.Exists(WatchList))
            {
                using (new StreamWriter(File.Create(WatchList))) { }
            }
            ATTG3Plugin.Banmemes.Add("Was banned from the server by BattlEye");
            ATTG3Plugin.Banmemes.Add("Was banned from the server by Ubisoft for Hacks");
            ATTG3Plugin.Banmemes.Add("Was REEEEEEEEEEEEEEED from the server ");
            ATTG3Plugin.Banmemes.Add("Was forced to uninstall life.exe");
            ATTG3Plugin.Banmemes.Add("Was uno reverse carded");
            ATTG3Plugin.Banmemes.Add("Was Uninstalled from the server");
            ATTG3Plugin.Banmemes.Add("Was Bamed from the server");
            var Mapfile = File.ReadAllLines(ATTG3Plugin.Mapseeds);
            ATTG3Plugin.Maplist = new List<string>(Mapfile);
            Events.Filesetup();
        }
        public override void OnDisable()
        {
            Info("ATTG Command Plugin disabled.");
        }
    }
}
