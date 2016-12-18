using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day18Test
	{
		[Test]
		public void ExampleA1()
		{
			var trapRoom = new Day18.TrapRoom(".^^.^.^^^^");
			trapRoom.AddRows(9);
			Assert.AreEqual(38, trapRoom.NumberOfSafeTiles);
		}

	}
}
