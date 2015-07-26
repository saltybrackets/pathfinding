namespace PathFinding.Geometry
{
	/// <summary>
	/// Encapsulates state information about an entity.
	/// </summary>
	public class Transform
	{
		#region Fields
		private Directions currentDirection;
		private Point position;
		#endregion


		#region Constructors
		public Transform() {}


		public Transform(Directions currentDirection, Point position)
		{
			this.currentDirection = currentDirection;
			this.position = position;
		}
		#endregion


		#region Properties
		public Directions CurrentDirection
		{
			get { return this.currentDirection; }
			set { this.currentDirection = value; }
		}


		public Point Position
		{
			get { return this.position; }
			set { this.position = value; }
		}
		#endregion
	}
}