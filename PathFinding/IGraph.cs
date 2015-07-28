using System.Collections.Generic;


namespace PathFinding
{
	/// <summary>
	/// Implemented by objects containing an orthogonal 2D collection of items.
	/// </summary>
	/// <typeparam name="T">Type of objects contained in collection.</typeparam>
	public interface IGraph<T>
	{
		int Width { get; }
		int Height { get; }

		List<T> GetNeighborElements(int x, int y);
		T GetElement(int x, int y);
	}


	/// <summary>
	/// Contains default implementations of some interface methods.
	/// </summary>
	public static class Graph
	{

		/// <summary>
		/// Get directly adjacent elements (does not include diagonals).
		/// </summary>
		/// <typeparam name="T">Type of elements contained in graph.</typeparam>
		/// <param name="graph">Graph to get elements from.</param>
		/// <param name="x">X coordinate to get adjacent elements of.</param>
		/// <param name="y">Y coordinate to get adjacent elements of.</param>
		/// <returns>List of surrounding elements.</returns>
		public static List<T> GetAdjacentElements<T>(IGraph<T> graph, int x, int y)
		{
			// Note: The following could also be done with a mod operation.
			// Sounds cool. Looks neat.
			// But mods are a performance hit. And can be confusing at a glance.
			// So we go manual. It's only three rows, anyway.
			List<T> adjacentElements = new List<T>();
			int maxX = graph.Width - 1;
			int maxY = graph.Height - 1;
			int rowCounter = 0;
			Position[] nodes = 
			{
				new Position(x, y - 1),	// Up
				new Position(x - 1, y),	// Left
				new Position(x + 1, y),	// Right
				new Position(x, y + 1)	// Down
			};

			foreach (Position node in nodes)
			{
				if ((node.X < 0) ||
					(node.Y < 0) ||
					(node.X > maxX) ||
					(node.Y > maxY))
					continue;
				
				adjacentElements.Add(graph.GetElement(node.X, node.Y));
			}

			return adjacentElements;
		}
		


		/// <summary>
		/// Get all surrounding elements (including diagonals).
		/// </summary>
		/// <typeparam name="T">Type of elements contained in graph.</typeparam>
		/// <param name="graph">Graph to get elements from.</param>
		/// <param name="x">X coordinate to get adjacent elements of.</param>
		/// <param name="y">Y coordinate to get adjacent elements of.</param>
		/// <returns>List of surrounding elements.</returns>
		public static List<T> GetSurroundingElements<T>(IGraph<T> graph, int x, int y)
		{
			List<T> surroundingElements = new List<T>();
			int maxX = graph.Width - 1;
			int maxY = graph.Height - 1;

			// Get cells row by row.
			for (int adjacentY = y - 1; adjacentY < y + 2; adjacentY++)
			{
				if ((adjacentY < 0) ||
					(adjacentY > maxY))
					continue;

				for (int adjacentX = x - 1; adjacentX < x + 2; adjacentX++)
				{
					if ((adjacentX < 0) ||
						(adjacentX > maxX) ||
						(adjacentX == x && adjacentY == y))
						continue;

					surroundingElements.Add(graph.GetElement(adjacentX, adjacentY));
				}
			}

			return surroundingElements;
		}

	}
}
