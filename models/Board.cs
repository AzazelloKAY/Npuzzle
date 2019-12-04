using System.Collections;
using System.Collections.Generic;

namespace Npuzzle.Models
{
	public class Board : IEnumerable<Board>
	{
		public Board(uint[,] value, Board prev, Position zero,int size, long deviation, int depth)
		{
			Value = value;
			Prev = prev;
			Zero = zero;
			Size = size;// Value.GetLength(0);

			Deviation = deviation;
			Depth = depth;
		}

		public Board Prev { get; private set; } = null;

		public uint[,] Value { get; private set; }

		public Position Zero { get; private set; }

		public int Size { get; private set; }

		public int Depth { get; set; } //how fare board from initial board

		public long Deviation { get; set; } //board weight, more is worse


		public IEnumerator<Board> GetEnumerator() //iterate excluding current Board
		{
			var b = Prev;
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
	}
}
