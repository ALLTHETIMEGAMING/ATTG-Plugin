using Smod2.API;
using Smod2.Commands;
using System.Linq;
using TMPro;

namespace ATTG3
{
    class GenDisable : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;

        public static TextMeshProUGUI Sign2 = new TextMeshProUGUI();

        public GenDisable(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }
        public static readonly string[] CA = new string[] { "AGGEND", "GEND", "GD" };


        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "Locks SCP-079s Generators";
        }
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.AdminRanks.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
            plugin.GenLock=!plugin.GenLock;

            return new[]
            {
                $"Generators {(plugin.GenLock ? "Unlocked" : "Locked")}."
			};


        }
    }
}
