using Npuzzle.Models;
using Npuzzle.src.heuristic;
using System;
using System.Collections.Generic;

namespace Npuzzle.src
{
	public class Solver
	{
		public List<Board> Solution { get; }
		public long SolvingDuration { get; }
		public long StepCount { get; }

		Func<int, uint[,]> GenerateTarget { get; set; }
		IHeuristic Heuristic { get; }


		public Solver(IHeuristic heuristic, Func<int, uint[,]> generateTarget)
		{
			Heuristic = heuristic;
			GenerateTarget = generateTarget;
			Solution = new List<Board>();
		}

		public List<Board> Solve(uint[,] boardVal)
		{
			var goalBoard = GenerateTarget(boardVal.Length);

			var initBoard = new Board
			{
				Value = boardVal,
				Prev = null,
				Weight = Heuristic.Calculate(boardVal, goalBoard),
				Depth = 0,
			};

			throw new NotImplementedException();
		}


		private List<Board> MoveZero(Board b)
		{
			throw new NotImplementedException();
		}


	}
}
