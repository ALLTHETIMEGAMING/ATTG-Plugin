using scp4aiur;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using Smod2;
using Smod2.API;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEC;
using UnityEngine;
using System.Linq;

namespace ATTG3
{
	[PluginDetails(
		author = "All The Time Gaming",
		description = "COMMAND MOD",
		id = "ATTG.ADMIN.COMMAND",
		name = "ATTG Admin",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 1,
		version = "4.0.0"
		)]
	public class ATTG3Plugin : Smod2.Plugin
	{
		public static ATTG3Plugin Instance { get; private set; }
		#region Values
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
		public static int itemid;
		public float Elevatord { get; set; }
		public bool GenSpam { get; set; }
		public bool GenHand { get; set; }
		public bool Running939P { get; set; }
		public bool Running939 { get; set; }
		public bool RoundStarted { get; set; }
		public bool Debuging { get; set; }
		public string[] UNO { get; set; }
		public bool UnoEnabled { get; set; }

		//Events Below
		public bool Event { get; set; }
		public bool Infectcontain { get; set; }
		public bool MTFSCP { get; set; }
		public bool VIP { get; set; }
		public bool O79Event { get; set; }
		public bool ClassD { get; set; }
		public bool Lerk { get; set; }
		public bool VicEvent { get; set; }
		public bool MTFCI { get; set; }
		public bool INFECT { get; set; }
		public bool QEvent { get; set; }
		#endregion
		public static List<ItemType> Randoimitem = new List<ItemType>();
		public static List<Vector3> TPRooms = new List<Vector3>();
        public static List<Vector3> NoRooms = new List<Vector3>();
        public static List<Vector3> NoRoomTP = new List<Vector3>();
        //End of Events
        public override void Register()
		{
			Instance = this;
			scp4aiur.Timing.Init(this);
			// Configs
			AddConfig(new ConfigSetting("attg_ranks", new[] { "owner", "coowner", "o51" }, false, true, ""));
			AddConfig(new ConfigSetting("attg_event_ranks", new[] { "owner", "coowner" }, false, true, "Ranks for all SCP Commands"));
			AddConfig(new ConfigSetting("attg_vote_ranks", new[] { "owner", "coowner", "admin", "o51" }, false, true, "Valid ranks for all voteing Commands"));
			AddConfig(new ConfigSetting("attg_all_ranks", new[] { "owner" }, false, true, "Valid ranks for all Commands"));
			AddConfig(new ConfigSetting("attg_049_infect", false, false, true, "Allows SCP-049 revive instantly"));
			AddConfig(new ConfigSetting("attg_096_door", false, false, true, "Allows SCP-096 able to open all doors when enraged"));
			AddConfig(new ConfigSetting("attg_door_hand", true, false, true, "Allows all players able to open keycard doors with out a keycard in hand"));
			AddConfig(new ConfigSetting("attg_gen_hand", true, false, true, "Allows all players able to open Generators with out a keycard in hand"));
			AddConfig(new ConfigSetting("attg_elevator_speed", 1f, false, true, "Default Elevator speed"));
			AddConfig(new ConfigSetting("attg_uno_user", new[] { "76561198126860363" }, false, true, "User can not be banned"));
			AddConfig(new ConfigSetting("attg_uno", true, false, true, "if uno user can not be banned"));

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
			// EVENTS
			this.AddCommands(EventMainCommand.CA, new EventMainCommand(this));


			//END OF EVENTS
			this.AddCommand("AGFAKE", new Fakedea(this));
			this.AddCommand("AGAMMO", new Ammo(this));
			this.AddCommand("AGBLAST", new Blast(this));
			this.AddCommand("AGLOCKER", new Locker1(this));
			this.AddCommand("AGWORK", new Work(this));
			this.AddCommand("AGGENM", new Genm(this));
			this.AddCommand("AGRANK", new Rank(this));
			this.AddCommand("AGTFF", new TFF(this));
			this.AddCommand("AGTP", new Teleport(this));
			this.AddCommands(Sniper.CA, new Sniper(this));
			//Event Handlers
			this.AddEventHandlers(new EventHandler(this), Priority.Normal);
			this.AddEventHandlers(new O79Handler(this), Priority.High);
			this.AddEventHandlers(new lerk(this), Priority.High);
			this.AddEventHandlers(new MTFCI(this), Priority.High);
			this.AddEventHandlers(new INFECT(this), Priority.High);
			this.AddEventHandlers(new INFECTCon(this), Priority.High);
			this.AddEventHandlers(new SCPMTF(this), Priority.High);
			this.AddEventHandlers(new VIPESCAPE(this), Priority.High);
			this.AddEventHandlers(new Question(this), Priority.High);
			this.AddEventHandlers(new PlayerConsole(this));
		}
		public void ReloadConfig()
		{
			// Command Perms
			AdminRanks = GetConfigList("attg_ranks");
			Voterank = GetConfigList("attg_vote_ranks");
			Eventrank = GetConfigList("attg_event_ranks");
			Allrank = GetConfigList("attg_all_ranks");
			//Dissable Config
			Disable = GetConfigBool("attg_command_disable");
			// Other
			O49infect = GetConfigBool("attg_049_infect");
			O96Door = GetConfigBool("attg_096_door");
			NoCHand = GetConfigBool("attg_door_hand");
			GenHand = GetConfigBool("attg_gen_hand");
			Elevatord = GetConfigFloat("attg_elevator_speed");
			UnoEnabled = GetConfigBool("attg_uno");
			UNO = GetConfigList("attg_uno_user");
		}
		public override void OnEnable()
		{
			Info("ATTG Command Plugin enabled.");
			Randoimitem.Add(ItemType.E11_STANDARD_RIFLE);
			Randoimitem.Add(ItemType.LOGICER);
		}
		public override void OnDisable()
		{
			Info("ATTG Command Plugin disabled.");
		}
	}
	public class Events
	{
		public static ItemType Invrandgive()
		{
			int RandomInt = new System.Random().Next(ATTG3Plugin.Randoimitem.Count);
			return ATTG3Plugin.Randoimitem[RandomInt];
		}
		public static IEnumerator<float> MTFCIRESPAWN(Player player,Player Attacker)
		{

			foreach (Smod2.API.Item item in player.GetInventory())
			{
				if (item.ItemType == ItemType.FRAG_GRENADE)
				{
					player.ThrowGrenade(GrenadeType.FRAG_GRENADE, false, Attacker.GetPosition(), true, player.GetPosition(), true, 0f, false);
				}
				item.Remove();
			}
            if (player.TeamRole.Role == Smod2.API.Role.CHAOS_INSURGENCY)
			{
				MTFCI.MTFKill++;
				PluginManager.Manager.Server.Map.ClearBroadcasts();
				PluginManager.Manager.Server.Map.Broadcast(5, "<color=#0080FF>MTF Has " + MTFCI.MTFKill + " Kills out of " + MTFCI.KillGoal + "</Color> <color=#0B7A00>CI Has " + MTFCI.CIKills + " Kills out of " + MTFCI.KillGoal + "</Color>", false);
				yield return MEC.Timing.WaitForSeconds(10);
				player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
			}
			else if (player.TeamRole.Role == Smod2.API.Role.NTF_COMMANDER)
			{
				MTFCI.CIKills++;
				PluginManager.Manager.Server.Map.ClearBroadcasts();
				PluginManager.Manager.Server.Map.Broadcast(5, "<color=#0080FF>MTF Has " + MTFCI.MTFKill + " Kills out of " + MTFCI.KillGoal + "</Color> <color=#0B7A00>CI Has " + MTFCI.CIKills + " Kills out of " + MTFCI.KillGoal + "</Color>", false);
				yield return MEC.Timing.WaitForSeconds(10);
				player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
			}
			
		}

