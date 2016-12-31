using System.Collections.Generic;
using System.Linq;

namespace adventofcode2016.Tools
{
	public static class Permutator
	{
		// http://stackoverflow.com/questions/33329184/generating-permutations-of-a-set-efficiently-and-with-distinction
		public static IEnumerable<T[]> Permute<T>(T[] list)
		{
			if (list.Length > 1)
			{
				T n = list[0];
				foreach (T[] subPermute in Permute(list.Skip(1).ToArray()))
				{
					for (int index = 0; index <= subPermute.Length; index++)
					{
						T[] pre = subPermute.Take(index).ToArray();
						T[] post = subPermute.Skip(index).ToArray();

						if (post.Contains(n))
						{
							continue;
						}

						yield return pre.Concat(new [] { n }).Concat(post).ToArray();
					}
				}
			}
			else
			{
				yield return list;
			}
		}
	}
}
