using System;
using System.Collections.Generic;
using Npuzzle.src.parser;

namespace Npuzzle
{
	class Program
	{
		static void Main(string[] args)
		{
			var parser = new Parser("3");

			var str = Console.ReadLine();


			var res = parser.ParseLine(str, out List<uint> resList);

			Console.ReadKey();
		}
	}
}
