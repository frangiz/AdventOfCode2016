using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace adventofcode2016
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var p = new Program();

			var runDay = string.Empty;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].ToLower().StartsWith("-d") && i < args.Length - 1)
				{
					runDay = args[i+1];
				}
			}

			if (!string.IsNullOrEmpty(runDay))
			{
				p.RunSingleDay(runDay);
			}
			else
			{
				p.Run();
			}
		}

		private void RunSingleDay(string day)
		{
			var instance = (from t in Assembly.GetExecutingAssembly().GetTypes()
							where t.GetInterfaces().Contains(typeof(IDay))
											 && t.GetConstructor(Type.EmptyTypes) != null
											 && t.Name.Replace("Day", "").Equals(day.Trim())
							select t).SingleOrDefault();

			if (instance == null) { Console.WriteLine("Daynumber \"{0}\" not found.", day); return; }

			ExecuteDays(new List<IDay> { Activator.CreateInstance(instance) as IDay });
		}

		private void Run()
		{
			var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
							where t.GetInterfaces().Contains(typeof(IDay))
											 && t.GetConstructor(Type.EmptyTypes) != null
							select Activator.CreateInstance(t) as IDay;

			instances = instances.OrderBy(
				instance => int.Parse(instance.GetType().Name.Replace("Day", "")));


			var quit = false;
			while (!quit)
			{
				Console.Clear();
				Console.WriteLine("Advent of Code 2016");
				Console.WriteLine();
				foreach (var day in instances.Select(d => d.Name))
				{
					Console.WriteLine(day);
				}

				var gotValidSelection = false;
				while (!gotValidSelection)
				{
					Console.WriteLine();
					Console.WriteLine("Enter the number of the day to display, 'all' for all days or 'quit' to quit.");
					Console.Write(">> ");
					var line = Console.ReadLine();
					if (line.ToLower().Trim().Equals("quit") || line.ToLower().Trim().Equals("exit"))
					{
						gotValidSelection = true;
						quit = true;
					}
					else if (line.ToLower().Trim().Equals("all"))
					{
						gotValidSelection = true;
						ExecuteDays(instances);
					}
					else
					{
						var selectedDay = instances.FirstOrDefault(d => d.GetType().Name.Replace("Day", "").Equals(line.Trim()));
						if (selectedDay == null) { continue; }
						gotValidSelection = true;

						ExecuteDays(new List<IDay> { selectedDay });
					}
				}
			}
		}

		private void ExecuteDays(IEnumerable<IDay> days)
		{
			Console.WriteLine();
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			foreach (var day in days)
			{
				Console.WriteLine(day.Name);
				day.PrintDay();
			}

			stopwatch.Stop();
			Console.WriteLine("Execution took: {0}.", stopwatch.Elapsed);
			Console.WriteLine("Done! Press enter to continue.");
			Console.ReadLine();
		}
	}
}
