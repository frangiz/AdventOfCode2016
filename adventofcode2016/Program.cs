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
			var benchmark = false;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].ToLower().StartsWith("-d") && i < args.Length - 1)
				{
					runDay = args[i+1].Length == 1 ? "0" +args[i + 1] : args[i + 1];
				}
				if (args[i].ToLower().StartsWith("-b"))
				{
					benchmark = true;
				}
			}

			if (benchmark)
			{
				p.Benchmark();
			}
			else if (!string.IsNullOrEmpty(runDay))
			{
				p.RunSingleDay(runDay);
			}
			else
			{
				p.Run();
			}
		}

		private void Benchmark()
		{
			var instances = GetAllDayInstances();
			var benchmarks = new Dictionary<string, float>();
			foreach (var day in instances)
			{
				Console.Write("Benchmarking " +day.GetType().Name +": ");
				benchmarks[day.GetType().Name] = DoBenchmark(day, 100);
				Console.WriteLine(benchmarks[day.GetType().Name] + " ms");
			}
			Console.WriteLine("Total time: " +benchmarks.Sum(b => b.Value) +" ms");
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
			var instances = GetAllDayInstances();

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
						var input = line.Trim();
						input = input.Length != 1 ? input : "0" + input;
						var selectedDay = instances.FirstOrDefault(d => d.GetType().Name.Replace("Day", "").Equals(input));
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
				var start = stopwatch.Elapsed;
				Console.WriteLine(day.Name);
				Console.WriteLine("Answer A: " +day.GetAnswerA(true));
				Console.WriteLine("Answer B: " + day.GetAnswerB(true));
				if (days.Count() > 1)
				{
					Console.WriteLine("Execution of day took: {0} ms.", (stopwatch.Elapsed - start).TotalMilliseconds);
					Console.WriteLine("----------------------------------------");
				}
			}

			stopwatch.Stop();
			Console.WriteLine("Execution took: {0} ms.", stopwatch.Elapsed.TotalMilliseconds);
			Console.WriteLine("Done! Press enter to continue.");
			Console.ReadLine();
		}

		private IEnumerable<IDay> GetAllDayInstances()
		{
			var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
											where t.GetInterfaces().Contains(typeof(IDay))
															 && t.GetConstructor(Type.EmptyTypes) != null
											select Activator.CreateInstance(t) as IDay;

			instances = instances.OrderBy(
				instance => int.Parse(instance.GetType().Name.Replace("Day", "")));

			return instances;
		}

		private static List<string> SlowDays = new List<string>
		{ "Day05", "Day11", "Day14", "Day16", "Day19"};
		private float DoBenchmark(IDay day, int iterations)
		{
			GC.Collect();
			Stopwatch sw = Stopwatch.StartNew();
			// Run once for slow days...
			if (SlowDays.Contains(day.GetType().Name))
			{
				day.GetAnswerA();
				day.GetAnswerB();
				sw.Stop();
				return sw.ElapsedMilliseconds;
			}
			else
			{
				for (int i = 0; i < iterations; i++)
				{
					day.GetAnswerA();
					day.GetAnswerB();
				}
			}
			sw.Stop();
			return sw.ElapsedMilliseconds / (iterations * 1.0f);
		}
	}
}
