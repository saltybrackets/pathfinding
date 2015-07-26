using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Geometry;


namespace PathFindingTests.Geometry
{
	
	public class PointTests
	{

		[TestClass]
		public class DistanceToMethod
		{

			[TestMethod]
			public void CalculateDiagonal()
			{
				Point start = new Point(0, 0);
				Point end = new Point(3, 4);
				Assert.AreEqual(5, start.DistanceTo(end));
			}


			[TestMethod]
			public void CalculateHorizontal()
			{
				Point start = new Point(0, 0);
				Point end = new Point(5, 0);
				Assert.AreEqual(5, start.DistanceTo(end));
			}


			[TestMethod]
			public void CalculateVertical()
			{
				Point start = new Point(0, 0);
				Point end = new Point(0, 5);
				Assert.AreEqual(5, start.DistanceTo(end));
			}

		}


		[TestClass]
		public class SetXMethod
		{

			[TestMethod]
			public void CheckX()
			{
				Point point = new Point(0, 0);
				point = point.SetX(5);
				Assert.AreEqual(5, point.X);
			}

		}


		[TestClass]
		public class SetYMethod
		{

			[TestMethod]
			public void CheckY()
			{
				Point point = new Point(0, 0);
				point = point.SetY(5);
				Assert.AreEqual(5, point.Y);
			}

		}

	}
}
