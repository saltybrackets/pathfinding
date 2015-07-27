using System.Collections.Generic;


namespace PathFinding
{
	public interface IGraph<T>
	{
		int Width { get; }
		int Height { get; }

		List<T> GetAdjacentElements(int x, int y);
		T GetElement(int x, int y);
		List<T> GetSurroundingElements(int x, int y);
	}


	/// <summary>
	/// Basic implementations of some interface methods.
	/// 
	/// Note: These could be made into extension methods, 
	/// but would result in difficulties overriding.
	/// </summary>
	public static class Graph
	{

		/// <summary>
		/// Get directly adjacent elements (does not include diagonals).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="graph"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static List<T> GetAdjacentElements<T>(IGraph<T> graph, int x, int y)
		{
			List<T> adjacentCells = new List<T>();

			int maxX = graph.Width - 1;
			int maxY = graph.Height - 1;

			// Get cells row by row.
			// Starts at top cell, skips every other, so diagonals not included.
			for (int adjacentY = y; adjacentY < y + 2; adjacentY += 2)
			{
				if ((adjacentY < 0) ||
					(adjacentY > maxY))
					continue;

				for (int adjacentX = x; adjacentX < x + 2; adjacentX += 2)
				{
					if ((adjacentX < 0) ||
						(adjacentX > maxX) ||
						(adjacentX == x && adjacentY == y))
						continue;

					adjacentCells.Add(graph.GetElement(adjacentX, adjacentY));
				}
			}

			return adjacentCells;
		}


		/// <summary>
		/// Get all surrounding elements (including diagonals).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="graph"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static List<T> GetSurroundingElements<T>(IGraph<T> graph, int x, int y)
		{
			List<T> surroundingCells = new List<T>();

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

					surroundingCells.Add(graph.GetElement(adjacentX, adjacentY));
				}
			}

			return surroundingCells;
		}

	}
}
