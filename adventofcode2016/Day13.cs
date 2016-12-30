using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode2016
{
	public class Day13 : IDay
	{
		private class Node
		{
			public enum NodeTypes { Wall, OpenSpace };

			public int X { get; private set; }
			public int Y { get; private set; }
			public NodeTypes NodeType { get; private set; }
			public int Distance { get; private set; }

			private string _hash;
			public string Hash { get { return _hash; } }

			public Node(int favoriteNumber, int x, int y, int distance)
			{
				X = x;
				Y = y;
				Distance = distance;
				_hash = X.ToString() + Y.ToString();
				NodeType = CalcNodeType(favoriteNumber);
			}

			private NodeTypes CalcNodeType(int favoriteNumber)
			{
				var solution = X * X + 3 * X + 2 * X * Y + Y + Y * Y;
				solution += favoriteNumber;
				return (Convert.ToString(solution, 2).Count(bit => bit == '1') % 2 == 0) ?
					NodeTypes.OpenSpace : NodeTypes.Wall;
			}

			public override int GetHashCode()
			{
				return _hash.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				var other = obj as Node;
				return other != null && X == other.X && Y == other.Y;
			}
		}

		public class Point
		{
			public int X { get; set; }
			public int Y { get; set; }
			public Point(int x, int y) { X = x; Y = y; }
		}

		public class PuzzleSolver
		{
			private readonly ISet<Node> _visitedNodes;
			private readonly List<Node> _unvisitedNodes;

			public PuzzleSolver()
			{
				_visitedNodes = new HashSet<Node>();
				_unvisitedNodes = new List<Node>();
			}

			public int FindShortestPath(Point start, Point destination, int favoriteNumber, bool partB = false)
			{
				_unvisitedNodes.Add(new Node(favoriteNumber, start.X, start.Y, 0));

				while (_unvisitedNodes.Count > 0)
				{
					var node = _unvisitedNodes.First();
					_unvisitedNodes.RemoveAt(0);

					if (partB && node.Distance > 50)
					{
						continue;
					}
					else
					{
						if (node.X == destination.X && node.Y == destination.Y)
						{
							return node.Distance;
						}
					}

					_visitedNodes.Add(node);
					foreach (var n in BreedNewNodes(favoriteNumber, node))
					{
						if (!_visitedNodes.Contains(n))
						{
							var duplicatedNode = _unvisitedNodes.FirstOrDefault(p => p.GetHashCode() == n.GetHashCode());
							if (duplicatedNode == null)
							{
								_unvisitedNodes.Add(n);
							}
							else if (duplicatedNode.Distance > n.Distance)
							{
								duplicatedNode = n;
							}
						}
					}
				}
				return partB ? _visitedNodes.Count : 0;
			}

			private IEnumerable<Node> BreedNewNodes(int favoriteNumber, Node node)
			{
				var nodes = new List<Node>();
				Node n = null;
				if (node.X > 0)
				{
					n = new Node(favoriteNumber, node.X - 1, node.Y, node.Distance + 1);
					if (n.NodeType == Node.NodeTypes.OpenSpace) { nodes.Add(n); }
				}
				if (node.Y > 0)
				{
					n = new Node(favoriteNumber, node.X, node.Y - 1, node.Distance + 1);
					if (n.NodeType == Node.NodeTypes.OpenSpace) { nodes.Add(n); }
				}
				n = new Node(favoriteNumber, node.X + 1, node.Y, node.Distance + 1);
				if (n.NodeType == Node.NodeTypes.OpenSpace) { nodes.Add(n); }

				n = new Node(favoriteNumber, node.X, node.Y + 1, node.Distance + 1);
				if (n.NodeType == Node.NodeTypes.OpenSpace) { nodes.Add(n); }

				return nodes;
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 13: A Maze of Twisty Little Cubicles ---"; } }

		public void PrintDay()
		{
			{
				var solver = new PuzzleSolver();
				Console.WriteLine("Answer A: " + solver.FindShortestPath(
					new Point(1, 1), new Point(31, 39), 1362));
			}
			{
				var solver = new PuzzleSolver();
				Console.WriteLine("Answer B: " + solver.FindShortestPath(
					new Point(1, 1), new Point(31, 39), 1362, true));
			}
			Console.WriteLine();
		}
	}
}
