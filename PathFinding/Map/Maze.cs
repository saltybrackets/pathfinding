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

		public const PixelFormat PixelFormat = System.Drawing.Imaging.PixelFormat.Format24bppRgb;

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
			bitmap = bitmap.Clone(dimensions, PixelFormat);
			BitmapData data = bitmap.LockBits(
				dimensions, 
				ImageLockMode.ReadWrite,
				bitmap.PixelFormat);

			int scanWidth = Math.Abs(data.Stride);
			int byteLength = scanWidth * data.Height;
			byte[] rgbValues = new byte[byteLength];
			int r, g, b, pixelBytes;
			Marshal.Copy(data.Scan0, rgbValues, 0, byteLength);

			int black = Color.Black.ToArgb();
			int blue = Color.Blue.ToArgb();
			int red = Color.Red.ToArgb();
			int pixelColor;

			this.features = new MapFeature[bitmap.Width, bitmap.Height];
			int rgbValuesIndex = 0;
			for (int row = 0; row < bitmap.Width; row++)
			{
				for (int column = 0; column < bitmap.Height; column++)
				{
					try
					{
						pixelBytes = row * 3;
						r = rgbValues[(column * scanWidth) + pixelBytes + 2];
						g = rgbValues[(column * scanWidth) + pixelBytes + 1];
						b = rgbValues[(column * scanWidth) + pixelBytes + 0];
						pixelColor = Color.FromArgb(255, r, g, b).ToArgb();

						if (pixelColor == black)
							this.features[column, row] = MapFeature.Wall;
						else if (pixelColor == red)
							this.features[column, row] = MapFeature.Start;
						else if (pixelColor == blue)
							this.features[column, row] = MapFeature.End;
						// Else, it's empty, which is the default enum value.
					}
					catch (Exception e)
					{
						throw new Exception("Failed with: column:"+column + " row:" + row + "\n" +
						"Dimensions:" + data.Width + "x" + data.Height, e);
					}
					

					rgbValuesIndex++;
				}
			}
		}


		public void SaveMap(string path = "output.bmp")
		{
			// TODO: Just use a different library.
		}

	}
}
