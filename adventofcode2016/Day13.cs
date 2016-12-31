using adventofcode2016.Tools;
using System;
using System.Linq;
using static adventofcode2016.Tools.Node;

namespace adventofcode2016
{
	public class Day13 : IDay
	{
		public class PuzzleSolver
		{
			private readonly int _favoriteNumber;
			public PuzzleSolver(int favoriteNumber)
			{
				_favoriteNumber = favoriteNumber;
			}

			public int FindShortestPath(Point start, Point destination)
			{
				var pathFinder = new PathFinder();
				return pathFinder.FindShortestPath(start, destination, CalcNodeType);
			}

			public int CountPathsLessThan50(Point start, Point destination)
			{
				var pathFinder = new PathFinder();
				return pathFinder.FindAllPaths(start, destination, CalcNodeType, 50).Count();
			}

			private NodeTypes CalcNodeType(int x, int y)
			{
				var solution = x * x + 3 * x + 2 * x * y + y + y * y;
				solution += _favoriteNumber;
				return (Convert.ToString(solution, 2).Count(bit => bit == '1') % 2 == 0) ?
					NodeTypes.OpenSpace : NodeTypes.Wall;
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 13: A Maze of Twisty Little Cubicles ---"; } }

		public void PrintDay()
		{
			{
				var solver = new PuzzleSolver(1362);
				Console.WriteLine("Answer A: " + solver.FindShortestPath(
					new Point(1, 1), new Point(31, 39)));
			}
			{
				var solver = new PuzzleSolver(1362);
				Console.WriteLine("Answer B: " + solver.CountPathsLessThan50(
					new Point(1, 1), new Point(31, 39)));
			}
			Console.WriteLine();
		}
	}
}
