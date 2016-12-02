using System;

namespace adventofcode2016
{
	public class Day2
	{
		private int _currentIndex;
		private readonly char[] _keypad;
		private readonly int _keypadWidth;
		private string _keysPressed = "";

		public string KeysPressed { get { return _keysPressed; } }

		public Day2(char startbutton, bool normalKeypad = true)
		{
			_currentIndex = -1;
			if (normalKeypad)
			{
				_keypad = new char[]
				{
					'1', '2', '3',
					'4', '5', '6',
					'7', '8', '9'
				};
				_keypadWidth = 3;
			}
			else
			{
				_keypad = new char[]
				{
					'_', '_', '1', '_', '_',
					'_', '2', '3', '4', '_',
					'5', '6', '7', '8', '9',
					'_', 'A', 'B', 'C', '_',
					'_', '_', 'D', '_', '_'
				};
				_keypadWidth = 5;
			}
			
			for (int i = 0; i < _keypad.Length; i++)
			{
				if (_keypad[i] == startbutton) { _currentIndex = i; }
			}
			if (startbutton == -1) { throw new Exception("Startbutton not available."); }
		}

		public char Move(string moveSequence)
		{
			foreach (var cmd in moveSequence)
			{
				if (CanMove(cmd))
				{
					MakeMove(cmd);
				}
			}
			_keysPressed += _keypad[_currentIndex];

			return _keypad[_currentIndex];
		}

		private bool CanMove(char direction)
		{
			if (direction == 'U')
			{
				var newIndex = _currentIndex - _keypadWidth;
				return newIndex >= 0 && _keypad[newIndex] != '_';
			}
			else if (direction == 'D')
			{
				var newIndex = _currentIndex + _keypadWidth;
				return newIndex < _keypad.Length && _keypad[newIndex] != '_';
			}
			else if (direction == 'L')
			{
				var newIndex = _currentIndex - 1;
				return (_currentIndex / _keypadWidth) == (newIndex / _keypadWidth)
					&& newIndex >= 0 && _keypad[newIndex] != '_';
			}
			else if (direction == 'R')
			{
				var newIndex = _currentIndex + 1;
				return (_currentIndex / _keypadWidth) == (newIndex / _keypadWidth)
					&& newIndex < _keypad.Length && _keypad[newIndex] != '_';
			}

			return false;
		}

		private void MakeMove(char direction)
		{
			if (direction == 'U')
			{
				_currentIndex -= _keypadWidth;
			}
			else if (direction == 'D')
			{
				_currentIndex += _keypadWidth;
			}
			else if (direction == 'L')
			{
				_currentIndex -= 1;
			}
			else if (direction == 'R')
			{
				_currentIndex += 1;
			}
		}
	}
}
