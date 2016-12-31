using adventofcode2016.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day24 : IDay
	{
		public class Solver
		{
			public string[] _map;

			private static List<char> InvalidMoves = new List<char> { '\0', '#' };

			public Solver(string[] map)
			{
				_map = map;
			}

			public int FindShortestDistance(bool includePathBack)
			{
				var pathFinder = new PathFinder();
				var points = FindAllPoints();
				var permutatios = Permutator.Permute(points.Keys.ToArray());
				permutatios = permutatios.Where(p => p[0] == '0');

				// Remembering all the distances speed things up a bit...
				var knownDistances = new Dictionary<string, int>();

				var shortest = int.MaxValue;
				foreach (var permutation in permutatios)
				{
					var sum = 0;
					char[] currentPermutation = permutation;
					if (includePathBack)
					{
						currentPermutation = permutation.Concat(new[] { '0' }).ToArray();
					}

					for (int i = 0; i < currentPermutation.Length - 1; i++)
					{
						var key = "" +currentPermutation[i] + currentPermutation[i + 1];
						if (!knownDistances.ContainsKey(key))
						{
							knownDistances.Add(key, pathFinder.FindShortestPath(
								points[currentPermutation[i]],
								points[currentPermutation[i + 1]],
								NodeTypeAtPos));
						}
						sum += knownDistances[key];
					}

					shortest = Math.Min(shortest, sum);
				}

				return shortest;
			}

			private Dictionary<char, Point> FindAllPoints()
			{
				var points = new Dictionary<char, Point>();
				for (int row = 0; row < _map.Length; row++)
				{
					for (int col = 0; col < _map[row].Length; col++)
					{
						var charAtPos = GetCharAt(col, row);
						if (char.IsDigit(charAtPos))
						{
							points.Add(charAtPos, new Point(col, row));
						}
					}
				}
				return points;
			}

			private Node.NodeTypes NodeTypeAtPos(int x, int y)
			{
				return InvalidMoves.Contains(GetCharAt(x, y)) ?
					Node.NodeTypes.Wall : Node.NodeTypes.OpenSpace;
			}

			private char GetCharAt(int x, int y)
			{
				if (y < 0 || y > _map.Length - 1)
				{
					return '\0';
				}
				if (x < 0 || x > _map[y].Length + 1)
				{
					return '\0';
				}

				return _map[y][x];
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 24: Air Duct Spelunking ---"; } }

		public void PrintDay()
		{
			{
				var solver = new Solver(File.ReadAllLines("Day24_input.txt"));
				Console.WriteLine("Answer A: " + solver.FindShortestDistance(false));
			}
			{
				var solver = new Solver(File.ReadAllLines("Day24_input.txt"));
				Console.WriteLine("Answer B: " + solver.FindShortestDistance(true));
			}
			Console.WriteLine();
		}
	}
}
