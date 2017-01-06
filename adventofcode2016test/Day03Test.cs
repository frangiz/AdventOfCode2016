using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using static adventofcode2016.Day03;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day03Test
	{
		[Test]
		public void ExampleA1()
		{
			Assert.AreEqual(0, Day03.ValidTriangles(
				new HorizontalParser(new List<string> { "5 10 25" })));
		}

		[Test]
		public void AnswerA()
		{
			Assert.AreEqual(1050, Day03.ValidTriangles(
				new HorizontalParser(File.ReadAllLines("Day3_input.txt"))));
		}

		[Test]
		public void AnswerB()
		{
			Assert.AreEqual(1921, Day03.ValidTriangles(
				new VerticalParser(File.ReadAllLines("Day3_input.txt"))));
		}
	}
}
