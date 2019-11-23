using Npuzzle.Models;
using Npuzzle.src.heuristic;
using System.Collections.Generic;

namespace Npuzzle.src
{
	public class Solver
	{
		public IHeuristic Heuristic { get; }

		public List<Board> Solution { get; }

		public Solver(IHeuristic heuristic)
		{
			Heuristic = heuristic;
			Solution = new List<Board>();
		}

		public Solver()
		{

		}


	}
}
