using System;
using System.Collections.Generic;

namespace adventofcode2016.Tools
{
	public class AssemBunny
	{
		public readonly int[] Registers;
		protected readonly Dictionary<string, Func<int, string, string, int>> _instructionHandlers;
		protected List<string> _instructions;

		public AssemBunny()
		{
			Registers = new int[4];
			_instructionHandlers = new Dictionary<string, Func<int, string, string, int>>();
			_instructionHandlers.Add("cpy", HandleCopy);
			_instructionHandlers.Add("inc", HandleIncrement);
			_instructionHandlers.Add("dec", HandleDecrement);
			_instructionHandlers.Add("jnz", HandleJump);
			_instructionHandlers.Add("tgl", HandleToggle);
		}

		public void ExecuteInstructions(List<string> instructions)
		{
			var index = 0;
			_instructions = instructions;
			while (index >= 0 && index < _instructions.Count)
			{
				var instruction = _instructions[index];
				var parts = instruction.Split(' ');
				index = _instructionHandlers[instruction.Substring(0, 3)].Invoke(
					index,
					parts[1],
					parts.Length >= 3 ? parts[2] : "");
			}
		}

		private int HandleCopy(int index, string arg1, string arg2)
		{
			var value = 0;
			if (int.TryParse(arg1, out value))
			{
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
			if (!string.IsNullOrEmpty(arg2)) { return ++index; }

			var registerIndex = CharToRegisterId(arg1[0]);
			Registers[registerIndex]++;
			return ++index;
		}

		private int HandleDecrement(int index, string arg1, string arg2)
		{
			if (!string.IsNullOrEmpty(arg2)) { return ++index; }

			var registerIndex = CharToRegisterId(arg1[0]);
			Registers[registerIndex]--;
			return ++index;
		}

		private int HandleJump(int index, string arg1, string arg2)
		{
			if (char.IsDigit(arg1[0]) && arg1[0] != '0')
			{
				return index + CharToValue(arg2);
			}
			else if (char.IsLetter(arg1[0]))
			{
				var value = Registers[CharToRegisterId(arg1[0])];
				value = value == 0 ? 1 : int.Parse(arg2);
				return index + value;
			}
			return ++index;
		}

		private int HandleToggle(int index, string arg1, string arg2)
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

		private int CharToRegisterId(char c)
		{
			return c - 'a';
		}

		private int CharToValue(string arg)
		{
			return char.IsDigit(arg[0]) ? int.Parse(arg) : Registers[CharToRegisterId(arg[0])];
		}
	}
}
