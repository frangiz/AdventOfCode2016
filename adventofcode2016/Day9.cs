using System;
using System.IO;
using System.Text.RegularExpressions;

namespace adventofcode2016
{
	public class Day9 : IDay
	{
		// ---------------------------------------------------------------------------
		public Day9() { }

		public static ulong Expand(string str, bool partB = false)
		{
			str = str.Trim();
			var regex = new Regex("\\((\\d+)x(\\d+)\\)");
			var match = regex.Match(str);
			if (!match.Success)
			{
				return (ulong)str.Length;
			}

			var pos = match.Index;
			var chars = int.Parse(match.Groups[1].Value);
			var times = ulong.Parse(match.Groups[2].Value);
			var index = pos + match.Length;

			if (partB)
			{
				return (ulong)(str.Substring(0, pos).Length)
				+ (Expand(str.Substring(index, chars), partB) * times)
				+ Expand(str.Substring(index + chars), partB);
			}
			
			return (ulong)str.Substring(0, pos).Length
				+ ((ulong)(str.Substring(index, chars).Length) * times)
				+ Expand(str.Substring(index + chars));
		}

		public void PrintDay()
		{
			Console.WriteLine("--- Day 9: Explosives in Cyberspace ---");
			Console.WriteLine("Answer A: " + Expand(File.ReadAllText("Day9_input.txt")));
			Console.WriteLine("Answer B: " + Expand(File.ReadAllText("Day9_input.txt"), true));
			Console.WriteLine();
		}
	}
}
