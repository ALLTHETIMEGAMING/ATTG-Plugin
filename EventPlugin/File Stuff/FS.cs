using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2;
using Smod2.API;
using Smod2.Commands;
using TMPro;
using System.IO;

namespace ATTG3
{
	public class Filestuff
	{

		public string[] Setfile(string text)
		{
			File.AppendAllText(ATTG3Plugin.Rooms, text);

			return new string[] { "Done" };
		}

		public string[] Setfile1(string text)
		{
			File.AppendAllText(ATTG3Plugin.Tlesla, text);

			return new string[] { "Done" };
		}

		public string[] Setfile2(string text)
		{
			File.AppendAllText(ATTG3Plugin.Doors, text);

			return new string[] { "Done" };
		}

		public string[] Setfile3(string text)
		{
			File.AppendAllText(ATTG3Plugin.Gen, text);

			return new string[] { "Done" };
		}

		public string[] Setfile4(string text)
		{
			File.AppendAllText(ATTG3Plugin.Cam, text);

			return new string[] { "Done" };
		}

		public string[] Setfile5(string text)
		{
			File.AppendAllText(ATTG3Plugin.Spawn, text);

			return new string[] { "Done" };
		}

	}
}
