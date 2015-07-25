
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace PathFinding
{
	public static class MapUtilities
	{
		
		/// <summary>
		/// Parse a bitmap into map structures by analyzing pixel color.
		/// <see href="http://stackoverflow.com/questions/6020406/travel-through-pixels-in-bmp">
		/// Derived from a bitmap pixel analysis algorithm found here.
		/// </see>
		/// </summary>
		/// <param name="bitmap"></param>
		/// <param name="pixelFormat">Color depth to process at.</param>
		public static MapData LoadBitmap(Bitmap bitmap, PixelFormat pixelFormat = Map.DefaultPixelFormat)
		{
			Rectangle dimensions = new Rectangle(
				0, 0,
				bitmap.Width, bitmap.Height);
			bitmap = bitmap.Clone(dimensions, pixelFormat); // Necessary to remove alpha bytes.
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

			MapFeature[,] features = new MapFeature[bitmap.Width, bitmap.Height];
			Point start = new Point();
			Point end = new Point();
			int rgbValuesIndex = 0;
			for (int x = 0; x < bitmap.Width; x++)
			{
				for (int y = 0; y < bitmap.Height; y++)
				{
					try
					{
						pixelBytes = x * 3;
						r = rgbValues[(y * scanWidth) + pixelBytes + 2];
						g = rgbValues[(y * scanWidth) + pixelBytes + 1];
						b = rgbValues[(y * scanWidth) + pixelBytes + 0];
						pixelColor = Color.FromArgb(255, r, g, b).ToArgb();

						if (pixelColor == black)
							features[x, y] = MapFeature.Wall;
						else if (pixelColor == red)
						{
							features[x, y] = MapFeature.Start;
							start = new Point(x, y);
						}

						else if (pixelColor == blue)
						{
							features[x, y] = MapFeature.End;
							end = new Point(x, y);
						}

						// Else, it's empty, which is the default enum value.
					}
					catch (Exception e)
					{
						throw new Exception("Failed with: column:" + x + " y:" + x + "\n" +
											"Dimensions:" + data.Width + "x" + data.Height, e);
					}

					rgbValuesIndex++;
				}
			}

			return new MapData(bitmap, features, start, end);
		}


		/// <summary>
		/// Encode and save out map feature data to a bitmap file.
		/// </summary>
		/// <param name="mapData">MapData containing map features to be encoded.</param>
		/// <param name="path">Path to save bitmap file to.</param>
		public static void SaveBitmap(this MapData mapData, string path)
		{
			int width = mapData.Features.GetLength(0);
			int height = mapData.Features.GetLength(1);
			int stride = width * Map.BytesPerPixel;

			byte[] pixels = new byte[height * stride];
			Color color;
			int pixelIndex = 0;
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					switch (mapData.Features[x, y])
					{
						default:
							color = Color.White;
							break;
						case MapFeature.End:
							color = Color.Blue;
							break;
						case MapFeature.Start:
							color = Color.Red;
							break;
						case MapFeature.Wall:
							color = Color.Black;
							break;
					}
					pixelIndex = (x * Map.BytesPerPixel) + (y * stride);
					pixels[pixelIndex] = color.B;
					pixels[pixelIndex + 1] = color.G;
					pixels[pixelIndex + 2] = color.R;
					pixels[pixelIndex + 3] = color.A;
				}
			}

			BitmapSource bitmapSource = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixels, stride);

			BitmapEncoder encoder = new BmpBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
			using (FileStream filestream = new FileStream(path, FileMode.Create))
				encoder.Save(filestream);
		}

	}
}
