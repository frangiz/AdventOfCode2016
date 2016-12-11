using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day11Test
	{
		[Test]
		public void ExampleA1()
		{
			var day = new Day11();
			var startState = new Day11.State(new[] { 1, 0, 2, 0 }, 0, 0, null);
			var endState = new Day11.State(new[] { 3, 3, 3, 3 }, 0, 3, null);
			Assert.AreEqual(11, day.SearchAnswer(startState, endState));
		}
	}
}
