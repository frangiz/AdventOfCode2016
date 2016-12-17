using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day16Test
	{
		[Test]
		public void ExampleA1()
		{
			var data = Day16.GenerateData("110010110100", 12);
			Assert.AreEqual("110010110100", data);
			Assert.AreEqual("100", Day16.GenerateChecksum(data));
		}

		[Test]
		public void ExampleA2()
		{
			var data = Day16.GenerateData("10000", 20);
			Assert.AreEqual("10000011110010000111", data);
			Assert.AreEqual("01100", Day16.GenerateChecksum(data));
		}

	}
}
