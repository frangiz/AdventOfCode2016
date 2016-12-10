using System;
using System.IO;

namespace adventofcode2016
{
	class Program
	{
		static void Main(string[] args)
		{
			var monitor = new Day8.Monitor(50, 6);
			foreach (var line in File.ReadAllLines("Day8_input.txt"))
			{
				monitor.ActivateCommand(line);
			}

			Console.WriteLine("Anser A: " +monitor.NumberOfPixelsLit());
			Console.WriteLine("Anser B: ");
			foreach (var line in monitor.GetOutput())
			{
				Console.WriteLine(line);
			}

			Console.ReadLine();
		}
	}
}
