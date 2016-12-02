using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day2Test
	{
		[Test]
		public void NoMoveReturnsStartButton()
		{
			Assert.AreEqual('5', new Day2('5').Move(""));
		}

		[Test]
		public void TestMoveOutsideOfLeftSideIsBlocked()
		{
			Assert.AreEqual('4', new Day2('5').Move("LL"));
		}

		[Test]
		public void TestMoveOutsideOfRightSideIsBlocked()
		{
			Assert.AreEqual('6', new Day2('5').Move("RR"));
		}

		[Test]
		public void TestMoveOutsideOfTopSideIsBlocked()
		{
			Assert.AreEqual('2', new Day2('5').Move("UU"));
		}

		[Test]
		public void TestMoveOutsideOfBottomSideIsBlocked()
		{
			Assert.AreEqual('8', new Day2('5').Move("DD"));
		}

		[Test]
		public void ExampleA1()
		{
			var day2 = new Day2('5');
			var moves = new List<string>
			{
				"ULL",
				"RRDDD",
				"LURDL",
				"UUUUD"
			};
			moves.ForEach(move => day2.Move(move));
			Assert.AreEqual("1985", day2.KeysPressed);
		}

		[Test]
		public void ExampleB1()
		{
			var day2 = new Day2('5', false);
			Assert.AreEqual('5', day2.Move(""));
			Assert.AreEqual('D', day2.Move("RRDDD"));
			Assert.AreEqual('B', day2.Move("DURRL"));
			Assert.AreEqual('3', day2.Move("UUUUD"));
			Assert.AreEqual("5DB3", day2.KeysPressed);
		}
	}
}
