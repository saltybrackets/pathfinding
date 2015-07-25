namespace PathFinding
{
	public struct Point
	{
		private readonly int x;
		private readonly int y;


		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}


		public int X
		{
			get { return this.x; }
		}


		public int Y
		{
			get { return this.y; }
		}
	}


	public static class PointUtilities
	{
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
