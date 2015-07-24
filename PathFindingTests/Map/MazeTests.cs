using System;

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
			Console.WriteLine("Analyzing maze1.");
			Maze maze = new Maze(PathFindingTests.Properties.Resources.maze1);
			CheckMap(maze);
			maze.SaveMap("maze1.bmp");
		}


		[TestMethod]
		public void ParseMaze2Test()
		{
			Console.WriteLine("Analyzing maze2.");
			Maze maze = new Maze(PathFindingTests.Properties.Resources.maze2);
			CheckMap(maze);
			maze.SaveMap("maze2.bmp");
		}


		[TestMethod]
		public void ParseMaze3Test()
		{
			Console.WriteLine("Analyzing maze3.");
			Maze maze = new Maze(PathFindingTests.Properties.Resources.maze3);
			CheckMap(maze);
			maze.SaveMap("maze3.bmp");
		}


		private void CheckMap(Maze maze)
		{
			Console.WriteLine(maze);
			Console.WriteLine();

			int bitmapPixels = maze.Data.Bitmap.Width * maze.Data.Bitmap.Height;
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
