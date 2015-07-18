

namespace PathFinding
{
	/// <summary>
	/// Encapsulates information about paths that are open.
	/// </summary>
	public struct Junction
	{
		#region Fields
		private readonly bool down;
		private readonly bool left;
		private readonly bool right;
		private readonly bool up;
		#endregion


		#region Constructors
		public Junction(bool up, bool down, bool left, bool right)
		{
			this.up = up;
			this.down = down;
			this.left = left;
			this.right = right;
		}
		#endregion


		#region Properties/Indexers
		public bool Down
		{
			get { return this.down; }
		}


		public bool Left
		{
			get { return this.left; }
		}


		public bool Right
		{
			get { return this.right; }
		}


		public bool Up
		{
			get { return this.up; }
		}
		#endregion
	}
}
