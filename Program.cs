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
                var solver = new Solver(initMap, SelectHeuristic(), GoalGenerator.Snail, SelectAlgorithm());

				if (solver.Solution.Count() == 0)
				{
					Console.WriteLine("The board is unsolvable");
				}
				else
				{
					solver.Solution?.ForEach(b =>
					{
						b.Print();
						Console.WriteLine();
					});

					Console.WriteLine($"Total time: {solver.SolvingDuration} mSec.");
					Console.WriteLine($"Path distanse: {solver.Solution.Count}");
					Console.WriteLine($"Maximum number of state: {solver.MaxStateNum}");
				}
			}
			else
			{
				Console.WriteLine("Some errors");
			}

			Console.ReadKey();
		}


		private static IHeuristic SelectHeuristic()
		{
			Console.WriteLine("Please select Heuristic method");
			Console.WriteLine("<1> - Hamming");
			Console.WriteLine("<2> - Manhattan");
			Console.WriteLine("<3> - Euclidean");
			IHeuristic heuristic;
			switch (Console.ReadKey().Key)
			{
				case ConsoleKey.D1:
					heuristic = new Hamming();
					break;
				case ConsoleKey.D2:
					heuristic = new Manhattan();
					break;
				case ConsoleKey.D3:
					heuristic = new Euclidean();
					break;
				default:
					heuristic = new Hamming();
					break;
			}
			Console.WriteLine();

			return heuristic;
		}

		private static bool SelectAlgorithm()
		{
			Console.WriteLine("Please select algorithm");
			Console.WriteLine("<1> - Greedy");
			Console.WriteLine("<2> - A*");
			var useGreedy = false;
			switch (Console.ReadKey().Key)
			{
				case ConsoleKey.D1:
					useGreedy = true;
					break;
				case ConsoleKey.D2:
					useGreedy = false;
					break;
				default:
					useGreedy = true;
					break;
			}
			Console.WriteLine();

			return useGreedy;
		}

	}
}
