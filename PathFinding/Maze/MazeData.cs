using System.Drawing;

using PathFinding.Map;


namespace PathFinding
{
	public class MazeData
	{

		#region Fields
		private readonly Bitmap bitmap;
		private readonly MapFeature[,] features;
		private readonly Point mazeEnd;
		private readonly Point mazeStart;
		#endregion


		#region Constructors
	    public MazeData(Bitmap bitmap, MapFeature[,] features, Point mazeStart, Point mazeEnd)
	    {
	        this.bitmap = bitmap;
	        this.features = features;
            this.mazeStart = mazeStart;
            this.mazeEnd = mazeEnd;
	    }
	    #endregion


		#region Properties/Indexers
		public Bitmap Bitmap
		{
			get { return this.bitmap; }
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
