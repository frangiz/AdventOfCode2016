using adventofcode2016.Tools;
using System;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day23 : IDay
	{
		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 23: Safe Cracking ---"; } }

		public void PrintDay()
		{
			{
				var assemBunny = new AssemBunny();
				assemBunny.Registers[0] = 7;
				assemBunny.ExecuteInstructions(File.ReadAllLines("Day23_input.txt").ToList());
				Console.WriteLine("Answer A: " + assemBunny.Registers[0]);
			}
			{
				var assemBunny = new AssemBunny();
				assemBunny.Registers[0] = 12;
				assemBunny.ExecuteInstructions(File.ReadAllLines("Day23_input.txt").ToList());
				Console.WriteLine("Answer B: " + assemBunny.Registers[0]);
			}
			Console.WriteLine();
		}
	}
}
