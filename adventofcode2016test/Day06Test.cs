﻿using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day06Test
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
			Assert.AreEqual("easter", Day06.Decode(lines[0].Length, lines));
		}

		[Test]
		public void AnswerA()
		{
			var lines = File.ReadAllLines("Day6_input.txt");
			Assert.AreEqual("qqqluigu", Day06.Decode(lines[0].Length, lines));
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
			Assert.AreEqual("advent", Day06.Decode(lines[0].Length, lines, true));
		}

		[Test]
		public void AnswerB()
		{
			var lines = File.ReadAllLines("Day6_input.txt");
			Assert.AreEqual("lsoypmia", Day06.Decode(lines[0].Length, lines, true));
		}
	}
}
