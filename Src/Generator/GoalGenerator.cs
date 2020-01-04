using System;

namespace Npuzzle.Src.Generator
{
	public static class GoalGenerator
	{
		public static uint[,] Serpentine(int size)
		{
			var res = new uint[size, size];

			for (int i = 0; i < size; i++)
			{
				for (var j = 0; j < size; j++)
				{
					res[i, j] = (uint)(i * size + j /*+ 1*/);
				}
			}

			return res;
		}

		public static uint[,] Snail(int size)
		{
            var res = new uint[size, size];

            for (int i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if(i == 0)
                    {
                        res[i, j] = (uint)(j + 1);
                    }
                    else if (j == size - 1)
                    {
                        res[i, j] = (uint)(size + 1 + i);
                    }
                    else if (i == size - 1)
                    {
                        res[i, j] = (uint)(size * 2 + (i - j - 1));
                    }
                    else if ()
                }
            }

            return res;
        }

	}
}
