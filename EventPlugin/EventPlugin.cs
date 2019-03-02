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
        version = "1.4.0"
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
        public CustomItemHandler<RECALL> Handler16 { get; private set; }
        public CustomWeaponHandler<Taze> Handler { get; private set; }
		public CustomWeaponHandler<Grenadec> Handler10 { get; private set; }
		public static float TAZEBodyDamage { get; private set; }
        public static float TAZEHeadDamage { get; private set; }
        public static float TAZELegDamage { get; private set; }
        public static float TAZEScp106Damage { get; private set; }
        public static float TAZETagDamage { get; private set; }

        public static float TAZEFireRate { get; private set; }
        public static int TAZEMagazine { get; private set; }

        public static int TAZEKrakatoa { get; private set; }
        public static int TAZESuppressedKrakatoa { get; private set; }

        public static float TAZEOverChargeRadius { get; private set; }
        public static float TAZEOverChargeDamage { get; private set; }
        public static bool TAZEOverCharageNukeEffect { get; private set; }

        public static float TAZETagTime { get; private set; }
        public static int TAZETagGlitches { get; private set; }
		public static float DoubleDropTime { get; private set; }

		public static float GrenadeBodyDamage { get; private set; }
		public static float GrenadeHeadDamage { get; private set; }
		public static float GrenadeLegDamage { get; private set; }
		public static float GrenadeScp106Damage { get; private set; }
		public static float GrenadeTagDamage { get; private set; }

		public static float GrenadeFireRate { get; private set; }
		public static int GrenadeMagazine { get; private set; }

		public static int GrenadeKrakatoa { get; private set; }
		public static int GrenadeSuppressedKrakatoa { get; private set; }

		public static float GrenadeOverChargeRadius { get; private set; }
		public static float GrenadeOverChargeDamage { get; private set; }
		public static bool GrenadeOverChargeable { get; private set; }
		public static bool GrenadeOverCharageNukeEffect { get; private set; }

		public static float GrenadeTagTime { get; private set; }
		public static int GrenadeTagGlitches { get; private set; }


		public string[] ValidLightsOutRanks { get; private set; }

        public string[] Customitemrank { get; private set; }
		public string[] Disablerank { get; private set; }
		public string[] Voterank { get; private set; }
        public string[] SCPrank { get; private set; }
        public bool Voteopen {get;set;}
        public static bool
           enabled = false,
           roundstarted = false;

		public bool Citems { get; set; } = false;

        public bool CIMTF {get;set;}

        public int ci_health = 100;

        public int ntf_health = 100;

        public int Yes { get;  set; }
        public int No { get;  set; }

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
			AddConfig(new ConfigSetting("attg_disable_ranks", new[]
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
            AddConfig(new ConfigSetting("attg_item_ranks", new[]
            {
                "owner"
            }, SettingType.LIST, true, "Valid ranks to Actavate custom items "));
			AddConfig(new ConfigSetting("attg_doubledrop_time", 0.25f, SettingType.FLOAT, true, ""));
			//TAZER Configs
			{

                AddConfig(new ConfigSetting("attg_taze_fire_rate", 0.5f, SettingType.FLOAT, true, ""));
                AddConfig(new ConfigSetting("attg_taze_magazine", 5, SettingType.NUMERIC, true, ""));
                AddConfig(new ConfigSetting("attg_taze_reserve_ammo", 1000, SettingType.NUMERIC, true, ""));

                AddConfig(new ConfigSetting("attg_taze_krakatoa", 15, SettingType.NUMERIC, true, ""));
                AddConfig(new ConfigSetting("attg_taze_suppressed_krakatoa", 7, SettingType.NUMERIC, true, ""));
                AddConfig(new ConfigSetting("attg_taze_overcharge_glitch", true, SettingType.BOOL, true, ""));

                AddConfig(new ConfigSetting("attg_taze_tag_time", 2f, SettingType.FLOAT, true, ""));
                AddConfig(new ConfigSetting("attg_taze_tag_glitches", 15, SettingType.NUMERIC, true, ""));
				// Grenade
				AddConfig(new ConfigSetting("attg_Grenade_fire_rate", 0.5f, SettingType.FLOAT, true, ""));
				AddConfig(new ConfigSetting("attg_Grenade_magazine", 5, SettingType.NUMERIC, true, ""));
				AddConfig(new ConfigSetting("attg_Grenade_reserve_ammo", 1000, SettingType.NUMERIC, true, ""));

				AddConfig(new ConfigSetting("attg_Grenade_krakatoa", 15, SettingType.NUMERIC, true, ""));
				AddConfig(new ConfigSetting("attg_Grenade_suppressed_krakatoa", 7, SettingType.NUMERIC, true, "."));
				AddConfig(new ConfigSetting("attg_Grenade_overcharge_radius", 0f, SettingType.FLOAT, true, ""));
				AddConfig(new ConfigSetting("attg_Grenade__damage", 0f, SettingType.FLOAT, true, ""));
				AddConfig(new ConfigSetting("attg_Grenade_overcharge_glitch", true, SettingType.BOOL, true, ""));

				AddConfig(new ConfigSetting("attg_Grenade_tag_time", 2f, SettingType.FLOAT, true, ""));
				AddConfig(new ConfigSetting("attg_Grenade_tag_glitches", 15, SettingType.NUMERIC, true, ""));
			} // Custom Items
            Handler = new CustomWeaponHandler<Taze>(200)
            {
                AmmoName = "Heavy Masses",
                DroppedAmmoCount = 5,
                DefaultType = ItemType.COM15
            };
			Handler10 = new CustomWeaponHandler<Grenadec>(201)
			{
				AmmoName = "Heavy Masses",
				DroppedAmmoCount = 5,
				DefaultType = ItemType.COM15
			};
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
            Handler16 = new CustomItemHandler<RECALL>(214)
            {
                DefaultType = ItemType.COIN
            };

            
            ReloadConfig();
            Handler.Register();
			Handler10.Register();
			Handler2.Register();
            Handler3.Register();
            Handler4.Register();
            Handler5.Register();
            Handler6.Register();
            Handler7.Register();
            Handler8.Register();
            Handler16.Register();
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
			DoubleDropTime = GetConfigFloat("attg_doubledrop_time");
			// Command Perms
			ValidLightsOutRanks = GetConfigList("attg_ranks");
            Customitemrank = GetConfigList("attg_item_ranks");
            Voterank = GetConfigList("attg_vote_ranks");
            SCPrank = GetConfigList("attg_scp_ranks");

            // Tazer
            TAZETagDamage = GetConfigFloat("attg_taze_tag_damage");
            TAZEFireRate = GetConfigFloat("attg_taze_fire_rate");
            TAZEMagazine = GetConfigInt("attg_taze_magazine");
            Handler.DefaultReserveAmmo = GetConfigInt("attg_taze_reserve_ammo");
            TAZEKrakatoa = GetConfigInt("attg_taze_krakatoa");
            TAZESuppressedKrakatoa = GetConfigInt("attg_taze_suppressed_krakatoa");
            TAZEOverCharageNukeEffect = GetConfigBool("attg_taze_overcharge_glitch");
            TAZETagTime = GetConfigFloat("attg_taze_tag_time");
            TAZETagGlitches = GetConfigInt("attg_taze_tag_glitches");
			// Grenade

			GrenadeFireRate = GetConfigFloat("attg_Grenade_fire_rate");
			GrenadeMagazine = GetConfigInt("attg_Grenade_magazine");
			Handler10.DefaultReserveAmmo = GetConfigInt("attg_Grenade_reserve_ammo");

			GrenadeKrakatoa = GetConfigInt("attg_Grenade_krakatoa");
			GrenadeSuppressedKrakatoa = GetConfigInt("attg_Grenade_suppressed_krakatoa");

			GrenadeOverChargeable = GetConfigBool("attg_Grenade_overchargeable");
			GrenadeOverChargeRadius = GetConfigFloat("attg_Grenade_overcharge_radius");
			GrenadeOverChargeDamage = GetConfigFloat("attg_Grenade_overcharge_damage");
			GrenadeOverCharageNukeEffect = GetConfigBool("attg_Grenade_overcharge_glitch");
		}

        public override void OnEnable()
        {
            Info("Event Plugin enabled.");
        }

        public override void OnDisable()
        {
            Info("Event Plugin disabled.");
        }

        public static void EnableGamemodeCIMTF()
        {
            enabled = true;
            if (!roundstarted)
            {
                plugin.pluginManager.Server.Map.ClearBroadcasts();
                plugin.pluginManager.Server.Map.Broadcast(25, "<color=#00ffff> CI VS MTF Gamemode is starting..</color>", false);
            }
        }

        public static void DisableGamemodeCIMTF()
        {
            enabled = false;
            plugin.pluginManager.Server.Map.ClearBroadcasts();
        }


    }
}

