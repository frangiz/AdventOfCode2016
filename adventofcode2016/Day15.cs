using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2016
{
	public class Day15 : IDay
	{
		public class Sculptur
		{
			private class Disc
			{
				public int Id { get; private set; }
				public int Positions { get; private set; }
				public int StartPosition { get; private set; }

				public Disc(string discInfo)
				{
					var regex = new Regex("Disc #(\\d+) has (\\d+) positions; at time=0, it is at position (\\d+).");
					var matches = regex.Match(discInfo);
					Id = int.Parse(matches.Groups[1].Value);
					Positions = int.Parse(matches.Groups[2].Value);
					StartPosition = int.Parse(matches.Groups[3].Value);
				}
			}

			private readonly List<Disc> _discs;

			public Sculptur(IEnumerable<string> discInfos)
			{
				_discs = new List<Disc>();
				foreach (var discInfo in discInfos)
				{
					_discs.Add(new Disc(discInfo));
				}
			}

			public void AddDisc(string discInfo)
			{
				_discs.Add(new Disc(discInfo));
			}

			public int AlignDiscs()
			{
				return Enumerable.Range(1, int.MaxValue)
					.First(t => _discs.All(d => (d.StartPosition + t + d.Id) % d.Positions == 0));
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 15: Timing is Everything ---"; } }

		public void PrintDay()
		{
			{
				var sculptur = new Sculptur(File.ReadAllLines("Day15_input.txt"));
				Console.WriteLine("Answer A: " + sculptur.AlignDiscs());
			}
			{
				var sculptur = new Sculptur(File.ReadAllLines("Day15_input.txt"));
				sculptur.AddDisc("Disc #7 has 11 positions; at time=0, it is at position 0.");
				Console.WriteLine("Answer B: " + sculptur.AlignDiscs());
			}
			Console.WriteLine();
		}
	}
}
