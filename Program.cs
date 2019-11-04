using System;
using System.Collections.Generic;
using Npuzzle.src.parser;

namespace Npuzzle
{
	class Program
	{
		static void Main(string[] args)
		{
			var parser = new Parser();
			List<string> strMap;

			if(args.Length == 0)
			{
				strMap = parser.ReadFromConsole();
			}
			else
			{
				strMap = parser.ReadFromFile(args[0]);
			}

			if (parser.Parse(strMap, out uint[,] mass))
			{
				Console.WriteLine("We got a mass");
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
