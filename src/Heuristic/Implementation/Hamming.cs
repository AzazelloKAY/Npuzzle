using Npuzzle.Src.Heuristic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Npuzzle.Src.Heuristic
{
	public class Hamming : IHeuristic //static?
	{
		public long Calculate(uint[,] board, uint[,] goal, int size)
		{
			var res = 0;

			for (int i = 0; i < size; i++)
			{
				for(var j = 0; j < size; j++)
				{
					if(/*goal[i,j] != 0 &&*/ board[i,j] != goal[i,j]) //why check zero tile??? if zero check - save board[i,j] to var, will it be faster???
					{
						res++;
					}
				}
			}

			return res;
		}
	}
}
