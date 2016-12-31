using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode2016.Tools
{
	public class PathFinder
	{
		private readonly ISet<Node> _visitedNodes;
		private readonly List<Node> _unvisitedNodes;
		private Func<int, int, Node.NodeTypes> _nodeTypeCallback;

		public PathFinder()
		{
			_visitedNodes = new HashSet<Node>();
			_unvisitedNodes = new List<Node>();
		}

		public int FindShortestPath(Point start, Point destination,
			Func<int, int, Node.NodeTypes> nodeTypeCallback)
		{
			return FindPaths(start, destination, nodeTypeCallback, true).First().Distance;
		}

		public IEnumerable<Node> FindAllPaths(Point start, Point destination,
			Func<int, int, Node.NodeTypes> nodeTypeCallback, int maxDistance)
		{
			return FindPaths(start, destination, nodeTypeCallback, false, maxDistance);
		}

		private IEnumerable<Node> FindPaths(Point start, Point destination,
			Func<int, int, Node.NodeTypes> nodeTypeCallback, bool shortestOnly, int maxDistance = int.MaxValue)
		{
			_nodeTypeCallback = nodeTypeCallback;
			_visitedNodes.Clear();
			_unvisitedNodes.Clear();

			_unvisitedNodes.Add(new Node(start.X, start.Y, 0, Node.NodeTypes.OpenSpace, null));

			while (_unvisitedNodes.Count > 0)
			{
				var node = _unvisitedNodes.First();
				_unvisitedNodes.RemoveAt(0);

				if (!shortestOnly && node.Distance > maxDistance)
				{
					continue;
				}
				else
				{
					if (node.X == destination.X && node.Y == destination.Y)
					{
						return new List<Node> { node };
					}
				}

				_visitedNodes.Add(node);
				foreach (var n in BreedNewNodes(node))
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
			return _visitedNodes.ToList();
		}

		private IEnumerable<Node> BreedNewNodes(Node node)
		{
			var nodes = new List<Node>();
			Node.NodeTypes nodeType;

			if (node.X > 0)
			{
				nodeType = _nodeTypeCallback.Invoke(node.X - 1, node.Y);
				if (nodeType == Node.NodeTypes.OpenSpace)
				{
					nodes.Add(new Node(node.X - 1, node.Y, node.Distance + 1, nodeType, node));
				}
			}

			if (node.Y > 0)
			{
				nodeType = _nodeTypeCallback.Invoke(node.X, node.Y - 1);
				if (nodeType == Node.NodeTypes.OpenSpace)
				{
					nodes.Add(new Node(node.X, node.Y - 1, node.Distance + 1, nodeType, node));
				}
			}

			nodeType = _nodeTypeCallback.Invoke(node.X + 1, node.Y);
			if (nodeType == Node.NodeTypes.OpenSpace)
			{
				nodes.Add(new Node(node.X + 1, node.Y, node.Distance + 1, nodeType, node));
			}

			nodeType = _nodeTypeCallback.Invoke(node.X, node.Y + 1);
			if (nodeType == Node.NodeTypes.OpenSpace)
			{
				nodes.Add(new Node(node.X, node.Y + 1, node.Distance + 1, nodeType, node));
			}

			return nodes;
		}
	}
}
