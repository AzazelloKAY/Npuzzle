using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Npuzzle.src.parser
{
	public class Parser
	{
		private static readonly Regex CommentLineRe = new Regex(@"^#.*\$$", RegexOptions.Compiled);
		private static readonly Regex FirstLineRe = new Regex(@"^(?<size>\d+)(\s*#.*)?\$$", RegexOptions.Compiled);
		private static readonly Regex AllNumbersRe = new Regex(@"\d+", RegexOptions.Compiled);
		private Regex NumberLineRe { get; set; }

		public int Size { get; private set; } = 0;
		private List<List<uint>> Field { get; set; } = new List<List<uint>>();

		public Parser()
		{
			Size = 0;
			NumberLineRe = new Regex(@"^(?<line>(?:\s*\d+){0})(?:\s*#.*)?\$$");
		}

		public Parser(int size)
		{
			Size = size;
			NumberLineRe = new Regex($@"^(?<line>(?:\s*\d+){{{Size}}})(?:\s*#.*)?\$$");
		}

		public bool IsCommentLine(string str)
		{
			var ret = false;

			if (!string.IsNullOrWhiteSpace(str))
			{
				ret = CommentLineRe.IsMatch(str);
			}

			return ret;
		}

		public bool IsFirstLine(string str)
		{
			var ret = false;

			if (!string.IsNullOrWhiteSpace(str))
			{
				var reg = FirstLineRe.Match(str);
				if (reg.Success)
				{
					Size = int.Parse(reg.Groups["size"].Value);
					NumberLineRe = new Regex($@"^(?<line>(?:\s*\d+){{{Size}}})(?:\s*#.*)?\$$");
				}
			}

			return ret;
		}

		public bool ParseLine(string str, out List<uint> res)
		{
			var ret = false;
			res = new List<uint>();

			if (!string.IsNullOrWhiteSpace(str))
			{
				var reg = NumberLineRe.Match(str);
				if (reg.Success && reg.Groups["line"]?.Length > 0)
				{
					res = AllNumbersRe.Matches(reg.Groups["line"].Value).Select(x => uint.Parse(x.Value)).ToList();
					ret = true;
				}
			}

			return ret;
		}

		public bool Parse(Func<string> readLine, out uint[,] res)
		{
			var ret = true;
			res = null;
			var field = new List<List<uint>>();

			try
			{
				do
				{

				}
				while (Size == 0);

				if (Size > 0)
				{
					do
					{

					}
					while (field.Count < Size);
				}
				else
				{
					Console.WriteLine("Wrong map.");
					ret = false;
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine("Something went wrong.");
				ret = false;
			}

			return ret;
		}
	}
}
