using Npuzzle.Models;
using Npuzzle.Src.Heuristic;
using System;
using System.Collections.Generic;

namespace Npuzzle.Src
{
	public class Solver
	{
		public List<Board> Solution { get; }
		public long SolvingDuration { get; }
		public long StepCount { get; }

		private Func<int, uint[,]> GenerateTarget { get; set; }
		private IHeuristic Heuristic { get; set; }
		private int Size { get; set; }
		private uint[,] GoalBoard { get; set; }


		public Solver(uint[,] board, int size, IHeuristic heuristic, Func<int, uint[,]> generateTarget)
		{
			Heuristic = heuristic;
			GenerateTarget = generateTarget;
			Solution = new List<Board>();

			Solve(board, size);
		}


		private void Solve(uint[,] startMap, int size)
		{
			Size = size;
			GoalBoard = GenerateTarget(Size);

			var initBoard = new Board(startMap, null, FindZero(startMap), Heuristic.Calculate(startMap, GoalBoard, Size), 0);
			
			var queue = new List<Board> { initBoard };

			while (true)
			{
				var board = queue[0];

				if (board.Deviation == 0) //save solution path
				{
					while(board.Prev != null)
					{
						Solution.Add(board);

						board = board.Prev;
					}
					
					return;
				}


				MoveZero(board).ForEach(move => 
				{
					//if(!containsInPath(board, board1))
					//{
					//	queue.Add(move);
					//}
				});

			}




			throw new NotImplementedException();
		}


		private List<Board> MoveZero(Board board)
		{
			var res = new List<Board>();

			Move(-1, 0, () => board.Zero.Line > 0);
			Move(1, 0, () => board.Zero.Line < Size - 1);
			Move(0, -1, () => board.Zero.Column > 0);
			Move(0, 1, () => board.Zero.Column < Size - 1);
		
			return res;

			void Move(int dx, int dy, Func< bool> check)
			{
				if (check())
				{
					return;
				}

				var newZero = new Position(board.Zero.Line + dx, board.Zero.Column + dy);

				if(!board.Prev.Zero.Equals(newZero))
				{				
					var newBoard = (uint[,])board.Value.Clone();
					newBoard[newZero.Line, newZero.Column] = 0;
					newBoard[board.Zero.Line, board.Zero.Column] = board.Value[newZero.Line, newZero.Column];
					var deviation = Heuristic.Calculate(newBoard, GoalBoard, Size);

					res.Add(new Board(newBoard, board, newZero, deviation, ++board.Depth));
				}
			}
		}


		#region HELPERS

		private Position FindZero(uint[,] board)
		{
			Position res = null;

			Iterate((i, j) =>
			{
				if (board[i, j] == 0)
				{
					res = new Position(i, j);
				}
			});

			return res;
		}

		private bool IsEqualBoard(Board b1, Board b2)
		{
			var res = true;

			Iterate((i, j) =>
			{
				if (b1.Value[i, j] != b2.Value[i, j])
				{
					res = false;
				}
			});

			return res;
		}

		private void Iterate(Action<int, int> act)
		{
			for (var i = 0; i < Size; i++)
			{
				for (var j = 0; j < Size; j++)
				{
					act(i, j);
				}
			}
		}

		#endregion

	}
}
