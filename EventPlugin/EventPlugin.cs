using scp4aiur;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;

namespace ATTG3
{
    [PluginDetails(
        author = "All The Time Gaming",
        description = "COMMAND Mod",
        id = "ATTG.ADMIN.COMMAND",
        name = "",
        SmodMajor = 3,
        SmodMinor = 3,
        SmodRevision = 0,
        version = "2.0.0"
        )]
    public class ATTG3Plugin : Smod2.Plugin
    {
        
        public static ATTG3Plugin Instance { get; private set; }
        public bool Running939 { get; set; }
        public string[] AdminRanks { get; private set; }
        public string[] Disablerank { get; private set; }
        public string[] Voterank { get; private set; }
        public string[] Allrank { get; private set; }
        public string[] SCPrank { get; private set; }
        public bool Voteopen { get; set; }
        public bool Disable { get; set; } = false;
        public int Yes { get; set; }
        public int No { get; set; }
        public override void Register()
        {
            Instance = this;
            Timing.Init(this);
            Timing2.Init(this);
            // Configs
            AddConfig(new ConfigSetting("attg_ranks", new[]
            {
                "owner",
                "coowner"
            }, SettingType.LIST, true, ""));
            AddConfig(new ConfigSetting("attg_command_disable_ranks", new[]
            {
                "owner"
            }, SettingType.LIST, true, "Valid ranks to disable the Event Plugin"));
            AddConfig(new ConfigSetting("attg_scp_ranks", new[]
            {
                "owner",
                "coowner"
            }, SettingType.LIST, true, "Ranks for all SCP Commands"));
            AddConfig(new ConfigSetting("attg_vote_ranks", new[]
            {
                "owner",
                "coowner",
                "admin"
            }, SettingType.LIST, true, "Valid ranks for all voteing Commands"));
            AddConfig(new ConfigSetting("attg_all_ranks", new[]
            {
                "owner"
            }, SettingType.LIST, true, "Valid ranks for all Commands"));
            ReloadConfig();
            this.AddCommand("AGTL", new Tleslad(this));
            this.AddCommand("AGEL", new ELEL(this));
            this.AddCommand("AG106D", new C106(this));
            this.AddCommand("AGTLR", new TleslR(this));
            this.AddCommand("AGP079", new P079(this));
            this.AddCommand("AGELS", new ELELS(this));
            this.AddCommand("AGHELP", new Help(this));
            this.AddCommand("AGVoteT", new VoteAD(this));
            this.AddCommand("AGVoteC", new VoteC(this));
            this.AddCommand("AGVoteS", new VoteA(this));
            this.AddCommand("AGAMMO", new Ammo(this));
            this.AddCommand("AGVoteBC", new VoteBC(this));
            this.AddCommand("AGUP", new Up(this));
            this.AddCommand("AGL079", new L079(this));
            this.AddCommand("AGE079", new E079(this));
            this.AddCommand("AGDISABLE", new Disable(this));
            this.AddCommand("AGSPEEDA", new Speed2(this));
            this.AddCommand("AGSPEED", new Speed(this));
            this.AddCommand("AGSHAKE", new Shake(this));
            this.AddEventHandlers(new EventHandler(this), Priority.Normal);
            this.AddEventHandlers(new No(this));
            this.AddEventHandlers(new Yes(this));
        }
        public void ReloadConfig()
        {
            // Command Perms
            AdminRanks = GetConfigList("attg_ranks");
            Voterank = GetConfigList("attg_vote_ranks");
            SCPrank = GetConfigList("attg_scp_ranks");
            Allrank = GetConfigList("attg_all_ranks");
            //Dissable Config
            Disable = GetConfigBool("attg_command_disable");
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


