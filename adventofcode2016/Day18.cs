using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2016
{
	public class Day18 : IDay
	{
		public class TrapRoom
		{
			public int NumberOfSafeTiles { get { return _tiles.Count(t => t == TileType.SafeTile); } }

			private enum TileType { SafeTile, Trap };
			private List<TileType> _tiles;
			private static int width;

			public TrapRoom(string firstRow)
			{
				_tiles = new List<TileType>();
				foreach (var tile in firstRow)
				{
					_tiles.Add(tile == '.' ? TileType.SafeTile : TileType.Trap);
				}
				width = _tiles.Count;
			}

			public void AddRows(int rows)
			{
				for (int i = 0; i < rows; i++)
				{
					// First tile in a row is a bit special
					var preRowCenterIndex = _tiles.Count - width;
					AddTile(TileType.SafeTile, _tiles[preRowCenterIndex], _tiles[preRowCenterIndex + 1]);
					preRowCenterIndex++;
					for (int j = 1; j < width - 1; j++)
					{
						AddTile(_tiles[preRowCenterIndex - 1], _tiles[preRowCenterIndex], _tiles[preRowCenterIndex + 1]);
						preRowCenterIndex++;
					}
					// Last tile in a row is a bit special
					AddTile(_tiles[preRowCenterIndex - 1], _tiles[preRowCenterIndex], TileType.SafeTile);
				}
			}

			private void AddTile(TileType left, TileType center, TileType right)
			{
				if (left == TileType.Trap && center == TileType.Trap && right != TileType.Trap)
				{
					_tiles.Add(TileType.Trap);
				}
				else if (left != TileType.Trap && center == TileType.Trap && right == TileType.Trap)
				{
					_tiles.Add(TileType.Trap);
				}
				else if (left == TileType.Trap && center != TileType.Trap && right != TileType.Trap)
				{
					_tiles.Add(TileType.Trap);
				}
				else if (left != TileType.Trap && center != TileType.Trap && right == TileType.Trap)
				{
					_tiles.Add(TileType.Trap);
				}
				else
				{
					_tiles.Add(TileType.SafeTile);
				}
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 18: Like a Rogue ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var trapRoom = new TrapRoom(File.ReadAllText("Day18_input.txt").Trim());
			trapRoom.AddRows(39);

			return "" + trapRoom.NumberOfSafeTiles;
		}

		public string GetAnswerB(bool animate = false)
		{
			var trapRoom = new TrapRoom(File.ReadAllText("Day18_input.txt").Trim());
			trapRoom.AddRows(399999);

			return "" + trapRoom.NumberOfSafeTiles;
		}
	}
}
