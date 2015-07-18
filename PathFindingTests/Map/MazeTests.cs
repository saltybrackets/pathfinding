using System;
using System.Runtime.Versioning;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PathFinding;


namespace PathFindingTests
{
	[TestClass]
	public class MazeTests
	{
		[TestMethod]
		public void ParseMazeTest()
		{
			System.Console.WriteLine("Reading maze bitmap.");
			Maze maze = new Maze(PathFindingTests.Properties.Resources.maze2);
			System.Console.WriteLine(maze);
			Assert.AreEqual(true, true);
		}
	}
}
