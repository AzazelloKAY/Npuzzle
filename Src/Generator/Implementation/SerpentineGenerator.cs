using System;
using System.Collections.Generic;

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

			var IsSolv = true;
			var ValBoard = new List<uint>();
			int PosZero = 0;
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (map[i, j] == 0)
					{
						PosZero = i;
					}
					ValBoard.Add(map[i, j]);
				}
			}

			int result = 0;
			foreach (var n in ValBoard)
			{
				int res = 0;
				foreach (var j in ValBoard)
				{
					if (n > j)
					{
						res++;
					}
				}

				res = res + PosZero;
				result += res;

			}

			if (result % 2 != 0)
			{
				IsSolv = false;
			}

			return IsSolv;
		}
	}
}
