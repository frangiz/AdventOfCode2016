using System;
using System.Security.Cryptography;
using System.Text;

namespace adventofcode2016
{
	public class Day5 : IDay
	{
		public static char[] FindPassword(string doorId, int charsToGet, bool part2 = false)
		{
			var counter = 0;
			var password = new char[8];
			int index = 0;
			while (index < charsToGet)
			{
				var hash = doorId + counter.ToString();
				using (var md5 = MD5.Create())
				{
					var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(hash));
					hash = BitConverter.ToString(bytes).Replace("-", "");
				}
				if (hash.StartsWith("00000"))
				{
					if (part2)
					{
						var i = 0;
						if (int.TryParse(hash[5].ToString(), out i) &&
							i < password.Length &&
							password[i] == '\0')
						{
							password[i] = char.ToLower(hash[6]);
							index++;
						}
					}
					else
					{
						password[index++] = char.ToLower(hash[5]);
					}
				}
				counter++;
			}

			return password;
		}

		// --------------------------------------------------------------------
		public string Name { get { return "--- Day 5: How About a Nice Game of Chess? ---"; } }

		public void PrintDay()
		{
			Console.WriteLine("Answer A: " + new string(FindPassword("ojvtpuvg", 8)));
			Console.WriteLine("Answer B: " + new string(FindPassword("ojvtpuvg", 8, true)));
			Console.WriteLine();
		}
	}
}
