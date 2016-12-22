using adventofcode2016;
using NUnit.Framework;
using System;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day21Test
	{
		[Test]
		public void ExampleA1()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("ebcda", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0"
			}));
		}

		[Test]
		public void ExampleA2()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("edcba", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0",
				"swap letter d with letter b"
			}));
		}

		[Test]
		public void ExampleA3()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("abcde", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0",
				"swap letter d with letter b",
				"reverse positions 0 through 4"
			}));
		}

		[Test]
		public void ExampleA4()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("bcdea", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0",
				"swap letter d with letter b",
				"reverse positions 0 through 4",
				"rotate left 1 step"
			}));
		}

		[Test]
		public void ExampleA5_1()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("bdeac", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0",
				"swap letter d with letter b",
				"reverse positions 0 through 4",
				"rotate left 1 step",
				"move position 1 to position 4"
			}));
		}

		[Test]
		public void ExampleA5_2()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("bacde", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0",
				"swap letter d with letter b",
				"reverse positions 0 through 4",
				"rotate left 1 step",
				"move position 4 to position 1"
			}));
		}

		[Test]
		public void ExampleA6()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("abdec", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0",
				"swap letter d with letter b",
				"reverse positions 0 through 4",
				"rotate left 1 step",
				"move position 1 to position 4",
				"move position 3 to position 0"
			}));
		}

		[Test]
		public void ExampleA7()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("ecabd", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0",
				"swap letter d with letter b",
				"reverse positions 0 through 4",
				"rotate left 1 step",
				"move position 1 to position 4",
				"move position 3 to position 0",
				"rotate based on position of letter b"
			}));
		}

		[Test]
		public void ExampleA8()
		{
			var scrambler = new Day21.Scrambler();
			Assert.AreEqual("decab", scrambler.Scramble("abcde", new[]
			{
				"swap position 4 with position 0",
				"swap letter d with letter b",
				"reverse positions 0 through 4",
				"rotate left 1 step",
				"move position 1 to position 4",
				"move position 3 to position 0",
				"rotate based on position of letter b",
				"rotate based on position of letter d"
			}));
		}

		[Test]
		public void ExampleB1()
		{
			var scrambler = new Day21.Scrambler();
			var instructions = new[]
			{
				"rotate based on position of letter d"
			};
			Assert.AreEqual("d_______", scrambler.Unscramble("_d______", instructions));
		}

		[Test]
		public void ExampleB2()
		{
			var scrambler = new Day21.Scrambler();
			var instructions = new[]
			{
				"rotate based on position of letter d"
			};
			Assert.AreEqual("_d______", scrambler.Unscramble("___d____", instructions));
		}

		[Test]
		public void ExampleB3()
		{
			var scrambler = new Day21.Scrambler();
			var instructions = new[]
			{
				"rotate based on position of letter d"
			};
			Assert.AreEqual("__d_____", scrambler.Unscramble("_____d__", instructions));
		}

		[Test]
		public void ExampleB4()
		{
			var scrambler = new Day21.Scrambler();
			var instructions = new[]
			{
				"rotate based on position of letter d"
			};
			Assert.AreEqual("___d____", scrambler.Unscramble("_______d", instructions));
		}

		[Test]
		public void ExampleB5()
		{
			var scrambler = new Day21.Scrambler();
			var instructions = new[]
			{
				"rotate based on position of letter d"
			};
			Assert.AreEqual("____d___", scrambler.Unscramble("__d_____", instructions));
		}

		[Test]
		public void ExampleB6()
		{
			var scrambler = new Day21.Scrambler();
			var instructions = new[]
			{
				"rotate based on position of letter d"
			};
			Assert.AreEqual("_____d__", scrambler.Unscramble("____d___", instructions));
		}

		[Test]
		public void ExampleB7()
		{
			var scrambler = new Day21.Scrambler();
			var instructions = new[]
			{
				"rotate based on position of letter d"
			};
			Assert.AreEqual("______d_", scrambler.Unscramble("______d_", instructions));
		}

		[Test]
		public void ExampleB8()
		{
			var scrambler = new Day21.Scrambler();
			var instructions = new[]
			{
				"rotate based on position of letter d"
			};
			Assert.AreEqual("_______d", scrambler.Unscramble("d_______", instructions));
		}
	}
}
