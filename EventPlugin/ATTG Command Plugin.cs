using scp4aiur;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Smod2.EventHandlers;
using Smod2;
using Smod2.Logging;
using Smod2.API;
using Smod2.Commands;

namespace ATTG3
{

	[PluginDetails(
		author = "All The Time Gaming",
		description = "COMMAND MOD",
		id = "ATTG.ADMIN.COMMAND",
		name = "",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0,
		version = "1.3.0"
		)]
	public class ATTG3Plugin : Smod2.Plugin
	{

		public static ATTG3Plugin Instance { get; private set; }
		public bool Running939P { get; set; }
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
		public bool Lights { get; set; }
        public bool GenLock { get; set; }
        public bool O49infect { get; set; }
		public bool O96Door { get; set; }
		public bool NoCHand { get; set; }
		public float Elevatord { get; set; }
        public string PlayerLD { get; set; }
        public bool Lockdownact { get; set; }
        public string PlayerUD { get; set; }
        public bool ULockdownact { get; set; }
        public override void Register()
		{
			Instance=this;
			Timing.Init(this);
			Timing2.Init(this);
			// Configs
			AddConfig(new ConfigSetting("attg_ranks", new[]{"owner","coowner"}, false, true, ""));
			AddConfig(new ConfigSetting("attg_command_disable_ranks", new[]{"owner"}, false, true, "Valid ranks to disable the Event Plugin"));
			AddConfig(new ConfigSetting("attg_scp_ranks", new[]{"owner","coowner"}, false, true, "Ranks for all SCP Commands"));
			AddConfig(new ConfigSetting("attg_vote_ranks", new[]{"owner","coowner","admin"},false, true, "Valid ranks for all voteing Commands"));
			AddConfig(new ConfigSetting("attg_all_ranks", new[]{"owner"}, false, true, "Valid ranks for all Commands"));
			AddConfig(new ConfigSetting("attg_049_infect", false, false, true, "Makes SCP-049 revive instantly"));
			AddConfig(new ConfigSetting("attg_096_door", true, false, true, "Makes SCP-096 able to open all doors when enraged"));
			AddConfig(new ConfigSetting("attg_card_hand", true, false, true, "Makes all players able to open keycard doors with out a keycard in hand"));
			AddConfig(new ConfigSetting("attg_elevator_speed", 1f, false, true, "Makes all players able to open keycard doors with out a keycard in hand"));

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
			this.AddCommand("AG079T", new GenTime(this));
            this.AddCommand("AGLights", new Overcharge(this));
            this.AddCommand("AGGEND", new GenDisable(this));
            this.AddCommand("AGTH", new ItemH(this));
            this.AddCommand("AGIH", new Teams(this));
            this.AddCommand("AGLOCK", new Lock(this));
            this.AddCommand("AGULOCK", new Unlock(this));
            this.AddCommands(RS.CA, new RS(this));
            this.AddCommand("AGG", new Gthrow(this));
            //is.AddCommand("AGHAND", new Handcuff(this));
            //is.AddCommand("AGUNHAND", new Unhandcuff(this));
            this.AddEventHandlers(new EventHandler(this), Priority.Highest);
			this.AddEventHandlers(new Vote(this));


		}
		public void ReloadConfig()
		{
			// Command Perms
			AdminRanks=GetConfigList("attg_ranks");
			Voterank=GetConfigList("attg_vote_ranks");
			SCPrank=GetConfigList("attg_scp_ranks");
			Allrank=GetConfigList("attg_all_ranks");
			//Dissable Config
			Disable=GetConfigBool("attg_command_disable");
			// Other
			O49infect=GetConfigBool("attg_049_infect");
			O96Door=GetConfigBool("attg_096_door");
			NoCHand=GetConfigBool("attg_card_hand");
			Elevatord=GetConfigFloat("attg_elevator_speed");
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


