using Npuzzle.Models;
using Npuzzle.Src.Heuristic;
using System;
using System.Collections.Generic;
using System.Linq;

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

			var initBoard = new Board(startMap, null, FindZero(startMap), Size, Heuristic.Calculate(startMap, GoalBoard, Size), 0);
			
			var queue = new List<Board> { initBoard };

			while (true)
			{
				var board = queue[0];
				queue.RemoveAt(0);

				if (board.Deviation == 0) //save solution path
				{
					Solution.Add(board);
					foreach (var b in board)
					{
						Solution.AddRange(b);
					}

					return;
				}


				MoveZero(board).ForEach(move => 
				{
					//AddWithPriorityCheck(queue, move)
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

					if (board.All(b => b != newBoard)) //add to res only if this board not already in path
					{
						res.Add(new Board(newBoard, board, newZero, Size, deviation, ++board.Depth));
					}
				}
			}
		}

		

		#region HELPERS

		private Position FindZero(uint[,] board)
		{
			for (var i = 0; i < Size; i++)
			{
				for (var j = 0; j < Size; j++)
				{
					if (board[i, j] == 0)
					{
						return new Position(i, j);
					}
				}
			}

			return null;
		}

		//TODO: OR AddWithPriorityCheck: add elem with lowest deviation to the front?????
		//deside where lowest step count
		private void AddWithPriorityCheck(List<Board> queue, Board b)
		{
			//queue.Insert();
			throw new NotImplementedException();

			//  вычисляем f(x)
			//private static int measure(ITEM item)
			//{
			//	ITEM item2 = item;
			//	int c = 0;   // g(x)
			//	int measure = item.getBoard().h();  // h(x)
			//	while (true)
			//	{
			//		c++;
			//		item2 = item2.prevBoard;
			//		if (item2 == null)
			//		{
			//			// g(x) + h(x)
			//			return measure + c;
			//		}
			//	}
			//}
			

		}

		#endregion

	}
}
