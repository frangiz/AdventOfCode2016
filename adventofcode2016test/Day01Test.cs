using adventofcode2016;
using NUnit.Framework;
using System.IO;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day01Test
	{
		[Test]
		public void ExampleA1()
		{
			Assert.AreEqual(5, new Day01.DistanceFinder().FindDistance("R2, L3"));
		}

		[Test]
		public void ExampleA2()
		{
			Assert.AreEqual(2, new Day01.DistanceFinder().FindDistance("R2, R2, R2"));
		}

		[Test]
		public void ExampleA3()
		{
			Assert.AreEqual(12, new Day01.DistanceFinder().FindDistance("R5, L5, R5, R3"));
		}

		[Test]
		public void AnswerA()
		{
			Assert.AreEqual(300, new Day01.DistanceFinder().FindDistance(File.ReadAllText("Day1_input.txt")));
		}

		[Test]
		public void ExampleB1()
		{
			Assert.AreEqual(4, new Day01.DistanceFinder(true).FindDistance("R8, R4, R4, R8"));
		}

		[Test]
		public void AnswerB()
		{
			Assert.AreEqual(159, new Day01.DistanceFinder(true).FindDistance(File.ReadAllText("Day1_input.txt")));
		}
	}
}
