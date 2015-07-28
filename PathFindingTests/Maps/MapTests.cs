using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Maps;


namespace PathFindingTests
{
	public class MapTests
	{

		private Bitmap simpleMap = Properties.Resources.maze1;
		private Bitmap largeMap = Properties.Resources.maze2;
		private Bitmap organicMap = Properties.Resources.maze3;

		
		
		[TestClass]
		public class LoadBitmapMethod : MapTests
		{

			[TestMethod]
			public void LoadSimpleMap()
			{
				Map map = new Map(this.simpleMap);
				CheckMap(map);
				//map.SaveBitmap("output.bmp");
			}


			[TestMethod]
			public void LoadLargeMap()
			{
				Map map = new Map(this.largeMap);
				CheckMap(map);
			}


			[TestMethod]
			public void LoadOrganicMap()
			{
				Map map = new Map(this.organicMap);
				CheckMap(map);
			}


			private void CheckMap(Map map)
			{
				Console.WriteLine(map);
				Console.WriteLine();
				
				int emptyPixels = map.GetEmptyPixelsAmount();
				int endPixels = map.GetEndPixelsAmount();
				int startPixels = map.GetStartPixelsAmount();
				int wallPixels = map.GetWallPixelsAmount();
				int totalFeatures = 
					emptyPixels + endPixels + startPixels + wallPixels;
				int bitmapPixels =
					map.Width * map.Height;
				
				Assert.AreNotEqual(emptyPixels, 0);
				Assert.AreNotEqual(endPixels, 0);
				Assert.AreNotEqual(startPixels, 0);
				Assert.AreNotEqual(wallPixels, 0);
				Assert.AreEqual(totalFeatures, bitmapPixels);
				Assert.IsNotNull(map.StartPosition);
				Assert.IsNotNull(map.EndPosition);
			}
		}

		
	}
}
