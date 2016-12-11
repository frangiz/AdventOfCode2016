using adventofcode2016;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day10Test
	{
		[Test]
		public void ExampleA1()
		{
			var botFactory = new Day10.BotFactory(3, 3);
			var instructions = new List<string>
			{
				"value 5 goes to bot 2",
				"bot 2 gives low to bot 1 and high to bot 0",
				"value 3 goes to bot 1",
				"bot 1 gives low to output 1 and high to bot 0",
				"bot 0 gives low to output 2 and high to output 0",
				"value 2 goes to bot 2"
			};
			botFactory.AddInstructions(instructions);
			Assert.AreEqual(5, botFactory.OutputBins[0].Value);
			Assert.AreEqual(2, botFactory.OutputBins[1].Value);
			Assert.AreEqual(3, botFactory.OutputBins[2].Value);

			Assert.True(botFactory.Bots[2].ComparedValues.Contains(new Tuple<int, int>(2,5)));

		}
	}
}
