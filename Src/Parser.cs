using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Npuzzle.Src.Parser
{
	public class Parser
	{
		private static readonly Regex CommentLineRe = new Regex(@"^#.*$", RegexOptions.Compiled);
		private static readonly Regex FirstLineRe = new Regex(@"^(?<size>\d+)(\s*#.*)?$", RegexOptions.Compiled);
		private static readonly Regex AllNumbersRe = new Regex(@"\d+", RegexOptions.Compiled);
		private Regex NumberLineRe { get; set; }

		public int Size { get; private set; } = 0;
		private List<List<uint>> Board { get; set; } = new List<List<uint>>();

		public Parser()
		{
			Size = 0;
			NumberLineRe = new Regex(@"^(?<line>(?:\s*\d+){0})(?:\s*#.*)?$");
		}

		public Parser(int size)
		{
			Size = size;
			NumberLineRe = new Regex($@"^(?<line>(?:\s*\d+){{{Size}}})(?:\s*#.*)?$");
		}

		public bool IsCommentLine(string str)
			=> !string.IsNullOrWhiteSpace(str) && CommentLineRe.IsMatch(str);


		public bool IsFirstLine(string str)
		{
			var ret = false;

			if (!string.IsNullOrWhiteSpace(str))
			{
				var reg = FirstLineRe.Match(str);
				if (reg.Success)
				{
					Size = int.Parse(reg.Groups["size"].Value);
					NumberLineRe = new Regex($@"^(?<line>(?:\s*\d+){{{Size}}})(?:\s*#.*)?$");
					ret = true;
				}
			}

			return ret;
		}

		public bool ParseLine(string str, out List<uint> res)
		{
			var ret = false;
			res = new List<uint>();

			if (string.IsNullOrWhiteSpace(str))
			{
				return ret;
			}

			var reg = NumberLineRe.Match(str);
			if (reg.Success && reg.Groups["line"]?.Length > 0)
			{
				res = AllNumbersRe.Matches(reg.Groups["line"].Value).Select(x => uint.Parse(x.Value)).ToList();
				ret = true;
			}

			
			return ret;
		}

		public bool Parse(List<string> input, out uint[,] res)
		{
			var ret = true;
			res = null;

			try
			{
				foreach(var line in input)
				{
					if (IsCommentLine(line))
					{
						continue;
					}

					if(Size == 0 && IsFirstLine(line))
					{
						continue;
					}
					
					if(Size > 0 && ParseLine(line, out List<uint> numLine))
					{
						Board.Add(numLine);

						if(numLine.Count != Size || Board.Count > Size)
						{
							//throw nessage "Wrong map size"
							ret = false;
							break;
						}

						continue;
					}

					//test mac last line comnd + D ??
					//if(map.Count == Size) { }

					ret = false;
					break;
				}
			}
			catch//(Exception ex)
			{
				Console.WriteLine("Something went wrong.");
				ret = false;
			}

			if(ret)
			{
				//make res array
				res = new uint[Size, Size];
				for(var r = 0; r < Size; r++)
				{
					for(var c = 0; c < Size; c++)
					{
						res[r, c] = Board[r][c];
					}
				}
			}

			return ret;
		}

		#region READERS

		public List<string> ReadFromConsole()
		{
			var ret = new List<string>();

			try
			{
				var line = string.Empty;
				while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
				{
					ret.Add(line);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error in reading");
			}

			return ret;
		}

		public List<string> ReadFromFile(string path)
		{
			var ret = new List<string>();

			if(!string.IsNullOrWhiteSpace(path))
			{
				try
				{
					using (var file = File.OpenText(path))
					{
						while (!file.EndOfStream)
						{
							ret.Add(file.ReadLine());
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error in file reading");
				}
			}

			return ret;
		}

		#endregion

        public bool Validate()
        {
            var result = false;
            var boardNumber = new List<uint>();

			Board.ForEach(l => boardNumber.AddRange(l));

			if (boardNumber.Count == Math.Pow(Size, 2))
            {
                boardNumber.Sort();
                if (boardNumber.Last() == boardNumber.Count - 1)
                {
					result = boardNumber.Select((x, idx) => { return x == idx; }).All(x => x == true);
                }
            }

			if(result)
			{
				Console.WriteLine("We got a map");
			}
			else
			{
				Console.WriteLine("Invalid map");
			}

            return result;
        }
	}
}
