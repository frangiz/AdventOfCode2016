using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adventofcode2016
{
	public class Day6
	{
		private readonly List<Dictionary<char, int>> _data = new List<Dictionary<char, int>>();

		public Day6(int length)
		{
			for (var i = 0; i < length; i++)
			{
				_data.Add(new Dictionary<char, int>());
			}
		}

		public string Decode(IEnumerable<string> lines, bool selectLeastCommonChar = false)
		{
			foreach (var line in lines)
			{
				AddLine(line);
			}

			var sb = new StringBuilder();
			foreach (var tokenInfo in _data)
			{
				if (selectLeastCommonChar)
				{
					sb.Append(tokenInfo.OrderBy(a => a.Value).ThenByDescending(a => a.Key).First().Key);
				}
				else
				{
					sb.Append(tokenInfo.OrderByDescending(a => a.Value).ThenBy(a => a.Key).First().Key);
				}
			}

			return sb.ToString();
		}

		private void AddLine(string line)
		{
			for (var charIndex = 0; charIndex < line.Length; charIndex++)
			{
				var currentChar = line[charIndex];
				if (!_data[charIndex].ContainsKey(currentChar))
				{
					_data[charIndex][currentChar] = 0;
				}
				_data[charIndex][currentChar]++;
			}
		}
	}
}
