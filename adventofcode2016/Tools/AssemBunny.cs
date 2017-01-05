using System;
using System.Collections.Generic;

namespace adventofcode2016.Tools
{
	public class AssemBunny
	{
		public readonly Dictionary<char, int> Registers;

		private enum InstructionType { Cpy, Inc, Dec, Jnz, Tgl };
		private readonly Dictionary<InstructionType, Func<int, object, object, int>> _instructionHandlers;

		private class Instruction
		{
			public InstructionType Type { get; set; }
			public object Operand1 { get; set; }
			public object Operand2 { get; set; }
		}
		private List<Instruction> _instructions;

		public AssemBunny()
		{
			Registers = new Dictionary<char, int> { { 'a', 0 }, { 'b', 0 }, { 'c', 0 }, { 'd', 0 } };

			_instructionHandlers = new Dictionary<InstructionType, Func<int, object, object, int>>();
			_instructionHandlers.Add(InstructionType.Cpy, HandleCopy);
			_instructionHandlers.Add(InstructionType.Inc, HandleIncrement);
			_instructionHandlers.Add(InstructionType.Dec, HandleDecrement);
			_instructionHandlers.Add(InstructionType.Jnz, HandleJump);
			_instructionHandlers.Add(InstructionType.Tgl, HandleToggle);
		}

		public void ExecuteInstructions(List<string> instructions)
		{
			var popularity = new List<int>();
			instructions.ForEach(i => popularity.Add(0));
			var index = 0;
			_instructions = new List<Instruction>();
			foreach (var instruction in instructions)
			{
				var parts = instruction.Split(' ');
				var type = char.ToUpper(parts[0][0]) + parts[0].Substring(1);
				_instructions.Add(new Instruction
				{
					Type = (InstructionType)Enum.Parse(typeof(InstructionType), type),
					Operand1 = ParseOperand(parts[1]),
					Operand2 = ParseOperand(parts.Length >= 3 ? parts[2] : "")
				});
			}

			while (index >= 0 && index < _instructions.Count)
			{
				popularity[index]++;
				var instruction = _instructions[index];
				index = _instructionHandlers[instruction.Type].Invoke(
					index, instruction.Operand1, instruction.Operand2);
			}
		}

		private object ParseOperand(string str)
		{
			var value = 0;
			if (int.TryParse(str, out value))
			{
				return value;
			}
			else if (str.Length == 1)
			{
				return str[0];
			}
			return "";
		}

		private int HandleCopy(int index, object arg1, object arg2)
		{
			if (IsMultiplyCAndDAndAddToAThenClearCAndDShorthand(index))
			{
				return MultiplyCAndDAndAddToAThenClearCAndDShorthand(index);
			}

			if (arg1 is int && arg2 is char)
			{
				Registers[(char)arg2] = (int)arg1;
			}
			else
			{
				Registers[(char)arg2] = Registers[(char)arg1];
			}

			return ++index;
		}

		private int HandleIncrement(int index, object arg1, object arg2)
		{
			if (arg2 is string && !string.IsNullOrEmpty((string)arg2)) { return ++index; }

			if (IsAddBToAThenClearBShorthand(index))
			{
				return AddBToAThenClearBShorthand(index);
			}

			Registers[(char)arg1]++;
			return ++index;
		}

		private int HandleDecrement(int index, object arg1, object arg2)
		{
			if (arg2 is string && !string.IsNullOrEmpty((string)arg2)) { return ++index; }

			Registers[(char)arg1]--;
			return ++index;
		}

		private int HandleJump(int index, object arg1, object arg2)
		{
			if (arg1 is int && (int)arg1 != 0)
			{
				return index + CharToValue(arg2);
			}
			else if (arg1 is char && char.IsLetter((char)arg1))
			{
				var value = Registers[(char)arg1];
				value = value == 0 ? 1 : (int)arg2;
				return index + value;
			}
			return ++index;
		}

		private int HandleToggle(int index, object arg1, object arg2)
		{
			var steps = CharToValue(arg1);
			if (steps + index < 0 || steps + index >= _instructions.Count)
			{
				return ++index;
			}

			if (steps == 0)
			{
				if (_instructions[steps + index].Type == InstructionType.Tgl)
				{
					_instructions[steps + index].Type = InstructionType.Inc;
				}
			}
			else
			{
				switch (_instructions[steps + index].Type)
				{
					case InstructionType.Inc:
						_instructions[steps + index].Type = InstructionType.Dec;
						break;
					case InstructionType.Dec:
					case InstructionType.Tgl:
						_instructions[steps + index].Type = InstructionType.Inc;
						break;
					case InstructionType.Jnz:
						_instructions[steps + index].Type = InstructionType.Cpy;
						break;
					case InstructionType.Cpy:
						_instructions[steps + index].Type = InstructionType.Jnz;
						break;
					default:
						throw new Exception("Invalid instruction found.");
				}
			}

			return ++index;
		}

		private int CharToValue(object arg)
		{
			return arg is int ? (int)arg : Registers[(char)arg];
		}

		private bool IsEqual(object operand1, object operand2)
		{
			if (operand1 is char && operand2 is char)
			{
				return (char)operand1 == (char)operand2;
			}
			else if (operand1 is int && operand2 is int)
			{
				return (int)operand1 == (int)operand2;
			}

			return false;
		}

		private bool IsAddBToAThenClearBShorthand(int index)
		{
			/*
			 * inc a
			 * dec b
			 * jnz b -2
			 */
			return _instructions[index].Type == InstructionType.Inc &&
				_instructions[index + 1].Type == InstructionType.Dec &&
				_instructions[index + 2].Type == InstructionType.Jnz &&
				IsEqual(_instructions[index + 1].Operand1, _instructions[index + 2].Operand1) &&
				IsEqual(_instructions[index + 2].Operand2, -2);
		}

		private int AddBToAThenClearBShorthand(int index)
		{
			var registerToInc = (char)_instructions[index].Operand1;
			var registerToDec = (char)_instructions[index + 1].Operand1;

			Registers[registerToInc] += Registers[registerToDec];
			Registers[registerToDec] = 0;
			return (index + 3);
		}

		private bool IsMultiplyCAndDAndAddToAThenClearCAndDShorthand(int index)
		{
			/*
			 * cpy b c
			 * inc a
			 * dec c
			 * jnz c -2
			 * dec d
			 * jnz d -5
			 */
			return _instructions[index].Type == InstructionType.Cpy &&
				IsAddBToAThenClearBShorthand(index + 1) &&
				_instructions[index + 4].Type == InstructionType.Dec &&
				_instructions[index + 5].Type == InstructionType.Jnz &&
				IsEqual(_instructions[index].Operand2, _instructions[index + 2].Operand1) &&
				IsEqual(_instructions[index + 4].Operand1, _instructions[index + 5].Operand1) &&
				IsEqual(_instructions[index + 5].Operand2, -5);
		}

		private int MultiplyCAndDAndAddToAThenClearCAndDShorthand(int index)
		{
			var registerA = (char)_instructions[index + 1].Operand1;
			var registerC = (char)_instructions[index].Operand2;
			var registerD = (char)_instructions[index + 4].Operand1;

			Registers[registerA] += CharToValue(_instructions[index].Operand1) * Registers[registerD];
			Registers[registerC] = 0;
			Registers[registerD] = 0;

			return (index + 6);
		}
	}
}
