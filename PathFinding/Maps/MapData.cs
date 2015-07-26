using System.Drawing;


namespace PathFinding
{
	public class MapData
	{

		#region Fields
		private readonly Bitmap originalBitmap;
		private readonly MapFeature[,] features;
		private readonly Point mazeEnd;
		private readonly Point mazeStart;
		#endregion


		#region Constructors
	    public MapData(Bitmap originalBitmap, MapFeature[,] features, Point mazeStart, Point mazeEnd)
	    {
	        this.originalBitmap = originalBitmap;
	        this.features = features;
            this.mazeStart = mazeStart;
            this.mazeEnd = mazeEnd;
	    }
	    #endregion


		#region Properties/Indexers
		public Bitmap OriginalBitmap
		{
			get { return this.originalBitmap; }
		}


		public MapFeature[,] Features
		{
			get { return this.features; }
		}


		public Point MazeEnd
		{
			get { return this.mazeEnd; }
		}


		public Point MazeStart
		{
			get { return this.mazeStart; }
		}
		#endregion

	}
}
