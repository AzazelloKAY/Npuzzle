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

			if (parser.Parse(lines, out uint[,] initMap))
			{
				Console.WriteLine("We got a map");

				//TODO: add selector
				var heuristic = new Hamming();

                //TODO: check map validity e.g.: no number duplication, map can be solvet etc.
                if (parser.Validate())
                {


                    //TODO: ??? IGoalgenerator => Generate and IsSolvabel ???
                    var solver = new Solver(initMap, heuristic, GoalGenerator.Serpentine);

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
                    Console.WriteLine("Map is not valid");
                }
			}
			else
			{
				Console.WriteLine("Some errors");
			}

			Console.ReadKey();
		}

	}
}
