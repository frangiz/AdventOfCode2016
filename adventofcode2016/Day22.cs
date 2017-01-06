using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace adventofcode2016
{
	public class Day22 : IDay
	{
		public class Node
		{
			public uint X { get; set; }
			public uint Y { get; set; }
			public uint Size { get; set; }
			public uint Used { get; set; }
			public uint Avail { get { return Size - Used; } }
			public bool Marked { get; set; }

			public Node() { }
		}

		public class Cluster
		{
			public int ViablePairs
			{
				get { return FindViablePairs().Count; }
			}

			private List<Node> _nodes;
			private uint _width;
			private Node _currentNode;

			public Cluster() { _nodes = new List<Node>(); }

			public void CreateNodes(IEnumerable<string> nodeInfos)
			{
				var nodeRegex = new Regex("/dev/grid/node-x(\\d+)-y(\\d+)\\s+(\\d+)T\\s+(\\d+)T\\s+(\\d+)T\\s+(\\d+)%");
				foreach (var nodeInfo in nodeInfos)
				{
					var match = nodeRegex.Match(nodeInfo);
					if (match.Success)
					{
						var x = uint.Parse(match.Groups[1].Value);
						_nodes.Add(new Node
						{
							X = x,
							Y = uint.Parse(match.Groups[2].Value),
							Size = uint.Parse(match.Groups[3].Value),
							Used = uint.Parse(match.Groups[4].Value)
						});
					}
				}

				_width = _nodes.Max(n => n.X) + 1;
				_nodes.Find(n => n.X == _width - 1 && n.Y == 0).Marked = true;
				_nodes = _nodes.OrderBy(n => n.Y).ThenBy(n => n.X).ToList();
			}

			private List<Tuple<Node, Node>> FindViablePairs()
			{
				var nodes = new List<Tuple<Node, Node>>();
				for (int i = 0; i < _nodes.Count; i++)
				{
					for (int j = i + 1; j < _nodes.Count; j++)
					{
						if (ViablePair(i, j)) { nodes.Add(new Tuple<Node, Node>(_nodes[i], _nodes[j])); }
						if (ViablePair(j, i)) { nodes.Add(new Tuple<Node, Node>(_nodes[j], _nodes[i])); }
					}
				}

				return nodes;
			}

			private bool ViablePair(int from, int to)
			{
				if (_nodes[from].Used > 0 && _nodes[from].Used <= _nodes[to].Avail)
				{
					return true;
				}

				return false;
			}

			public long SolvePartB(bool animate)
			{
				var steps = 0;
				_currentNode = _nodes.Find(n => n.Used == 0);

				steps += Move(0, -1, 2, animate);
				steps += Move(-1, 0, 6, animate);
				steps += Move(0, -1, 23, animate);
				steps += Move(1, 0, 12, animate);

				for (int i = 0; i < _width - 2; i++)
				{
					steps += Move(0, -1, 1, animate);
					steps += Move(1, 0, 1, animate);
					steps += Move(0, 1, 1, animate);
					steps += Move(-1, 0, 2, animate);
				}

				steps += Move(0, -1, 1, animate);
				steps += Move(1, 0, 1, animate);

				return steps;
			}

			private int Move(int dx, int dy, int steps, bool animate)
			{
				for (int i = 0; i < steps; i++)
				{
					var targetNode = _nodes.Find(n => _currentNode.X + dx == n.X && _currentNode.Y +dy == n.Y);
					if (targetNode == null || targetNode.Used > _currentNode.Avail)
					{
						throw new Exception("Invalid move!");
					}
					_currentNode.Used += targetNode.Used;
					targetNode.Used = 0;
					_currentNode.Marked = targetNode.Marked;
					targetNode.Marked = false;

					_currentNode = targetNode;

					if (animate) { Draw(); }
				}

				return steps;
			}

			private void Draw()
			{
				var sb = new StringBuilder();
				foreach (var node in _nodes)
				{
					if (node.X == 0 && node.Y == 0 && !node.Marked)
					{
						sb.Append(" S ");
					}
					else if (node.Marked)
					{
						sb.Append(" G ");
					}
					else if (node.Used == 0)
					{
						sb.Append(" _ ");
					}
					else if (node.Used > 100)
					{
						sb.Append(" # ");
					}
					else
					{
						sb.Append(" . ");
					}

					if (node.X == _width - 1)
					{
						sb.Append(Environment.NewLine);
					}
				}
				Console.Clear();
				Console.Write(sb.ToString());
				Thread.Sleep(50);
			}
		}

		// ---------------------------------------------------------------------------
		public string Name { get { return "--- Day 22: Grid Computing ---"; } }

		public string GetAnswerA(bool animate = false)
		{
			var cluster = new Cluster();
			cluster.CreateNodes(File.ReadAllLines("Day22_input.txt"));

			return "" + cluster.ViablePairs;
		}

		public string GetAnswerB(bool animate = false)
		{
			var cluster = new Cluster();
			cluster.CreateNodes(File.ReadAllLines("Day22_input.txt"));

			return "" + cluster.SolvePartB(animate);
		}
	}
}
