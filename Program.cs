using System;
using System.Collections.Generic;
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
			if(args.Length == 0)
			{
				lines = parser.ReadFromConsole();
			}
			else
			{
				lines = parser.ReadFromFile(args[0]);
			}

			//parsing
			if (parser.Parse(lines, out uint[,] initMap))
			{
				Console.WriteLine("We got a map");

				var heuristic = new Hamming();

				//TODO: check map validity e.g.: no number duplication, map can be solvet etc.

				var solver = new Solver(initMap, parser.Size, heuristic, GoalGenerator.Serpentine);

				var result = solver.Solution;
			}
			else
			{
				Console.WriteLine("Some errors");
			}


			//do
			//{ 
			//	var str = Console.ReadLine();
			//	var res = parser.ParseLine(str, out List<uint> resList);
			//}
			//while (Console.ReadKey(true).Key != ConsoleKey.Escape);
			//Console.ReadKey();
		}


		


	}
}
