namespace PathFinding.Geometry
{
	/// <summary>
	/// Encapsulate information 
	/// </summary>
	public class Line
	{
		#region Fields
		private readonly Point endPosition;
		private readonly Point startPosition;
		#endregion


		#region Constructors
		public Line(Point startPosition, Point endPosition)
		{
			this.startPosition = startPosition;
			this.endPosition = endPosition;
		}
		#endregion


		#region Properties
		public Point EndPosition
		{
			get { return this.endPosition; }
		}


		public Point StartPosition
		{
			get { return this.startPosition; }
		}
		#endregion


		/// <summary>
		/// Return the distance between the start and end ponts.
		/// </summary>
		/// <returns></returns>
		public double Distance()
		{
			return this.startPosition.DistanceTo(this.endPosition);
		}

	}
}