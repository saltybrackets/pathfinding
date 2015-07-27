namespace PathFinding.Search
{
	public interface ISearchNode<T>
	{
		T Item { get; }
		ISearchNode<T> PreviousNode { get; } 
	}


	public interface IGraphSearchNode<T> :
	ISearchNode<T>
	{
		int X { get; }
		int Y { get; }
	}
}
