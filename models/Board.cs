﻿using Npuzzle.src.heuristic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Npuzzle.Models
{
	public class Board
	{
		public Board Prev { get; set; } = null;

		public uint[,] Value { get; set; }

		public Position Zero { get; set; }

		public long Weight { get; set; } //board weight, more is worse

		public int Depth { get; set; } //how fare board from initial board


		//public void CalcWeight(uint[,] targetBoard, Func<uint[,], uint[,], long> Heuristic)
		//{
		//	Weight = Heuristic(Value, targetBoard);
		//}
	}
}
