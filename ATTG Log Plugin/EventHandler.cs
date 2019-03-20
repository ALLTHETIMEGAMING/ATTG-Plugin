using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ATTG_Test
{
    internal class EventHandler : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandler079CameraTeleport, IEventHandlerSpawn
    {
        private ATTG_Test.Filestuff FL;
        private readonly ATTGLogPlugin plugin;
        public EventHandler(ATTGLogPlugin plugin) => this.plugin=plugin;

        public void OnRoundStart(RoundStartEvent ev)
        {
            if (plugin.Disable)
            {
                this.plugin.pluginManager.DisablePlugin(this.plugin);
            }
        }
        public void OnRoundEnd(RoundEndEvent ev)
        {
            if (plugin.ServerData)
            {
				FL=new ATTG_Test.Filestuff();
				foreach (Room room in PluginManager.Manager.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
                {

                    string Round = "Round: "+plugin.Server.Round+Environment.NewLine;
                    string Map = "Map: "+plugin.Server.Map+Environment.NewLine;
                    string zone = "Zone Type: "+room.ZoneType+Environment.NewLine;
                    string room2 = "Room Type: "+room.RoomType+Environment.NewLine;
                    string pos = "Position: "+room.Position+Environment.NewLine;
                    string forward = "Forward: "+room.Forward+Environment.NewLine;
                    string line = "------------------------------"+Environment.NewLine;
                    FL.Setfile(Round);
                    FL.Setfile(Map);
                    FL.Setfile(zone);
                    FL.Setfile(room2);
                    FL.Setfile(pos);
                    FL.Setfile(forward);
                    FL.Setfile(line);

                }
                foreach (Smod2.API.TeslaGate tes in PluginManager.Manager.Server.Map.GetTeslaGates())
                {

                    string Round = "Round: "+plugin.Server.Round+Environment.NewLine;
                    string Map = "Map: "+plugin.Server.Map+Environment.NewLine;
                    string pos = "Position: "+tes.Position+Environment.NewLine;
                    string line = "------------------------------"+Environment.NewLine;
                    FL.Setfile1(Round);
                    FL.Setfile1(Map);
                    FL.Setfile1(pos);
                    FL.Setfile1(line);

                }
                foreach (Smod2.API.Door door in PluginManager.Manager.Server.Map.GetDoors())
                {

                    string Round = "Round: "+plugin.Server.Round+Environment.NewLine;
                    string Map = "Map: "+plugin.Server.Map+Environment.NewLine;
                    string name = "Name: "+door.Name+Environment.NewLine;
                    string pos = "Position: "+door.Position+Environment.NewLine;
                    string line = "------------------------------"+Environment.NewLine;
                    FL.Setfile2(Round);
                    FL.Setfile2(Map);
                    FL.Setfile2(name);
                    FL.Setfile2(pos);
                    FL.Setfile2(line);


                }
                foreach (Generator Generator in PluginManager.Manager.Server.Map.GetGenerators())
                {

                    string Round = "Round: "+plugin.Server.Round+Environment.NewLine;
                    string Map = "Map: "+plugin.Server.Map+Environment.NewLine;
                    string room2 = "Room Type: "+Generator.Room+Environment.NewLine;
                    string pos = "Position: "+Generator.Position+Environment.NewLine;
                    string line = "------------------------------"+Environment.NewLine;
                    FL.Setfile3(Round);
                    FL.Setfile3(Map);
                    FL.Setfile3(room2);
                    FL.Setfile3(pos);
                    FL.Setfile3(line);

                }
                foreach (Elevator elevator in PluginManager.Manager.Server.Map.GetElevators())
                {
                    List<Vector> ele = elevator.GetPositions();
                    string Round = "Round: "+plugin.Server.Round+Environment.NewLine;
                    string Map = "Map: "+plugin.Server.Map+Environment.NewLine;
                    string pos = "Position: "+ ele + Environment.NewLine;
                    string line = "------------------------------"+Environment.NewLine;
                    FL.Setfile6(Round);
                    FL.Setfile6(Map);
                    FL.Setfile6(pos);
                    FL.Setfile6(line);

                }
                Dictionary<Vector, Vector> eletele = new Dictionary<Vector, Vector>();
                eletele=PluginManager.Manager.Server.Map.GetElevatorTeleportPoints();
                foreach (KeyValuePair<Vector, Vector> eleteleport in eletele)
                {
                    
                    string Round = "Round: "+plugin.Server.Round+Environment.NewLine;
                    string Map = "Map: "+plugin.Server.Map+Environment.NewLine;
                    string pos = "Position: " + eleteleport.ToString() + Environment.NewLine;
                    string line = "------------------------------"+Environment.NewLine;
                    FL.Setfile7(Round);
                    FL.Setfile7(Map);
                    FL.Setfile7(pos);
                    FL.Setfile7(line);
                }
            }
        }
        public void On079CameraTeleport(Player079CameraTeleportEvent ev)
        {
            if (plugin.ServerData)
            {

                string Round = "Round: "+plugin.Server.Round+Environment.NewLine;
                string Map = "Map: "+plugin.Server.Map+Environment.NewLine;
                string Pos = "Camera POS: "+ev.Camera+Environment.NewLine;
                string line = "------------------------------"+Environment.NewLine;
                FL.Setfile4(Round);
                FL.Setfile4(Map);
                FL.Setfile4(Pos);
                FL.Setfile4(line);
            }
        }
        public void OnSpawn(PlayerSpawnEvent ev)
        {
            if (plugin.ServerData)
            {
                GameObject gameObject = GameObject.Find("Server");
                int seed = -1;
                seed=gameObject.GetComponent<RandomSeedSync>().seed;
                string Round = "Round: "+plugin.Round+Environment.NewLine;
                string Seed = "Round: "+seed+Environment.NewLine;
                string Pos = "Spawn POS:"+ev.Player.GetPosition();
                string line = "------------------------------"+Environment.NewLine;
                FL.Setfile5(Round);
                FL.Setfile5(Seed);
                FL.Setfile5(Pos);
                FL.Setfile5(line);
            }
        }
    }
}


