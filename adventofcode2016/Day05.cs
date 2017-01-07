using System;
using System.Security.Cryptography;
using System.Text;

namespace adventofcode2016
{
	public class Day05 : IDay
	{
		public static char[] FindPassword(string doorId, int charsToGet, bool part2 = false)
		{
			var counter = 0;
			var password = new char[8];
			int index = 0;
			while (index < charsToGet)
			{
				using (var md5 = MD5.Create())
				{
					var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(doorId + counter.ToString()));
					if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] <= 0x0F)
					{
						if (part2)
						{
							var i = (bytes[2] & 0x0F);
							if (i < 0x0A && i < password.Length && password[i] == '\0')
							{
								password[i] = char.ToLower((bytes[3] & 0xF0).ToString("X")[0]);
								index++;
							}
						}
						else
						{
							password[index++] = char.ToLower((bytes[2] & 0x0F).ToString("X")[0]);
						}
					}
					counter++;
				}
			}

			return password;
		}

		// --------------------------------------------------------------------
		public string Name { get { return "--- Day 5: How About a Nice Game of Chess? ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			return new string(FindPassword("ojvtpuvg", 8));
		}

		public string GetAnswerB(bool animate = false)
		{
			return new string(FindPassword("ojvtpuvg", 8, true));
		}
	}
}
