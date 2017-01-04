using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day4 : IDay
	{
		// ---------------------------------------------------------------------------
		public class Room
		{
			private string _roomName;
			public string RoomName { get { return _roomName; } }

			private int _sectorId;
			public int SectorId { get { return _sectorId; } }

			private string _checksum;
			public string Checksum { get { return _checksum; } }

			public Room(string encryptedName)
			{
				_roomName = FindRoomName(encryptedName);
				_sectorId = FindSectorId(encryptedName);
				_checksum = FindChecksum(encryptedName);
			}

			private static string FindRoomName(string encryptedName)
			{
				var name = "";
				foreach (var token in encryptedName)
				{
					if (char.IsDigit(token)) { break; }
					name += token;
				}
				return name;
			}

			private static int FindSectorId(string encryptedName)
			{
				var sectorId = "";
				foreach (var token in encryptedName)
				{
					if (char.IsDigit(token)) { sectorId += token; }
					if (token == '[') { break; }
				}
				return int.Parse(sectorId);
			}

			private static string FindChecksum(string encryptedName)
			{
				return encryptedName.Substring(encryptedName.IndexOf('[') + 1, 5);
			}

			private string SuggestedChecksum()
			{
				var occurences = FindCharOccurrences(_roomName);
				var suggestedChecksum = new string(occurences.OrderByDescending(kvp => kvp.Value)
					.ThenBy(kvp => kvp.Key)
					.Select(kvp => kvp.Key).ToArray());
				if (suggestedChecksum.Length > 5) { suggestedChecksum = suggestedChecksum.Substring(0, 5); }

				return suggestedChecksum;
			}

			public bool IsValidRoom()
			{
				return SuggestedChecksum().Equals(_checksum);
			}

			public void RotateRoomName(int steps)
			{
				var newName = "";
				var charsInAlphabet = 'z' - 'a' + 1;
				foreach (var token in _roomName)
				{
					if (char.IsLetter(token))
					{
						int newChar = token + (steps % charsInAlphabet);
						if (newChar > 'z') { newChar -= charsInAlphabet; }
						newName += (char)newChar;
					}
					else if (token == '-') { newName += " "; }
				}
				_roomName = newName.Trim();
			}

			private static Dictionary<char, int> FindCharOccurrences(string name)
			{
				var occurrences = new Dictionary<char, int>();
				foreach (var token in name)
				{
					if (char.IsLetter(token))
					{
						if (!occurrences.ContainsKey(token)) { occurrences[token] = 0; }
						occurrences[token]++;
					}
				}

				return occurrences;
			}
		}

		// ---------------------------------------------------------------------------
		public static Tuple<int, int> FindSectorId(IEnumerable<string> encryptedNames, bool rotateRoomNames = false)
		{
			var sectorIdSum = 0;
			var northPoleRoom = 0;
			foreach (var name in encryptedNames)
			{
				var room = new Room(name);
				if (rotateRoomNames)
				{
					room.RotateRoomName(room.SectorId);
					if (room.RoomName.StartsWith("north"))
					{
						northPoleRoom = room.SectorId;
					}
				}
				if (room.IsValidRoom())
				{
					sectorIdSum += room.SectorId;
				}
			}
			return new Tuple<int, int>(sectorIdSum, northPoleRoom);
		}

		// --------------------------------------------------------------------
		public string Name { get { return "--- Day 4: Security Through Obscurity ---"; } }

		public void PrintDay()
		{
			{
				Console.WriteLine("Answer A: " + FindSectorId(File.ReadAllLines("Day4_input.txt")).Item1);
			}
			{
				Console.WriteLine("Answer B: " + FindSectorId(File.ReadAllLines("Day4_input.txt"), true).Item2);
			}
			Console.WriteLine();
		}
	}
}
