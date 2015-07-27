using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;


namespace PathFinding.Maps
{
	public class Map : IGraph<MapFeature>
	{
		#region Constants
		public const PixelFormat DefaultPixelFormat = PixelFormat.Format24bppRgb;
		public const int BytesPerPixel = 4;
		#endregion


		#region Fields
		private Position endPosition;
		private MapFeature[,] features;
		private Position startPosition;
		#endregion


		#region Constructors
		public Map(Bitmap bitmap)
		{
			LoadBitmap(bitmap);
		}
		#endregion


		#region Properties
		public Position EndPosition
		{
			get { return this.endPosition; }
		}


		public MapFeature[,] Features
		{
			get { return this.features; }
		}


		public int Height
		{
			get { return this.features.GetLength(1); }
		}


		public Position StartPosition
		{
			get { return this.startPosition; }
		}


		public int Width
		{
			get { return this.features.GetLength(0); }
		}
		#endregion


		public List<MapFeature> GetAdjacentElements(int x, int y)
		{
			return Graph.GetAdjacentElements(this, x, y);
		}


		public MapFeature GetElement(int x, int y)
		{
			return this.features[x, y];
		}


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


		/// <summary>
		/// Encode and save out map feature data to a bitmap file.
		/// </summary>
		/// <param name="mapData">MapData containing map features to be encoded.</param>
		/// <param name="path">Path to save bitmap file to.</param>
		public byte[] GetPixels()
		{
			int width = this.Width;
			int height = this.Height;
			int stride = width*BytesPerPixel;

			byte[] pixels = new byte[height*stride];
			Color color;
			int pixelIndex;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					switch (this.features[x, y])
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
					pixelIndex = (x*BytesPerPixel) + (y*stride);
					pixels[pixelIndex] = color.B;
					pixels[pixelIndex + 1] = color.G;
					pixels[pixelIndex + 2] = color.R;
					pixels[pixelIndex + 3] = color.A;
				}
			}

			return pixels;
		}


		public int GetStartPixelsAmount()
		{
			MapFeature[] mapFeatures = this.features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.Start).Length;
		}


		public List<MapFeature> GetSurroundingElements(int x, int y)
		{
			return Graph.GetSurroundingElements(this, x, y);
		}


		public int GetWallPixelsAmount()
		{
			MapFeature[] mapFeatures = this.features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.Wall).Length;
		}


		/// <summary>
		/// Parse a bitmap into map structures by analyzing pixel color.
		/// <see href="http://stackoverflow.com/questions/6020406/travel-through-pixels-in-bmp">
		/// Derived from a bitmap pixel analysis algorithm found here.
		/// </see>
		/// </summary>
		/// <param name="bitmap"></param>
		/// <param name="pixelFormat">Color depth to process at.</param>
		public void LoadBitmap(Bitmap bitmap, PixelFormat pixelFormat = DefaultPixelFormat)
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
			int byteLength = scanWidth*data.Height;
			byte[] rgbValues = new byte[byteLength];
			int r, g, b, pixelBytes;
			Marshal.Copy(data.Scan0, rgbValues, 0, byteLength);

			int black = Color.Black.ToArgb();
			int blue = Color.Blue.ToArgb();
			int red = Color.Red.ToArgb();
			int pixelColor;

			this.features = new MapFeature[bitmap.Width, bitmap.Height];
			int rgbValuesIndex = 0;
			for (int y = 0; y < bitmap.Height; y++)
			{
				for (int x = 0; x < bitmap.Width; x++)
				{
					try
					{
						pixelBytes = x*3;
						r = rgbValues[(y*scanWidth) + pixelBytes + 2];
						g = rgbValues[(y*scanWidth) + pixelBytes + 1];
						b = rgbValues[(y*scanWidth) + pixelBytes + 0];
						pixelColor = Color.FromArgb(255, r, g, b).ToArgb();

						if (pixelColor == black)
							this.features[x, y] = MapFeature.Wall;
						else if (pixelColor == red)
						{
							this.features[x, y] = MapFeature.Start;
							this.startPosition = new Position(x, y);
						}

						else if (pixelColor == blue)
						{
							this.features[x, y] = MapFeature.End;
							this.endPosition = new Position(x, y);
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

	}
}