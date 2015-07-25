namespace PathFinding
{
	class PathNode
	{
		public PathNode LeftFork { get; set; }
		public PathNode RightFork { get; set; }
		public Directions Junction { get; set; }

		public PathNode(Directions junction)
		{
			this.Junction = junction;
		}
	}


	class Path
	{

		private PathNode start;


		public void Add(PathNode node)
		{

		}

		//public void Insert()

	}
}
