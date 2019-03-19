using System.IO;

namespace ATTG_Test
{
	public class Filestuff
	{
		public string[] Setfile(string text)
		{
			File.AppendAllText(ATTGLogPlugin.Rooms, text);

			return new string[] { "Done" };
		}
		public string[] Setfile1(string text)
		{
			File.AppendAllText(ATTGLogPlugin.Tlesla, text);

			return new string[] { "Done" };
		}
		public string[] Setfile2(string text)
		{
			File.AppendAllText(ATTGLogPlugin.Doors, text);

			return new string[] { "Done" };
		}
		public string[] Setfile3(string text)
		{
			File.AppendAllText(ATTGLogPlugin.Gen, text);

			return new string[] { "Done" };
		}
		public string[] Setfile4(string text)
		{
			File.AppendAllText(ATTGLogPlugin.Cam, text);

			return new string[] { "Done" };
		}
		public string[] Setfile5(string text)
		{
			File.AppendAllText(ATTGLogPlugin.Spawn, text);

			return new string[] { "Done" };
		}
	}
}
