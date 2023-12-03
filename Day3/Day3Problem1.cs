using System.Text.RegularExpressions;

namespace Day3
{
	class Day3Problem1
	{
		static void Main()
		{
			List<MatchCollection> symbolMatches = new List<MatchCollection>();
			
			var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
			if (File.Exists(path))
			{
				// Read file using StreamReader. Reads file line by line
				using StreamReader file = new StreamReader(path);
				int counter = 0;

				/*
				 * either turn input into a vast 2d array and write a custom comparator, or
				 * use regexes and match indices which works out to nearly the same thing
				 *
				 * 0) what is a symbol? Anything that *isn't* a digit or .
				 * 1) match any group of digits
				 * 2) get indices of symbols in line n-1, n, n+1; check if there's a symbol between
				 *		(index of matched digits-1) and (index of matched digits+match length+1)
				 */

				Regex symbolPattern = new Regex(@"[^0123456789\.]",
					RegexOptions.Compiled | RegexOptions.IgnoreCase);
				Regex partNumberPattern = new Regex(@"(\d+)",
					RegexOptions.Compiled | RegexOptions.IgnoreCase);

				//there is probably a way to do this that only goes through the input file once, but
				//make it work then make it better
				while (file.ReadLine() is { } ln && counter < 5)
				{
					//Console.WriteLine($"processing line {counter}: " + ln);
					// foreach (Match match in symbolPattern.Matches(ln))
					// {
					// 	Console.WriteLine("symbol pattern matched "+match.Value+" at index "+match.Index);
					// }
					symbolMatches.Add(symbolPattern.Matches(ln));

					counter++;
				}

				int indexOfLastLineRead = counter - 1;
				//restart StreamReader at beginning of file
				file.DiscardBufferedData();
				file.BaseStream.Seek(0, SeekOrigin.Begin);
				counter = 0;
				int firstIndexToCheckForSymbol = -1;
				int lastIndexToCheckForSymbol = -1;
				while (file.ReadLine() is { } ln && counter < 4)
				{
					Console.WriteLine($"processing line {counter}: " + ln);
					foreach (Match match in partNumberPattern.Matches(ln))
					{
						
						firstIndexToCheckForSymbol = (Math.Max(0, match.Index - 1));
						lastIndexToCheckForSymbol = match.Index + match.Value.Length;
						Console.WriteLine("part number pattern matched "+match.Value+
						                  $", first index {firstIndexToCheckForSymbol}"+
						                  $", last index {lastIndexToCheckForSymbol}");
						//check lines n-1, n, n+1 for nearby symbols
						for (int i = Math.Max(0, counter-1); i <= Math.Min(indexOfLastLineRead, counter+1); i++)
						{
							Console.WriteLine($"checking symbols in line {i}");

							foreach (Match symbolMatch in symbolMatches[i])
							{
								if (symbolMatch.Index >= firstIndexToCheckForSymbol &&
								    symbolMatch.Index <= lastIndexToCheckForSymbol)
								{
									Console.WriteLine($"found an adjacent symbol on line {i}: "+symbolMatch.Value);
								}
							}
						}
						
					}

					counter++;
				}

				file.Close();
			}
			else
			{
				Console.WriteLine($"no file found at {path}");
			}
		}
	}
}