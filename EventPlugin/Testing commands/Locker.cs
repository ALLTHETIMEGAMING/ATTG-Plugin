using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;

namespace ATTG3
{
	class Locker1 : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public Locker1(ATTG3Plugin plugin) => this.plugin=plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "";
		//Variables Below
		int count;
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
					Vector pos = myPlayer.GetPosition();
					Vector ros = myPlayer.GetRotation();
					foreach (Locker Lock in Object.FindObjectsOfType<Locker>())
					{
						count++;
						if (count==1)
						{
							GameObject player1 = (GameObject)myPlayer.GetGameObject();
							/*plugin.Info(Lock.transform.position.ToString()+ "Locker Pos Code 1");
							plugin.Info(Lock.transform.localPosition.ToString() + "Locker Pos Code 2");
							plugin.Info(player1.transform.position.ToString() + "player for locker Pos Code 1");
							plugin.Info(player1.transform.localPosition.ToString() + "player for locker Pos Code 2");*/
							Vector3 playerpos = new Vector3(pos.x, pos.y, pos.z + 2);
							Vector3 playerros = new Vector3(ros.x, ros.y, ros.z);
							Quaternion playerqua = player1.transform.rotation;
							Transform test = player1.transform;
							Lock.NetworklocalPos = new Offset
							{
								position = test.InverseTransformPoint(playerpos),
								rotation = playerros,
								scale =Vector3.one
							};
                            //Lock.transform.SetPositionAndRotation(test.InverseTransformPoint(playerpos), playerqua);
							//Lock.transform.position = test.InverseTransformPoint(playerpos);
							//Lock.transform.localPosition = test.InverseTransformPoint(playerpos);
							//Lock.transform.rotation = playerqua;
							//Lock.transform.localRotation = playerqua;
						}
					}
					count=0;
				}
			}
			return new string[] { "Locker Moved to " };
		}
	}
}
