using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day17Test
	{
		[Test]
		public void ExampleA1()
		{
			var solver = new Day17.PuzzleSolver();
			Assert.AreEqual("DDRRRD", solver.FindShortestPath("ihgpwlah"));
		}

		[Test]
		public void ExampleA2()
		{
			var solver = new Day17.PuzzleSolver();
			Assert.AreEqual("DDUDRLRRUDRD", solver.FindShortestPath("kglvqrro"));
		}

		[Test]
		public void ExampleA3()
		{
			var solver = new Day17.PuzzleSolver();
			Assert.AreEqual("DRURDRUDDLLDLUURRDULRLDUUDDDRR", solver.FindShortestPath("ulqzkmiv"));
		}

		[Test]
		public void ExampleB1()
		{
			var solver = new Day17.PuzzleSolver();
			Assert.AreEqual(370, solver.FindLongestPath("ihgpwlah").Length);
		}

		[Test]
		public void ExampleB2()
		{
			var solver = new Day17.PuzzleSolver();
			Assert.AreEqual(492, solver.FindLongestPath("kglvqrro").Length);
		}

		[Test]
		public void ExampleB3()
		{
			var solver = new Day17.PuzzleSolver();
			Assert.AreEqual(830, solver.FindLongestPath("ulqzkmiv").Length);
		}

	}
}
