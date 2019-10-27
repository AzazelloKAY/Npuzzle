using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Npuzzle.src.parser
{
	class Parser
	{
		private static Regex CommentLineRe = new Regex(@"^#.*\$$", RegexOptions.Compiled);
		private static Regex FirstLineRe = new Regex(@"^(?<size>\d+)(\s*#.*)?\$$", RegexOptions.Compiled);

		public int Size { get; private set; }
		private Regex StrRe { get; set; }

		public Parser(string size)
		{
			Size = int.Parse(size);
			StrRe = new Regex($@"^(?:\s*(?<num>\d+)){{{size}}}(\s*#.*)?\$$");//TODO: change regex for kapture all number!
		}

		public static bool IsCommentLine(string str)
		{
			var ret = false;

			if (!string.IsNullOrWhiteSpace(str))
			{
				ret = CommentLineRe.IsMatch(str);
			}

			return ret;
		}

		public static bool IsFirstLine(string str, out string size)
		{
			var ret = false;
			size = string.Empty;

			if (!string.IsNullOrWhiteSpace(str))
			{
				var reg = FirstLineRe.Match(str);
				if(reg.Success)
				{
					size = reg.Groups["size"].Value;
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
				var reg = StrRe.Match(str);
				if (reg.Success)
				{
					//reg.Groups;
				}
			}

			return ret;
		}
	}
}
