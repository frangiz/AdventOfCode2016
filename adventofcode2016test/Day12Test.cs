using adventofcode2016.Tools;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day12Test
	{
		[Test]
		[Category("AssemBunny")]
		public void ExampleA1()
		{
			var assemBunny = new AssemBunny();
			var instructions = new List<string>
			{
				"cpy 41 a",
				"inc a",
				"inc a",
				"dec a",
				"jnz a 2",
				"dec a"
			};
			assemBunny.ExecuteInstructions(instructions);
			Assert.AreEqual(42, assemBunny.Registers['a']);
		}

		[Test]
		[Category("AssemBunny")]
		public void AnswerA()
		{
			var assemBunny = new AssemBunny();
			assemBunny.ExecuteInstructions(File.ReadAllLines("Day12_input.txt").ToList());
			Assert.AreEqual(318083, assemBunny.Registers['a']);
		}

		[Test]
		[Category("AssemBunny")]
		public void AnswerB()
		{
			var assemBunny = new AssemBunny();
			assemBunny.Registers['c'] = 1;
			assemBunny.ExecuteInstructions(File.ReadAllLines("Day12_input.txt").ToList());
			Assert.AreEqual(9227737, assemBunny.Registers['a']);
		}
	}
}
