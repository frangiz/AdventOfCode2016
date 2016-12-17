using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day15Test
	{
		[Test]
		public void ExampleA1()
		{
			
			var discInfo = new string[]
			{
				"Disc #1 has 5 positions; at time=0, it is at position 4.",
				"Disc #2 has 2 positions; at time=0, it is at position 1."
			};
			var sculptur = new Day15.Sculptur(discInfo);
			Assert.AreEqual(5, sculptur.AlignDiscs());
		}

	}
}
