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
			do
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

				var goalGenerator = SelectTargetConfiguration();

				if (!parser.Parse(lines, out uint[,] initMap) || !parser.Validate())
				{
					Console.WriteLine("Map invalid");
					return;
				}

				if (!goalGenerator.IsSolvabel(initMap, parser.Size))
				{
					Console.WriteLine("Map is unsolvable");
					return;
				}

				var solver = new Solver(initMap, SelectHeuristic(), goalGenerator.Generate, SelectAlgorithm());

				if (solver.Solution.Count() == 0)
				{
					Console.WriteLine("The board is unsolvable");
					return;
				}

				solver.Solution?.ForEach(b =>
				{
					b.Print();
					Console.WriteLine();
				});

				Console.WriteLine($"Total time: {solver.SolvingDuration} mSec.");
				Console.WriteLine($"Path distanse: {solver.Solution.Count}");
				Console.WriteLine($"Maximum number of state: {solver.MaxStateNum}");

				Console.WriteLine("[?] Press any key to repeat or <Esc> for exit >");
			}
			while (Console.ReadKey(true).Key != ConsoleKey.Escape);
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

		private static IGoalGenerator SelectTargetConfiguration()
		{
			Console.WriteLine("Please select desired configuration");
			Console.WriteLine("<1> - Snail");
			Console.WriteLine("<2> - Serpentine");
			IGoalGenerator conf;
			switch (Console.ReadKey().Key)
			{
				case ConsoleKey.D1:
					conf = new SnailGenerator();
					break;
				case ConsoleKey.D2:
					conf = new SerpentineGenerator();
					break;
				default:
					conf = new SnailGenerator();
					break;
			}
			Console.WriteLine();

			return conf;
		}



		bool IsSolvabel(uint[,] map, int size)
		{

			var IsSolv = true;
			var ValBoard = new  List<uint>();
			int PosZero = 0;
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (map[i, j] == 0)
					{
						PosZero = i;
					}
					ValBoard.Add(map[i, j]);
				}
			}

			int result = 0;
			foreach (var n in  ValBoard)
			{
				int res = 0;
				foreach (var j in ValBoard)
				{
					if (n > j)
					{
						res++;
					}
				}

				res = res + PosZero;
				result += res;

			}

			if (result % 2 != 0)
			{
				IsSolv = false;
			}

			return IsSolv;
		}
		bool IsSolvabel2(uint[,] map, int size)
		{

			var IsSolv = true;
			var ValBoard = new  List<uint>();
			int PosZero = 0;
			var halfSize = size / 2;

			
			var left = 0;
			var right = size - 1;
			for (int i = 0; i < halfSize; i++)
			{
				for(int j = left; j < right; j++) //>
				{
					ValBoard.Add(map[left, j]);
				}

				for(int j = left; j < right; j++) //V
				{
					ValBoard.Add(map[j, right]);
				}

				for (int j = right; j > left; j--) //<
				{
					ValBoard.Add(map[right, j]);
				}

				for (int j = right; j > left; j--) //^
				{
					ValBoard.Add(map[j, left]);
				}

				left++;
				right--;
			}

			int result = 0;
			foreach (var n in  ValBoard)
			{
				int res = 0;
				foreach (var j in ValBoard)
				{
					if (n > j)
					{
						res++;
					}
				}

				res = res + PosZero;
				result += res;

			}

			if (result % 2 != 0)
			{
				IsSolv = false;
			}

			return IsSolv;
		}

	}	
}


