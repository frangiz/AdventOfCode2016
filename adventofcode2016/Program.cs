using System;
using System.Linq;
using System.Reflection;

namespace adventofcode2016
{
	class Program
	{
		static void Main(string[] args)
		{
			var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
											where t.GetInterfaces().Contains(typeof(IDay))
															 && t.GetConstructor(Type.EmptyTypes) != null
											orderby t.Name
											select Activator.CreateInstance(t) as IDay;

			foreach (var day in instances)
			{
				day.PrintDay();
			}

			Console.ReadLine();
		}
	}
}
