using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day14Test
	{
		[Test]
		public void ExampleA1()
		{
			var generator = new Day14.KeyGenerator("abc");
			generator.GenerateKeys();
			Assert.AreNotEqual(18, generator.IndexForKeyIndex(0));
		}

		[Test]
		public void ExampleA2()
		{
			var generator = new Day14.KeyGenerator("abc");
			generator.GenerateKeys();
			Assert.AreEqual(39, generator.IndexForKeyIndex(0));
		}

		[Test]
		public void ExampleA3()
		{
			var generator = new Day14.KeyGenerator("abc");
			generator.GenerateKeys();
			Assert.AreEqual(92, generator.IndexForKeyIndex(1));
		}

		[Test]
		public void ExampleA4()
		{
			var generator = new Day14.KeyGenerator("abc");
			generator.GenerateKeys();
			Assert.AreEqual(22728, generator.IndexForKeyIndex(63));
		}
	}
}
