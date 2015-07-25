using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;


namespace PathFinding
{
	public class Map
	{
		#region Constants
		public const PixelFormat DefaultPixelFormat = PixelFormat.Format24bppRgb;
		public const int BytesPerPixel = 4;
		#endregion


		#region Fields
		private MapData data;
		#endregion


		#region Constructors
		public Map(MapData data)
		{
			this.data = data;
		}


		public Map(Bitmap bitmap)
		{
			this.data = MapUtilities.LoadBitmap(bitmap);
		}
		#endregion


		#region Properties
		public MapData Data
		{
			get { return this.data; }
		}
		#endregion


		public int GetEmptyPixelsAmount()
		{
			MapFeature[] mapFeatures = this.data.Features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.Empty).Length;
		}


		public int GetEndPixelsAmount()
		{
			MapFeature[] mapFeatures = this.data.Features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.End).Length;
		}


		public int GetStartPixelsAmount()
		{
			MapFeature[] mapFeatures = this.data.Features.Cast<MapFeature>().ToArray();
			return Array.FindAll(mapFeatures, x => x == MapFeature.Start).Length;
		}


		public int GetWallPixelsAmount()
		{
			MapFeature[] mapFeatures = this.data.Features.Cast<MapFeature>().ToArray();
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


		

	}
}