namespace adventofcode2016
{
	public class Day19 : IDay
	{
		public class WhiteElephantGame
		{
			private class Elf
			{
				public int Id { get; set; }
				public int Presents { get; set; }
				public Elf Next { get; set; }
				public Elf Previous { get; set; }

				public Elf Skip(int steps)
				{
					var result = this;
					for (int i = 0; i < steps; i++)
					{
						result = result.Next;
					}
					return result;
				}

				public void Delete()
				{
					Previous.Next = Next;
					Next.Previous = Previous;
				}
			}

			public int WhoHasAllThePresents { get { return _currentElf.Id; } }

			private Elf _currentElf;
			private readonly int _numberOfElves;

			public WhiteElephantGame(int numberOfElves)
			{
				_numberOfElves = numberOfElves;

				_currentElf = new Elf { Id = 1, Presents = 1 };
				var startElf = _currentElf;

				for (int i = 2; i <= numberOfElves; i++)
				{
					_currentElf.Next = new Elf { Id = i, Presents = 1, Previous = _currentElf };
					_currentElf = _currentElf.Next;
				}

				_currentElf.Next = startElf;
				startElf.Previous = _currentElf;
				_currentElf = startElf;
			}

			public void TakePresentsFromLeft()
			{
				while (_currentElf.Next != _currentElf)
				{
					_currentElf.Presents += _currentElf.Next.Presents;
					_currentElf.Next.Delete();
					_currentElf = _currentElf.Next;
				}
			}

			public void TakePresentsFromOpposite()
			{
				var alternator = (_numberOfElves +1) % 2;
				var oppositeElf = _currentElf.Skip(_numberOfElves / 2);
				while (_currentElf.Next != _currentElf)
				{
					// Steal present and delete it.
					_currentElf.Presents += oppositeElf.Presents;
					oppositeElf.Delete();

					// update opposite
					alternator = ++alternator % 2;
					oppositeElf = oppositeElf.Skip(alternator + 1);

					_currentElf = _currentElf.Next;
				}
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 19: An Elephant Named Joseph ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var game = new WhiteElephantGame(3005290);
			game.TakePresentsFromLeft();

			return "" + game.WhoHasAllThePresents;
		}

		public string GetAnswerB(bool animate = false)
		{
			var game = new WhiteElephantGame(3005290);
			game.TakePresentsFromOpposite();

			return "" + game.WhoHasAllThePresents;
		}
	}
}
