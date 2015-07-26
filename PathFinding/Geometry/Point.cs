using System;


namespace PathFinding.Geometry
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
			int xDiff = origin.X - destination.X;
			int yDiff = origin.Y - destination.Y;
            double a2 = xDiff * xDiff;
			double b2 = yDiff * yDiff;
			return Math.Sqrt(Math.Abs(a2 + b2));
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