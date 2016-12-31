using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day24Test
	{
		[Test]
		public void ExampleA1()
		{
			var map = new []
			{
				"###########",
				"#0.1.....2#",
				"#.#######.#",
				"#4.......3#",
				"###########"
			};
			var solver = new Day24.Solver(map);
			Assert.AreEqual(14, solver.FindShortestDistance(false));
		}
	}
}
