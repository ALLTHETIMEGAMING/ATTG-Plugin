using ItemManager.Utilities;
using scp4aiur;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;

namespace ATTG4
{
    [PluginDetails(
        author = "All The Time Gaming",
        description = "ATTG ITEM MOD",
        id = "ATTG.ADMIN.ITEM",
        name = "",
        SmodMajor = 3,
        SmodMinor = 3,
        SmodRevision = 0,
        version = "1.0.0"
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
        public string[] Disablerank { get; private set; }
        public string[] Allrank { get; private set; }
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
            AddConfig(new ConfigSetting("attg_item_disable_ranks", new[]
            {
                "owner"
            }, SettingType.LIST, true, "Valid ranks to disable the Event Plugin"));
            AddConfig(new ConfigSetting("attg_scp_ranks", new[]
            {
                "owner",
                "coowner"
            }, SettingType.LIST, true, "Ranks for all SCP Commands"));
            AddConfig(new ConfigSetting("attg_all_ranks", new[]
            {
                "owner"

            }, SettingType.LIST, true, "Valid ranks for all Commands"));
            ReloadConfig();
            AddConfig(new ConfigSetting("attg_item_disable", false, SettingType.BOOL, true, "Disables Event Plugin"));
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
            //Dissable Config
            Disable = GetConfigBool("attg_item_disable");
        }
        public override void OnEnable()
        {
            Info("ATTG Item Plugin enabled.");
        }
        public override void OnDisable()
        {
            Info("ATTG Item Plugin disabled.");
        }
    }
}


