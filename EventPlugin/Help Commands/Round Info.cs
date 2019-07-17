using Smod2.Commands;
using Smod2;
using Smod2.API;

namespace ATTG3
{
    class RS : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public static readonly string[] CA = new string[] { "RI", "ROUNDSINFO", "AGRI", "AGROUNDINFO" };
        public RS(ATTG3Plugin plugin)
        {
            
            this.plugin=plugin;
        }

        public string GetCommandDescription()
        {
            
            return "";
        }

        public string GetUsage()
        {
            
            return "AGRS";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            RoundStats stats = PluginManager.Manager.Server.Round.Stats;
            return new[]
            {
                
                "-------------Round Stats-------------",
				stats.ClassDAlive + "/" + stats.ClassDStart + " Class D Alive",
                stats.ClassDEscaped + " Class D Escaped",
                stats.ClassDDead + " Class D Killed",
                "",
                stats.ScientistsAlive + "/" + stats.ScientistsStart + " Scientists Alive",
                stats.ScientistsEscaped + " Scientists Escaped",
                stats.ScientistsDead + " Scientests Killed",
                "",
                stats.SCPAlive + "/" + stats.SCPStart + " SCPs Alive",
                stats.SCPDead + " Dead SCPs",
                stats.SCPKills + " SCP Kills",
                stats.Zombies + " Zombies Alive",
                "",
                stats.CiAlive +  " CI Alive",
                stats.NTFAlive + " MTF Alive",
                "",
                stats.WarheadDetonated + " Nuke Detonated",
                stats.GrenadeKills + " Grenade Kills",
                "----------------------------------------",
                "Plugin created by All The Time Gaming"
        };
        }
    }
}
