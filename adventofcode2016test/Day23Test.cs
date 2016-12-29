using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day23Test
	{
		[Test]
		public void ExampleA1()
		{
			var computer = new Day23.Computer();
			var instructions = new List<string>
			{
				"cpy 2 a",
				"tgl a",
				"tgl a",
				"tgl a",
				"cpy 1 a",
				"dec a",
				"dec a"
			};
			computer.ExecuteInstructions(instructions);
			Assert.AreEqual(3, computer.Registers[0]);
		}
	}
}
