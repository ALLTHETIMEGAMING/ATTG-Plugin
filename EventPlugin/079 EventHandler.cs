using MEC;
using RemoteAdmin;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ATTG3
{
	internal class o79EventHandler : IEventHandler079Elevator, IEventHandler079TeslaGate
	{
		private readonly ATTG3Plugin plugin;
		public o79EventHandler(ATTG3Plugin plugin) => this.plugin = plugin;

	}
}
