using System.Linq;
using System.Text;

namespace adventofcode2016
{
	public class Day16 : IDay
	{
		public static string GenerateData(string initialState, int length)
		{
			var data = initialState;
			while (data.Length < length)
			{
				data += "0" +new string(data.Reverse().Select(c => c == '1' ? '0' : '1').ToArray());
			}

			return data.Substring(0, length);
		}

		public static string GenerateChecksum(string data)
		{
			var sb = new StringBuilder();
			for (int i = 0; i < data.Length; i+=2)
			{
				sb.Append(data[i] == data[i+1] ? '1' : '0');
			}

			return sb.ToString().Length % 2 == 0 ? GenerateChecksum(sb.ToString()) : sb.ToString();
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 16: Dragon Checksum ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var data = GenerateData("01000100010010111", 272);

			return GenerateChecksum(data);
		}

		public string GetAnswerB(bool animate = false)
		{
			var data = GenerateData("01000100010010111", 35651584);

			return GenerateChecksum(data);
		}
	}
}
