using System.Text.RegularExpressions;
/*
 * https://adventofcode.com/2023/day/4
 * use c# list comparison to count winning numbers per line
 * for each winning number, get a copy of the next card after this one, continue until no winning numbers
 * ugh, recursion
 *
 */

namespace Day4
{
	class Day4Problem2CardProcessor
	{

		int totalNumberOfCards = 0;
		private List<CardData> cards = new List<CardData>();
		public void Run()
		{
			ProcessFile();
		}
		
		private void ProcessFile()
		{
			Regex numbersPattern = new Regex(@"(?:.*\:\s+)(?:(\d+)+\s+)+(?:\|\s+)(?:(\d+)+(?:\s|\n)+)+",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);
			var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
			if (File.Exists(path))
			{
				using StreamReader file = new StreamReader(path);
				int counter = 0;
				while (file.ReadLine() is { } ln && counter < 5)
				{
					//turn this into a function so that it can recur
					Console.WriteLine($"processing line {counter}: " + ln);
					ln = String.Concat(ln, " ");
					
					List<int> winningNumbers = new List<int>();
					List<int> scratchedOffNumbers = new List<int>();

					//Group 0 has the whole string, Group 1 has the winning numbers, Group 2 has the scratched numbers
					foreach (Capture capture in numbersPattern.Match(ln).Groups[1].Captures)
					{ winningNumbers.Add(Int32.Parse(capture.Value)); }
					
					foreach (Capture capture in numbersPattern.Match(ln).Groups[2].Captures)
					{ scratchedOffNumbers.Add(Int32.Parse(capture.Value)); }
					
					IEnumerable<int> commonNumbers = winningNumbers.Intersect(scratchedOffNumbers);
					Console.Write("common numbers found: ");
					foreach (int commonNumber in commonNumbers)
					{
						Console.Write(commonNumber+", ");
					}
					Console.WriteLine();
					CardData newCardData = new CardData(counter, commonNumbers.Count());
					Console.WriteLine($"created card data {newCardData}");
					cards.Add(newCardData);
					counter++;
				}

				file.Close();
			}
			else
			{
				Console.WriteLine($"no file found at {path}");
			}
			
		}


		public struct CardData
		{
			public CardData(int lineNo, int numWinners)
			{
				lineNumber = lineNo;
				numberOfWinners = numWinners;
			}

			public int lineNumber;
			public int numberOfWinners;
			public override string ToString() => $"({lineNumber}, {numberOfWinners})";
		}
			
	}
}