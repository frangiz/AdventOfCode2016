using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2016
{
	public class Day8 : IDay
	{
		public class Monitor
		{
			private readonly char[] _pixels;
			private readonly int _width;
			private readonly int _height;

			public Monitor(int width, int height)
			{
				_pixels = new char[width * height];
				for (int i = 0; i < _pixels.Length; i++) { _pixels[i] = '.'; }
				_width = width;
				_height = height;
			}

			public void ActivateCommand(string cmd)
			{
				if (cmd.StartsWith("rect"))
				{
					HandleRectCommand(cmd);
				}
				else if (cmd.StartsWith("rotate column"))
				{
					HandleRotateColumn(cmd);
				}
				else if (cmd.StartsWith("rotate row"))
				{
					HandleRotateRow(cmd);
				}
			}

			private void HandleRectCommand(string cmd)
			{
				var parts = cmd.Substring(4).Split('x');
				int width = int.Parse(parts[0]);
				int height = int.Parse(parts[1]);

				for (int row = 0; row < height; row++)
				{
					for (int col = 0; col < width; col++)
					{
						int index = row * _width + col;
						_pixels[index] = '#';
					}
				}
			}

			private void HandleRotateColumn(string cmd)
			{
				var regex = new Regex("x=(\\d+) by (\\d+)");
				var match = regex.Match(cmd);

				var column = int.Parse(match.Groups[1].Value);
				var steps = int.Parse(match.Groups[2].Value);

				Rotate(column, _pixels.Length, _width, steps);
			}

			private void HandleRotateRow(string cmd)
			{
				var regex = new Regex("y=(\\d+) by (\\d+)");
				var match = regex.Match(cmd);

				var row = int.Parse(match.Groups[1].Value);
				var steps = int.Parse(match.Groups[2].Value);

				Rotate(row*_width, (row * _width) + _width, 1, steps);
			}

			private void Rotate(int fromIndex, int toIndex, int stepDistance, int steps)
			{
				for (int i = 0; i < steps; i++)
				{
					RotateOneStep(fromIndex, toIndex, stepDistance);
				}
			}

			private void RotateOneStep(int fromIndex, int toIndex, int stepDistance)
			{
				var indices = new List<int>();
				for (int i = fromIndex; i < toIndex; i+=stepDistance) { indices.Add(i); }

				var lastValue = _pixels[indices.Last()];
				for (var i = indices.Count - 1; i >= 0; i--)
				{
					_pixels[indices[i]] = _pixels[indices[Math.Abs((i-1)) % indices.Count]];
				}
				_pixels[fromIndex] = lastValue;
			}

			public IEnumerable<string> GetOutput()
			{
				var result = new List<string>();
				for (int i = 0; i < _pixels.Length; i+=_width)
				{
					result.Add(new string(_pixels.Skip(i).Take(_width).ToArray()));
				}

				return result;
			}

			public int NumberOfPixelsLit()
			{
				return _pixels.Count(p => p == '#');
			}
		}

		// ---------------------------------------------------------------------------
		private readonly Monitor _monitor;
		public Day8()
		{
			_monitor = new Monitor(50, 6);
			foreach (var line in File.ReadAllLines("Day8_input.txt"))
			{
				_monitor.ActivateCommand(line);
			}
		}

		public void PrintDay()
		{
			Console.WriteLine("--- Day 8: Two-Factor Authentication ---");
			Console.WriteLine("Answer A: " + _monitor.NumberOfPixelsLit());
			Console.WriteLine("Answer B: ");
			foreach (var line in _monitor.GetOutput())
			{
				Console.WriteLine(line);
			}
			Console.WriteLine();
		}
	}
}
