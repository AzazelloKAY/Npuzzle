using System;
using System.Collections.Generic;

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
			var IsSolv = true;
			var ValBoard = new List<uint>();
			int PosZero = 0;
			var halfSize = size / 2;

			var left = 0;
			var right = size - 1;
			for (int i = 0; i < halfSize; i++)
			{
				for (int j = left; j < right; j++) //>
				{
					ValBoard.Add(map[left, j]);
				}

				for (int j = left; j < right; j++) //V
				{
					ValBoard.Add(map[j, right]);
				}

				for (int j = right; j > left; j--) //<
				{
					ValBoard.Add(map[right, j]);
				}

				for (int j = right; j > left; j--) //^
				{
					ValBoard.Add(map[j, left]);
				}

				left++;
				right--;
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
