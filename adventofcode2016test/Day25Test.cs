using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day25Test
	{
		[Test]
		[Category("AssemBunny")]
		public void AnswerA()
		{
			Assert.AreEqual("192", new Day25().GetAnswerA());
		}
	}
}
