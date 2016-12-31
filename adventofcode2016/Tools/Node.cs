namespace adventofcode2016.Tools
{
	public class Node
	{
		public enum NodeTypes { Wall, OpenSpace };

		public int X { get; private set; }
		public int Y { get; private set; }
		public NodeTypes NodeType { get; private set; }
		public int Distance { get; private set; }

		private string _hash;
		public string Hash { get { return _hash; } }
		public Node Parent { get; private set; }

		public Node(int x, int y, int distance, NodeTypes nodeType, Node parent)
		{
			X = x;
			Y = y;
			Distance = distance;
			_hash = X.ToString() + Y.ToString();
			NodeType = nodeType;
			Parent = parent;
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
}
