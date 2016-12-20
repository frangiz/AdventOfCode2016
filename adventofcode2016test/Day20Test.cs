using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day20Test
	{
		[Test]
		public void ExampleA1()
		{
			var fw = new Day20.Firewall(new[] { "5-8", "0-2", "4-7" });
			Assert.AreEqual(3, fw.LowestAvailableIP(0, 9));
		}

		[Test]
		public void ExampleB1()
		{
			var fw = new Day20.Firewall(new[] { "5-8", "0-2", "4-7" });
			Assert.AreEqual(2, fw.NumberOfAllowedIps(0, 9));
		}
	}
}
