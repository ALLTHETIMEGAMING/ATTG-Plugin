using ItemManager;
using ItemManager.Utilities;
using scp4aiur;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Smod2.EventHandlers;
using UnityEngine;
using Random = UnityEngine.Random;


namespace ATTG3
{
    [PluginDetails(
        author = "All The Time Gaming",
        description = "Event Mod",
        id = "ATTG.custom.Event",
        name = "",
        SmodMajor = 3,
        SmodMinor = 3,
        SmodRevision = 0,
        version = "1.0.0"
        )]
    public class ATTG3Plugin : Smod2.Plugin
    {
        internal static ATTG3Plugin plugin;
        public static ATTG3Plugin Instance { get; private set; }
        public CustomItemHandler<LAR> Handler2 { get; private set; }
        public CustomItemHandler<O49> Handler3 { get; private set; }
        public CustomItemHandler<N39> Handler4 { get; private set; }
        public CustomItemHandler<NUT> Handler5 { get; private set; }
        public CustomItemHandler<SHY> Handler6 { get; private set; }
        public CustomItemHandler<ZOM> Handler7 { get; private set; }
        public CustomItemHandler<COM> Handler8 { get; private set; }
        public CustomItemHandler<MTFL> Handler9 { get; private set; }
        public CustomItemHandler<tptop> Handler10 { get; private set; }
        public CustomItemHandler<NUKE> Handler11 { get; private set; }
        public CustomItemHandler<CITP> Handler12 { get; private set; }
        public CustomItemHandler<CD> Handler13 { get; private set; }
        public CustomItemHandler<ELE> Handler14 { get; private set; }
        public CustomItemHandler<LD> Handler15 { get; private set; }
        public CustomItemHandler<RECALL> Handler16 { get; private set; }
        public CustomItemHandler<O79> Handler17 { get; private set; }

        public string[] ValidLightsOutRanks { get; private set; }

        public string[] Customitemrank { get; private set; }
        public bool Voteopen {get;set;}
        public static bool
           enabled = false,
           roundstarted = false;
           
        public bool Citems { get; set; }

        public bool CIMTF {get;set;}

        public int ci_health = 100;

        public int ntf_health = 100;

        public int Yes { get;  set; }
        public int No { get;  set; }

        public override void Register()
        {
            Instance = this;
            Timing.Init(this);
            //List<int>;
            AddConfig(new ConfigSetting("attg_ranks", new[]
            {
                "owner",
				"coowner"
            }, SettingType.LIST, true, "Valid ranks for the tl Command"));

            AddConfig(new ConfigSetting("attg_item_ranks", new[]
            {
                "owner"
            }, SettingType.LIST, true, "Valid ranks to Actavate custom items "));

            Handler2 = new CustomItemHandler<LAR>(207)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler3 = new CustomItemHandler<O49>(208)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler4 = new CustomItemHandler<N39>(209)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler5 = new CustomItemHandler<NUT>(210)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler6 = new CustomItemHandler<SHY>(211)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler7 = new CustomItemHandler<ZOM>(212)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler8 = new CustomItemHandler<COM>(213)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler9 = new CustomItemHandler<MTFL>(214)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler10 = new CustomItemHandler<tptop>(215)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler11 = new CustomItemHandler<NUKE>(216)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler12 = new CustomItemHandler<CITP>(217)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler13 = new CustomItemHandler<CD>(218)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler14 = new CustomItemHandler<ELE>(219)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler15 = new CustomItemHandler<LD>(220)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler16 = new CustomItemHandler<RECALL>(221)
            {
                DefaultType = ItemType.COIN
            };
            Handler17 = new CustomItemHandler<O79>(222)
            {
                DefaultType = ItemType.COIN
            };
            ReloadConfig();
            Handler2.Register();
            Handler3.Register();
            Handler4.Register();
            Handler5.Register();
            Handler6.Register();
            Handler7.Register();
            Handler8.Register();
            Handler9.Register();
            Handler10.Register();
            Handler11.Register();
            Handler12.Register();
            Handler13.Register();
            Handler14.Register();
            Handler15.Register();
            Handler16.Register();
            Handler17.Register();
            this.AddCommand("AGTL", new Tleslad(this));
            this.AddCommand("AGEL", new ELEL(this));
            this.AddCommand("AG106D", new C106(this));
            this.AddCommand("AGS079", new ST079(this));
            this.AddCommand("AGTLR", new TleslR(this));
            this.AddCommand("AGP079", new P079(this));
            this.AddCommand("AGELR", new ELELS(this));
            this.AddCommand("AGHELP", new Help(this));
            this.AddCommand("VoteT", new VoteAD(this));
            this.AddCommand("VoteC", new VoteC(this));
            this.AddCommand("VoteS", new VoteA(this));
			this.AddCommand("AGAmmo", new Ammo(this));
			this.AddCommand("VoteBC", new VoteBC(this));
			this.AddCommand("AGCIMTF", new CIMTFC(this));
            this.AddCommand("AGUP", new Up(this));
            this.AddCommand("AGL079", new L079(this));
            this.AddCommand("AGE079", new E079(this));
            this.AddCommand("AGCitem", new Citem(this));
            this.AddEventHandlers(new Events(this), Priority.Normal);
            this.AddEventHandlers(new EventHandler(this), Priority.Normal);
            this.AddEventHandlers(new No(this));
			this.AddEventHandlers(new Yes(this));
		}

        public void ReloadConfig()
        {

			ValidLightsOutRanks = GetConfigList("attg_ranks");
            Customitemrank = GetConfigList("attg_item_ranks");
        }

        public override void OnEnable()
        {
            Info("ATTG3 enabled.");
        }

        public override void OnDisable()
        {
            Info("ATTG3 disabled.");
        }

        public static void EnableGamemodeCIMTF()
        {
            enabled = true;
            if (!roundstarted)
            {
                plugin.pluginManager.Server.Map.ClearBroadcasts();
                plugin.pluginManager.Server.Map.Broadcast(25, "<color=#00ffff> Gangwar Gamemode is starting..</color>", false);
            }
        }

        public static void DisableGamemodeCIMTF()
        {
            enabled = false;
            plugin.pluginManager.Server.Map.ClearBroadcasts();
        }


    }
}

