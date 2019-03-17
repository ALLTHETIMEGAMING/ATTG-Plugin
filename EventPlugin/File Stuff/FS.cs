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

		private readonly ATTG3Plugin plugin;
		private string FilePath;
		public string[] Setfile(string text)
		{
			if (FilePath == "")
				return new string[] { "Error: missing filepath." };
			File.AppendAllText(ATTG3Plugin.Rooms, text);

			return new string[] { "Done" };
		}

	}
}
