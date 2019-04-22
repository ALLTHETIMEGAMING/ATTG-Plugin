using Smod2;
using Smod2.API;
using Smod2.Commands;
using System.Linq;
using UnityEngine;

namespace ATTG3
{
	class Genm : ICommandHandler
	{
		private readonly ATTG3Plugin plugin;
		Server Server => PluginManager.Manager.Server;
		IConfigFile Config => ConfigManager.Manager.Config;
		public Genm(ATTG3Plugin plugin) => this.plugin=plugin;
		public string GetCommandDescription() => "";
		public string GetUsage() => "";
		//Variables Below
		int count;
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
				Player myPlayer = GetPlayerFromString.GetPlayer(args[0]);
				if (myPlayer==null) { return new string[] { "Couldn't get player: "+args[0] }; }
				if (myPlayer.TeamRole.Role!=Role.SPECTATOR)
				{
					foreach (Generator079 Gen in Object.FindObjectsOfType<Generator079>())
					{
						count++;
						if (count==1)
						{
							GameObject player1 = (GameObject)myPlayer.GetGameObject();
							Gen.Networkposition=new Offset
							{
								position=player1.transform.position,
								rotation=Vector3.zero,
								scale=Vector3.one
							};
						}
					}
					count=0;
				}
			}
			return new string[] { "Locker Moved" };
		}
	}
}
