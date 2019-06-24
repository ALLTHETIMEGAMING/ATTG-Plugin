using scp4aiur;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace ATTG3
{
    class GenSpam1 : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;

        public static TextMeshProUGUI Sign2 = new TextMeshProUGUI();
        public static readonly string[] CA = new string[] { "GENSPAM", "AGGENSPAM", "GS" };

        public GenSpam1(ATTG3Plugin plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin=plugin;
        }
        public float converted;
        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "";
        }
        public string GetUsage()
        {
            // This prints when someone types HELP HELLO
            return "Locks SCP-079S Generators";
        }
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server)&&
                sender is Player player&&
                !plugin.Allrank.Contains(player.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
			plugin.GenSpam=!plugin.GenSpam;
            if (plugin.GenSpam)
            {
                if (args.Length>0)
                {
                    converted=float.Parse(args[1]);
                }
                else
                {
                    converted=10f;
                }
                Timing.Run(TimingDelay(converted));
            }
            return new[]
            {
                $"Generator Spam {(plugin.GenSpam ? "On" : "Off")}."
			};
        }
		private IEnumerable<float> TimingDelay(float time)
		{
			while (plugin.GenSpam)
			{
				foreach (Generator079 gen in Generator079.generators)
				{
					gen.NetworkisTabletConnected=true;
					gen.EjectTablet();

				}
				yield return converted;
			}
		}
	}
}
