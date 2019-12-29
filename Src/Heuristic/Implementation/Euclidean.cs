using System;
using System.Collections.Generic;
using System.Text;

namespace Npuzzle.Src.Heuristic
{
	public class Euclidean : IHeuristic
	{
        public long Calculate(uint[,] board, uint[,] goal, int size)
        {
            var res = 0;

            for (int i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (board[i, j] != goal[i, j])
                    {
                        for (int y = 0; i < size; y++)
                        {
                            for (var x = 0; j < size; x++)
                            {
                                if (board[i, j] == goal[y, x])
                                {
                                    res = (int)Math.Sqrt(Math.Pow((i - y), 2) + Math.Pow((j - x), 2));
                                }
                            }
                        }

                    }
                }
            }

            return res;
        }
    }
}
