using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding;
using PathFinding.Maps;
using PathFinding.Strategies;


namespace PathFindingTests
{
	
	public class PathFinderTests
	{

		private Bitmap simpleMap = Properties.Resources.maze1;
		private Bitmap largeMap = Properties.Resources.maze2;
		private Bitmap organicMap = Properties.Resources.maze3;


		[TestClass]
		public class FindPathMethod : PathFinderTests
		{

			[TestMethod]
			public void FindPathWithBreadthFirstStrategy()
			{
				Map map;
				ITraversalStrategy strategy = Strategies.BreadthFirst;
				
				MapPath solution;

				map = new Map(this.simpleMap);
				solution = PathFinder.FindPath(map, strategy);
				PathFinder.OutputBitmap(solution, map, "simpleSolution.png");
				Assert.IsNotNull(solution);

				map = new Map(this.largeMap);
				solution = PathFinder.FindPath(map, strategy);
				PathFinder.OutputBitmap(solution, map, "largeSolution.png");
				Assert.IsNotNull(solution);

				// TODO: Technically, this fails, because the path goes through
				// some diagonal walls.
				map = new Map(this.organicMap);
				solution = PathFinder.FindPath(map, strategy);
				PathFinder.OutputBitmap(solution, map, "organicSolution.png");
				Assert.IsNotNull(solution);
			}
			
		}

		
	}
}
