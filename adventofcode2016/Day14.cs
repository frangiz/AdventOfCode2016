using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace adventofcode2016
{
	public class Day14 : IDay
	{
		public class KeyGenerator
		{
			private class SecretKey
			{
				public int Index { get; set; }

				public List<char> RepeatingListOfThrees { get; set; }
				public List<char> RepeatingListOfFives { get; set; }
			}

			private readonly string _salt;

			private int _index;
			private List<SecretKey> _keys;
			private List<SecretKey> _potentialKeys;

			public KeyGenerator(string salt)
			{
				_salt = salt;
				_keys = new List<SecretKey>();
				_potentialKeys = new List<SecretKey>();
			}

			public void GenerateKeys(int iterations = 1)
			{
				while (true)
				{
					var key = GeneratePotentialKey(iterations);
					if (key.RepeatingListOfThrees.Count == 0 && key.RepeatingListOfFives.Count == 0) { continue; }

					// Are we done?
					if (_keys.Count >= 64 && _keys[_keys.Count - 1].Index + 1000 < key.Index)
					{
						break;
					}

					foreach (var pk in _potentialKeys.ToArray())
					{
						foreach (var c in pk.RepeatingListOfThrees)
						{
							if (pk.Index + 1000 > key.Index && key.RepeatingListOfFives.Contains(c))
							{
								_keys.Add(pk);
								_potentialKeys.Remove(pk);
								_keys.Sort((SecretKey sk1, SecretKey sk2) => { return sk1.Index.CompareTo(sk2.Index); });
							}
						}
					}

					_potentialKeys.Add(key);
				}
			}

			public int IndexForKeyIndex(int keyNumber)
			{
				return _keys[keyNumber].Index;
			}

			private SecretKey GeneratePotentialKey(int iterations)
			{
				var hash = _salt + _index;
				using (var md5 = MD5.Create())
				{
					for (int i = 0; i < iterations; i++)
					{
						var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(hash));
						hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
					}
				}

				return new SecretKey
				{
					Index = _index++,
					RepeatingListOfThrees = TryFindRepeatingChars(hash, 3).Take(1).ToList(),
					RepeatingListOfFives = TryFindRepeatingChars(hash, 5)
				};
			}

			private List<char> TryFindRepeatingChars(string hash, int charsInARow)
			{
				var inARow = new List<char>();
				char lastChar = '\0';
				int counter = 1;

				for (int i = 0; i < hash.Length; i++)
				{
					if (lastChar == hash[i])
					{
						counter++;
						if (counter == charsInARow)
						{
							if (!inARow.Contains(lastChar))
							{
								inARow.Add(lastChar);
							}
						}
					}
					else
					{
						lastChar = hash[i];
						counter = 1;
					}
				}

				return inARow;
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 14: One-Time Pad ---"; } }

		public void PrintDay()
		{
			var generator = new KeyGenerator("yjdafjpo");
			generator.GenerateKeys();
			Console.WriteLine("Answer A: " + generator.IndexForKeyIndex(63));

			generator = new KeyGenerator("yjdafjpo");
			generator.GenerateKeys(2016+1);
			Console.WriteLine("Answer B: " + generator.IndexForKeyIndex(63));

			Console.WriteLine();
		}
	}
}
