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
		public class GetSurroundingCellsMethod : MapTests 
		{

			[TestMethod]
			public void InsideMap()
			{
				Map map = new Map(this.simpleMap);
				List<MapFeature> cells = 
					map.GetSurroundingElements(44, 396);

				Assert.AreEqual(8, cells.Count);
				Assert.AreEqual(MapFeature.Wall, cells[1]);
				Assert.AreEqual(MapFeature.End, cells[3]);
			}


			[TestMethod]
			public void MapEdges()
			{
				Map map = new Map(this.simpleMap);
				List<MapFeature> cells;

				// Upper left.
				cells = map.GetSurroundingElements(0, 0);
				Assert.AreEqual(3, cells.Count);
				Assert.AreEqual(MapFeature.Wall, cells[0]);
				Assert.AreEqual(MapFeature.Empty, cells[2]);

				// Lower Right.
				cells = map.GetSurroundingElements(440, 440);
				Assert.AreEqual(3, cells.Count);
				Assert.AreEqual(MapFeature.Empty, cells[0]);
				Assert.AreEqual(MapFeature.Wall, cells[2]);	
			}
		}

		
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
