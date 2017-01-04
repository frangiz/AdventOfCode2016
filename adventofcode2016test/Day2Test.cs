using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day2Test
	{
		[Test]
		public void NoMoveReturnsStartButton()
		{
			Assert.AreEqual('5', new Day2.KeyPad('5').Move(""));
		}

		[Test]
		public void TestMoveOutsideOfLeftSideIsBlocked()
		{
			Assert.AreEqual('4', new Day2.KeyPad('5').Move("LL"));
		}

		[Test]
		public void TestMoveOutsideOfRightSideIsBlocked()
		{
			Assert.AreEqual('6', new Day2.KeyPad('5').Move("RR"));
		}

		[Test]
		public void TestMoveOutsideOfTopSideIsBlocked()
		{
			Assert.AreEqual('2', new Day2.KeyPad('5').Move("UU"));
		}

		[Test]
		public void TestMoveOutsideOfBottomSideIsBlocked()
		{
			Assert.AreEqual('8', new Day2.KeyPad('5').Move("DD"));
		}

		[Test]
		public void ExampleA1()
		{
			var keypad = new Day2.KeyPad('5');
			var moves = new List<string>
			{
				"ULL",
				"RRDDD",
				"LURDL",
				"UUUUD"
			};
			moves.ForEach(move => keypad.Move(move));
			Assert.AreEqual("1985", keypad.KeysPressed);
		}

		[Test]
		public void AnswerA()
		{
			var keypad = new Day2.KeyPad('5');
			foreach (var move in File.ReadAllLines("Day2_input.txt"))
			{
				keypad.Move(move);
			}
			Assert.AreEqual("76792", keypad.KeysPressed);
		}

		[Test]
		public void ExampleB1()
		{
			var keypad = new Day2.KeyPad('5', false);
			Assert.AreEqual('5', keypad.Move(""));
			Assert.AreEqual('D', keypad.Move("RRDDD"));
			Assert.AreEqual('B', keypad.Move("DURRL"));
			Assert.AreEqual('3', keypad.Move("UUUUD"));
			Assert.AreEqual("5DB3", keypad.KeysPressed);
		}

		[Test]
		public void AnswerB()
		{
			var keypad = new Day2.KeyPad('5', false);
			foreach (var move in File.ReadAllLines("Day2_input.txt"))
			{
				keypad.Move(move);
			}
			Assert.AreEqual("A7AC3", keypad.KeysPressed);
		}
	}
}
