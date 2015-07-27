using System;
using System.Collections.Generic;


namespace PathFinding.Search
{
	/// <summary>
	/// Assembles a decorated 2D graph of objects that may be searched in a Breadth-First fashion.
	/// </summary>
	/// <typeparam name="T">Type of elements stored in the graph.</typeparam>
	public class BreadthFirstSearchGraph<T> :
		IGraph<BreadthFirstSearchGraph<T>.BreadthFirstSearchNode>
	{
		#region Fields
		private IGraph<T> sourceGraph;
		private BreadthFirstSearchNode[,] searchGraph;
		#endregion


		#region Constructors
		public BreadthFirstSearchGraph(IGraph<T> sourceGraph)
		{
			this.sourceGraph = sourceGraph;
		}
		#endregion


		#region Properties
		public int Height
		{
			get { return this.searchGraph.GetLength(1); }
		}


		public int Width
		{
			get { return this.searchGraph.GetLength(0); }
		}
		#endregion


		/// <summary>
		/// Creates a mirror of an existing graph, but decorated with search markers.
		/// </summary>
		/// <param name="sourceGraph">Graph to duplicate.</param>
		/// <returns></returns>
		private BreadthFirstSearchNode[,] AssembleSearchArray()
		{
			int width = this.sourceGraph.Width;
			int height = this.sourceGraph.Height;

			BreadthFirstSearchNode[,] searchArray = new BreadthFirstSearchNode[width, height];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					searchArray[x, y] =
						new BreadthFirstSearchNode(this.sourceGraph.GetElement(x, y), x, y);
				}
			}

			return searchArray;
		}


		/// <summary>
		/// Get a List of adjacent search nodes relative to given position.
		/// </summary>
		/// <param name="x">Horizontal coordinate of graph.</param>
		/// <param name="y">Vertical coordinate of graph.</param>
		/// <returns></returns>
		public List<BreadthFirstSearchNode> GetAdjacentElements(int x, int y)
		{
			return Graph.GetAdjacentElements(this, x, y);
		}


		/// <summary>
		/// Get a List of adjacent search nodes relative to given position.
		/// </summary>
		/// <param name="x">Horizontal coordinate of graph.</param>
		/// <param name="y">Vertical coordinate of graph.</param>
		/// <returns></returns>
		public List<BreadthFirstSearchNode> GetSurroundingElements(int x, int y)
		{
			return Graph.GetSurroundingElements(this, x, y);
		}


		/// <summary>
		/// Retrieve search node at the specified coordinate.
		/// </summary>
		/// <param name="x">Horizontal coordinate of graph.</param>
		/// <param name="y">Vertical coordinate of graph.</param>
		/// <returns></returns>
		public BreadthFirstSearchNode GetElement(int x, int y)
		{
			return this.searchGraph[x, y];
		}


		/// <summary>
		/// Do a Breadth-First search of the source graph to find a node matching
		/// the provided criteria.
		/// </summary>
		/// <param name="matchCondition">A delegate method stating criteria that must be met.</param>
		/// <param name="markCondition">A delegate method stating criteria where a node should be marked anyway.</param>
		/// <param name="rootX">X position to start the search at.</param>
		/// <param name="rootY">Y position to start the search at.</param>
		/// <returns></returns>
		public BreadthFirstSearchNode Search(
			Predicate<IGraphSearchNode<T>> matchCondition,
			Predicate<IGraphSearchNode<T>> markCondition, 
			int rootX, int rootY)
		{
			// TODO: Overload where root defaults to (0,0) for other projects.
			
			// TODO: Test what happens if another search is performed under different critera.
			// If previous search nodes lose their references because search graph is re-initialized,
			// may need to turn this into an immutable, functionally pure class.

			// Initialize a new search graph, with all nodes unmarked.
			this.searchGraph = AssembleSearchArray();
			Queue<BreadthFirstSearchNode> queue = new Queue<BreadthFirstSearchNode>();
			queue.Enqueue(this.searchGraph[rootX, rootY]);

			while (queue.Count > 0)
			{
				BreadthFirstSearchNode currentNode = queue.Dequeue();

				// Found it! Return the node containing successful search path.
				if (matchCondition(currentNode))
					return currentNode;

				// Still searching.
				List<BreadthFirstSearchNode> adjacentNodes =
					GetSurroundingElements(currentNode.X, currentNode.Y);
				foreach (BreadthFirstSearchNode adjacentNode in adjacentNodes)
				{
					if (markCondition(adjacentNode))
					{
						adjacentNode.Status = BreadthFirstSearchNode.State.Marked;
						continue;
					}

					if (adjacentNode.Status == BreadthFirstSearchNode.State.Unmarked)
					{
						adjacentNode.Status = BreadthFirstSearchNode.State.Queued;
						adjacentNode.PreviousNode = currentNode;
						queue.Enqueue(adjacentNode);
					}
				}
				
				// Finished checking this node.
				currentNode.Status = BreadthFirstSearchNode.State.Marked;
			}

			// No match found.
			return null;
		}


		#region Types
		public class BreadthFirstSearchNode :
			IGraphSearchNode<T>
		{
			#region Fields
			private readonly T item;
			private ISearchNode<T> previousNode;
			private State status;
			private readonly int x;
			private readonly int y;
			#endregion


			#region Constructors
			public BreadthFirstSearchNode(
				T item,
				int x,
				int y,
				BreadthFirstSearchNode previousNode = null)
			{
				this.status = State.Unmarked;
				this.item = item;
				this.x = x;
				this.y = y;
				this.previousNode = previousNode;
			}
			#endregion


			#region Enums
			public enum State
			{
				Unmarked,
				Marked,
				Queued
			}
			#endregion


			#region Properties
			public T Item
			{
				get { return this.item; }
			}


			public ISearchNode<T> PreviousNode
			{
				get { return this.previousNode; }
				set { this.previousNode = value; }
			}


			public State Status
			{
				get { return this.status; }
				set { this.status = value; }
			}


			public int X
			{
				get { return this.x; }
			}


			public int Y
			{
				get { return this.y; }
			}
			#endregion
		}
		#endregion
	}
}