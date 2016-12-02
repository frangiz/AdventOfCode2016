using System;
using System.IO;

namespace adventofcode2016
{
	class Program
	{
		static void Main(string[] args)
		{
			//FindDay1Answers();
			FindDay2Answers();
			Console.ReadLine();
		}

		private static void FindDay1Answers()
		{
			var cmds = string.Join("", File.ReadAllLines("Day1_input.txt"));
			Console.WriteLine("Part A: " + new Day1().FindDistance(cmds));
			Console.WriteLine("Part B: " + new Day1(true).FindDistance(cmds));
		}

		private static void FindDay2Answers()
		{
			var lines = File.ReadAllLines("Day2_input.txt");
			var day2Part1 = new Day2('5');
			foreach (var line in lines) { day2Part1.Move(line); }
			Console.WriteLine("Part A: " + day2Part1.KeysPressed);

			var day2Part2 = new Day2('5', false);
			foreach (var line in lines) { day2Part2.Move(line); }
			Console.WriteLine("Part B: " + day2Part2.KeysPressed);
		}
	}
}
