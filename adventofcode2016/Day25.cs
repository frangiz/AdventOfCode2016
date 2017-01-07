using adventofcode2016.Tools;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day25 : IDay
	{
		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 25: Clock Signal ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var assemBunny = new AssemBunny();
			var lines = File.ReadAllLines("Day25_input.txt").ToList();

			for (var i = 0; i < int.MaxValue; i++)
			{
				assemBunny.Reset();
				assemBunny.Registers['a'] = i;
				assemBunny.ExecuteInstructions(lines);
				if (assemBunny.OutputCounter >= AssemBunny.MaxOutputLength)
				{
					return i.ToString();
				}
			}

			return "";
		}

		public string GetAnswerB(bool animate = false)
		{
			return "Push the button!";
		}
	}
}
