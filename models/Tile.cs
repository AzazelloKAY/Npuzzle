using System;
using System.Collections.Generic;
using System.Text;

namespace Npuzzle.models
{
	class Tile
	{
		public uint Val { get; set; }
		public Position CurrentP { get; set; }
		public Position TargetP { get; set; }
	}
}
