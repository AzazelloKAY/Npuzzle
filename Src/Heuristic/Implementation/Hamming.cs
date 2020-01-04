namespace Npuzzle.Src.Heuristic
{
	public class Hamming : IHeuristic
	{
		public long Calculate(uint[,] board, uint[,] goal, int size)
		{
			var res = 0;

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (board[i, j] != goal[i, j])
					{
						res++;
					}
				}
			}

			return res;
		}
	}
}
