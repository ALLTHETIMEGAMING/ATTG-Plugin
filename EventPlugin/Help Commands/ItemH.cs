using Smod2.Commands;
namespace ATTG3
{
    class ItemH : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;

        public ItemH(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Prints All Items in SCP:SL";
        }

        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "AGIH";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            return new[]
            {
                "ALL ITEMS IN SCPSL",
                "-------------ITEMS-------------",
				"ID       Name                             Description",
				"0       | JANITOR_KEYCARD               | Janitor Keycard",
				"1       | SCIENTIST_KEYCARD             | Scientist Keycard",
				"2       | MAJOR_SCIENTIST_KEYCARD       | Major Scientist Keycard",
				"3       | ZONE_MANAGER_KEYCARD          | Zone Manager Keycard",
				"4       | GUARD_KEYCARD                 | Guard Keycard",
				"5       | SENIOR_GUARD_KEYCARD          | Senior Guard Keycard",
				"6       | CONTAINMENT_ENGINEER_KEYCARD  | Containment Engineer Keycard",
				"7       | MTF_LIEUTENANT_KEYCARD        | MTF Lieutenant Keycard",
				"8       | MTF_COMMANDER_KEYCARD         | MTF Commander Keycard",
				"9       | FACILITY_MANAGER_KEYCARD      | Facility Manager Keycard",
				"10      | CHAOS_INSURGENCY_DEVICE       | Chaos Card",
				"11      | O5_LEVEL_KEYCARD              | 05 Card",
				"12      | RADIO                         | Radio",
				"13      | COM15                         | Com15 Pistol",
				"14      | MEDKIT                        | Medkit",
				"15      | FLASHLIGHT                    | Flashlight",
				"16      | MICROHID                      | MicroHID",
				"17      | COIN                          | Coin",
				"18      | CUP                           | Cup",
				"19      | WEAPON_MANAGER_TABLET         | Weapon Manager Tablet",
				"20      | E11_STANDARD_RIFLE            | Epsilon-11 Standard Rifle",
				"21      | P90                           | P-90 ",
				"22      | DROPPED_5                     | Epsilon ammo",
				"23      | MP4                           | MP7",
				"24      | LOGICER                       | Logicer",
				"25      | FRAG_GRENADE                  | Grenade",
				"26      | FLASHBANG                     | Flash Grenade",
				"27      | DISARMER                      | Detainer",
				"28      | DROPPED_7                     | MP7/Logicer ammo",
				"29      | DROPPED_9                     | Pistol/P90/USP ammo",
				"30      | USP                           | USP",
				"Item List Created by KarlofDuty",
				"----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
