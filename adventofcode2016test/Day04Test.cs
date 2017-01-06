using adventofcode2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace adventofcode2016test
{
	[TestFixture]
	internal class Day04Test
	{
		[Test]
		public void ExampleA1()
		{
			var encryptedName = "aaaaa-bbb-z-y-x-123[abxyz]";
			var room = new Day04.Room(encryptedName);

			Assert.AreEqual("aaaaa-bbb-z-y-x-", room.RoomName);
			Assert.AreEqual(123, room.SectorId);
			Assert.AreEqual("abxyz", room.Checksum);
			Assert.True(room.IsValidRoom());
		}

		[Test]
		public void ExampleA2()
		{
			var encryptedName = "a-b-c-d-e-f-g-h-987[abcde]";
			var room = new Day04.Room(encryptedName);

			Assert.AreEqual("a-b-c-d-e-f-g-h-", room.RoomName);
			Assert.AreEqual(987, room.SectorId);
			Assert.AreEqual("abcde", room.Checksum);
			Assert.True(room.IsValidRoom());
		}

		[Test]
		public void ExampleA3()
		{
			var encryptedName = "not-a-real-room-404[oarel]";
			var room = new Day04.Room(encryptedName);

			Assert.AreEqual("not-a-real-room-", room.RoomName);
			Assert.AreEqual(404, room.SectorId);
			Assert.AreEqual("oarel", room.Checksum);
			Assert.True(room.IsValidRoom());
		}

		[Test]
		public void ExampleA4()
		{
			var encryptedName = "totally-real-room-200[decoy]";
			var room = new Day04.Room(encryptedName);

			Assert.AreEqual("totally-real-room-", room.RoomName);
			Assert.AreEqual(200, room.SectorId);
			Assert.AreEqual("decoy", room.Checksum);
			Assert.False(room.IsValidRoom());
		}

		[Test]
		public void ExampleA5()
		{
			var encryptedNames = new List<string>
			{
				"aaaaa-bbb-z-y-x-123[abxyz]",
				"a-b-c-d-e-f-g-h-987[abcde]",
				"not-a-real-room-404[oarel]",
				"totally-real-room-200[decoy]"
			};

			Assert.AreEqual(1514, Day04.FindSectorId(encryptedNames).Item1);
		}

		[Test]
		public void AnswerA()
		{
			Assert.AreEqual(409147, Day04.FindSectorId(File.ReadAllLines("Day4_input.txt")).Item1);
		}

		[Test]
		public void ExampleB1()
		{
			var encryptedName = "qzmt-zixmtkozy-ivhz-343[abcde]";
			var room = new Day04.Room(encryptedName);
			room.RotateRoomName(room.SectorId);
			Assert.AreEqual("very encrypted name", room.RoomName);
			Assert.AreEqual(343, room.SectorId);
		}

		[Test]
		public void AnswerB()
		{
			Assert.AreEqual(991, Day04.FindSectorId(File.ReadAllLines("Day4_input.txt"), true).Item2);
		}
	}
}
