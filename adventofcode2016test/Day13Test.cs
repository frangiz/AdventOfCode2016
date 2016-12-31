using adventofcode2016;
using adventofcode2016.Tools;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day13Test
	{
		[Test]
		public void ExampleA1()
		{
			var solver = new Day13.PuzzleSolver(10);
			Assert.AreEqual(11, solver.FindShortestPath(new Point(1, 1), new Point(7, 4)));
		}

		[Test]
		public void AnswerA()
		{
			var solver = new Day13.PuzzleSolver(1362);
			Assert.AreEqual(82, solver.FindShortestPath(new Point(1, 1), new Point(31, 39)));
		}
		[Test]
		public void AnswerB()
		{
			var solver = new Day13.PuzzleSolver(1362);
			Assert.AreEqual(138, solver.CountPathsLessThan50(new Point(1, 1), new Point(31, 39)));
		}

	}
}
