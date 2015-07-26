using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Geometry;


namespace PathFindingTests.Geometry
{
	
	public class LineTests
	{

		[TestClass]
		public class DistanceMethod
		{

			[TestMethod]
			public void CalculateDiagonal()
			{
				Line raycast = new Line(new Point(0,0), new Point(3, 4));
				Assert.AreEqual(5, raycast.Distance());
			}


			[TestMethod]
			public void CalculateHorizontal()
			{
				Line raycast = new Line(new Point(0, 0), new Point(0, 5));
				Assert.AreEqual(5, raycast.Distance());
			}


			[TestMethod]
			public void CalculateVertical()
			{
				Line raycast = new Line(new Point(0, 0), new Point(5, 0));
				Assert.AreEqual(5, raycast.Distance());
			}

		}
		
	}
}
