using PathFinding.Maps;
using PathFinding.Search;


namespace PathFinding.Strategies
{
	public class BreadthFirstStrategy :
		ITraversalStrategy
	{

		public IGraphSearchNode<MapFeature> Traverse(Map map)
		{
			BreadthFirstSearchGraph<MapFeature> searchGraph =
				new BreadthFirstSearchGraph<MapFeature>(map);

			IGraphSearchNode<MapFeature> path =
				searchGraph.Search(
					IsEndpoint,
					PivotAtWalls,
					map.StartPosition.X, map.StartPosition.Y);

			return path;
		}


		private bool IsEndpoint(IGraphSearchNode<MapFeature> node)
		{
			return node.Item == MapFeature.End;
		}


		private bool PivotAtWalls(IGraphSearchNode<MapFeature> node)
		{
			return node.Item == MapFeature.Wall;
		}

	}
}