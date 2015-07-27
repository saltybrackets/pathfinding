using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding;


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
				Position start = new Position(0, 0);
				Position end = new Position(3, 4);
				Assert.AreEqual(5, start.DistanceTo(end));
			}


			[TestMethod]
			public void CalculateHorizontal()
			{
				Position start = new Position(0, 0);
				Position end = new Position(5, 0);
				Assert.AreEqual(5, start.DistanceTo(end));
			}


			[TestMethod]
			public void CalculateVertical()
			{
				Position start = new Position(0, 0);
				Position end = new Position(0, 5);
				Assert.AreEqual(5, start.DistanceTo(end));
			}

		}


		[TestClass]
		public class SetXMethod
		{

			[TestMethod]
			public void CheckX()
			{
				Position position = new Position(0, 0);
				position = position.SetX(5);
				Assert.AreEqual(5, position.X);
			}

		}


		[TestClass]
		public class SetYMethod
		{

			[TestMethod]
			public void CheckY()
			{
				Position position = new Position(0, 0);
				position = position.SetY(5);
				Assert.AreEqual(5, position.Y);
			}

		}

	}
}
