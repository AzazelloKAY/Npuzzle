using System;
using System.Collections.Generic;
using System.Linq;
using Npuzzle.Models;
using Npuzzle.Src;
using Npuzzle.Src.Generator;
using Npuzzle.Src.Heuristic;
using Npuzzle.Src.Parser;

namespace Npuzzle
{
	class Program
	{
		static void Main(string[] args)
		{
			var parser = new Parser();
			List<string> lines;

			//read whole map
			if (args.Length == 0)
			{
				lines = parser.ReadFromConsole();
			}
			else
			{
				lines = parser.ReadFromFile(args[0]);
			}

			if (parser.Parse(lines, out uint[,] initMap) && parser.Validate())
			{
				Console.WriteLine("Please select Heuristic method");
				Console.WriteLine("<1> - Hamming");
				Console.WriteLine("<2> - Manhattan");
				Console.WriteLine("<3> - Euclidean");

				IHeuristic heuristic;
				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.D1: heuristic = new Hamming();
						break;
					case ConsoleKey.D2: heuristic = new Manhattan();
						break;
					case ConsoleKey.D3: heuristic = new Euclidean();
						break;
					default: heuristic = new Hamming();
						break;
				}

                var solver = new Solver(initMap, heuristic, GoalGenerator.Serpentine); //TODO: replase with snail!!!!

                solver.Solution?.ForEach(b =>
                {
                    b.Print();
                    Console.WriteLine();
                });

                Console.WriteLine($"Total time: {solver.SolvingDuration} mSec.");
                Console.WriteLine($"Path distanse: {solver.Solution.Count}");
			}
			else
			{
				Console.WriteLine("Some errors");
			}

			Console.ReadKey();
		}

	}
}
