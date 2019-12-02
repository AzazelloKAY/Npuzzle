namespace Npuzzle.Src.Heuristic
{
	public interface IHeuristic
	{
		long Calculate(uint[,] board, uint[,] goal, int size);
	}
}
