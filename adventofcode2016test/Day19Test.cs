using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day19Test
	{
		[Test]
		public void ExampleA1()
		{
			var game = new Day19.WhiteElephantGame(5);
			game.TakePresentsFromLeft();
			Assert.AreEqual(3, game.WhoHasAllThePresents);
		}

		[Test]
		public void ExampleB1()
		{
			var game = new Day19.WhiteElephantGame(5);
			game.TakePresentsFromOpposite();
			Assert.AreEqual(2, game.WhoHasAllThePresents);
		}

		[Test]
		public void ExampleB2()
		{
			var game = new Day19.WhiteElephantGame(6);
			game.TakePresentsFromOpposite();
			Assert.AreEqual(3, game.WhoHasAllThePresents);
		}
	}
}
