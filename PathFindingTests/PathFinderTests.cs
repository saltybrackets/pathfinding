using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding;
using PathFinding.Maps;


namespace PathFindingTests
{
	[TestClass]
	public class PathFinderTests
	{

		Map simpleMap = new Map(Properties.Resources.maze1);

		public class FindPathMethod : PathFinderTests
		{

			

			[TestMethod]
			public void FindPathWithLeftHandStrategy()
			{
				
				PathFinder pathFinder = new PathFinder();

			}
			
		}

		
	}
}
