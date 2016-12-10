using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day8Test
	{
		[Test]
		public void ExampleA1()
		{
			var monitor = new Day8.Monitor(7, 3);
			monitor.ActivateCommand("rect 3x2");
			var expectedOutput = new List<string>
			{
				"###....",
				"###....",
				"......."
			};

			Assert.AreEqual(expectedOutput, monitor.GetOutput());
		}

		[Test]
		public void ExampleA2()
		{
			var monitor = new Day8.Monitor(7, 3);
			monitor.ActivateCommand("rect 3x2");
			monitor.ActivateCommand("rotate column x=1 by 1");
			var expectedOutput = new List<string>
			{
				"#.#....",
				"###....",
				".#....."
			};

			Assert.AreEqual(expectedOutput, monitor.GetOutput());
		}

		[Test]
		public void ExampleA3()
		{
			var monitor = new Day8.Monitor(7, 3);
			monitor.ActivateCommand("rect 3x2");
			monitor.ActivateCommand("rotate column x=1 by 1");
			monitor.ActivateCommand("rotate row y=0 by 4");
			var expectedOutput = new List<string>
			{
				"....#.#",
				"###....",
				".#....."
			};

			Assert.AreEqual(expectedOutput, monitor.GetOutput());
		}

		[Test]
		public void ExampleA4()
		{
			var monitor = new Day8.Monitor(7, 3);
			monitor.ActivateCommand("rect 3x2");
			monitor.ActivateCommand("rotate column x=1 by 1");
			monitor.ActivateCommand("rotate row y=0 by 4");
			monitor.ActivateCommand("rotate column x=1 by 1");
			var expectedOutput = new List<string>
			{
				".#..#.#",
				"#.#....",
				".#....."
			};

			Assert.AreEqual(expectedOutput, monitor.GetOutput());
		}
	}
}
