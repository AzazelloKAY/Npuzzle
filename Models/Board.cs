using System;
using System.Collections;
using System.Collections.Generic;

namespace Npuzzle.Models
{
	public class Board : IEnumerable<Board>
	{
		public uint[,] Value { get; private set; }

		public Board Prev { get; private set; } = null;

		public Position Zero { get; private set; }

		public int Size { get; private set; }

		public int Depth { get; set; } = 0; //how fare board from initial board

		public long Deviation { get; set; } = 0; //h(x) //board weight, more is worse

		public long Measure => GetMeasure();

		private long DepthAndDev { get; set; }

		private Func<long> GetMeasure { get; set; }

		public Board(uint[,] value, Board prev, Position zero, int size, long deviation, int depth, bool useGreedy = false)
		{
			Value = value;
			Prev = prev;
			Zero = zero;
			Size = size;// Value.GetLength(0);

			Deviation = deviation;
			Depth = depth;
			DepthAndDev = Depth + Deviation;

			if (useGreedy)
			{
				GetMeasure = () => { return Deviation; };
			}
			else
			{
				GetMeasure = () => { return DepthAndDev; };
			}
		}

		public Board(uint[,] value, Board prev, bool useGreedy = false)
		{
			Value = value;
			Prev = prev;

			FindSizeAndZero();

			if (useGreedy)
			{
				GetMeasure = () => { return Deviation; };
			}
			else
			{
				GetMeasure = () => { return Depth + Deviation; };
			}
		}


		public IEnumerator<Board> GetEnumerator() //iterate excluding current Board
		{
			var b = this;
			while (b != default(Board))
			{
				yield return b;
				b = b.Prev;
			}
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			uint[,] b2 = null;

			if (obj is uint[,] array)
			{
				b2 = array;
			}
			else if (obj is Board b)
			{
				b2 = b.Value;
			}
			else
			{
				return false;
			}

			for (var i = 0; i < Size; i++) //no size check for this proj
			{
				for (var j = 0; j < Size; j++)
				{
					if (Value[i, j] != b2[i, j]) return false;
				}
			}

			return true;
		}

		public static bool operator ==(Board b1, uint[,] b2)
		{
			if (ReferenceEquals(b1, null))
			{
				return ReferenceEquals(b2, null);
			}

			return b1.Equals(b2);
		}
		public static bool operator !=(Board b1, uint[,] b2)
		{
			return !(b1 == b2);
		}

		public static bool operator ==(Board b1, Board b2)
		{
			if (ReferenceEquals(b1, null))
			{
				return ReferenceEquals(b2, null);
			}

			return b1.Equals(b2);
		}
		public static bool operator !=(Board b1, Board b2)
		{
			return !(b1 == b2);
		}

		public void FindSizeAndZero()
		{
			Size = Value.GetLength(0);

			for (var i = 0; i < Size; i++)
			{
				for (var j = 0; j < Size; j++)
				{
					if (Value[i, j] == 0)
					{
						Zero = new Position(i, j);
					}
				}
			}
		}

		public void Print(bool printStat = false)
		{
			if (printStat)
			{
				Console.WriteLine();
				Console.WriteLine($"dev> {Deviation}\tdep> {Depth}");
			}

			for (var i = 0; i < Size; i++)
			{
				for (var j = 0; j < Size; j++)
				{
					Console.Write($"{Value[i, j],-4}");
				}
				Console.WriteLine();
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
