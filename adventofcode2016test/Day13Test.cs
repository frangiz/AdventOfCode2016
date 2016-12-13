using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day13Test
	{
		[Test]
		public void ExampleA1()
		{
			var solver = new Day13.PuzzleSolver();
			Assert.AreEqual(11, solver.FindShortestPath(new Day13.Point(1, 1), new Day13.Point(7, 4), 10));
		}
	}
}
