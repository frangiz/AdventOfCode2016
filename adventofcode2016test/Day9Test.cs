using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day9Test
	{
		[Test]
		public void ExampleA1()
		{
			Assert.AreEqual("ADVENT".Length, Day9.Expand("ADVENT"));
		}

		[Test]
		public void ExampleA2()
		{
			Assert.AreEqual("ABBBBBC".Length, Day9.Expand("A(1x5)BC"));
		}

		[Test]
		public void ExampleA3()
		{
			Assert.AreEqual("XYZXYZXYZ".Length, Day9.Expand("(3x3)XYZ"));
		}

		[Test]
		public void ExampleA4()
		{
			Assert.AreEqual("ABCBCDEFEFG".Length, Day9.Expand("A(2x2)BCD(2x2)EFG"));
		}

		[Test]
		public void ExampleA5()
		{
			Assert.AreEqual("(1x3)A".Length, Day9.Expand("(6x1)(1x3)A"));
		}

		[Test]
		public void ExampleA6()
		{
			Assert.AreEqual("X(3x3)ABC(3x3)ABCY".Length, Day9.Expand("X(8x2)(3x3)ABCY"));
		}

		[Test]
		public void ExampleB1()
		{
			Assert.AreEqual("XYZXYZXYZ".Length, Day9.Expand("(3x3)XYZ", true));
		}

		[Test]
		public void ExampleB2()
		{
			Assert.AreEqual("XABCABCABCABCABCABCY".Length, Day9.Expand("X(8x2)(3x3)ABCY", true));
		}

		[Test]
		public void ExampleB3()
		{
			Assert.AreEqual(241920, Day9.Expand("(27x12)(20x12)(13x14)(7x10)(1x12)A", true));
		}

		[Test]
		public void ExampleB4()
		{
			Assert.AreEqual(445, Day9.Expand("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", true));
		}
	}
}
