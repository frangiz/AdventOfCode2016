using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace adventofcode2016
{
	public class Day6 : IDay
	{
		public static string Decode(int length, IEnumerable<string> lines, bool selectLeastCommonChar = false)
		{
			var data = new List<Dictionary<char, int>>();
			for (var i = 0; i < length; i++)
			{
				data.Add(new Dictionary<char, int>());
			}

			foreach (var line in lines)
			{
				AddLine(line, ref data);
			}

			var sb = new StringBuilder();
			foreach (var tokenInfo in data)
			{
				if (selectLeastCommonChar)
				{
					sb.Append(tokenInfo.OrderBy(a => a.Value).ThenByDescending(a => a.Key).First().Key);
				}
				else
				{
					sb.Append(tokenInfo.OrderByDescending(a => a.Value).ThenBy(a => a.Key).First().Key);
				}
			}

			return sb.ToString();
		}

		private static void AddLine(string line, ref List<Dictionary<char, int>> data)
		{
			for (var charIndex = 0; charIndex < line.Length; charIndex++)
			{
				var currentChar = line[charIndex];
				if (!data[charIndex].ContainsKey(currentChar))
				{
					data[charIndex][currentChar] = 0;
				}
				data[charIndex][currentChar]++;
			}
		}

		// --------------------------------------------------------------------
		public string Name { get { return "--- Day 6: Signals and Noise ---"; } }

		public void PrintDay()
		{
			var lines = File.ReadAllLines("Day6_input.txt");
			Console.WriteLine("Answer A: " + Day6.Decode(lines[0].Length, lines));
			Console.WriteLine("Answer B: " + Day6.Decode(lines[0].Length, lines, true));
			Console.WriteLine();
		}
	}
}
