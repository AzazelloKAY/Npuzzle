using System;
using System.Collections.Generic;
using System.Text;

namespace Npuzzle.Models
{
	class Tile //not need this class // DROP?
	{
		public uint Val { get; set; }
		public Position CurrentP { get; set; }
		public Position TargetP { get; set; }
	}
}
