using System;
using PathFinding.Maps;
using PathFinding.Search;
using PathFinding.Search.BreadthFirst;


namespace PathFinding.Strategies
{
	/// <summary>
	/// Traversal strategy utilizing Breadth-First Search method.
	/// </summary>
	public class BreadthFirstStrategy :
		ITraversalStrategy
	{
		
		#region Enums
		/// <summary>
		/// Allowed movement types using this strategy.
		/// </summary>
		public enum Movement
		{
			CardinalOnly,
			Any
		}
		#endregion


		/// <summary>
		/// Get a solution path for the given map.
		/// If no solution was found, will return null.
		/// </summary>
		/// <param name="map">Map to traverse.</param>
		/// <returns>Graph node series.</returns>
		public IGraphSearchNode<MapFeature> Traverse(Map map, string options = null)
		{
			IGraphSearchNode<MapFeature> path;

			// Move in any direction, even diagonals.
			if (options != null &&
				String.Equals(options, Movement.Any.ToString(), 
				StringComparison.CurrentCultureIgnoreCase))
			{
				path = new BreadthFirstSearch<MapFeature>.Query(map)
					.AdjacencyMethod(Graph.GetSurroundingElements)
					.FailCondition(PivotAtWalls)
					.MatchCondition(IsEndpoint)
					.SearchAt(map.StartPosition.X, map.StartPosition.Y);
			}

			// Can only move up/down/left/right.
			else
			{
				path = new BreadthFirstSearch<MapFeature>.Query(map)
					.AdjacencyMethod(Graph.GetAdjacentElements)
					.FailCondition(PivotAtWalls)
					.MatchCondition(IsEndpoint)
					.SearchAt(map.StartPosition.X, map.StartPosition.Y);
			}

			return path;
		}


		// Match condition delegate.
		private bool IsEndpoint(IGraphSearchNode<MapFeature> node)
		{
			return node.Element == MapFeature.End;
		}


		// Auto-fail condition delegate.
		private bool PivotAtWalls(IGraphSearchNode<MapFeature> node)
		{
			return node.Element == MapFeature.Wall;
		}

	}
}