using System.Collections.Generic;
using PathFinding.Search;
using System.Drawing;


namespace PathFinding.Maps
{
	/// <summary>
	/// Simplified facade for map paths to more easily handle path nodes.
	/// </summary>
	public class MapPath
	{
		#region Fields
		private readonly IGraphSearchNode<MapFeature> startNode;
		private readonly IGraphSearchNode<MapFeature> endNode;
		private readonly List<IGraphSearchNode<MapFeature>> pathNodes;
		#endregion


		#region Constructors
		/// <summary>
		/// Create a new MapPath object from a graph search node series.
		/// </summary>
		/// <param name="path">Graph search node containing path node series.</param>
		public MapPath(IGraphSearchNode<MapFeature> path)
		{
			this.pathNodes = new List<IGraphSearchNode<MapFeature>>();
			this.endNode = path;
			IGraphSearchNode<MapFeature> pathNode = path;
			while (pathNode != null)
			{
				this.pathNodes.Add(pathNode);
				if (pathNode.PreviousNode == null)
					this.endNode = pathNode;
				pathNode = (IGraphSearchNode<MapFeature>) pathNode.PreviousNode;
			}
		}
		#endregion


		#region Properties
		/// <summary>
		/// Get all path waypoints nodes.
		/// </summary>
		public List<IGraphSearchNode<MapFeature>> PathNodes
		{
			get { return this.pathNodes; }
		}
		#endregion


		/// <summary>
		/// Trace path over pixels of a given map.
		/// Traces in lime green by default.
		/// </summary>
		/// <param name="map">Map to trace over.</param>
		/// <returns>Pixel byte array representing traced over map.</returns>
		public byte[] TraceOverMap(Map map)
		{
			return TraceOverMap(map, Color.Lime);
		}


		/// <summary>
		/// Trace path over pixels of a given map in a given color.
		/// </summary>
		/// <param name="map">Map to trace over.</param>
		/// <param name="color">Color to trace in.</param>
		/// <returns>Pixel byte array representing traced over map.</returns>
		public byte[] TraceOverMap(Map map, Color color)
		{
			// Get pixels from original map data.
			byte[] pixels = map.GetPixels();

			// Replace pixels at path node coordinates.
			int stride = map.Width*Map.BytesPerPixel;
			foreach (IGraphSearchNode<MapFeature> pathNode in this.pathNodes)
			{
				int pixelIndex = (pathNode.X*Map.BytesPerPixel) + (pathNode.Y*stride);
				pixels[pixelIndex] = color.B;
				pixels[pixelIndex + 1] = color.G;
				pixels[pixelIndex + 2] = color.R;
				pixels[pixelIndex + 3] = color.A;
			}

			return pixels;
		}

	}
}