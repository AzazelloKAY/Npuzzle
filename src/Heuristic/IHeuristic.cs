namespace Npuzzle.src.heuristic
{
	public interface IHeuristic
	{
		long Calculate(uint[,] b1, uint[,] b2);
	}
}
