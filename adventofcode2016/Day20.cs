using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day20 : IDay
	{
		public class Firewall
		{
			private readonly List<Tuple<ulong, ulong>> _blockedRanges;

			public Firewall(IEnumerable<string> blockedRanges)
			{
				_blockedRanges = new List<Tuple<ulong, ulong>>();
				foreach (var range in blockedRanges)
				{
					var parts = range.Split('-');
					_blockedRanges.Add(new Tuple<ulong, ulong>(ulong.Parse(parts[0]), ulong.Parse(parts[1])));
				}
			}

			public ulong LowestAvailableIP(ulong min, ulong max)
			{
				var current = min;
				while (current <= max)
				{
					var blocked = _blockedRanges.FirstOrDefault(r => r.Item1 <= current && current <= r.Item2);
					if (blocked == null)
					{
						return current;
					}
					current = blocked.Item2 + 1;
				}

				return current;
			}

			public ulong NumberOfAllowedIps(ulong min, ulong max)
			{
				ulong counter = 0;
				var current = min;

				while (current <= max)
				{
					var blocked = _blockedRanges.FirstOrDefault(r => r.Item1 <= current && current <= r.Item2);
					if (blocked == null)
					{
						counter++;
						current++;
					}
					else
					{
						current = blocked.Item2 + 1;
					}
				}

				return counter;
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 20: Firewall Rules ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var fw = new Firewall(File.ReadAllLines("DAy20_input.txt"));

			return "" + fw.LowestAvailableIP(0, ulong.MaxValue);
		}

		public string GetAnswerB(bool animate = false)
		{
			var fw = new Firewall(File.ReadAllLines("DAy20_input.txt"));

			return "" + fw.NumberOfAllowedIps(0, uint.MaxValue);
		}
	}
}
