using System;
using System.Collections.Generic;


namespace PathFinding.Search.BreadthFirst
{
	public partial class BreadthFirstSearch<T>
	{
		#region Types
		/// <summary>
		/// A Builder object for putting together searches in Breadth-First fashion.
		/// </summary>
		public class Query
		{
			#region Fields
			private Func<IGraph<BreadthFirstSearchNode<T>>, int, int, List<BreadthFirstSearchNode<T>>> adjacencyMethod =
				Graph.GetAdjacentElements;
			private Predicate<IGraphSearchNode<T>> failCondition = null;
			private Predicate<IGraphSearchNode<T>> matchCondition = null;
			private BreadthFirstSearchNode<T>[,] searchGraph;
			private IGraph<T> sourceGraph;
			#endregion


			#region Constructors
			/// <summary>
			/// Create a new query for a graph object.
			/// </summary>
			/// <param name="sourceGraph">Graph to search.</param>
			public Query(IGraph<T> sourceGraph)
			{
				this.sourceGraph = sourceGraph;
				this.searchGraph = AssembleSearchArray(sourceGraph);
			}
			#endregion


			/// <summary>
			/// Delegate method used to get adjacent elements from the graph.
			/// By default, gets adjacent elements in all eight directions.
			/// </summary>
			/// <param name="method">Method for determining adjacent elements.</param>
			/// <returns>Original Query for chaining.</returns>
			public Query AdjacencyMethod(Func<IGraph<BreadthFirstSearchNode<T>>, int, int, List<BreadthFirstSearchNode<T>>> method)
			{
				this.adjacencyMethod = method;
				return this;
			}


			/// <summary>
			/// Additional delegate method used to automatically fail nodes instead of queue them.
			/// If not specified, query simply queues nodes it comes across that have not yet been checked.
			/// </summary>
			/// <param name="method">Method for auto-failing node queued nodes.</param>
			/// <returns>Original Query for chaining.</returns>
			public Query FailCondition(Predicate<IGraphSearchNode<T>> method)
			{
				this.failCondition = method;
				return this;
			}


			/// <summary>
			/// Delegate method used to determine when a searched node is a match.
			/// If not specified, queries will always return null.
			/// </summary>
			/// <param name="method">Method for determining valid node match.</param>
			/// <returns>Original Query for chaining.</returns>
			public Query MatchCondition(Predicate<IGraphSearchNode<T>> method)
			{
				this.matchCondition = method;
				return this;
			}


			/// <summary>
			/// Begins the search query.
			/// </summary>
			/// <param name="rootX">X coordinate in graph to begin search at.</param>
			/// <param name="rootY">Y coordinate in graph to begin search at.</param>
			/// <returns></returns>
			public IGraphSearchNode<T> SearchAt(int rootX, int rootY)
			{
				return new BreadthFirstSearch<T>(
					this.adjacencyMethod,
					this.failCondition,
					this.matchCondition,
					this.searchGraph,
					this.sourceGraph)
					.SearchAt(rootX, rootY);
			}
		}
		#endregion
	}
}