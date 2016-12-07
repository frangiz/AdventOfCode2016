using adventofcode2016;
using NUnit.Framework;
using System.IO;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day7Test
	{
		[Test]
		public void ExampleA1()
		{
			Assert.True(new Day7.IPv7("abba[mnop]qrst").TLSSupport());
		}

		[Test]
		public void ExampleA2()
		{
			Assert.False(new Day7.IPv7("abcd[bddb]xyyx").TLSSupport());
		}

		[Test]
		public void ExampleA3()
		{
			Assert.False(new Day7.IPv7("aaaa[qwer]tyui").TLSSupport());
		}

		[Test]
		public void ExampleA4()
		{
			Assert.True(new Day7.IPv7("ioxxoj[asdfgh]zxcvbn").TLSSupport());
		}

		[Test]
		public void AnswerA()
		{
			Assert.AreEqual(115, Day7.CountIPv7TLSSupport(File.ReadAllLines("Day7_input.txt")));
		}

		[Test]
		public void ExampleB1()
		{
			Assert.True(new Day7.IPv7("aba[bab]xyz").SSLSupport());
		}

		[Test]
		public void ExampleB2()
		{
			Assert.False(new Day7.IPv7("xyx[xyx]xyx").SSLSupport());
		}

		[Test]
		public void ExampleB3()
		{
			Assert.True(new Day7.IPv7("aaa[kek]eke").SSLSupport());
		}

		[Test]
		public void ExampleB4()
		{
			Assert.True(new Day7.IPv7("zazbz[bzb]cdb").SSLSupport());
		}

		[Test]
		public void AnswerB()
		{
			Assert.AreEqual(231, Day7.CountIPv7SSLSupport(File.ReadAllLines("Day7_input.txt")));
		}

	}
}
