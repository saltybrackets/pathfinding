using PathFinding.Maps;
using PathFinding.Search;


namespace PathFinding.Strategies
{
	public interface ITraversalStrategy
	{
		IGraphSearchNode<MapFeature> Traverse(Map map, string options);
	}
}
