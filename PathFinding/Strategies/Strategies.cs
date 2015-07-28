namespace PathFinding.Strategies
{
	/// <summary>
	/// Collection of map traversal strategies.
	/// </summary>
	public class Strategies
	{
		#region Constants
		private static ITraversalStrategy breadthFirst;
		#endregion


		#region Properties
		/// <summary>
		/// Breadth-First traversal strategy for pathfinding.
		/// </summary>
		public static ITraversalStrategy BreadthFirst
		{
			get
			{
				if (breadthFirst == null)
					breadthFirst = new BreadthFirstStrategy();
				return breadthFirst;
			}
		}
		#endregion
	}
}