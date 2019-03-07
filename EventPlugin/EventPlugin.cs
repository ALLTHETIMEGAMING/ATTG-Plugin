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
        version = "1.8.1"
        )]
    public class ATTG3Plugin : Smod2.Plugin
    {
        internal static ATTG3Plugin plugin;

		// ITEMS
        public static ATTG3Plugin Instance { get; private set; }
        public CustomItemHandler<LAR> Handler2 { get; private set; }
        public CustomItemHandler<O49> Handler3 { get; private set; }
        public CustomItemHandler<N39> Handler4 { get; private set; }
        public CustomItemHandler<NUT> Handler5 { get; private set; }
        public CustomItemHandler<SHY> Handler6 { get; private set; }
        public CustomItemHandler<ZOM> Handler7 { get; private set; }
        public CustomItemHandler<COM> Handler8 { get; private set; }
        public CustomItemHandler<RECALL> Handler16 { get; private set; }
        //public CustomWeaponHandler<Taze> Handler { get; private set; }
		public CustomWeaponHandler<Grenadec> Handler10 { get; private set; }

		//TASER
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

        public static float TAZETagTime { get; private set; }
        public static int TAZETagGlitches { get; private set; }

		//GRENADE
		public static float GrenadeFireRate { get; private set; }
		public static int GrenadeMagazine { get; private set; }

		public static int GrenadeKrakatoa { get; private set; }
		public static int GrenadeSuppressedKrakatoa { get; private set; }

		public static float GrenadeTagTime { get; private set; }
		public static int GrenadeTagGlitches { get; private set; }

        public bool running939 { get; set; }
        public string[] AdminRanks { get; private set; }

        public string[] Customitemrank { get; private set; }
		public string[] Disablerank { get; private set; }
		public string[] Voterank { get; private set; }
        public string[] SCPrank { get; private set; }
        public bool Voteopen {get;set;}
		public static bool
		   enabledcimtf = false,
		   roundstartedcimtf = false,
		   enabledjug = false,
		   roundstartedjug = false;

		public bool Disable { get; set; } = false;

		public bool Citems { get; set; } = false;

        public bool CIMTF {get;set;}

        public bool Jug { get; set; }

        public int CIMTFci_health;

        public int CIMTFntf_health;

        public int Yes { get;  set; }
        public int No { get;  set; }
		// Jug Gamemode
		public static bool JugNTF_Disarmer;
		public static int Jugg_base;
		public static int Jugg_increase;
		public static int Jugg_grenade;
		public static int jugNTF_ammo;
		public static int jugNTF_Health;
		public static bool jugg_infinite_nades;
		public static Player juggernaut;
		public static Player activator = null;
		public static int juggernaut_healh;
		public static int jugntf_health;
		public static string[] juggernaut_prevRank = new string[2];
		public static Player selectedJuggernaut = null;
		public static float jugcritical_damage;
		public static Player jugg_killer = null;

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
			//TAZER Configs
               /* AddConfig(new ConfigSetting("attg_taze_fire_rate", 0.5f, SettingType.FLOAT, true, ""));
                AddConfig(new ConfigSetting("attg_taze_magazine", 1, SettingType.NUMERIC, true, ""));
                AddConfig(new ConfigSetting("attg_taze_reserve_ammo", 10, SettingType.NUMERIC, true, ""));
                AddConfig(new ConfigSetting("attg_taze_krakatoa", 4, SettingType.NUMERIC, true, ""));
                AddConfig(new ConfigSetting("attg_taze_tag_time", 2f, SettingType.FLOAT, true, ""));
                AddConfig(new ConfigSetting("attg_taze_tag_glitches", 15, SettingType.NUMERIC, true, "")); */
				// Grenade
				AddConfig(new ConfigSetting("attg_grenade_fire_rate", 3F, SettingType.FLOAT, true, ""));
				AddConfig(new ConfigSetting("attg_grenade_magazine", 1, SettingType.NUMERIC, true, ""));
				AddConfig(new ConfigSetting("attg_grenade_reserve_ammo", 10, SettingType.NUMERIC, true, ""));
				AddConfig(new ConfigSetting("attg_grenade_krakatoa", 10, SettingType.NUMERIC, true, ""));
				AddConfig(new ConfigSetting("attg_grenade_suppressed_krakatoa", 7, SettingType.NUMERIC, true, ""));

			// Event Configs
			AddConfig(new ConfigSetting("attg_cimtf__ntf_health", 100, SettingType.NUMERIC, true, ""));
			AddConfig(new ConfigSetting("attg_cimtf__ci_health", 100, SettingType.NUMERIC, true, ""));
			AddConfig(new ConfigSetting("attg_juggernaut_base_health", 500, SettingType.NUMERIC, true, ""));
			AddConfig(new ConfigSetting("attg_juggernaut_increase_amount", 500, SettingType.NUMERIC, true, ""));
			AddConfig(new ConfigSetting("attg_ juggernaut_jugg_grenades", 6, SettingType.NUMERIC, true, ""));
			AddConfig(new ConfigSetting("attg_juggernaut_ntf_disarmer", false, SettingType.BOOL, true, ""));
			AddConfig(new ConfigSetting("attg_juggernaut_ntf_health", 150, SettingType.NUMERIC, true, ""));
			AddConfig(new ConfigSetting("attg_juggernaut_critical_damage", 0.15, SettingType.FLOAT, true, ""));
			AddConfig(new ConfigSetting("attg_juggernaut_jugg_infinite_nades", true, SettingType.BOOL, true, ""));


			AddConfig(new ConfigSetting("attg_disable", false, SettingType.BOOL, true, "Disables Event Plugin"));
			// Custom Items
			/*Handler = new CustomWeaponHandler<Taze>(200)
            {
                AmmoName = "Heavy Masses",
                DroppedAmmoCount = 5,
                DefaultType = ItemType.COM15
            };*/
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
            //Handler.Register();
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
            this.AddCommand("AGELS", new ELELS(this));
            this.AddCommand("AGHELP", new Help(this));
            this.AddCommand("AGVoteT", new VoteAD(this));
            this.AddCommand("AGVoteC", new VoteC(this));
            this.AddCommand("AGVoteS", new VoteA(this));
			this.AddCommand("AGAmmo", new Ammo(this));
			this.AddCommand("AGVoteBC", new VoteBC(this));
			this.AddCommand("AGCIMTF", new CIMTFC(this));
			this.AddCommand("AGJUG", new Jugc(this));
			this.AddCommand("AGUP", new Up(this));
            this.AddCommand("AGL079", new L079(this));
            this.AddCommand("AGE079", new E079(this));
            this.AddCommand("AGCitem", new Citem(this));
			this.AddCommand("AGDISABLE", new Disable(this));
			this.AddCommand("AGSPEEDA", new Speed2(this));
			this.AddCommand("AGSPEED", new Speed(this));
            this.AddCommand("AGSHAKE", new Shake(this));
            this.AddEventHandlers(new CIMTF(this), Priority.Normal);
            this.AddEventHandlers(new EventHandler(this), Priority.Normal);
            this.AddEventHandlers(new No(this));
			this.AddEventHandlers(new Yes(this));
		}
        public void ReloadConfig()
        {
			// Command Perms
			AdminRanks = GetConfigList("attg_ranks");
            Customitemrank = GetConfigList("attg_item_ranks");
            Voterank = GetConfigList("attg_vote_ranks");
            SCPrank = GetConfigList("attg_scp_ranks");
			
            // Tazer
            //TAZEFireRate = GetConfigFloat("attg_taze_fire_rate");
            //TAZEMagazine = GetConfigInt("attg_taze_magazine");
            //Handler.DefaultReserveAmmo = GetConfigInt("attg_taze_reserve_ammo");
            //TAZEKrakatoa = GetConfigInt("attg_taze_krakatoa");
            //TAZETagTime = GetConfigFloat("attg_taze_tag_time");
            //TAZETagGlitches = GetConfigInt("attg_taze_tag_glitches");
			// Grenade
			GrenadeFireRate = GetConfigFloat("attg_grenade_fire_rate");
			GrenadeMagazine = GetConfigInt("attg_grenade_magazine");
			Handler10.DefaultReserveAmmo = GetConfigInt("attg_grenade_reserve_ammo");
			GrenadeKrakatoa = GetConfigInt("attg_grenade_krakatoa");
			GrenadeSuppressedKrakatoa = GetConfigInt("attg_grenade_suppressed_krakatoa");
			//Dissable Config
			Disable = GetConfigBool("attg_disable");
			CIMTFci_health = GetConfigInt("attg_cimtf_ci_health");
			CIMTFntf_health = GetConfigInt("attg_cimtf__ntf_health");

			Jugg_base = GetConfigInt("attg_juggernaut_base_health");
			Jugg_increase = GetConfigInt("attg_juggernaut_increase_amount");
			JugNTF_Disarmer = GetConfigBool("attg_juggernaut_ntf_disarmer");
			Jugg_grenade = GetConfigInt("attg_juggernaut_jugg_grenades");
			jugNTF_Health = GetConfigInt("attg_juggernaut_ntf_health");
			jugcritical_damage = GetConfigFloat("attg_juggernaut_critical_damage");
			jugg_infinite_nades = GetConfigBool("attg_juggernaut_infinite_jugg_nades");
		}
        public override void OnEnable()
        {
            Info("Event Plugin enabled.");
        }
        public override void OnDisable()
        {
            Info("Event Plugin disabled.");
        }
		public static void EnableGamemodecimtf()
			{
				ATTG3Plugin.enabledcimtf = true;
				if (!ATTG3Plugin.roundstartedcimtf)
				{
					ATTG3Plugin.plugin.pluginManager.Server.Map.ClearBroadcasts();
					ATTG3Plugin.plugin.pluginManager.Server.Map.Broadcast(25, "<color=#00ffff> CI VS MTF Gamemode is starting..</color>", false);
				}
			}

			public static void DisableGamemodecimtf()
			{
				ATTG3Plugin.enabledcimtf = false;
				ATTG3Plugin.plugin.pluginManager.Server.Map.ClearBroadcasts();
			}

			public static void EnableGamemodejug()
			{
				ATTG3Plugin.enabledjug = true;
				if (!ATTG3Plugin.roundstartedjug)
				{
					ATTG3Plugin.plugin.pluginManager.Server.Map.ClearBroadcasts();
					ATTG3Plugin.plugin.pluginManager.Server.Map.Broadcast(25, "<color=#228B22>Juggernaut Gamemode</color> is starting...", false);
				}
			}
			public static void DisableGamemodejug()
			{
				ATTG3Plugin.enabledjug = false;
				ATTG3Plugin.plugin.pluginManager.Server.Map.ClearBroadcasts();
			}
			public static void EndGamemodeRound()
			{
				ATTG3Plugin.plugin.Info("EndgameRound Function.");
				ATTG3Plugin.roundstartedcimtf = false;
				ATTG3Plugin.roundstartedjug = false;
				ATTG3Plugin.enabledjug = false;
				ATTG3Plugin.enabledcimtf = false;
				ATTG3Plugin.plugin.Server.Round.EndRound();
			}
			public static IEnumerable<float> SpawnChaos(Player player, float delay)
			{
				yield return delay;
				player.ChangeRole(Role.CHAOS_INSURGENCY, false, true, false, true);
				yield return 2;
				foreach (Smod2.API.Item item in player.GetInventory())
				{
					item.Remove();
				}
				player.GiveItem(ItemType.E11_STANDARD_RIFLE);
				player.GiveItem(ItemType.COM15);
				player.GiveItem(ItemType.FRAG_GRENADE);
				player.GiveItem(ItemType.MEDKIT);
				player.GiveItem(ItemType.FLASHBANG);

				player.SetAmmo(AmmoType.DROPPED_5, 500);
				player.SetAmmo(AmmoType.DROPPED_7, 500);
				player.SetAmmo(AmmoType.DROPPED_9, 500);
				player.SetHealth(plugin.CIMTFci_health);
			}
			public static IEnumerable<float> SpawnNTF(Player player, float delay)
			{
				yield return delay;
				player.ChangeRole(Role.NTF_COMMANDER, false, true, false, false);
				yield return 2;
				foreach (Smod2.API.Item item in player.GetInventory())
				{
					item.Remove();
				}
				player.GiveItem(ItemType.E11_STANDARD_RIFLE);
				player.GiveItem(ItemType.COM15);
				player.GiveItem(ItemType.FRAG_GRENADE);
				player.GiveItem(ItemType.MEDKIT);
				player.GiveItem(ItemType.FLASHBANG);

				player.SetAmmo(AmmoType.DROPPED_5, 500);
				player.SetAmmo(AmmoType.DROPPED_7, 500);
				player.SetAmmo(AmmoType.DROPPED_9, 500);
				player.SetHealth(plugin.CIMTFntf_health);
			}
			public static void SpawnAsNTFCommander(Player player)
			{
				player.ChangeRole(Role.NTF_COMMANDER, false, true, true, true);

				ATTG3Plugin.jugntf_health = ATTG3Plugin.jugNTF_Health;
				ATTG3Plugin.plugin.Info("SpawnNTF Health");
				player.SetHealth(ATTG3Plugin.jugntf_health);

				player.PersonalClearBroadcasts();
				if (ATTG3Plugin.juggernaut != null)
					player.PersonalBroadcast(15, "You are an <color=#002DB3>NTF Commander</color> Work with others to eliminate the <color=#228B22>Juggernaut " +	ATTG3Plugin.juggernaut.Name + "</color>", false);
				else
					player.PersonalBroadcast(15, "You are an <color=#002DB3>NTF Commander</color> Work with others to eliminate the <color=#228B22>Juggernaut</color>", false);
			}

			public static void SpawnAsJuggernaut(Player player)
			{
				ATTG3Plugin.juggernaut = player;

				//Spawned as Juggernaut in 939s spawn location
				Vector spawn = ATTG3Plugin.plugin.Server.Map.GetRandomSpawnPoint(Role.SCP_939_53);
				player.ChangeRole(Role.CHAOS_INSURGENCY, false, false, true, true);
				player.Teleport(spawn);

				ATTG3Plugin.juggernaut_prevRank = new string[] { player.GetUserGroup().Color, player.GetUserGroup().Name };

				// Given a Juggernaut badge
				player.SetRank("silver", "Juggernaut");

				// Health scales with amount of players in round
				int health = ATTG3Plugin.Jugg_base + (ATTG3Plugin.Jugg_increase * ATTG3Plugin.plugin.Server.NumPlayers) - 500;
				player.SetHealth(health);
				ATTG3Plugin.juggernaut_healh = health;

				// Clear Inventory
				foreach (Smod2.API.Item item in player.GetInventory())
					item.Remove();

				//Increased Ammo
				player.SetAmmo(AmmoType.DROPPED_7, 2000);
				player.SetAmmo(AmmoType.DROPPED_5, 2000);
				player.SetAmmo(AmmoType.DROPPED_9, 2000);

				// 1 Logicer
				player.GiveItem(ItemType.LOGICER);

				// 1 O5 Keycard
				player.GiveItem(ItemType.O5_LEVEL_KEYCARD);

				// Frag Grenades
				for (int i = 0; i < ATTG3Plugin.Jugg_grenade; i++)
				{
					player.GiveItem(ItemType.FRAG_GRENADE);
				}

				player.PersonalClearBroadcasts();
				player.PersonalBroadcast(15, "You are the <color=#228B22>Juggernaut</color> Eliminate all <color=#002DB3>NTF Commanders</color>", false);
			}

			public static bool IsJuggernaut(Player player)
			{
				if (ATTG3Plugin.juggernaut != null)
				{
					if (player.Name == ATTG3Plugin.juggernaut.Name || player.SteamId == ATTG3Plugin.juggernaut.SteamId)
						return true;
					else
						return false;
				}
				else
					return false;
			}

			public static Player GetJuggernautPlayer()
			{
				foreach (Player player in ATTG3Plugin.plugin.pluginManager.Server.GetPlayers())
				{
					if (IsJuggernaut(player))
					{
						return player;
					}
					else
					{
						ATTG3Plugin.plugin.Warn("Juggernaut not found!");
						//ResetJuggernaut();
					}
				}
				return null;
			}

			public static Vector GetRandomPDExit()
			{
				List<Vector3> list = new List<Vector3>();
				GameObject[] exits_array = GameObject.FindGameObjectsWithTag("RoomID");
				foreach (GameObject exit in exits_array)
				{
					if (exit.GetComponent<Rid>() != null)
						list.Add(exit.transform.position);
				}

				Vector3 chosenExit = list[UnityEngine.Random.Range(0, list.Count)];

				Vector SmodExit = new Vector(chosenExit.x, chosenExit.y += 2f, chosenExit.z);
				return SmodExit;
			}

			public static void CriticalHitJuggernaut(Player player)
			{
				//Vector position = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD);
				Vector position = GetRandomPDExit();
				int damage = (int)(ATTG3Plugin.juggernaut_healh * ATTG3Plugin.jugcritical_damage);
				player.Damage(damage, DamageType.FRAG);
				player.Teleport(position);
				ATTG3Plugin.plugin.pluginManager.Server.Map.Broadcast(10, "The <color=#228B22>Juggernaut</color> take a <b>critical hit <i><color=#ff0000> -" + damage + "</color></i></b> and has been <b>transported</b> across the facility!", false);
				ATTG3Plugin.plugin.Debug("Juggernaut Disarmed & Teleported");
			}

			public static void CriticalHitJuggernaut(Player player, Player activator)
			{
				//Vector position = PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.CLASSD);
				Vector position = GetRandomPDExit();
				int damage = (int)(ATTG3Plugin.juggernaut_healh * ATTG3Plugin.jugcritical_damage);
				player.Damage(damage, DamageType.FRAG);
				player.Teleport(position);
				ATTG3Plugin.plugin.pluginManager.Server.Map.Broadcast(10, "" + activator.Name + " has sacrifieced themselves and made the <color=#228B22>Juggernaut</color> take a <b>critical hit <i><color=#ff0000> -" + damage + "</color></i></b> and has been <b>transported</b> across the facility!", false);
				ATTG3Plugin.plugin.Debug("Juggernaut Disarmed & Teleported");
			}

			public static void ResetJuggernaut(Player player)
			{
				if (ATTG3Plugin.juggernaut_prevRank != null && ATTG3Plugin.juggernaut_prevRank.Length == 2)
					player.SetRank(ATTG3Plugin.juggernaut_prevRank[0], ATTG3Plugin.juggernaut_prevRank[1]);
				else
					ATTG3Plugin.juggernaut.SetRank();
				ResetJuggernaut();
			}

			public static void ResetJuggernaut()
			{
				ATTG3Plugin.plugin.Info("Resetting Juggernaut.");
				ATTG3Plugin.juggernaut = null;
				ATTG3Plugin.juggernaut_prevRank = null;
				ATTG3Plugin.juggernaut_healh = 0;
			}

		}
	}


