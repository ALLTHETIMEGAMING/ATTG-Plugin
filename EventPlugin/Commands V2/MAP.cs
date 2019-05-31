using scp4aiur;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;

namespace ATTG3
{
    class MAP : ICommandHandler
    {
        private readonly ATTG3Plugin plugin;
        public MAP(ATTG3Plugin plugin) { this.plugin=plugin; }
        public string GetCommandDescription()
        {
            return "";
        }
        public string GetUsage()
        {
            return "All Map Commands";
        }
        public static readonly string[] CA = new string[] { "AGMAP", "MAP" };
		public static bool Shake;
		public static bool Tleslad;
		public static bool Tleslas;
		public static float Desplay2;
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
            if (args.Length>0)
            {
                string args2 = args[0].ToLower();
                if (args2=="lae")
                {
                    foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
                    {
                        if (Elevator.Locked==false)
                        {
                            Elevator.Locked=true;
                        }
                    }
                    return new string[] { "All Elevators Locked" };
                }
                else if (args2=="uae")
                {
                    foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
                    {
                        if (Elevator.Locked==true)
                        {
                            Elevator.Locked=false;
                        }
                    }
                    return new string[] { "All Elevators Unocked" };
                }
                else if (args2=="shake")
                {
                    Shake=!Shake;
                    if (Shake)
                    {
                        Timing.Run(TimingDelay(0.1f));
                    }
                    return new[] { $"Map Shake is {(Shake ? "On" : "Off")}." };
                }
                else if (args2=="tr")
                {
                    Tleslad=!Tleslad;
                    if (Tleslad)
                    {
                        foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
                        {
                            TeslaGate.TriggerDistance=0;
                        }
                    }
                    if (!Tleslad)
                    {
                        foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
                        {
                            TeslaGate.TriggerDistance=5.5f;
                        }
                    }
                    return new[]
                    {
                     $"all Tleslas are now {(Tleslad ? "DEACTAVATED" : "ACTAVATED")}."
                    };
                }
				else if (args2=="ts")
				{
					Tleslas=!Tleslas;
					if (Tleslas)
					{
						Timing.Run(TimingDelay2(0.1f));
					}
					return new[]
					{
					 $"Tlesla spam {(Tleslad ? "ACTAVATED" : "DEACTAVATED")}."
					};
				}
				else if (args2=="in")
				{
					
					return new[] { " " };
				}
				else if (args2=="es")
                {
                    if (args.Length>1)
                    {
                        foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
                        {
                            float converted = float.Parse(args[1]);
                            Elevator.MovingSpeed=converted;
                            Desplay2=converted;
                        }
                    }
                    else
                    {
                        foreach (Smod2.API.Elevator Elevator in Smod2.PluginManager.Manager.Server.Map.GetElevators())
                        {
                            Elevator.MovingSpeed=5;
                            Desplay2=5;
                        }
                    }
                    return new[]
                    {
                        $"Elevator Speed set to {(Desplay2)}"
                    };
                }
                else
                {
                    return new[]
                    {
                        CA.First() + " Help" + " Shows this",
                        CA.First() + " Lae" + " Locks all Elevator.",
                        CA.First() + " Uae" + " Unlocks all Elevators.",
                        CA.First() + " Shake" + " Shakes Map.",
                        CA.First() + " TR" + " Actavates/Deactavates all Tesla gates.",
                        CA.First() + " TS" + " Spams the tlesla gates.",
                        CA.First() + " ES" + " Number" + " Sets speed for all elevators.",
                    };
                }
            }
            else
            {
                return new[]
                     {
                     CA.First() + " Help" + " Shows this",
                     CA.First() + " Lae" + " Locks all Elevator.",
                     CA.First() + " Uae" + " Unlocks all Elevators.",
                     CA.First() + " Shake" + " Shakes Map.",
                     CA.First() + " TR" + " Actavates/Deactavates all Tesla gates.",
                     CA.First() + " TS" + " Spams the tlesla gates.",
                     CA.First() + " ES" + " Number" + " Sets speed for all elevators.",
                    };
            }
        }
        private IEnumerable<float> TimingDelay(float time)
        {
            while (Shake)
            {
                plugin.Server.Map.Shake();
                yield return 3f;
            }
        }
        private IEnumerable<float> TimingDelay2(float time)
        {
            while (Tleslas)
            {
                foreach (Smod2.API.TeslaGate TeslaGate in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
                {
                    TeslaGate.Activate(true);
                }
                yield return 0.5f;
            }
        }
    }
}
