using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace adventofcode2016
{
	// https://en.wikipedia.org/wiki/Dijkstra's_algorithm
	public class Day11 : IDay
	{
		// ---------------------------------------------------------------------------
		public class State
		{
			private int[] _itemToFloor;
			public int[] ItemToFloor { get { return _itemToFloor; } }

			private int _distance;
			public int Distance { get { return _distance; } }

			private int _elevator;
			public int Elevator { get { return _elevator; } }

			private string _compareString;

			private State _parent;
			public State Parent { get { return _parent; } }

			public State(int[] itemToFloor, int distance, int elevator, State parent)
			{
				_itemToFloor = new int[itemToFloor.Length];
				Array.Copy(itemToFloor, _itemToFloor, itemToFloor.Length);
				_distance = distance;
				_elevator = elevator;

				_compareString = string.Join("", _itemToFloor) + _elevator;
				_parent = parent;
			}

			public State(State state) : this(state.ItemToFloor, state.Distance + 1, state.Elevator, state)
			{
			}

			public bool ValidState()
			{
				return BoundsOK() && ItemsOK();
			}

			public void Move(int itemIndex, int direction)
			{
				_itemToFloor[itemIndex] += direction;
				_elevator += direction;

				_compareString = string.Join("", _itemToFloor) + _elevator;
			}

			public void Move(int item1Index, int item2Index, int direction)
			{
				_itemToFloor[item1Index] += direction;
				_itemToFloor[item2Index] += direction;
				_elevator += direction;

				_compareString = string.Join("", _itemToFloor) + _elevator;
			}

			private bool BoundsOK()
			{
				if (_elevator < 0 || _elevator > 3) { return false; }

				for (var i = 0; i < _itemToFloor.Length; i++)
				{
					if (_itemToFloor[i] < 0 || _itemToFloor[i] > 3) { return false; }
				}

				return true;
			}

			private bool ItemsOK()
			{
				for (int chipIndex = 1; chipIndex < _itemToFloor.Length; chipIndex+=2)
				{
					// Microchip and RTG is not on the same floor.
					if (_itemToFloor[chipIndex] != _itemToFloor[chipIndex-1])
					{
						for (int rtgIndex = 0; rtgIndex < _itemToFloor.Length; rtgIndex += 2)
						{
							// Microchip and another RTG is in the same floor.
							if (_itemToFloor[chipIndex] == _itemToFloor[rtgIndex]) { return false; }
						}
					}
				}

				return true;
			}

			public override int GetHashCode()
			{
				return _compareString.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				var other = obj as State;
				if (other == null) { return false; }

				return _compareString.Equals(other._compareString);
			}
		}

		// ---------------------------------------------------------------------------
		private readonly ISet<State> _visited;
		private readonly Queue<State> _unvisited;
		private State _finalState;

		public Day11()
		{
			_visited = new HashSet<State>();
			_unvisited = new Queue<State>();
		}

		public int SearchAnswer(State startSate, State endState)
		{
			_finalState = endState;
			_unvisited.Enqueue(startSate);

			while (_unvisited.Count > 0)
			{
				var state = _unvisited.Dequeue();

				if (_visited.Contains(state) || !state.ValidState()) { continue; }
				_visited.Add(state);

				if (state.Equals(_finalState))
				{
					Animate(state);
					return state.Distance;
				}

				BreedNewStates(state);
			}

			return 0;
		}

		private void BreedNewStates(State state)
		{
			for (int i = 0; i < state.ItemToFloor.Length; i++)
			{
				if (state.Elevator != state.ItemToFloor[i]) { continue; }
				{
					var s = new State(state);
					s.Move(i, 1);
					_unvisited.Enqueue(s);
				}
				{
					var s = new State(state);
					s.Move(i, -1);
					_unvisited.Enqueue(s);
				}
				for (int j = i + 1; j < state.ItemToFloor.Length; j++)
				{
					if (state.Elevator != state.ItemToFloor[j]) { continue; }
					{
						var s = new State(state);
						s.Move(i, j, 1);
						_unvisited.Enqueue(s);
					}
					{
						var s = new State(state);
						s.Move(i, j, -1);
						_unvisited.Enqueue(s);
					}
				}
			}
		}

		private void Animate(State finalState)
		{
			var parents = new List<State>();
			var state = finalState;
			while (state != null)
			{
				parents.Add(state);
				state = state.Parent;
			}
			parents.Reverse();
			var sb = new StringBuilder();
			var indicies = new[] { "pog", "pom", "tmg", "tmm", "pmg", "pmm",
					"rug", "rum", "cog", "com", "elg", "elm", "dlg", "dlm"};
			foreach (var s in parents)
			{
				Console.Clear();
				sb.Clear();
				for (int floorId = 0; floorId < 4; floorId++)
				{
					sb.AppendFormat("F{0}: ", floorId + 1);
					for (int i = 0; i < s.ItemToFloor.Length; i++)
					{
						sb.Append(s.ItemToFloor[i] == floorId ? indicies[i].ToUpper() + " ": "    ");
					}
					if (s.Elevator == floorId) { sb.Append(" [E]"); }
					sb.Append(Environment.NewLine);
				}
				Console.WriteLine(sb.ToString());
				Thread.Sleep(200);
			}
		}

		// ---------------------------------------------------------------------------
		public void PrintDay()
		{
			Console.WriteLine("--- Day 11: Radioisotope Thermoelectric Generators ---");
			{
				var startState = new State(new[] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0 }, 0, 0, null);
				var endState = new State(new[]   { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, 0, 3, null);
				Console.WriteLine("Answer A: " + SearchAnswer(startState, endState));
			}
			{
				/*var startState = new State(new[] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 0, null);
				var endState = new State(new[]   { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, 0, 3, null);
				Console.WriteLine("Answer B: " + SearchAnswer(startState, endState));*/
			}
			Console.WriteLine();
		}

	}
}
