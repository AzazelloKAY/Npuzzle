namespace Npuzzle.Models
{
	public class Position
	{
		public int Line { get; set; }
		public int Column { get; set; }

		public Position()
		{ }

		public Position(int line, int column)
		{
			Line = line;
			Column = column;
		}

		public override bool Equals(object obj)
		{
			if(obj is Position pos)
			{
				if(Line == pos.Line && Column == pos.Column)
				{
					return true;
				}
			}

			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
