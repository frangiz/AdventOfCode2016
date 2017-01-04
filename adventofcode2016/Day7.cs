using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace adventofcode2016
{
	public class Day7 : IDay
	{
		public class IPv7
		{
			private readonly string[] _supernetSequences;
			private readonly string[] _hypernetSequences;

			public IPv7(string address)
			{
				var subparts = address.Split('[', ']');

				string search = @"\[(\w+)\]";
				_hypernetSequences = Regex.Matches(address, search).Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
				_supernetSequences = subparts.Where(p => !_hypernetSequences.Contains(p)).ToArray();
			}

			public bool TLSSupport()
			{
				return _supernetSequences.Any(a => HasABBAPattern(a)) &&
					!_hypernetSequences.Any(a => HasABBAPattern(a));
			}

			public bool SSLSupport()
			{
				var babPatterns = new List<string>();
				foreach (var pattern in _supernetSequences)
				{
					babPatterns.AddRange(FindBABPatterns(pattern));
				}
				var sb = new StringBuilder();
				foreach (var pattern in babPatterns)
				{
					sb.Append(pattern[1]);
					sb.Append(pattern[0]);
					sb.Append(pattern[1]);
					if (_hypernetSequences.Any(s => s.Contains(sb.ToString())))
					{
						return true;
					}
					sb.Clear();
				}
				sb.Clear();

				return false;
			}

			private bool HasABBAPattern(string part)
			{
				for (int i = 0; i < part.Length - 3; i++)
				{
					if (part[i] == part[i+3] && part[i+1] == part[i + 2]
						&& part[i] != part[i+1])
					{
						return true;
					}
				}

				return false;
			}

			private IEnumerable<string> FindBABPatterns(string part)
			{
				var results = new List<string>();
				for (int i = 0; i < part.Length - 2; i++)
				{
					if (part[i] == part[i + 2] && part[i] != part[i + 1])
					{
						results.Add(part.Substring(i, 3));
					}
				}

				return results;
			}
		}

		public static int CountIPv7TLSSupport(IEnumerable<string> addresses)
		{
			return addresses.Count(a => new IPv7(a).TLSSupport());
		}

		public static int CountIPv7SSLSupport(IEnumerable<string> addresses)
		{
			return addresses.Count(a => new IPv7(a).SSLSupport());
		}

		// --------------------------------------------------------------------
		public string Name { get { return "--- Day 7: Internet Protocol Version 7 ---"; } }

		public void PrintDay()
		{
			Console.WriteLine("Answer A: " + CountIPv7TLSSupport(File.ReadAllLines("Day7_input.txt")));
			Console.WriteLine("Answer B: " + CountIPv7SSLSupport(File.ReadAllLines("Day7_input.txt")));
			Console.WriteLine();
		}
	}
}