		public static IEnumerator<float> GiveAmmo(Player player)
		{
			yield return MEC.Timing.WaitForSeconds(1f);
			player.SetAmmo(AmmoType.DROPPED_5, 1000);
			player.SetAmmo(AmmoType.DROPPED_7, 1000);
			player.SetAmmo(AmmoType.DROPPED_9, 1000);
		}
		public static int TUTCOUNT(Role role)
		{
			int rolecount = 0;
			foreach (Player player in PluginManager.Manager.Server.GetPlayers())
			{
				if (player.TeamRole.Role == role)
				{
					rolecount++;
				}
			}
			return rolecount;
		}
		public static void Inventoryset(Player player, int invpos)
		{
			List<Smod2.API.Item> inventory = player.GetInventory();
			#region Itemids
			ItemType inv1 = inventory[invpos].ItemType;
			if (inv1 == ItemType.JANITOR_KEYCARD)
			{
				ATTG3Plugin.itemid = 0;
			}
			else if (inv1 == ItemType.SCIENTIST_KEYCARD)
			{
				ATTG3Plugin.itemid = 1;
			}
			else if (inv1 == ItemType.MAJOR_SCIENTIST_KEYCARD)
			{
				ATTG3Plugin.itemid = 2;
			}
			else if (inv1 == ItemType.ZONE_MANAGER_KEYCARD)
			{
				ATTG3Plugin.itemid = 3;
			}
			else if (inv1 == ItemType.GUARD_KEYCARD)
			{
				ATTG3Plugin.itemid = 4;
			}
			else if (inv1 == ItemType.SENIOR_GUARD_KEYCARD)
			{
				ATTG3Plugin.itemid = 5;
			}
			else if (inv1 == ItemType.CONTAINMENT_ENGINEER_KEYCARD)
			{
				ATTG3Plugin.itemid = 6;
			}
			else if (inv1 == ItemType.MTF_LIEUTENANT_KEYCARD)
			{
				ATTG3Plugin.itemid = 7;
			}
			else if (inv1 == ItemType.MTF_COMMANDER_KEYCARD)
			{
				ATTG3Plugin.itemid = 8;
			}
			else if (inv1 == ItemType.FACILITY_MANAGER_KEYCARD)
			{
				ATTG3Plugin.itemid = 9;
			}
			else if (inv1 == ItemType.CHAOS_INSURGENCY_DEVICE)
			{
				ATTG3Plugin.itemid = 10;
			}
			else if (inv1 == ItemType.O5_LEVEL_KEYCARD)
			{
				ATTG3Plugin.itemid = 11;
			}
			else if (inv1 == ItemType.RADIO)
			{
				ATTG3Plugin.itemid = 12;
			}
			else if (inv1 == ItemType.COM15)
			{
				ATTG3Plugin.itemid = 13;
			}
			else if (inv1 == ItemType.MEDKIT)
			{
				ATTG3Plugin.itemid = 14;
			}
			else if (inv1 == ItemType.FLASHLIGHT)
			{
				ATTG3Plugin.itemid = 15;
			}
			else if (inv1 == ItemType.MICROHID)
			{
				ATTG3Plugin.itemid = 16;
			}
			else if (inv1 == ItemType.COIN)
			{
				ATTG3Plugin.itemid = 17;
			}
			else if (inv1 == ItemType.CUP)
			{
				ATTG3Plugin.itemid = 18;
			}
			else if (inv1 == ItemType.WEAPON_MANAGER_TABLET)
			{
				ATTG3Plugin.itemid = 19;
			}
			else if (inv1 == ItemType.E11_STANDARD_RIFLE)
			{
				ATTG3Plugin.itemid = 20;
			}
			else if (inv1 == ItemType.P90)
			{
				ATTG3Plugin.itemid = 21;
			}
			else if (inv1 == ItemType.DROPPED_5)
			{
				ATTG3Plugin.itemid = 22;
			}
			else if (inv1 == ItemType.MP4)
			{
				ATTG3Plugin.itemid = 23;
			}
			else if (inv1 == ItemType.LOGICER)
			{
				ATTG3Plugin.itemid = 24;
			}
			else if (inv1 == ItemType.FRAG_GRENADE)
			{
				ATTG3Plugin.itemid = 25;
			}
			else if (inv1 == ItemType.FLASHBANG)
			{
				ATTG3Plugin.itemid = 26;
			}
			else if (inv1 == ItemType.DISARMER)
			{
				ATTG3Plugin.itemid = 27;
			}
			else if (inv1 == ItemType.DROPPED_7)
			{
				ATTG3Plugin.itemid = 28;
			}
			else if (inv1 == ItemType.DROPPED_9)
			{
				ATTG3Plugin.itemid = 29;
			}
			else if (inv1 == ItemType.USP)
			{
				ATTG3Plugin.itemid = 30;
			}
			else if (inv1 == ItemType.NULL)
			{
				ATTG3Plugin.itemid = -1;
			}
			#endregion
			GameObject playerobject = (GameObject)player.GetGameObject();
			Inventory plainv = playerobject.GetComponent<Inventory>();
			plainv.SetCurItem(ATTG3Plugin.itemid);
        }
        public static void Noroom()
        {
            #region Noroom
            foreach (Room room in PluginManager.Manager.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Where(x => x.ZoneType == ZoneType.LCZ).ToArray())
            {
                if (room.RoomType == RoomType.AIRLOCK_00)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.ENTRANCE_CHECKPOINT)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.CHECKPOINT_B)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.CHECKPOINT_A)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.GATE_A)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.GATE_B)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.NUKE)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.MICROHID)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.UNDEFINED)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.SCP_914)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.SCP_106)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
            }
            #endregion
        }
        public static void GetRoundStartRoom()
        {
            GameObject[] array = GameObject.FindGameObjectsWithTag("RoomID");
            foreach (GameObject val2 in array)
            {
                if (val2.GetComponent<Rid>() != null)
                {
                    ATTG3Plugin.NoRoomTP.Add(val2.transform.position);
                }
            }
        }
        public static void RemoveNoRooms()
        {
            foreach (Vector3 vector3 in ATTG3Plugin.NoRoomTP)
            {
                if (ATTG3Plugin.NoRooms.Contains(vector3))
                {
                    ATTG3Plugin.NoRoomTP.Remove(vector3);
                }
            }
        }
        public static void RemoveRandRoomTP(Player player)
        {
            Vector3 val4 = ATTG3Plugin.NoRoomTP[Random.Range(0, ATTG3Plugin.NoRoomTP.Count)];
            val4.y += 2f;
            Vector TORoom = new Vector(val4.x, val4.y, val4.z);
            player.Teleport(TORoom, true);
            ATTG3Plugin.TPRooms.Remove(val4);
        }
        public static void FullRandRoomTP(Player player)
		{
            Vector3 val4 = ATTG3Plugin.TPRooms[Random.Range(0, ATTG3Plugin.TPRooms.Count)];
			val4.y += 2f;
			Vector TORoom = new Vector(val4.x, val4.y, val4.z);
			player.Teleport(TORoom, true);
		}
	}
}
