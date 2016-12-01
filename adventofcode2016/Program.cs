using System;
using System.IO;

namespace adventofcode2016
{
	class Program
	{
		static void Main(string[] args)
		{
			var cmds = string.Join("", File.ReadAllLines("Day1_input.txt"));
			Console.WriteLine("Part A: " +new Day1().FindDistance(cmds));
			Console.WriteLine("Part B: " + new Day1(true).FindDistance(cmds));
			Console.ReadLine();
		}
	}
}
