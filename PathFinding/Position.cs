using System;


namespace PathFinding
{
	/// <summary>
	/// Simple structure for encapsulating positional information.
	/// </summary>
	public struct Position
	{
		#region Fields
		private readonly int x;
		private readonly int y;
		#endregion


		#region Constructors
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		#endregion


		#region Properties
		/// <summary>
		/// Horizontal X coordinate of position.
		/// </summary>
		public int X
		{
			get { return this.x; }
		}


		/// <summary>
		/// Vertical Y coordinate of position.
		/// </summary>
		public int Y
		{
			get { return this.y; }
		}
		#endregion
	}


	public static class PointUtilities
	{
		/// <summary>
		/// Get the straight-line distance between two points.
		/// </summary>
		/// <param name="origin">Starting point.</param>
		/// <param name="destination">Ending point.</param>
		/// <returns></returns>
		public static double DistanceTo(this Position origin, Position destination)
		{
			int xDiff = origin.X - destination.X;
			int yDiff = origin.Y - destination.Y;
			double a2 = xDiff * xDiff;
			double b2 = yDiff * yDiff;
			return Math.Sqrt(Math.Abs(a2 + b2));
		}


		/// <summary>
		/// Clone a new Position object with the given X.
		/// </summary>
		/// <param name="position">Position to clone.</param>
		/// <param name="newX">New X value.</param>
		/// <returns></returns>
		public static Position SetX(this Position position, int newX)
		{
			return new Position(newX, position.Y);
		}


		/// <summary>
		/// Clone a new Position object with the given Y.
		/// </summary>
		/// <param name="position">Position to clone.</param>
		/// <param name="newY">New Y value.</param>
		/// <returns></returns>
		public static Position SetY(this Position position, int newY)
		{
			return new Position(position.X, newY);
		}

	}
}