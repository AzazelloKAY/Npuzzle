using Npuzzle.src.heuristic;

namespace Npuzzle.src
{
	class Solver
	{
		public IHeuristic Heuristic { get; }

		public Solver(IHeuristic heuristic)
		{
			Heuristic = heuristic;
		}



	}
}
