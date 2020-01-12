using System;
using System.Collections.Generic;
using System.Linq;

namespace Npuzzle.Src.Generator
{
	public class SnailGenerator : IGoalGenerator
	{
		public uint[,] Generate(int size)
		{
			var res = new uint[size, size];
			var halfSize = size / 2;

			uint num = 1;
			var left = 0;
			var right = size - 1;

			for (int i = 0; i < halfSize; i++)
			{
				for(int j = left; j < right; j++) //>
				{
					res[left, j] = num++;
				}

				for(int j = left; j < right; j++) //V
				{
					res[j, right] = num++;
				}

				for (int j = right; j > left; j--) //<
				{
					res[right, j] = num++;
				}

				for (int j = right; j > left; j--) //^
				{
					res[j, left] = num++;
				}

				left++;
				right--;
			}

			if(size % 2 == 0)
			{
				res[halfSize, halfSize - 1] = 0;
			}

			return res;
		}

		public bool IsSolvabel(uint[,] map, int size)
		{
			var isSolv = true;
			var valBoard = new List<uint>();
			var halfSize = size / 2;

			var left = 0;
			var right = size - 1;
			for (int i = 0; i < halfSize; i++)
			{
				for (int j = left; j < right; j++) //>
				{
					IfAdd(map[left, j], left);
				}

				for (int j = left; j < right; j++) //V
				{
					IfAdd(map[j, right], j);
				}

				for (int j = right; j > left; j--) //<
				{
					IfAdd(map[right, j], right);
				}

				for (int j = right; j > left; j--) //^
				{
					IfAdd(map[j, left], j);
				}

				left++;
				right--;
			}

			if (size % 2 > 0)
			{
				IfAdd(map[halfSize, halfSize], halfSize);
			}

			var result = valBoard.Select((x, idx) => valBoard.Skip(idx).Count(y => y < x)).Sum();

			if (result % 2 != 0)
			{
				isSolv = false;
			}

			return isSolv;

			void IfAdd(uint n, int i)
			{
				if (n != 0)
				{
					valBoard.Add(n);
				}
			}
		}
		
	}
}
