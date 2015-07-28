using System;
using System.Collections.Generic;


namespace PathFinding.Search.BreadthFirst
{
	public partial class BreadthFirstSearch<T> :
		IGraph<BreadthFirstSearchNode<T>>
	{
		#region Fields
		private readonly Func<IGraph<BreadthFirstSearchNode<T>>, int, int, List<BreadthFirstSearchNode<T>>> adjacencyMethod;
		private readonly Predicate<IGraphSearchNode<T>> failCondition;
		private readonly Predicate<IGraphSearchNode<T>> matchCondition;
		private readonly BreadthFirstSearchNode<T>[,] searchGraph;
		private readonly IGraph<T> sourceGraph;
		#endregion


		#region Constructors
		/// <summary>
		/// Used by Query Builder nested class.
		/// See that class for more details on parameters.
		/// </summary>
		public BreadthFirstSearch(
			Func<IGraph<BreadthFirstSearchNode<T>>, int, int, List<BreadthFirstSearchNode<T>>> adjacencyMethod,
			Predicate<IGraphSearchNode<T>> failCondition,
			Predicate<IGraphSearchNode<T>> matchCondition,
			BreadthFirstSearchNode<T>[,] searchGraph,
			IGraph<T> sourceGraph)
		{
			this.adjacencyMethod = adjacencyMethod;
			this.failCondition = failCondition;
			this.matchCondition = matchCondition;
			this.searchGraph = searchGraph;
			this.sourceGraph = sourceGraph;
		}
		#endregion


		#region Properties
		/// <summary>
		/// Unit height of search graph.
		/// </summary>
		public int Height
		{
			get { return this.searchGraph.GetLength(1); }
		}


		/// <summary>
		/// Unit width of search graph.
		/// </summary>
		public int Width
		{
			get { return this.searchGraph.GetLength(0); }
		}
		#endregion


		/// <summary>
		/// Get a List of adjacent search nodes relative to given position.
		/// </summary>
		/// <param name="x">Horizontal coordinate of graph.</param>
		/// <param name="y">Vertical coordinate of graph.</param>
		/// <returns></returns>
		public List<BreadthFirstSearchNode<T>> GetNeighborElements(int x, int y)
		{
			return this.adjacencyMethod(this, x, y);
		}


		/// <summary>
		/// Retrieve search node at the specified coordinate.
		/// </summary>
		/// <param name="x">Horizontal coordinate of graph.</param>
		/// <param name="y">Vertical coordinate of graph.</param>
		/// <returns></returns>
		public BreadthFirstSearchNode<T> GetElement(int x, int y)
		{
			return this.searchGraph[x, y];
		}


		/// <summary>
		/// Do a Breadth-First search of the source graph to find a node matching
		/// the provided criteria.
		/// </summary>
		/// <param name="rootX">X position to start the search at.</param>
		/// <param name="rootY">Y position to start the search at.</param>
		/// <returns></returns>
		public BreadthFirstSearchNode<T> SearchAt(int rootX, int rootY)
		{
			if (this.matchCondition == null)
				return null;

			Queue<BreadthFirstSearchNode<T>> queue = new Queue<BreadthFirstSearchNode<T>>();
			queue.Enqueue(this.searchGraph[rootX, rootY]);

			while (queue.Count > 0)
			{
				BreadthFirstSearchNode<T> currentNode = queue.Dequeue();

				// Found it! Return the node containing successful search path.
				if (this.matchCondition(currentNode))
					return currentNode;

				// Still searching.
				List<BreadthFirstSearchNode<T>> adjacentNodes =
					GetNeighborElements(currentNode.X, currentNode.Y);
				foreach (BreadthFirstSearchNode<T> adjacentNode in adjacentNodes)
				{
					if (this.failCondition != null &&
					    this.failCondition(adjacentNode))
					{
						adjacentNode.SearchStatus = BreadthFirstSearchNode.SearchState.Checked;
						continue;
					}

					if (adjacentNode.SearchStatus == BreadthFirstSearchNode.SearchState.Unchecked)
					{
						adjacentNode.SearchStatus = BreadthFirstSearchNode.SearchState.Queued;
						adjacentNode.PreviousNode = currentNode;
						queue.Enqueue(adjacentNode);
					}
				}

				// Finished checking this node.
				currentNode.SearchStatus = BreadthFirstSearchNode.SearchState.Checked;
			}

			// No match found.
			return null;
		}


		/// <summary>
		/// Creates a mirror of an existing graph, but decorated with search markers.
		/// </summary>
		/// <param name="sourceGraph">Graph to duplicate.</param>
		/// <returns></returns>
		private static BreadthFirstSearchNode<T>[,] AssembleSearchArray(IGraph<T> sourceGraph)
		{
			int width = sourceGraph.Width;
			int height = sourceGraph.Height;

			BreadthFirstSearchNode<T>[,] searchArray = new BreadthFirstSearchNode<T>[width, height];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					searchArray[x, y] =
						new BreadthFirstSearchNode<T>(sourceGraph.GetElement(x, y), x, y);
				}
			}

			return searchArray;
		}

	}
}