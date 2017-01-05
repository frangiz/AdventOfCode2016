using adventofcode2016.Tools;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day23Test
	{
		[Test]
		[Category("AssemBunny")]
		public void ExampleA1()
		{
			var assemBunny = new AssemBunny();
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
			assemBunny.ExecuteInstructions(instructions);
			Assert.AreEqual(3, assemBunny.Registers['a']);
		}

		[Test]
		[Category("AssemBunny")]
		public void AnswerA()
		{
			var assemBunny = new AssemBunny();
			assemBunny.Registers['a'] = 7;
			assemBunny.ExecuteInstructions(File.ReadAllLines("Day23_input.txt").ToList());
			Assert.AreEqual(11760, assemBunny.Registers['a']);
		}

		[Test]
		[Category("AssemBunny")]
		public void AnswerB()
		{
			var assemBunny = new AssemBunny();
			assemBunny.Registers['a'] = 12;
			assemBunny.ExecuteInstructions(File.ReadAllLines("Day23_input.txt").ToList());
			Assert.AreEqual(479008320, assemBunny.Registers['a']);
		}
	}
}
