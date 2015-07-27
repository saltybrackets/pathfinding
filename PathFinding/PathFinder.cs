using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PathFinding.Maps;
using PathFinding.Strategies;


namespace PathFinding
{
	/// <summary>
	/// Facade utility class for solving maps.
	/// </summary>
	public static class PathFinder
	{
		/// <summary>
		/// Find a solution to a map.
		/// </summary>
		/// <param name="map">Map to solve.</param>
		/// <param name="strategy">Strategy to use to solve the map.</param>
		/// <returns></returns>
		public static MapPath FindPath(Map map, ITraversalStrategy strategy)
		{
			return new MapPath(strategy.Traverse(map));
		}


		/// <summary>
		/// Save a solved map to an image file.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="map"></param>
		/// <param name="outputPath"></param>
		public static void OutputBitmap(
			MapPath path, 
			Map map, 
			string outputPath)
		{
			// Get pixels from original map data.
			byte[] pixels = path.TraceOverMap(map);

			// Output the bitmap.
			string extension = Path.GetExtension(outputPath);
			BitmapEncoder encoder;
			switch (extension)
			{
				default:
					encoder = new BmpBitmapEncoder();
					break;
				case ".png":
					encoder = new PngBitmapEncoder();
					break;
				case ".gif":
					encoder = new GifBitmapEncoder();
					break;
				case ".tif": case "tiff":
					encoder = new TiffBitmapEncoder();
					break;
				case ".jpg": case "jpeg":
					encoder = new JpegBitmapEncoder();
					break;
				case ".wmp":
					encoder = new WmpBitmapEncoder();
					break;
			}

			Console.WriteLine(extension);

			// TODO: Figure out why files have no compression.
			int stride = map.Width * Map.BytesPerPixel;
			BitmapSource bitmapSource = BitmapSource.Create(
				map.Width, map.Height, 
				96, 96, 
				PixelFormats.Bgra32, null, pixels, stride);
			
			encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
			
			using (FileStream filestream = new FileStream(outputPath, FileMode.Create))
			{
				encoder.Save(filestream);
			}
		}

	}
}