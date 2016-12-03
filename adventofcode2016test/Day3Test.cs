using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;
using static adventofcode2016.Day3;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day3Test
	{
		[Test]
		public void ExampleA1()
		{
			
			Assert.AreEqual(0, new Day3(
				new HorizontalParser(
					new List<string> { "5 10 25" }))
				.ValidTriangles());
		}
	}
}
