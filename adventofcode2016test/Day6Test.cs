using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day6Test
	{
		[Test]
		public void ExampleA1()
		{
			var lines = new List<string>
			{
				"eedadn",
				"drvtee",
				"eandsr",
				"raavrd",
				"atevrs",
				"tsrnev",
				"sdttsa",
				"rasrtv",
				"nssdts",
				"ntnada",
				"svetve",
				"tesnvt",
				"vntsnd",
				"vrdear",
				"dvrsen",
				"enarar"
			};
			Assert.AreEqual("easter", new Day6(lines[0].Length).Decode(lines));
		}

		[Test]
		public void AnswerA()
		{
			var lines = File.ReadAllLines("Day6_input.txt");
			Assert.AreEqual("qqqluigu", new Day6(lines[0].Length).Decode(lines));
		}

		[Test]
		public void ExampleB1()
		{
			var lines = new List<string>
			{
				"eedadn",
				"drvtee",
				"eandsr",
				"raavrd",
				"atevrs",
				"tsrnev",
				"sdttsa",
				"rasrtv",
				"nssdts",
				"ntnada",
				"svetve",
				"tesnvt",
				"vntsnd",
				"vrdear",
				"dvrsen",
				"enarar"
			};
			Assert.AreEqual("advent", new Day6(lines[0].Length).Decode(lines, true));
		}

		[Test]
		public void AnswerB()
		{
			var lines = File.ReadAllLines("Day6_input.txt");
			Assert.AreEqual("lsoypmia", new Day6(lines[0].Length).Decode(lines, true));
		}
	}
}
