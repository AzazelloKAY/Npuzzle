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
			throw new NotImplementedException();
		}

	}
}
