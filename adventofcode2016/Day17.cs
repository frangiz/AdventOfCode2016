using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace adventofcode2016
{
	public class Day17 : IDay
	{
		private class Room
		{
			public int X { get; set; }
			public int Y { get; set; }
			public string Path { get; set; }

			public override bool Equals(object obj)
			{
				var other = obj as Room;
				return other != null && Path == other.Path;
			}

			public override int GetHashCode()
			{
				return Path.GetHashCode();
			}
		}

		public class PuzzleSolver
		{
			private readonly List<Room> _unvisitedRooms;

			public PuzzleSolver()
			{
				_unvisitedRooms = new List<Room>();
			}

			public string FindShortestPath(string passcode)
			{
				var paths = FindPaths(passcode, PathType.Shortest);
				return paths.Count > 0 ? paths.First() : "";
			}

			public string FindLongestPath(string passcode)
			{
				var paths = FindPaths(passcode, PathType.All);
				return paths.Count > 0 ? paths.OrderByDescending(p => p.Length).First() : "";
			}

			private enum PathType { Shortest, All };
			private List<string> FindPaths(string passcode, PathType pathType)
			{
				var paths = new List<string>();

				_unvisitedRooms.Add(new Room() { X = 0, Y = 0, Path = "" });
				while (_unvisitedRooms.Count > 0)
				{
					var room = _unvisitedRooms.First();
					_unvisitedRooms.RemoveAt(0);

					if (room.X == 3 && room.Y == 3)
					{
						paths.Add(room.Path);
						if (pathType == PathType.Shortest) { break; }
					}
					else
					{
						foreach (var r in BreedNewRooms(passcode, room))
						{
							if (!_unvisitedRooms.Contains(r))
							{
								_unvisitedRooms.Add(r);
							}
						}
					}
				}

				return paths;
			}

			private static char[] OpenDoorChars = new [] { 'b', 'c', 'd', 'e', 'f' };

			private IEnumerable<Room> BreedNewRooms(string passcode, Room room)
			{
				var rooms = new List<Room>();
				var hash = passcode + room.Path;
				using (var md5 = MD5.Create())
				{
					var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(hash));
					hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
				}
				if (room.Y > 0 && room.Y <= 3 && OpenDoorChars.Contains(hash[0]))
				{
					rooms.Add(new Room { X = room.X, Y = room.Y - 1, Path = room.Path + "U" });
				}
				if (room.Y >= 0 && room.Y < 3 && OpenDoorChars.Contains(hash[1]))
				{
					rooms.Add(new Room { X = room.X, Y = room.Y + 1, Path = room.Path + "D" });
				}
				if (room.X > 0 && room.X <= 3 && OpenDoorChars.Contains(hash[2]))
				{
					rooms.Add(new Room { X = room.X - 1, Y = room.Y, Path = room.Path + "L" });
				}
				if (room.X >= 0 && room.X < 3 && OpenDoorChars.Contains(hash[3]))
				{
					rooms.Add(new Room { X = room.X + 1, Y = room.Y, Path = room.Path + "R" });
				}

				return rooms;
			}
		}

		// ---------------------------------------------------------------------------
		public void PrintDay()
		{
			Console.WriteLine("--- Day 17: Two Steps Forward ---");
			{
				var solver = new PuzzleSolver();
				Console.WriteLine("Answer A: " + solver.FindShortestPath("qljzarfv"));
			}
			{
				var solver = new PuzzleSolver();
				Console.WriteLine("Answer B: " + solver.FindLongestPath("qljzarfv").Length);
			}
			Console.WriteLine();
		}
	}
}
