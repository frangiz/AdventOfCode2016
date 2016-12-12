using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day12 : IDay
	{
		public class Computer
		{
			public readonly int[] Registers;
			private readonly Dictionary<string, Func<int, string, string, int>> _instructionHandlers;

			public Computer()
			{
				Registers = new int[4];
				_instructionHandlers = new Dictionary<string, Func<int, string, string, int>>();
				_instructionHandlers.Add("cpy", HandleCopy);
				_instructionHandlers.Add("inc", HandleIncrement);
				_instructionHandlers.Add("dec", HandleDecrement);
				_instructionHandlers.Add("jnz", HandleJump);
			}

			public void ExecuteInstructions(List<string> instructions)
			{
				var index = 0;
				while (index >= 0 && index < instructions.Count)
				{
					var instruction = instructions[index];
					var parts = instruction.Split(' ');
					index = _instructionHandlers[instruction.Substring(0, 3)].Invoke(
						index,
						parts[1],
						parts.Length >= 3 ? parts[2] : "");
				}
			}

			private int HandleCopy(int index, string arg1, string arg2)
			{
				if (char.IsDigit(arg1[0]))
				{
					var value = int.Parse(arg1);
					var registerIndex = CharToRegisterId(arg2[0]);
					Registers[registerIndex] = value;
				}
				else
				{
					var sourceIndex = CharToRegisterId(arg1[0]);
					var targetIndex = CharToRegisterId(arg2[0]);
					Registers[targetIndex] = Registers[sourceIndex];
				}

				return ++index;
			}

			private int HandleIncrement(int index, string arg1, string arg2)
			{
				var registerIndex = CharToRegisterId(arg1[0]);
				Registers[registerIndex]++;
				return ++index;
			}

			private int HandleDecrement(int index, string arg1, string arg2)
			{
				var registerIndex = CharToRegisterId(arg1[0]);
				Registers[registerIndex]--;
				return ++index;
			}

			private int HandleJump(int index, string arg1, string arg2)
			{
				if (char.IsDigit(arg1[0]) && arg1[0] != '0')
				{
					return index + int.Parse(arg2);
				}
				else if (char.IsLetter(arg1[0]))
				{
					var value = Registers[CharToRegisterId(arg1[0])];
					value = value == 0 ? 1 : int.Parse(arg2);
					return index + value;
				}
				return ++index;
			}

			private int CharToRegisterId(char c)
			{
				return c - 'a';
			}
		}

		// ---------------------------------------------------------------------------
		public void PrintDay()
		{
			Console.WriteLine("--- Day 12: Leonardo's Monorail ---");
			{
				var computer = new Computer();
				computer.ExecuteInstructions(File.ReadAllLines("Day12_input.txt").ToList());
				Console.WriteLine("Answer A: " + computer.Registers[0]);
			}
			{
				var computer = new Computer();
				computer.Registers[2] = 1;
				computer.ExecuteInstructions(File.ReadAllLines("Day12_input.txt").ToList());
				Console.WriteLine("Answer B: " + computer.Registers[0]);
			}
			Console.WriteLine();
		}
	}
}
