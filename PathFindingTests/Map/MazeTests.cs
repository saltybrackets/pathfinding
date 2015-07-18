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
		public void ParseMaze1Test()
		{
			System.Console.WriteLine("Analyzing maze1.");
			Maze maze = new Maze(PathFindingTests.Properties.Resources.maze1);
			CheckMap(maze);
		}


		[TestMethod]
		public void ParseMaze2Test()
		{
			System.Console.WriteLine("Analyzing maze2.");
			Maze maze = new Maze(PathFindingTests.Properties.Resources.maze2);
			CheckMap(maze);
		}


		[TestMethod]
		public void ParseMaze3Test()
		{
			System.Console.WriteLine("Analyzing maze3.");
			Maze maze = new Maze(PathFindingTests.Properties.Resources.maze3);
			CheckMap(maze);
		}


		private void CheckMap(Maze maze)
		{
			System.Console.WriteLine(maze);
			System.Console.WriteLine();

			int bitmapPixels = maze.Bitmap.Width * maze.Bitmap.Height;
			int emptyPixels = maze.GetEmptyPixelsAmount();
			int endPixels = maze.GetEndPixelsAmount();
			int startPixels = maze.GetStartPixelsAmount();
			int wallPixels = maze.GetWallPixelsAmount();
			int totalFeatures = emptyPixels + endPixels + startPixels + wallPixels;

			Assert.AreNotEqual(emptyPixels, 0);
			Assert.AreNotEqual(endPixels, 0);
			Assert.AreNotEqual(startPixels, 0);
			Assert.AreNotEqual(wallPixels, 0);
			Assert.AreEqual(totalFeatures, bitmapPixels);
		}

	}
}
