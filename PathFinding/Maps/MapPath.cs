using System.Collections.Generic;
using PathFinding.Search;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace PathFinding.Maps
{
	/// <summary>
	/// Facade for map paths.
	/// </summary>
	public class MapPath
	{
		#region Fields
		private readonly IGraphSearchNode<MapFeature> startNode;
		private readonly IGraphSearchNode<MapFeature> endNode;
		private readonly List<IGraphSearchNode<MapFeature>> pathNodes;
		#endregion


		#region Constructors
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
		public List<IGraphSearchNode<MapFeature>> PathNodes
		{
			get { return this.pathNodes; }
		}
		#endregion


		public byte[] TraceOverMap(Map map)
		{
			return TraceOverMap(map, Color.Lime);
		}


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