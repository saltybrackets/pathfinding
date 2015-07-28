namespace PathFinding.Search
{
	public interface ISearchNode
	{
		ISearchNode PreviousNode { get; } 
	}

	public interface ISearchNode<T> :
		ISearchNode
	{
		T Element { get; }
	}


	public interface IGraphSearchNode :
		ISearchNode
	{
		int X { get; }
		int Y { get; }
	}


	public interface IGraphSearchNode<T> :
		IGraphSearchNode,
		ISearchNode<T>
	{
		
	}
}
