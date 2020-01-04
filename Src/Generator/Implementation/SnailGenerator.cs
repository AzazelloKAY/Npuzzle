using System;

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
			throw new NotImplementedException();
		}
	}
}
