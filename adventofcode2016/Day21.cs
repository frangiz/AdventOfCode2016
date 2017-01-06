using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2016
{
	public class Day21 : IDay
	{
		public class Scrambler
		{
			// -------------------------------------------------------------------------
			private interface ICommand { void Execute(ref char[] password); }
			private class NopCommand : ICommand { public void Execute(ref char[] password) { } }

			private Regex _swapPositionRegex = new Regex("swap position (\\d+) with position (\\d+)");
			private class SwapPositionCmd : ICommand
			{
				public int X { get; set; }
				public int Y { get; set; }
				public void Execute(ref char[] password)
				{
					var tmp = password[X]; password[X] = password[Y]; password[Y] = tmp;
				}
			}

			private Regex _swapCharRegex = new Regex("swap letter (\\w) with letter (\\w)");
			private class SwapCharCmd : ICommand
			{
				public char X { get; set; }
				public char Y { get; set; }
				public void Execute(ref char[] password)
				{
					var pos = Array.IndexOf(password, X);
					password[Array.IndexOf(password, Y)] = X;
					password[pos] = Y;
				}
			}

			private Regex _rotateStepsRegex = new Regex("rotate (left|right) (\\d+) step");
			private class RotateStepsCmd : ICommand
			{
				public enum Directions { Left, Right };
				public Directions Direction { get; set; }
				public int Steps { get; set; }
				public void Execute(ref char[] password)
				{
					var newIndexFor0 = (Steps * (Direction == Directions.Left ? 1 : -1)) % password.Length;
					if (newIndexFor0 < 0) { newIndexFor0 += password.Length; }
					var tmp = new char[password.Length];
					Array.Copy(password, newIndexFor0, tmp, 0, password.Length - newIndexFor0);
					Array.Copy(password, 0, tmp, password.Length - newIndexFor0, newIndexFor0);

					password = tmp;
				}
			}

			private Regex _rotatePositionRegex = new Regex("rotate based on position of letter (\\w)");
			private class RotatePositionCmd : ICommand
			{
				public char Letter { get; set; }
				public bool Reversed { get; set; }
				public void Execute(ref char[] password)
				{
					var index = Array.IndexOf(password, Letter);
					var cmd = new RotateStepsCmd
					{
						Direction = RotateStepsCmd.Directions.Right,
						Steps = index + 1 + (index >= 4 ? 1 : 0)
					};
					if (Reversed)
					{
						cmd.Direction = RotateStepsCmd.Directions.Left;
						for (int i = 0; i < password.Length; i++)
						{
							var steps = i + 1 + (i >= 4 ? 1 : 0);
							if ((i + steps) % password.Length == index)
							{
								cmd.Steps = steps;
								break;
							}
						}
					}

					cmd.Execute(ref password);
				}
			}

			private Regex _reversePositionsRegex = new Regex("reverse positions (\\d+) through (\\d+)");
			private class ReversePositionsCmd : ICommand
			{
				public int Start { get; set; }
				public int End { get; set; }
				public void Execute(ref char[] password)
				{
					Array.Reverse(password, Start, End - Start + 1);
				}
			}

			private Regex _moveCharRegex = new Regex("move position (\\d+) to position (\\d+)");
			private class MoveCharCmd : ICommand
			{
				public int From { get; set; }
				public int To { get; set; }
				public void Execute(ref char[] password)
				{
					var startVal = password[From];
					if (From < To)
					{
						Array.Copy(password, From + 1, password, From, To - From);
					}
					else
					{
						Array.Copy(password, To, password, To + 1, From - To);
					}
					password[To] = startVal;
				}
			}

			// -------------------------------------------------------------------------
			private char[] _password;

			public string Scramble(string password, IEnumerable<string> instructions)
			{
				_password = password.ToArray();

				foreach (var instruction in instructions)
				{
					var cmd = ParseCommand(instruction);
					cmd.Execute(ref _password);
				}

				return new string(_password);
			}

			public string Unscramble(string password, IEnumerable<string> instructions)
			{
				_password = password.ToArray();

				foreach (var instruction in instructions.Reverse())
				{
					var cmd = ParseCommand(instruction, true);
					cmd.Execute(ref _password);
				}

				return new string(_password);
			}

			private ICommand ParseCommand(string instructions, bool reversed = false)
			{
				var match = _swapPositionRegex.Match(instructions);
				if (match.Success)
				{
					return new SwapPositionCmd
					{
						X = int.Parse(match.Groups[1].Value),
						Y = int.Parse(match.Groups[2].Value)
					};
				}

				match = _swapCharRegex.Match(instructions);
				if (match.Success)
				{
					return new SwapCharCmd
					{
						X = match.Groups[1].Value[0],
						Y = match.Groups[2].Value[0]
					};
				}

				match = _rotateStepsRegex.Match(instructions);
				if (match.Success)
				{
					var dir = match.Groups[1].Value.ToLower() == "left" ?
							RotateStepsCmd.Directions.Left : RotateStepsCmd.Directions.Right;
					if (reversed)
					{
						dir = dir == RotateStepsCmd.Directions.Left ?
							RotateStepsCmd.Directions.Right : RotateStepsCmd.Directions.Left;
					}
					return new RotateStepsCmd
					{
						Direction = dir,
						Steps = int.Parse(match.Groups[2].Value)
					};
				}

				match = _rotatePositionRegex.Match(instructions);
				if (match.Success)
				{
					return new RotatePositionCmd
					{
						Letter = match.Groups[1].Value[0],
						Reversed = reversed
					};
				}

				match = _reversePositionsRegex.Match(instructions);
				if (match.Success)
				{
					return new ReversePositionsCmd
					{
						Start = int.Parse(match.Groups[1].Value),
						End = int.Parse(match.Groups[2].Value)
					};
				}

				match = _moveCharRegex.Match(instructions);
				if (match.Success)
				{
					return new MoveCharCmd
					{
						From = int.Parse(match.Groups[reversed ? 2 : 1].Value),
						To = int.Parse(match.Groups[reversed ? 1 : 2].Value)
					};
				}

				return new NopCommand();
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 21: Scrambled Letters and Hash ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var scrambler = new Scrambler();

			return scrambler.Scramble("abcdefgh", File.ReadAllLines("Day21_input.txt"));
		}

		public string GetAnswerB(bool animate = false)
		{
			var scrambler = new Scrambler();

			return scrambler.Unscramble("fbgdceah", File.ReadAllLines("Day21_input.txt"));
		}
	}
}
