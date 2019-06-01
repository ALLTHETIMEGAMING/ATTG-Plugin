using scp4aiur;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;

namespace ATTG3
{

	[PluginDetails(
		author = "All The Time Gaming",
		description = "COMMAND MOD",
		id = "ATTG.ADMIN.COMMAND",
		name = "ATTG Admin",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0,
		version = "2.0.0"
		)]
	public class ATTG3Plugin : Smod2.Plugin
	{

		public static ATTG3Plugin Instance { get; private set; }
		
        // Command Perms
		public string[] AdminRanks { get; private set; }
		public string[] Voterank { get; private set; }
		public string[] Allrank { get; private set; }
		public string[] Eventrank { get; private set; }
        // Command Vars
		public bool Voteopen { get; set; }
		public bool Disable { get; set; } = false;
		public int Yes { get; set; }
		public int No { get; set; }
		public bool Lights { get; set; }
        public bool GenLock { get; set; }
        public bool O49infect { get; set; }
		public bool O96Door { get; set; }
		public bool NoCHand { get; set; }
		public float Elevatord { get; set; }
		public bool GenSpam { get; set; }
		public bool O79Event { get; set; }
		public bool ClassD { get; set; }
		public bool Lerk { get; set; }
		public bool VicEvent { get; set; }
        public bool GenHand { get; set; }
        public bool Running939P { get; set; }
        public bool Running939 { get; set; }
        public bool RoundStarted { get; set; }
        public override void Register()
		{
			Instance=this;
			Timing.Init(this);
			Timing2.Init(this);
			// Configs
			AddConfig(new ConfigSetting("attg_ranks", new[]{"owner","coowner", "o51" }, false, true, ""));
			AddConfig(new ConfigSetting("attg_event_ranks", new[]{"owner","coowner"}, false, true, "Ranks for all SCP Commands"));
			AddConfig(new ConfigSetting("attg_vote_ranks", new[]{"owner","coowner","admin", "o51"},false, true, "Valid ranks for all voteing Commands"));
			AddConfig(new ConfigSetting("attg_all_ranks", new[]{"owner"}, false, true, "Valid ranks for all Commands"));
			AddConfig(new ConfigSetting("attg_049_infect", false, false, true, "Allows SCP-049 revive instantly"));
			AddConfig(new ConfigSetting("attg_096_door", true, false, true, "Allows SCP-096 able to open all doors when enraged"));
            AddConfig(new ConfigSetting("attg_door_hand", true, false, true, "Allows all players able to open keycard doors with out a keycard in hand"));
            AddConfig(new ConfigSetting("attg_gen_hand", true, false, true, "Allows all players able to open Generators with out a keycard in hand"));
            AddConfig(new ConfigSetting("attg_elevator_speed", 1f, false, true, "Default Elevator speed"));

			ReloadConfig();
            //SCP-079/Genorator Commands
            this.AddCommands(GenSpam1.CA, new GenSpam1(this));
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
            this.AddCommands(Speed2.CA, new Speed2(this));
            this.AddCommands(Speed.CA, new Speed(this));
            this.AddCommands(Overcharge.CA, new Overcharge(this));
            this.AddCommand("AGSGOD", new O79EVENT(this));
			this.AddCommand("AGLURK", new Lurkcom(this));
			this.AddCommand("AGFAKE", new Fakedea(this));
            this.AddCommand("AGAMMO", new Ammo(this));
            this.AddCommand("AGBLAST", new Blast(this));
            this.AddCommand("AGLOCKER", new Locker1(this));
			this.AddCommand("AGWORK", new Work(this));
            this.AddCommand("AGGENM", new Genm(this));
			//Event Handlers
			this.AddEventHandlers(new EventHandler(this), Priority.Normal);
            this.AddEventHandlers(new O79Handler(this), Priority.High);
			this.AddEventHandlers(new lerk(this), Priority.High);
			this.AddEventHandlers(new Vote(this));
		}
		public void ReloadConfig()
		{
			// Command Perms
			AdminRanks=GetConfigList("attg_ranks");
			Voterank=GetConfigList("attg_vote_ranks");
			Eventrank=GetConfigList("attg_event_ranks");
			Allrank=GetConfigList("attg_all_ranks");
			//Dissable Config
			Disable=GetConfigBool("attg_command_disable");
			// Other
			O49infect=GetConfigBool("attg_049_infect");
			O96Door=GetConfigBool("attg_096_door");
			NoCHand=GetConfigBool("attg_door_hand");
            GenHand=GetConfigBool("attg_gen_hand");
            Elevatord=GetConfigFloat("attg_elevator_speed");
		}
		public override void OnEnable()
		{
            Info("ATTG Command Plugin enabled.");
		}
		public override void OnDisable()
		{
			Info("ATTG Command Plugin disabled.");
		}
	}
}


