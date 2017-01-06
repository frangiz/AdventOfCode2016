using adventofcode2016.Tools;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day12 : IDay
	{
		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 12: Leonardo's Monorail ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var computer = new AssemBunny();
			computer.ExecuteInstructions(File.ReadAllLines("Day12_input.txt").ToList());

			return "" + computer.Registers['a'];
		}

		public string GetAnswerB(bool animate = false)
		{
			var computer = new AssemBunny();
			computer.Registers['c'] = 1;
			computer.ExecuteInstructions(File.ReadAllLines("Day12_input.txt").ToList());

			return "" + computer.Registers['a'];
		}
	}
}
