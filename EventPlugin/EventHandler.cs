using Smod2.EventHandlers;
using Smod2.Events;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;
using System.IO;

namespace ATTG3
{
    internal class EventHandler : IEventHandlerRoundStart, IEventHandlerWarheadStopCountdown, IEventHandlerRoundEnd
    {
		private ATTG3.Filestuff FL;
		private readonly ATTG3Plugin plugin;
        public EventHandler(ATTG3Plugin plugin) => this.plugin=plugin;
        public void OnRoundStart(RoundStartEvent ev)
        {
            if (plugin.Disable)
            {
                this.plugin.pluginManager.DisablePlugin(this.plugin);
            }
            plugin.Running939=false;
            plugin.Voteopen=false;
            plugin.Yes=0;
            plugin.No=0;
			FL = new ATTG3.Filestuff();
			foreach (Room room in PluginManager.Manager.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
			{
				string zone = "Zone Type: " + room.ZoneType + Environment.NewLine;
				string room2 = "Room Type: " + room.RoomType + Environment.NewLine;
				string pos = "Position: " + room.Position + Environment.NewLine;
				string forward = "Forward: " + room.Forward + Environment.NewLine;
				string line = "------------------------------" + Environment.NewLine;
				FL.Setfile(zone);
				FL.Setfile(room2);
				FL.Setfile(pos);
				FL.Setfile(forward);
				FL.Setfile(line);
			}
			foreach (Smod2.API.TeslaGate tes in Smod2.PluginManager.Manager.Server.Map.GetTeslaGates())
			{
				string pos = "Position: " + tes.Position + Environment.NewLine;
				string line = "------------------------------" + Environment.NewLine;
				FL.Setfile1(pos);
				FL.Setfile1(line);
			}
			foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
			{
				string name = "Name: " + door.Name + Environment.NewLine;
				string pos = "Position: " + door.Position + Environment.NewLine;
				string line = "------------------------------" + Environment.NewLine;
				FL.Setfile2(name);
				FL.Setfile2(pos);
				FL.Setfile2(line);

			}

		}
        public void OnStopCountdown(WarheadStopEvent ev)
        {
            foreach (Smod2.API.Door door in Smod2.PluginManager.Manager.Server.Map.GetDoors())
            {
                if (door.Name=="CHECKPOINT_ENT")
                {
                    door.Open=true;
                    door.Locked=false;
                }
            }
        }
		public void OnRoundEnd(RoundEndEvent ev)
		{
			
			
		}
	}
}


