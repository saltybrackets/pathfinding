using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding;


namespace PathFindingTests
{
	[TestClass]
	public class MazeTests
	{

		[TestMethod]
		public void ParseSimpleMap()
		{
			Console.WriteLine("Analyzing maze1.");
			Map map = new Map(PathFindingTests.Properties.Resources.maze1);
			CheckMap(map);
			map.Data.SaveBitmap("maze1.bmp");
		}


		[TestMethod]
		public void ParseLargeMap()
		{
			Console.WriteLine("Analyzing maze2.");
			Map map = new Map(PathFindingTests.Properties.Resources.maze2);
			CheckMap(map);
			map.Data.SaveBitmap("maze2.bmp");
		}


		[TestMethod]
		public void ParseOrganicMap()
		{
			Console.WriteLine("Analyzing maze3.");
			Map map = new Map(PathFindingTests.Properties.Resources.maze3);
			CheckMap(map);
			map.Data.SaveBitmap("maze3.bmp");
		}


		private void CheckMap(Map map)
		{
			Console.WriteLine(map);
			Console.WriteLine();

			int bitmapPixels = map.Data.Bitmap.Width * map.Data.Bitmap.Height;
			int emptyPixels = map.GetEmptyPixelsAmount();
			int endPixels = map.GetEndPixelsAmount();
			int startPixels = map.GetStartPixelsAmount();
			int wallPixels = map.GetWallPixelsAmount();
			int totalFeatures = emptyPixels + endPixels + startPixels + wallPixels;

			Assert.AreNotEqual(emptyPixels, 0);
			Assert.AreNotEqual(endPixels, 0);
			Assert.AreNotEqual(startPixels, 0);
			Assert.AreNotEqual(wallPixels, 0);
			Assert.AreEqual(totalFeatures, bitmapPixels);
		}

	}
}
