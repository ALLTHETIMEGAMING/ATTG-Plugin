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
        id = "ATTG.ADMIN.Event",
        name = "",
        SmodMajor = 3,
        SmodMinor = 3,
        SmodRevision = 0,
        version = "1.9.0"
        )]
    public class ATTG4Plugin : Smod2.Plugin
    {
        internal static ATTG4Plugin plugin;
        public static ATTG4Plugin Instance { get; private set; }
        public CustomItemHandler<LAR> Handler1 { get; private set; }
        public CustomItemHandler<O49> Handler2 { get; private set; }
        public CustomItemHandler<N39> Handler3 { get; private set; }
        public CustomItemHandler<NUT> Handler4 { get; private set; }
        public CustomItemHandler<SHY> Handler5 { get; private set; }
        public CustomItemHandler<ZOM> Handler6 { get; private set; }
        public CustomItemHandler<COM> Handler7 { get; private set; }
        public CustomItemHandler<RECALL> Handler8 { get; private set; }

        public string[] AdminRanks { get; private set; }
        public string[] Disablerank { get; private set; }
        public string[] Allrank { get; private set; }
        public bool Voteopen { get; set; }
        public bool Disable { get; set; } = false;
     
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
            AddConfig(new ConfigSetting("attg_all_ranks", new[]
            {
                "owner"

            }, SettingType.LIST, true, "Valid ranks for all Commands"));
            ReloadConfig();


            AddConfig(new ConfigSetting("attg_disable", false, SettingType.BOOL, true, "Disables Event Plugin"));
            Handler1 = new CustomItemHandler<LAR>(200)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler2 = new CustomItemHandler<O49>(201)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler3 = new CustomItemHandler<N39>(202)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler4 = new CustomItemHandler<NUT>(204)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler5 = new CustomItemHandler<SHY>(205)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler6 = new CustomItemHandler<ZOM>(206)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler7 = new CustomItemHandler<COM>(207)
            {
                DefaultType = ItemType.MEDKIT
            };
            Handler8 = new CustomItemHandler<RECALL>(208)
            {
                DefaultType = ItemType.COIN
            };


            ReloadConfig();
            //Handler.Register();
            Handler1.Register();
            Handler2.Register();
            Handler3.Register();
            Handler4.Register();
            Handler5.Register();
            Handler6.Register();
            Handler7.Register();
            Handler8.Register();


    }
        public void ReloadConfig()
        {
            // Command Perms


            //Dissable Config
            Disable = GetConfigBool("attg_item_disable");

        }
        public override void OnEnable()
        {
            Info("Event Plugin enabled.");
        }
        public override void OnDisable()
        {
            Info("Event Plugin disabled.");
        }

    }
}


