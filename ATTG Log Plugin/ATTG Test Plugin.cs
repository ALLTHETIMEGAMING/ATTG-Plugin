using scp4aiur;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using System;
using Smod2;
using Smod2.API;
using System.IO;
using System.Collections.Generic;

namespace ATTG_Test
{

    [PluginDetails(
        author = "All The Time Gaming",
        description = "LOG MOD",
        id = "ATTG.ADMIN.TEST",
        name = "",
        SmodMajor = 3,
        SmodMinor = 3,
        SmodRevision = 0,
        version = "1.1.0"
        )]
    public class ATTGLogPlugin : Smod2.Plugin
    {

        public static ATTGLogPlugin Instance { get; private set; }
        public bool Disable { get; set; } = false;
        public string[] AdminRanks { get; private set; }
        public string[] Disablerank { get; private set; }
        public bool ServerData = false;
        public int Roundnum = 0;
        public static string attgfolder = FileManager.GetAppFolder()+"ATTG";
        public static string Rooms = FileManager.GetAppFolder()+"ATTG"+Path.DirectorySeparatorChar+"Rooms.txt";
        public static string Tlesla = FileManager.GetAppFolder()+"ATTG"+Path.DirectorySeparatorChar+"Tlesla.txt";
        public static string Doors = FileManager.GetAppFolder()+"ATTG"+Path.DirectorySeparatorChar+"Door.txt";
        public static string Gen = FileManager.GetAppFolder()+"ATTG"+Path.DirectorySeparatorChar+"Generator.txt";
        public static string Cam = FileManager.GetAppFolder()+"ATTG"+Path.DirectorySeparatorChar+"Camera.txt";
        public static string Spawn = FileManager.GetAppFolder()+"ATTG"+Path.DirectorySeparatorChar+"Spawn.txt";
        public static string Elevator = FileManager.GetAppFolder()+"ATTG"+Path.DirectorySeparatorChar+"Elevator.txt";
        public static string Elevator2 = FileManager.GetAppFolder()+"ATTG"+Path.DirectorySeparatorChar+"ElevatorTeleport.txt";
        public override void Register()
        {
            Instance=this;
            // Configs
            AddConfig(new ConfigSetting("attg_log_ranks", new[]
            {
                "owner",
                "coowner"
            }, SettingType.LIST, true, ""));
            AddConfig(new ConfigSetting("attg_log_disable_ranks", new[]
            {
                "owner"
            }, SettingType.LIST, true, "Valid ranks to disable the Event Plugin"));
            ReloadConfig();
            //this.AddCommand("AGDISABLE", new Disable(this));
            this.AddCommand("DATA", new Data(this));
            this.AddEventHandlers(new EventHandler(this), Priority.Highest);

        }
        public void ReloadConfig()
        {
            // Command Perms
            AdminRanks=GetConfigList("attg_log_ranks");
            //Dissable Config
            Disable=GetConfigBool("attg_log_disable");
        }
        public override void OnEnable()
        {
            if (!Directory.Exists(attgfolder))
            {
                Directory.CreateDirectory(attgfolder);
            }
            if (!File.Exists(Rooms))
            {
                using (new StreamWriter(File.Create(Rooms))) { }
            }
            if (!File.Exists(Doors))
            {
                using (new StreamWriter(File.Create(Doors))) { }
            }
            if (!File.Exists(Tlesla))
            {
                using (new StreamWriter(File.Create(Tlesla))) { }
            }
            if (!File.Exists(Gen))
            {
                using (new StreamWriter(File.Create(Gen))) { }
            }
            if (!File.Exists(Cam))
            {
                using (new StreamWriter(File.Create(Cam))) { }
            }
            if (!File.Exists(Spawn))
            {
                using (new StreamWriter(File.Create(Spawn))) { }
            }
            if (!File.Exists(Elevator))
            {
                using (new StreamWriter(File.Create(Elevator))) { }
            }
            if (!File.Exists(Elevator2))
            {
                using (new StreamWriter(File.Create(Elevator2))) { }
            }
            Info("ATTG Test Plugin enabled.");
        }
        public override void OnDisable()
        {
            Info("ATTG Test Plugin disabled.");
        }
    }
}


