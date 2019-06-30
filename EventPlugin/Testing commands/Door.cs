﻿using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


namespace ATTG3
{
	class DoorMain : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public DoorMain(ATTG3Plugin plugin) => this.plugin=plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "";
		//Variables Below
		public Door door1;
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
			if (args.Length>0)
			{
				Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
				if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
				if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
				{
					
					//Door[] doors = Object.FindObjectsOfType<Door>();
					//Random rnd = new Random();
					//int index = Random.Range(0, doors.Length);
					//Door door = doors[index];
					foreach (Door door in Object.FindObjectsOfType<Door>())
					{
						if (door.DoorName == "096")
						{
							door1 = door;
						}
					}
                    GameObject player1 = (GameObject)myPlayer.GetGameObject();
					// Test 1
					Transform test = player1.transform;
					Vector pos = myPlayer.GetPosition();
					
					 plugin.Info(door1.localPos.ToString() + "Door POS Part 1");
					plugin.Info(pos.ToString() + " Player Pos");
					door1.localPos = test.InverseTransformPoint(new Vector3(pos.x, pos.y, pos.z + 2));
					plugin.Info(door1.localPos.ToString() + "Door end POS Part 1");
					
					//plugin.Info(door1.transform.position.ToString() + "Door POS Part 2");
					//door1.transform.position = test.TransformPoint(new Vector3(pos.x, pos.y, pos.z + 2));
					//plugin.Info(door1.transform.position.ToString() + "Door end POS Part 2");
					/*
					door.transform.SetPositionAndRotation(test.InverseTransformPoint(new Vector3(pos.x, pos.y, pos.z + 2)), Quaternion.identity);
					plugin.Info(door.transform.localPosition.ToString() + "Door POS Part 3");
					door.transform.localPosition = test.InverseTransformPoint(new Vector3(pos.x, pos.y, pos.z + 2));
					plugin.Info(door.transform.localPosition.ToString() + "Door end POS Part 3");
					*/
					door1.SetLocalPos();
					door1.UpdatePos();
				}
			}
			return new string[] { "Door Moved" };
		}
	}
}
