using System;

namespace Npuzzle.Src.Heuristic
{
	public class Euclidean : IHeuristic
	{
		public long Calculate(uint[,] board, uint[,] goal, int size)
		{
			long res = 0;

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (board[i, j] == goal[i, j] || board[i, j] == 0)
					{
						continue;
					}

					res += GetDeviation(board[i, j], i, j, goal, size);					
				}
			}

			return res;
		}

		private long GetDeviation(uint n, int x, int y, uint[,] goal, int size)
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (n == goal[i, j])
					{
						return (long)Math.Sqrt((i - x) * (i - x) + (j - y) * (j - y));
					}
				}
			}

			return 0;
		}
	}
}
