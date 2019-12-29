using Npuzzle.Models;
using Npuzzle.Src.Heuristic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Npuzzle.Src
{
	public class Solver
	{
		public List<Board> Solution { get; }
		public long StepCount { get; }
		public long SolvingDuration => StopWatch.ElapsedMilliseconds;

		private Stopwatch StopWatch { get; }
		private Func<int, uint[,]> GenerateTarget { get; set; }
		private IHeuristic Heuristic { get; set; }
		private int Size { get; set; }
		private uint[,] GoalBoard { get; set; }


		public Solver(uint[,] board, IHeuristic heuristic, Func<int, uint[,]> generateTarget)
		{
			Heuristic = heuristic;
			GenerateTarget = generateTarget;
			Solution = new List<Board>();
			StopWatch = new Stopwatch();

			Solve(board);
		}


		private void Solve(uint[,] startMap)
		{
			var initBoard = new Board(startMap, null);

			Size = initBoard.Size;

			GoalBoard = GenerateTarget(Size);

			initBoard.Deviation = Heuristic.Calculate(startMap, GoalBoard, Size);

			var queue = new List<Board> { initBoard };

			//COUNTERS
			StopWatch.Start();

			while (queue.Count > 0)
			{
				var board = queue[0];
				queue.RemoveAt(0);

				if (board.Deviation == 0) //save solution path
				{
					foreach (var b in board)
					{
						Solution.Add(b);
					}
					Solution.Reverse();

					StopWatch.Stop();

					return;
				}

				MoveZero(board).ForEach(move =>
				{
					AddWithPriorityCheck(queue, move);
				});
			}

		}


		private List<Board> MoveZero(Board board)
		{
			var res = new List<Board>();

			Move(-1, 0, () => board.Zero.Line < 1);
			Move(1, 0, () => board.Zero.Line > Size - 2);
			Move(0, -1, () => board.Zero.Column < 1);
			Move(0, 1, () => board.Zero.Column > Size - 2);

			return res;

			void Move(int lineDlt, int columnDlt, Func<bool> breakCondition)
			{
				if (breakCondition())
				{
					return;
				}

				var newZero = new Position(board.Zero.Line + lineDlt, board.Zero.Column + columnDlt);

				if (board.Prev?.Zero.Equals(newZero) ?? false)
				{
					return;
				}

				var newBoard = (uint[,])board.Value.Clone();
				newBoard[newZero.Line, newZero.Column] = 0;
				newBoard[board.Zero.Line, board.Zero.Column] = board.Value[newZero.Line, newZero.Column];
				var deviation = Heuristic.Calculate(newBoard, GoalBoard, Size);

				if (board.Skip(1).All(b => b != newBoard)) //add to res only if this board not already in path
				{
					res.Add(new Board(newBoard, board, newZero, Size, deviation, board.Depth + 1));
				}
			}
		}


		private void AddWithPriorityCheck(List<Board> queue, Board b)
		{
			var m = b.Measure;
			var middle = 0;
			var left = 0;
			var right = queue.Count;

			while (left < right)
			{
				middle = (left + right) >> 1; // /2
				if (queue[middle].Measure == m)
				{
					break;
				}
				else if(queue[middle].Measure < m)
				{
					left = middle + 1;
				}
				else
				{
					right = middle - 1;
				}
			}

			queue.Insert((queue.Count == 0 || queue[middle].Measure >= m) ? middle : middle + 1, b);
		}


	}
}
