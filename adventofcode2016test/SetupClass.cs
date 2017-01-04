using NUnit.Framework;
using System;
using System.IO;

namespace adventofcode2016test
{
	[SetUpFixture]
	internal class SetUpClass
	{
		[OneTimeSetUp]
		public void Setup()
		{
			Environment.CurrentDirectory = Path.GetDirectoryName(typeof(SetUpClass).Assembly.Location);
		}
	}
}
