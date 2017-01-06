using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2016
{
	public class Day10 : IDay
	{
		public class Bot
		{
			public event Action<int> GiveLowValue;
			public event Action<int> GiveHighValue;

			private readonly List<int> _values = new List<int>();
			public List<int> Values { get { return _values; } }

			private readonly List<Tuple<int, int>> _comparedValues = new List<Tuple<int, int>>();
			public List<Tuple<int, int>> ComparedValues { get { return _comparedValues; } }

			private readonly int _botId;
			public int BotId { get { return _botId; } }

			public Bot(int botId) { _botId = botId; }

			public void Receive(int value)
			{
				_values.Add(value);
				if (_values.Count == 2)
				{
					var lowValue = _values.Min();
					var highValue = _values.Max();
					_values.Clear();
					_comparedValues.Add(new Tuple<int, int>(lowValue, highValue));

					GiveLowValue?.Invoke(lowValue);
					GiveHighValue?.Invoke(highValue);
				}
			}
		}

		// ---------------------------------------------------------------------------
		public class OutputBin
		{
			private int _value;
			public int Value { get { return _value; } }

			public void Receive(int value) { _value = value; }
		}

		// ---------------------------------------------------------------------------
		public class BotFactory
		{
			private readonly Bot[] _bots;
			public Bot[] Bots { get { return _bots; } }

			private readonly OutputBin[] _outputbins;
			public OutputBin[] OutputBins { get { return _outputbins; } }

			public BotFactory(int bots, int outputBins)
			{
				_bots = new Bot[bots];
				_outputbins = new OutputBin[outputBins];

				for (int i = 0; i < _bots.Length; i++) { _bots[i] = new Bot(i); }
				for (int i = 0; i < _outputbins.Length; i++) { _outputbins[i] = new OutputBin(); }
			}

			public void AddInstructions(IEnumerable<string> instructions)
			{
				var regex = new Regex("bot (\\d+) gives low to ((bot|output) \\d+) and high to ((bot|output) \\d+)");
				var regexInit = new Regex("value (\\d+) goes to bot (\\d+)");
				foreach (var instruction in instructions)
				{
					var match = regex.Match(instruction);
					if (match.Success)
					{
						OnMatchGive(match);
					}
					else
					{
						var matchInit = regexInit.Match(instruction);
						if (matchInit.Success)
						{
							OnMatchInitialValue(matchInit);
						}
					}
				}
			}

			private void OnMatchGive(Match match)
			{
				var sourceBotId = match.Groups[1].Value;
				var lowValueReceiver = match.Groups[2].Value;
				var highValueReceiver = match.Groups[4].Value;

				// Setup low value part
				if (lowValueReceiver.StartsWith("bot"))
				{
					_bots[int.Parse(sourceBotId)].GiveLowValue +=
						_bots[int.Parse(lowValueReceiver.Replace("bot", "").Trim())].Receive;
				}
				else if (lowValueReceiver.StartsWith("output"))
				{
					_bots[int.Parse(sourceBotId)].GiveLowValue +=
						_outputbins[int.Parse(lowValueReceiver.Replace("output", "").Trim())].Receive;
				}

				// Setup high value part
				if (highValueReceiver.StartsWith("bot"))
				{
					_bots[int.Parse(sourceBotId)].GiveHighValue +=
						_bots[int.Parse(highValueReceiver.Replace("bot", "").Trim())].Receive;
				}
				else if (highValueReceiver.StartsWith("output"))
				{
					_bots[int.Parse(sourceBotId)].GiveHighValue +=
						_outputbins[int.Parse(highValueReceiver.Replace("output", "").Trim())].Receive;
				}
			}

			private void OnMatchInitialValue(Match match)
			{
				var value = match.Groups[1].Value;
				var receiverBot = match.Groups[2].Value;

				_bots[int.Parse(receiverBot)].Receive(int.Parse(value));
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 10: Balance Bots ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var botFactory = new BotFactory(210, 200);
			botFactory.AddInstructions(File.ReadAllLines("Day10_input.txt").OrderBy(l => l));

			return "" + botFactory.Bots
				.First(b => b.ComparedValues.Contains(new Tuple<int, int>(17, 61))).BotId;
		}

		public string GetAnswerB(bool animate = false)
		{
			var botFactory = new BotFactory(210, 200);
			botFactory.AddInstructions(File.ReadAllLines("Day10_input.txt").OrderBy(l => l));

			return "" + botFactory.OutputBins.Take(3)
				.Select(o => o.Value).Aggregate(1, (x, y) => x * y);
		}
	}
}
