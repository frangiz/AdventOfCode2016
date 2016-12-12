using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day12Test
	{
		[Test]
		public void ExampleA1()
		{
			var computer = new Day12.Computer();
			var instructions = new List<string>
			{
				"cpy 41 a",
				"inc a",
				"inc a",
				"dec a",
				"jnz a 2",
				"dec a"
			};
			computer.ExecuteInstructions(instructions);
			Assert.AreEqual(42, computer.Registers[0]);
		}
	}
}
