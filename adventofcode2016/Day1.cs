using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace adventofcode2016
{
	public class Day1
	{
		private Vector2 _currentPos = new Vector2(0, 0);
		private Vector2 _direction = new Vector2(0, 1);
		private readonly List<Vector2> _positions = new List<Vector2>();
		private bool _done = false;
		private bool _stopOnDuplicatePos = false;

		public Day1(bool stopOnDuplicatePos = false)
		{
			_stopOnDuplicatePos = stopOnDuplicatePos;
		}

		public int FindDistance(string path)
		{
			var cmds = path.Split(',').Select(p => p.Trim());
			foreach (var cmd in cmds)
			{
				Turn(cmd.Substring(0, 1));
				Walk(int.Parse(cmd.Substring(1)));
				if (_done)
				{
					break;
				}
			}

			return (int)(Math.Abs(_currentPos.X) + Math.Abs(_currentPos.Y));
		}

		private void Walk(int steps)
		{
			for (int i = 0; i < steps; i++)
			{
				TakeStep();
				if (PositionAlreadyVisited(_currentPos) && _stopOnDuplicatePos)
				{
					_done = true;
					break;
				}
				_positions.Add(_currentPos);
			}
		}

		private bool PositionAlreadyVisited(Vector2 position)
		{
			return _positions.Contains(position);
		}

		private void Turn(string dir)
		{
			var rotation = 0;
			if (dir == "L")
			{
				rotation = -90;
			}
			else if (dir == "R")
			{
				rotation = +90;
			}
			_direction = Vector2.Transform(_direction,
					Matrix3x2.CreateRotation((float)(rotation * (Math.PI / 180))));
			var l = _direction.Length();
		}

		private void TakeStep()
		{
			_currentPos = Vector2.Add(_currentPos, _direction);
		}
	}
}
