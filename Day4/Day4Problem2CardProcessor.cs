using System.Security.Cryptography;
using System.Text.RegularExpressions;
/*
 * https://adventofcode.com/2023/day/4
 * use c# list comparison to count winning numbers per line
 * for each winning number, get a copy of the next card after this one, continue until no winning numbers
 *
 */

namespace Day4
{
	class Day4Problem2CardProcessor
	{

		int totalNumberOfCards = 0;
		//standard C# dictionaries do not guarantee key order but it matters here
		private SortedDictionary<int, int> cardNumberToWinCount;
		private SortedDictionary<int, int> cardCounts;
		
		public void Run()
		{
			cardNumberToWinCount = new SortedDictionary<int, int>();
			cardCounts = new SortedDictionary<int, int>();
			GetCardWinCounts();
			ProcessCardCopies();
		}
		
		private void GetCardWinCounts()
		{
			Regex numbersPattern = new Regex(@"(?:.*\:\s+)(?:(\d+)+\s+)+(?:\|\s+)(?:(\d+)+(?:\s|\n)+)+",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);
			var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
			if (File.Exists(path))
			{
				using StreamReader file = new StreamReader(path);
				int counter = 0;
				
				while (file.ReadLine() is { } ln)
				//DEBUG VERSION
				//while (file.ReadLine() is { } ln && counter < 5)
				{
					//Console.WriteLine($"processing line {counter}: " + ln);
					ln = String.Concat(ln, " ");
					
					List<int> winningNumbers = new List<int>();
					List<int> scratchedOffNumbers = new List<int>();

					//Group 0 has the whole string, Group 1 has the winning numbers, Group 2 has the scratched numbers
					foreach (Capture capture in numbersPattern.Match(ln).Groups[1].Captures)
					{ winningNumbers.Add(Int32.Parse(capture.Value)); }
					
					foreach (Capture capture in numbersPattern.Match(ln).Groups[2].Captures)
					{ scratchedOffNumbers.Add(Int32.Parse(capture.Value)); }
					
					IEnumerable<int> commonNumbers = winningNumbers.Intersect(scratchedOffNumbers);
					cardNumberToWinCount.Add(counter, commonNumbers.Count());
					counter++;
				}

				file.Close();
			}
			else
			{
				Console.WriteLine($"no file found at {path}");
			}
			
		}

		private void ProcessCardCopies()
		{
			//one copy of each card initially
			for (int i = 0; i < cardNumberToWinCount.Count; i++)
			//DEBUG VERSION
			// for (int i = 0; i < 100; i++)
			{
				cardCounts.Add(i, 1);
			}
			foreach (int lineNumber in cardNumberToWinCount.Keys)
			{
				
				// Console.WriteLine($"line number {lineNumber} has {cardNumberToWinCount[lineNumber]} winners");
				// Console.WriteLine($"processing {cardCounts[lineNumber]} copies of this card");
				
				for (int i = 0; i < cardCounts[lineNumber]; i++)
				{
					// Console.WriteLine($"processing {i}th copy");
					for (int j = lineNumber + 1; j <= lineNumber + cardNumberToWinCount[lineNumber]; j++)
					{
						// Console.WriteLine($"incrementing card count for card {j}");
						cardCounts[j]++;
					}
				}
			}

			foreach (int lineNumber in cardCounts.Keys)
			{
				Console.WriteLine($"card count for line {lineNumber} is {cardCounts[lineNumber]}");
				totalNumberOfCards += cardCounts[lineNumber];
			}
			Console.WriteLine($"totalNumberOfCards is {totalNumberOfCards}");
		}


	}
}