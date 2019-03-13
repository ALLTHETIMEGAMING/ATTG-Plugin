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
        public bool Running939 { get; set; }
        public string[] AdminRanks { get; private set; }
        public string[] Disablerank { get; private set; }
        public string[] Voterank { get; private set; }
        public string[] allrank { get; private set; }
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
            
            this.AddCommand("AGSHAKE", new Shake(this));
        }
        public void ReloadConfig()
        {
            // Command Perms


            //Dissable Config
            Disable = GetConfigBool("attg__disable");

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


