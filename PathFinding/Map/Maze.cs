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
		private Bitmap bitmap;
		private MapFeature[,] features;
		private Point mazeStart;
		private Point mazeEnd;


		public Maze(Bitmap bitmap)
		{
			this.bitmap = bitmap;
			ParseBitmap(bitmap);
		}


		public Maze(string mapImagePath)
			: this((Bitmap) Image.FromFile(mapImagePath))
		{
			
		}


		public override string ToString()
		{
			MapFeature[] mapFeatures = this.features.Cast<MapFeature>().ToArray();
			int startPixels = Array.FindAll(mapFeatures, x => x == MapFeature.Start).Length;
			int endPixels = Array.FindAll(mapFeatures, x => x == MapFeature.End).Length;
			int wallPixels = Array.FindAll(mapFeatures, x => x == MapFeature.Wall).Length;
			int emptyPixels = Array.FindAll(mapFeatures, x => x == MapFeature.Empty).Length;

			return new StringBuilder()
				.Append("Maze:")
				.Append("\nStartPixels: ").Append(startPixels)
				.Append("\nEndPixels: ").Append(endPixels)
				.Append("\nWallPixels: ").Append(wallPixels)
				.Append("\nEmptyPixels: ").Append(emptyPixels)
				.ToString();
		}

		
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
