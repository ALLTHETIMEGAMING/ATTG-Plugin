using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine;
using MEC;
using Smod2;
namespace ATTG3
{
    class Locate : ICommandHandler
    {
        public static bool GASLCZ;
        private readonly ATTG3Plugin plugin;
        public Locate(ATTG3Plugin plugin)
        {
            this.plugin = plugin;
        }
        public string GetCommandDescription()
        {
            return "";
        }
        public string GetUsage()
        {
            return "";
        }
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!(sender is Server) &&
                sender is Player player1 &&
                !plugin.Allrank.Contains(player1.GetRankName()))
            {
                return new[]
                {
                    $"You (rank {player1.GetRankName() ?? "Server"}) do not have permissions to that command."
                };
            }
			int MTFLCZ = 0;
			int MTFHCZ = 0;
			int MTFECZ = 0;
			int CLASSDLCZ = 0;
			int CLASSDHCZ = 0;
			int CLASSDECZ = 0;
			int CILCZ = 0;
			int CIHCZ = 0;
			int CIECZ = 0;
			int SCILCZ = 0;
			int SCIHCZ = 0;
			int SCIECZ = 0;
			int SCPLCZ = 0;
			int SCPHCZ = 0;
			int SCPECZ = 0;
			foreach (Player player in PluginManager.Manager.Server.GetPlayers())
			{
				ZoneType playerZone = Events.FindRoomAtPoint(new Vector3(player.GetPosition().x, player.GetPosition().y, player.GetPosition().z));
				ATTG3Plugin.Instance.Info(player.Name + "Is in " + playerZone);
				switch (playerZone)
				{
					case ZoneType.ENTRANCE:
						break;
					case ZoneType.HCZ:
						break;
					case ZoneType.LCZ:
						break;
					case ZoneType.UNDEFINED:
						break;
				}
				if (playerZone == ZoneType.ENTRANCE)
				{
					if (player.TeamRole.Role == Role.CHAOS_INSURGENCY)
					{
						CIECZ++;
					}
					else if (player.TeamRole.Role == Role.CLASSD)
					{
						CLASSDECZ += 1;
					}
					else if (player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
					{
						MTFECZ += 1;
					}
					else if (player.TeamRole.Role == Role.SCIENTIST)
					{
						SCIECZ += 1;
					}
					else if (player.TeamRole.Team == Smod2.API.Team.SCP)
					{
						SCPECZ += 1;
					}
				}
				else if (playerZone == ZoneType.HCZ)
				{
					if (player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY)
						CIHCZ = +1;
					else if (player.TeamRole.Team == Smod2.API.Team.CLASSD)
						CLASSDHCZ = +1;
					else if (player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
						MTFHCZ = +1;
					else if (player.TeamRole.Team == Smod2.API.Team.SCIENTIST)
						SCIHCZ = +1;
					else if (player.TeamRole.Team == Smod2.API.Team.SCP)
						SCPHCZ = +1;
				}
				else if (playerZone == ZoneType.LCZ)
				{
					if (player.TeamRole.Role == Role.CHAOS_INSURGENCY)
						CILCZ += 1;
					else if (player.TeamRole.Role == Role.CLASSD)
						CLASSDLCZ += 1;
					else if (player.TeamRole.Team == Smod2.API.Team.NINETAILFOX)
						MTFLCZ += 1;
					else if (player.TeamRole.Role == Role.SCIENTIST)
						SCILCZ += 1;
					else if (player.TeamRole.Team == Smod2.API.Team.SCP)
						SCPLCZ += 1;
				}
			}
			string ann = MTFLCZ.ToString() + " M T F in Light . " + MTFHCZ.ToString() + " M T F in heavy . " + MTFECZ.ToString() + " M T F in Entrance . "
			+ CLASSDLCZ.ToString() + " CLASSD in Light . " + CLASSDHCZ.ToString() + " CLASSD in heavy . " + CLASSDECZ.ToString() + " CLASSD in Entrance . "
			+ CILCZ.ToString() + " C I in Light . " + CIHCZ.ToString() + " C I in heavy . " + CIECZ.ToString() + " C I in Entrance . "
			+ SCILCZ.ToString() + " Scientists in Light . " + SCIHCZ.ToString() + " Scientists in heavy . " + SCIECZ.ToString() + " Scientists in Entrance . "
			+ SCPLCZ.ToString() + " SCP in Light . " + SCPHCZ.ToString() + " SCP in heavy . " + SCPECZ.ToString() + " SCP in Entrance . ";
			return new[] 
            {
                ann
            };
        }
    }
}
