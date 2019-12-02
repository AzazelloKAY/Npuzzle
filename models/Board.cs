namespace Npuzzle.Models
{
	public class Board
	{
		public Board()
		{ }

		public Board(uint[,] value, Board prev, Position zero, long deviation, int depth)
		{
			Value = value;
			Prev = prev;
			Zero = zero;
			Deviation = deviation;
			Depth = depth;
		}

		public Board Prev { get; set; } = null;

		public uint[,] Value { get; set; }

		public Position Zero { get; set; }

		public long Deviation { get; set; } //board weight, more is worse

		public int Depth { get; set; } //how fare board from initial board


		//public void CalcWeight(uint[,] targetBoard, Func<uint[,], uint[,], long> Heuristic)
		//{
		//	Weight = Heuristic(Value, targetBoard);
		//}
	}
}
