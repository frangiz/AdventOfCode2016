using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day1Test
	{
		[Test]
		public void ExampleA1()
		{
			Assert.AreEqual(5, new Day1().FindDistance("R2, L3"));
		}

		[Test]
		public void ExampleA2()
		{
			Assert.AreEqual(2, new Day1().FindDistance("R2, R2, R2"));
		}

		[Test]
		public void ExampleA3()
		{
			Assert.AreEqual(12, new Day1().FindDistance("R5, L5, R5, R3"));
		}

		[Test]
		public void ExampleB1()
		{
			Assert.AreEqual(4, new Day1(true).FindDistance("R8, R4, R4, R8"));
		}
	}
}
