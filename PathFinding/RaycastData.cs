

using System;
using System.Threading;


namespace PathFinding.Physics
{
	/// <summary>
	/// Encapsulate information 
	/// </summary>
	public class RaycastData
	{
		private readonly Point startPosition;
		private readonly Point endPosition;


		public RaycastData(Point startPosition, Point endPosition)
		{
			this.startPosition = startPosition;
			this.endPosition = endPosition;
		}


		public Point StartPosition
		{
			get { return this.startPosition; }
		}


		public Point EndPosition
		{
			get { return this.endPosition; }
		}
	}


	public static class RaycastDataUtilities
	{
		
		public static double Distance(this RaycastData raycastData)
		{
			double a2 = (raycastData.StartPosition.X - raycastData.EndPosition.X) ^ 2;
			double b2 = (raycastData.StartPosition.Y - raycastData.EndPosition.Y) ^ 2;
			return Math.Abs(Math.Sqrt(a2 + b2));
		}

	}

}
