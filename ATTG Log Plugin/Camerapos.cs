using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace ATTG_Test
{
	class Data_Command : ICommandHandler
	{
		private readonly ATTGLogPlugin plugin;
		private Filestuff FL;
		public object FilePath { get; private set; }

		public Data_Command(ATTGLogPlugin plugin)
		{

			FL = new Filestuff();
			//Constructor passing plugin refrence to this class
			this.plugin = plugin;
		}



		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "";
		}
		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "";
		}
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			
			if (!(sender is Server) &&
				sender is Player player &&
				!plugin.AdminRanks.Contains(player.GetRankName()))
			{
				return new[]
				{
					$"You (rank {player.GetRankName() ?? "Server"}) do not have permissions to that command."
				};
			}

			foreach (Room room in PluginManager.Manager.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA))
			{

				
				string zone = "Zone Type: " + room.ZoneType + Environment.NewLine;
				string room2 = "Room Type: " + room.RoomType + Environment.NewLine;
				string pos = "Position: " + room.Position + Environment.NewLine;
				string GenID = "Generic ID: " + room.GenericID + Environment.NewLine;
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


			return new[]
			{ "Done"};
			
		}
	}
}

