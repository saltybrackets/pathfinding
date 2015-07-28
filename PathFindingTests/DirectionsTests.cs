using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Maps;


namespace PathFindingTests
{
	
	public class DirectionsTests
	{

		[TestClass]
		public class HasMethods
		{

			[TestMethod]
			public void HasCumulativeFlags()
			{
				Directions directions = Directions.None;

				directions = directions | Directions.Up;
				Assert.AreEqual(true, directions.HasUp());

				directions = directions | Directions.UpperRight;
				Assert.AreEqual(true, directions.HasUpperRight());

				directions = directions | Directions.Right;
				Assert.AreEqual(true, directions.HasRight());

				directions = directions | Directions.LowerRight;
				Assert.AreEqual(true, directions.HasLowerRight());

				directions = directions | Directions.Down;
				Assert.AreEqual(true, directions.HasDown());

				directions = directions | Directions.LowerLeft;
				Assert.AreEqual(true, directions.HasLowerLeft());

				directions = directions | Directions.Left;
				Assert.AreEqual(true, directions.HasLeft());

				directions = directions | Directions.UpperLeft;
				Assert.AreEqual(true, directions.HasUpperLeft());

				Assert.AreEqual(Directions.All, directions);
			}

		}


		[TestClass]
		public class SetMethods
		{

			[TestMethod]
			public void SetCumulativeFlagsOn()
			{
				Directions directions = Directions.None;

				directions = directions.SetUp();
				Assert.AreEqual(directions |= Directions.Up, directions);

				directions = directions.SetUpperRight();
				Assert.AreEqual(directions |= Directions.UpperRight, directions);

				directions = directions.SetRight();
				Assert.AreEqual(directions |= Directions.UpperRight, directions);

				directions = directions.SetLowerRight();
				Assert.AreEqual(directions |= Directions.LowerRight, directions);

				directions = directions.SetDown();
				Assert.AreEqual(directions |= Directions.Down, directions);

				directions = directions.SetLowerLeft();
				Assert.AreEqual(directions |= Directions.LowerLeft, directions);

				directions = directions.SetLeft();
				Assert.AreEqual(directions |= Directions.Left, directions);

				directions = directions.SetUpperLeft();
				Assert.AreEqual(directions |= Directions.UpperLeft, directions);

				Assert.AreEqual(Directions.All, directions);
			}


			[TestMethod]
			public void SetCumulativeFlagsOff()
			{
				Directions directions = Directions.All;

				directions = directions.SetUp(false);
				directions = directions.SetUpperRight(false);
				directions = directions.SetRight(false);
				directions = directions.SetLowerRight(false);
				directions = directions.SetDown(false);
				directions = directions.SetLowerLeft(false);
				directions = directions.SetLeft(false);
				directions = directions.SetUpperLeft(false);
				
				Assert.AreEqual(Directions.None, directions);
			}

		}


		[TestClass]
		public class RotateMethod
		{

			[TestMethod]
			public void RotateClockwise()
			{
				Directions directions = Directions.Up;

				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.UpperRight, directions);

				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.Right, directions);

				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.LowerRight, directions);

				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.Down, directions);

				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.LowerLeft, directions);

				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.Left, directions);

				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.UpperLeft, directions);

				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.Up, directions);

				directions = Directions.None;
				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.None, directions);

				directions = Directions.All;
				directions = directions.Rotate(Rotation.Clockwise);
				Assert.AreEqual(Directions.None, directions);
			}


			[TestMethod]
			public void RotateCounterClockwise()
			{
				Directions directions = Directions.Up;

				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.UpperLeft, directions);

				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.Left, directions);

				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.LowerLeft, directions);

				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.Down, directions);

				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.LowerRight, directions);

				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.Right, directions);

				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.UpperRight, directions);

				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.Up, directions);

				directions = Directions.None;
				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.None, directions);

				directions = Directions.All;
				directions = directions.Rotate(Rotation.CounterClockwise);
				Assert.AreEqual(Directions.None, directions);
			}

		}
		
	}
}
