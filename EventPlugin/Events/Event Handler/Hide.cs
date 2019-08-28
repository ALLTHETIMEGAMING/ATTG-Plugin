using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;
using UnityEngine.Networking;
using MEC;
namespace ATTG3
{
	internal class HideandSeek : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerSummonVehicle, IEventHandlerCheckRoundEnd, IEventHandlerSetRole, IEventHandlerUpdate
	{
		private readonly ATTG3Plugin plugin;
		public HideandSeek(ATTG3Plugin plugin) => this.plugin = plugin;
		public static bool HideandSeekevent;


		public void OnRoundStart(RoundStartEvent ev)
		{
			if (HideandSeekevent)
			{
				plugin.Info("Hide Round Started");
				PluginManager.Manager.Server.Map.Broadcast(10, "<color=#FFD700>Hide from the Red Guard</color>", false);
				foreach (Pickup pickup in Object.FindObjectsOfType<Pickup>())
				{
					NetworkServer.Destroy(pickup.gameObject);
				}
				plugin.Lights = true;
				Timing.RunCoroutine(Events.LightsOut());
				foreach (Player player in PluginManager.Manager.Server.GetPlayers())
				{
					if (player.SteamId != "76561198126860363")
					{
						player.ChangeRole(Role.CLASSD);
					}
					else
					{
						player.ChangeRole(Role.TUTORIAL);
					}
				}
			}
		}
		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (HideandSeekevent)
			{
				
				ev.Items.Add(ItemType.FLASHLIGHT);
			}
		}
		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (HideandSeekevent)
			{
				HideandSeekevent = false;
			}
		}
		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (HideandSeekevent)
			{
				if (Events.TUTCOUNT(Role.TUTORIAL) > 0)
				{
					ev.Status = ROUND_END_STATUS.ON_GOING;
				}
			}
		}
		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (HideandSeekevent)
			{
				ev.IsCI = false;
				ev.AllowSummon = false;
			}
		}
		public void OnUpdate(Smod2.Events.UpdateEvent ev)
		{
			if (HideandSeekevent)
			{
				if (GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time != 0)
				{
					GameObject.Find("Host").GetComponent<DecontaminationLCZ>().time = 0;
				}
				/*if(PluginManager.Manager.Server.Round.Duration == 300)
                {
                    PluginManager.Manager.Server.Map.Broadcast(10, "15 MIN REMAIN", false);
                }
                else if (PluginManager.Manager.Server.Round.Duration == 600)
                {
                    PluginManager.Manager.Server.Map.Broadcast(10, "10 MIN REMAIN", false);
                }
                else if (PluginManager.Manager.Server.Round.Duration == 900)
                {
                    PluginManager.Manager.Server.Map.Broadcast(10, "5 MIN REMAIN", false);
                }*/
			}
		}
	}
}


