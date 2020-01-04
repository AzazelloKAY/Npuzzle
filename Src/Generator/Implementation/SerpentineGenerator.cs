using System;
using System.Collections.Generic;
using System.Linq;

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

			var isSolv = true;
			var valBoard = new List<uint>();
			int posZero = 0;
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (map[i, j] == 0)
					{
						posZero = i + 1;
						continue;
					}
					valBoard.Add(map[i, j]);
				}
			}

			var result = valBoard.Select((x, idx) => valBoard.Skip(idx).Count(y => y < x)).Sum();

			result += posZero;
			if (result % 2 != 0)
			{
				isSolv = false;
			}

			return isSolv;
		}
	}
}
