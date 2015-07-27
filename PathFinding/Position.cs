using System;


namespace PathFinding
{
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


	public static class PointUtilities
	{

		public static double DistanceTo(this Position origin, Position destination)
		{
			int xDiff = origin.X - destination.X;
			int yDiff = origin.Y - destination.Y;
			double a2 = xDiff * xDiff;
			double b2 = yDiff * yDiff;
			return Math.Sqrt(Math.Abs(a2 + b2));
		}


		public static Position SetX(this Position position, int newX)
		{
			return new Position(newX, position.Y);
		}


		public static Position SetY(this Position position, int newY)
		{
			return new Position(position.X, newY);
		}

	}
}