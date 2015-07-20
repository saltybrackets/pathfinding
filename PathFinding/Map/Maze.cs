using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using PathFinding.Map;


namespace PathFinding
{
	public class Maze
	{
		#region Fields
		private Bitmap bitmap;
		private MapFeature[,] features;
		private Point mazeEnd;
		private Point mazeStart;
		#endregion


		#region Constructors
		public Maze(Bitmap bitmap)
		{
			this.bitmap = bitmap;
			ParseBitmap(bitmap);
		}


		public Maze(string mapImagePath)
			: this((Bitmap) Image.FromFile(mapImagePath))
		{
			
		}
		#endregion


		#region Properties/Indexers
		public Bitmap Bitmap
		{
			get { return this.bitmap; }
		}


		public MapFeature[,] Features
		{
			get { return this.features; }
		}


		public Point MazeEnd
		{
			get { return this.mazeEnd; }
		}


		public Point MazeStart
		{
			get { return this.mazeStart; }
		}
		#endregion


		public int GetEmptyPixelsAmount()
		{
			MapFeature[] mapFeatures = this.features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.Empty).Length;
		}


		public int GetEndPixelsAmount()
		{
			MapFeature[] mapFeatures = this.features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.End).Length;
		}


		public int GetStartPixelsAmount()
		{
			MapFeature[] mapFeatures = this.features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.Start).Length;
		}


		public int GetWallPixelsAmount()
		{
			MapFeature[] mapFeatures = this.features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.Wall).Length;
		}


		public override string ToString()
		{
			return new StringBuilder()
				.Append("Maze:")
				.Append("\nEmptyPixels: ").Append(GetEmptyPixelsAmount())
				.Append("\nWallPixels: ").Append(GetWallPixelsAmount())
				.Append("\nStartPixels: ").Append(GetStartPixelsAmount())
				.Append("\nEndPixels: ").Append(GetEndPixelsAmount())
				.ToString();
		}


		/// <summary>
		/// Parse a bitmap into map structures by analyzing pixel color.
		/// <see href="http://stackoverflow.com/questions/6020406/travel-through-pixels-in-bmp">
		/// Derived from a bitmap pixel analysis algorithm found here.
		/// </see>
		/// </summary>
		/// <param name="bitmap"></param>
		private void ParseBitmap(Bitmap bitmap)
		{
			Rectangle dimensions = new Rectangle(
				0, 0, 
				bitmap.Width, bitmap.Height);
			BitmapData data = bitmap.LockBits(
				dimensions, 
				ImageLockMode.ReadWrite,
				bitmap.PixelFormat);

			int scanWidth = data.Stride;
			int byteLength = scanWidth * data.Height;
			byte[] rgbValues = new byte[byteLength];
			int r, g, b, pixelBytes;
			Marshal.Copy(data.Scan0, rgbValues, 0, byteLength);
			
			this.features = new MapFeature[data.Width, data.Height];
			int rgbValuesIndex = 0;
			for (int column = 0; column < data.Height; column++)
			{
				for (int row = 0; row < data.Width; row++)
				{
					pixelBytes = row * 3;
					r = rgbValues[(column * scanWidth) + pixelBytes + 2];
					g = rgbValues[(column * scanWidth) + pixelBytes + 1];
					b = rgbValues[(column*scanWidth) + pixelBytes];
					Color color = Color.FromArgb(r, g, b);

					if (color.ToArgb() == Color.Black.ToArgb())
						this.features[column, row] = MapFeature.Wall;
					else if (color.ToArgb() == Color.Red.ToArgb())
						this.features[column, row] = MapFeature.Start;
					else if (color.ToArgb() == Color.Blue.ToArgb())
						this.features[column, row] = MapFeature.End;
					// Else, it's empty, which is the default enum value.

					rgbValuesIndex++;
				}
			}
		}
	}
}
