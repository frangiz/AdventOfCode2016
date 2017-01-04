using System;
using System.Collections.Generic;
using System.IO;

namespace adventofcode2016
{
	public class Day3 : IDay
	{
		// ---------------------------------------------------------------------------
		public class Parser
		{
			public List<int> _data;
			public Parser(IEnumerable<string> lines)
			{
				_data = new List<int>();
				ParseLines(lines);
			}

			public virtual bool HasNext() { return false; }
			public virtual int[] Next() { return new int[] { 0, 0, 0 }; }

			private void ParseLines(IEnumerable<string> lines)
			{
				foreach (var line in lines)
				{
					foreach (var side in line.Split(' '))
					{
						var s = side.Trim();
						if (string.IsNullOrWhiteSpace(s)) { continue; }
						_data.Add(int.Parse(s));
					}
				}
			}
		}

		// ---------------------------------------------------------------------------
		public class HorizontalParser : Parser
		{
			private int _index;
			public HorizontalParser(IEnumerable<string> lines)
				:base(lines) { }

			public override bool HasNext() { return _index + 3 < _data.Count; }

			public override int[] Next()
			{
				var result = new int[]
				{
					_data[_index],
					_data[_index+1],
					_data[_index+2],
					
				};
				_index += 3;
				return result;
			}
		}

		// ---------------------------------------------------------------------------
		public class VerticalParser : Parser
		{
			private int _row;
			private int _column;
			private readonly int WIDTH = 3;

			public VerticalParser(IEnumerable<string> lines)
				: base(lines) { }

			public override bool HasNext()
			{
				return FindIndex() + 6 < _data.Count;
			}

			public override int[] Next()
			{
				var index = FindIndex();
				var result = new int[]
				{
					_data[index],
					_data[index+3],
					_data[index+6],

				};
				_column++;
				if (_column == 3)
				{
					_column = 0;
					_row += 3;
				}

				return result;
			}

			private int FindIndex() { return _row * WIDTH + _column; }
		}

		// ---------------------------------------------------------------------------
		public static int ValidTriangles(Parser parser)
		{
			int count = 0;
			while (parser.HasNext())
			{
				if (IsValidTriangle(parser.Next())) { count++; }
			}
			return count;
		}

		public static bool IsValidTriangle(int[] sides)
		{
			if (sides[0] + sides[1] <= sides[2]) { return false; }
			if (sides[1] + sides[2] <= sides[0]) { return false; }
			if (sides[2] + sides[0] <= sides[1]) { return false; }

			return true;
		}

		// --------------------------------------------------------------------
		public string Name { get { return "--- Day 3: Squares With Three Sides ---"; } }

		public void PrintDay()
		{
			{
				Console.WriteLine("Answer A: " + Day3.ValidTriangles(
					new HorizontalParser(File.ReadAllLines("Day3_input.txt"))));
			}
			{
				Console.WriteLine("Answer A: " + Day3.ValidTriangles(
					new VerticalParser(File.ReadAllLines("Day3_input.txt"))));
			}
			Console.WriteLine();
		}
	}
}
