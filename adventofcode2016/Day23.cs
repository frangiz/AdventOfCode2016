using System;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day23 : IDay
	{
		public class Computer : Day12.Computer
		{
			public Computer()
			{
				_instructionHandlers.Add("tgl", HandleToggle);
			}

			protected override int HandleIncrement(int index, string arg1, string arg2)
			{
				if (!string.IsNullOrEmpty(arg2)) { return ++index; }
				return base.HandleIncrement(index, arg1, arg2);
			}

			protected override int HandleDecrement(int index, string arg1, string arg2)
			{
				if (!string.IsNullOrEmpty(arg2)) { return ++index; }
				return base.HandleDecrement(index, arg1, arg2);
			}

			protected int HandleToggle(int index, string arg1, string arg2)
			{
				var steps = CharToValue(arg1);
				if (steps + index < 0 || steps + index >= _instructions.Count)
				{
					return ++index;
				}

				if (steps == 0)
				{
					_instructions[steps + index] = _instructions[steps + index].Replace("tgl", "inc");
				}
				else
				{
					var instruction = _instructions[steps + index];
					if (instruction.StartsWith("inc"))
					{
						instruction = instruction.Replace("inc", "dec");
					}
					else if (instruction.StartsWith("dec"))
					{
						instruction = instruction.Replace("dec", "inc");
					}
					else if (instruction.StartsWith("tgl"))
					{
						instruction = instruction.Replace("tgl", "inc");
					}
					else if (instruction.StartsWith("jnz"))
					{
						instruction = instruction.Replace("jnz", "cpy");
					}
					else if (instruction.StartsWith("cpy"))
					{
						instruction = instruction.Replace("cpy", "jnz");
					}
					else
					{
						throw new Exception("Invalid instruction found.");
					}
					_instructions[steps + index] = instruction;
				}

				return ++index;
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 23: Safe Cracking ---"; } }

		public void PrintDay()
		{
			{
				var computer = new Computer();
				computer.Registers[0] = 7;
				computer.ExecuteInstructions(File.ReadAllLines("Day23_input.txt").ToList());
				Console.WriteLine("Answer A: " + computer.Registers[0]);
			}
			{
				var computer = new Computer();
				computer.Registers[0] = 12;
				computer.ExecuteInstructions(File.ReadAllLines("Day23_input.txt").ToList());
				Console.WriteLine("Answer B: " + computer.Registers[0]);
			}
			Console.WriteLine();
		}
	}
}
