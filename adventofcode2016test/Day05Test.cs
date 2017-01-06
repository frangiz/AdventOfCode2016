using adventofcode2016;
using NUnit.Framework;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day05Test
	{
		[Test]
		public void ExampleA1()
		{
			Assert.AreEqual(new char[] { '1','\0','\0','\0','\0','\0','\0','\0' },
				Day05.FindPassword("abc", 1));
		}

		[Test]
		public void ExampleA2()
		{
			Assert.AreEqual(new char[] { '1','8','\0','\0','\0','\0','\0','\0' },
				Day05.FindPassword("abc", 2));
		}

		[Test]
		public void ExampleA3()
		{
			Assert.AreEqual(new char[] { '1', '8', 'f', '\0', '\0', '\0', '\0', '\0' },
				Day05.FindPassword("abc", 3));
		}

		[Test]
		public void ExampleA4()
		{
			Assert.AreEqual(new char[] { '1', '8', 'f', '4', '7', 'a', '3', '0' }, 
				Day05.FindPassword("abc", 8));
		}

		[Test]
		public void AnswerA()
		{
			Assert.AreEqual(new char[] { '4', '5', '4', '3', 'c', '1', '5', '4' }, 
				Day05.FindPassword("ojvtpuvg", 8));
		}

		[Test]
		public void ExampleB1()
		{
			Assert.AreEqual(new char[] { '\0', '5', '\0', '\0', '\0', '\0', '\0', '\0' },
				Day05.FindPassword("abc", 1, true));
		}

		[Test]
		public void ExampleB2()
		{
			Assert.AreEqual(new char[] { '\0', '5', '\0', '\0', 'e', '\0', '\0', '\0' },
				Day05.FindPassword("abc", 2, true));
		}

		[Test]
		public void ExampleB3()
		{
			Assert.AreEqual(new char[] { '0', '5', 'a', 'c', 'e', '8', 'e', '3' },
				Day05.FindPassword("abc", 8, true));
		}

		[Test]
		public void AnswerB()
		{
			Assert.AreEqual(new char[] { '1', '0', '5', '0', 'c', 'b', 'b', 'd' },
				Day05.FindPassword("ojvtpuvg", 8, true));
		}
	}
}
