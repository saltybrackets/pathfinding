namespace PathFinding.Search.BreadthFirst
{
	/// <summary>
	/// A search node used in a BreadthFirstSearch graph.
	/// </summary>
	public class BreadthFirstSearchNode :
		ISearchNode
	{
		#region Fields
		private ISearchNode previousNode;
		private SearchState searchStatus;
		private readonly int x;
		private readonly int y;
		#endregion


		#region Constructors
		/// <summary>
		/// Create a new search node for BreadFirstSearch.
		/// </summary>
		/// <param name="x">X coordinate of the search node.</param>
		/// <param name="y">Y coordinate of the search node.</param>
		/// <param name="previousNode">Search node preceeding this one.</param>
		public BreadthFirstSearchNode(int x, int y,
			BreadthFirstSearchNode previousNode = null)
		{
			this.x = x;
			this.y = y;
			this.searchStatus = SearchState.Unchecked;
			this.previousNode = previousNode;
		}
		#endregion


		#region Enums
		/// <summary>
		/// Indicates whether a node is unchecked, checked, or queued to be checked.
		/// </summary>
		public enum SearchState
		{
			Unchecked = 0,
			Checked,
			Queued
		}
		#endregion


		#region Properties
		/// <summary>
		/// The search node that preceeded this node.
		/// </summary>
		public ISearchNode PreviousNode
		{
			get { return this.previousNode; }
			set { this.previousNode = value; }
		}


		/// <summary>
		/// Whether the node is checked, unchecked, or queued.
		/// </summary>
		public SearchState SearchStatus
		{
			get { return this.searchStatus; }
			set { this.searchStatus = value; }
		}


		/// <summary>
		/// Horizontal X coordinate (column) of the node in the search graph.
		/// </summary>
		public int X
		{
			get { return this.x; }
		}


		/// <summary>
		/// Vertical Y coordinate (row) of the node in the search graph.
		/// </summary>
		public int Y
		{
			get { return this.y; }
		}
		#endregion
	}


	/// <summary>
	/// A search node used in a BreadthFirstSearch graph that tracks an object.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BreadthFirstSearchNode<T> : BreadthFirstSearchNode,
		IGraphSearchNode<T>
	{
		#region Fields
		private readonly T element;
		#endregion


		#region Constructors
		/// <summary>
		/// Creates a new search node for use in BreadthFirstSearch.
		/// </summary>
		/// <param name="element">Object tracked by the node.</param>
		/// <param name="x">X coordinate of the node.</param>
		/// <param name="y">Y coordinate of the node.</param>
		/// <param name="previousNode">Search node that preceeded this one.</param>
		public BreadthFirstSearchNode(T element, int x, int y,
			BreadthFirstSearchNode<T> previousNode = null)
			: base(x, y, previousNode)
		{
			this.SearchStatus = SearchState.Unchecked;
			this.element = element;
		}
		#endregion


		#region Properties
		/// <summary>
		/// Object tracked by this node.
		/// </summary>
		public T Element
		{
			get { return this.element; }
		}
		#endregion
	}
}