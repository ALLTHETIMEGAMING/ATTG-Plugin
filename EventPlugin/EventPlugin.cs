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
        version = "1.2.0"
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
        public CustomItemHandler<NUKE> Handler11 { get; private set; }
        public CustomItemHandler<RECALL> Handler16 { get; private set; }
        public CustomWeaponHandler<Taze> TAZEHandler { get; private set; }
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


        public string[] ValidLightsOutRanks { get; private set; }

        public string[] Customitemrank { get; private set; }

        public string[] Voterank { get; private set; }
        public string[] SCPrank { get; private set; }
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
            Timing2.Init(this);
            // Configs
            AddConfig(new ConfigSetting("attg_ranks", new[]
            {
                "owner",
				"coowner"
            }, SettingType.LIST, true, ""));
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
            //TAZER Configs
            {
                AddConfig(new ConfigSetting("taze_body_damage", 0f, SettingType.FLOAT, true, "Damage per shot of the rifle on bodies."));
                AddConfig(new ConfigSetting("taze_head_damage", 0f, SettingType.FLOAT, true, "Damage per shot of the rifle on heads."));
                AddConfig(new ConfigSetting("taze_leg_damage", 0f, SettingType.FLOAT, true, "Damage per shot of the rifle on legs."));
                AddConfig(new ConfigSetting("taze_106_damage", 0f, SettingType.FLOAT, true, "Damage per shot of the rifle on SCP-106."));
                AddConfig(new ConfigSetting("taze_tag_damage", 0f, SettingType.FLOAT, true, "Damage per shot of the rifle when overcharged."));

                AddConfig(new ConfigSetting("taze_fire_rate", 0.5f, SettingType.FLOAT, true, "Time (in seconds) between each shot."));
                AddConfig(new ConfigSetting("taze_magazine", 5, SettingType.NUMERIC, true, "Amount of shots per magazine."));
                AddConfig(new ConfigSetting("taze_reserve_ammo", 1000, SettingType.NUMERIC, true, "Amount of HMD masses in reserve. Refreshed on server restart."));

                AddConfig(new ConfigSetting("taze_krakatoa", 15, SettingType.NUMERIC, true, "Additional shot sounds per HMD shot."));
                AddConfig(new ConfigSetting("taze_suppressed_krakatoa", 7, SettingType.NUMERIC, true, "Additional shot sounds pre suppressed HMD shot."));

                AddConfig(new ConfigSetting("taze_overchargeable", false, SettingType.BOOL, true, "Allows toggling of overcharge mode."));
                AddConfig(new ConfigSetting("taze_overcharge_radius", 0f, SettingType.FLOAT, true, "Radius of the overcharge device's bodyDamage."));
                AddConfig(new ConfigSetting("taze_overcharge_damage", 0f, SettingType.FLOAT, true, "Damage of the overcharge device per person."));
                AddConfig(new ConfigSetting("taze_overcharge_glitch", true, SettingType.BOOL, true, "Whether or not to apply the glitchy (nuke) effect to players hit by the overcharge device."));

                AddConfig(new ConfigSetting("taze_tag_time", 2f, SettingType.FLOAT, true, "Time after tagging someone with overcharge to detonation."));
                AddConfig(new ConfigSetting("taze_tag_glitches", 15, SettingType.NUMERIC, true, "Additional glitch effects to play when an overcharge device detonates on the tagged player."));
            } // Custom Items
            TAZEHandler = new CustomWeaponHandler<Taze>(200)
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
            TAZEHandler.Register();
            Handler2.Register();
            Handler3.Register();
            Handler4.Register();
            Handler5.Register();
            Handler6.Register();
            Handler7.Register();
            Handler8.Register();
            Handler11.Register();
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
            // Command Perms
			ValidLightsOutRanks = GetConfigList("attg_ranks");
            Customitemrank = GetConfigList("attg_item_ranks");
            Voterank = GetConfigList("attg_vote_ranks");
            SCPrank = GetConfigList("attg_scp_ranks");

            // Tazer
            TAZEBodyDamage = GetConfigFloat("taze_body_damage");
            TAZEHeadDamage = GetConfigFloat("taze_head_damage");
            TAZELegDamage = GetConfigFloat("taze_leg_damage");
            TAZEScp106Damage = GetConfigFloat("taze_106_damage");
            TAZETagDamage = GetConfigFloat("taze_tag_damage");
            TAZEFireRate = GetConfigFloat("taze_fire_rate");
            TAZEMagazine = GetConfigInt("taze_magazine");
            TAZEHandler.DefaultReserveAmmo = GetConfigInt("taze_reserve_ammo");
            TAZEKrakatoa = GetConfigInt("taze_krakatoa");
            TAZESuppressedKrakatoa = GetConfigInt("taze_suppressed_krakatoa");

            TAZEOverChargeRadius = GetConfigFloat("taze_overcharge_radius");
            TAZEOverChargeDamage = GetConfigFloat("taze_overcharge_damage");
            TAZEOverCharageNukeEffect = GetConfigBool("taze_overcharge_glitch");
            TAZETagTime = GetConfigFloat("taze_tag_time");
            TAZETagGlitches = GetConfigInt("taze_tag_glitches");
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

