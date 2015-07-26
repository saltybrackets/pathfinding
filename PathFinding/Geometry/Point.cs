using System;


namespace PathFinding
{
	public struct Point
	{
		#region Fields
		private readonly int x;
		private readonly int y;
		#endregion


		#region Constructors
		public Point(int x, int y)
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

		public static double DistanceTo(this Point origin, Point destination)
		{
			double a2 = (origin.X - destination.X) ^ 2;
			double b2 = (origin.Y - destination.Y) ^ 2;
			return Math.Abs(Math.Sqrt(a2 + b2));
		}


		public static Point SetX(this Point point, int newX)
		{
			return new Point(newX, point.Y);
		}


		public static Point SetY(this Point point, int newY)
		{
			return new Point(point.X, newY);
		}

	}
}