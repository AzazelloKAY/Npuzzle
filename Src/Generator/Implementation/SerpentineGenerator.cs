using System;

namespace Npuzzle.Src.Generator
{
	public class SerpentineGenerator : IGoalGenerator
	{
		public uint[,] Generate(int size)
		{
			var res = new uint[size, size];

			for (int i = 0; i < size; i++)
			{
				for (var j = 0; j < size; j++)
				{
					res[i, j] = (uint)(i * size + j + 1);
				}
			}

			res[size - 1, size - 1] = 0;

			return res;
		}

		public bool IsSolvabel(uint[,] map, int size)
		{
			throw new NotImplementedException();
		}
	}
}
