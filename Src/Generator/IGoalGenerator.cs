namespace Npuzzle.Src.Generator
{
	public interface IGoalGenerator
	{
		uint[,] Generate(int size);
		bool IsSolvabel(uint[,] map, int size);
	}
}
