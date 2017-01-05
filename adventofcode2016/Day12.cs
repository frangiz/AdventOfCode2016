﻿using adventofcode2016.Tools;
using System;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day12 : IDay
	{
		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 12: Leonardo's Monorail ---"; } }

		public void PrintDay()
		{
			{
				var computer = new AssemBunny();
				computer.ExecuteInstructions(File.ReadAllLines("Day12_input.txt").ToList());
				Console.WriteLine("Answer A: " + computer.Registers[0]);
			}
			{
				var computer = new AssemBunny();
				computer.Registers[2] = 1;
				computer.ExecuteInstructions(File.ReadAllLines("Day12_input.txt").ToList());
				Console.WriteLine("Answer B: " + computer.Registers[0]);
			}
			Console.WriteLine();
		}
	}
}
