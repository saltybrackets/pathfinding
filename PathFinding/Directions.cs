using System;


namespace PathFinding.Maps
{
	/// <summary>
	/// Flags for holding directional information.
	/// </summary>
	[Flags] public enum Directions
	{
		None = 0,
		Up = 1,
		Down = 2,
		Left = 4,
		Right = 8,
		UpperLeft = 16,
		UpperRight = 32,
		LowerLeft = 64,
		LowerRight = 128,
		All = Up | UpperRight | Right | LowerRight | Down | LowerLeft | Left | UpperLeft
	}

	
	public enum Rotation
	{
		Clockwise,
		CounterClockwise
	}


	/// <summary>
	/// Bitwise operations are hard to remember.
	/// Extensions to simplify Direction flags manipulation.
	/// </summary>
	public static class DirectionUtilities
	{
		
		public static bool HasDown(this Directions directions)
		{
			return (directions & Directions.Down) == Directions.Down;
		}


		public static bool HasLeft(this Directions directions)
		{
			return (directions & Directions.Left) == Directions.Left;
		}


		public static bool HasLowerLeft(this Directions directions)
		{
			return (directions & Directions.LowerLeft) == Directions.LowerLeft;
		}


		public static bool HasLowerRight(this Directions directions)
		{
			return (directions & Directions.LowerRight) == Directions.LowerRight;
		}


		public static bool HasRight(this Directions directions)
		{
			return (directions & Directions.Right) == Directions.Right;
		}


		public static bool HasUp(this Directions directions)
		{
			return (directions & Directions.Up) == Directions.Up;
		}


		public static bool HasUpperLeft(this Directions directions)
		{
			return (directions & Directions.UpperLeft) == Directions.UpperLeft;
		}


		public static bool HasUpperRight(this Directions directions)
		{
			return (directions & Directions.UpperRight) == Directions.UpperRight;
		}


		public static Directions Rotate(this Directions direction, Rotation rotation)
		{
			// Counter Clockwise
			if (rotation == Rotation.CounterClockwise)
			{
				switch (direction)
				{
					default:
						return Directions.None;
					case Directions.Up:
						return Directions.UpperLeft;
					case Directions.UpperLeft:
						return Directions.Left;
					case Directions.Left:
						return Directions.LowerLeft;
					case Directions.LowerLeft:
						return Directions.Down;
					case Directions.Down:
						return Directions.LowerRight;
					case Directions.LowerRight:
						return Directions.Right;
					case Directions.Right:
						return Directions.UpperRight;
					case Directions.UpperRight:
						return Directions.Up;
				}
			}

			// Clockwise
			switch (direction)
			{
				default:
					return Directions.None;
				case Directions.Up:
					return Directions.UpperRight;
				case Directions.UpperRight:
					return Directions.Right;
				case Directions.Right:
					return Directions.LowerRight;
				case Directions.LowerRight:
					return Directions.Down;
				case Directions.Down:
					return Directions.LowerLeft;
				case Directions.LowerLeft:
					return Directions.Left;
				case Directions.Left:
					return Directions.UpperLeft;
				case Directions.UpperLeft:
					return Directions.Up;
			}
		}


		public static Directions SetDown(this Directions directions, bool value = true)
		{
			if (value)
				return directions |= Directions.Down;
			else
				return directions &= ~Directions.Down;
		}


		public static Directions SetLeft(this Directions directions, bool value = true)
		{
			if (value)
				return directions |= Directions.Left;
			else
				return directions &= ~Directions.Left;
		}


		public static Directions SetLowerLeft(this Directions directions, bool value = true)
		{
			if (value)
				return directions |= Directions.LowerLeft;
			else
				return directions &= ~Directions.LowerLeft;
		}


		public static Directions SetLowerRight(this Directions directions, bool value = true)
		{
			if (value)
				return directions |= Directions.LowerRight;
			else
				return directions &= ~Directions.LowerRight;
		}


		public static Directions SetRight(this Directions directions, bool value = true)
		{
			if (value)
				return directions |= Directions.Right;
			else
				return directions &= ~Directions.Right;
		}


		public static Directions SetUp(this Directions directions, bool value = true)
		{
			if (value)
				return directions |= Directions.Up;
			else
				return directions &= ~Directions.Up;
		}


		public static Directions SetUpperLeft(this Directions directions, bool value = true)
		{
			if (value)
				return directions |= Directions.UpperLeft;
			else
				return directions &= ~Directions.UpperLeft;
		}


		public static Directions SetUpperRight(this Directions directions, bool value = true)
		{
			if (value)
				return directions |= Directions.UpperRight;
			else
				return directions &= ~Directions.UpperRight;
		}

	}
}