using System.Text.RegularExpressions;

namespace Day4
{
	class Day4Problem2CardProcessor
	{

		private int _totalNumberOfCards;
		//standard C# dictionaries do not guarantee key order but it matters here
		private SortedDictionary<int, int> _cardNumberToWinCount = new SortedDictionary<int, int>();
		private SortedDictionary<int, int> _cardCounts = new SortedDictionary<int, int>();
		
		public void Run()
		{
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
				{
					ln = String.Concat(ln, " ");
					
					List<int> winningNumbers = new List<int>();
					List<int> scratchedOffNumbers = new List<int>();

					//Group 0 has the whole string, Group 1 has the winning numbers, Group 2 has the scratched numbers
					foreach (Capture capture in numbersPattern.Match(ln).Groups[1].Captures)
					{ winningNumbers.Add(Int32.Parse(capture.Value)); }
					
					foreach (Capture capture in numbersPattern.Match(ln).Groups[2].Captures)
					{ scratchedOffNumbers.Add(Int32.Parse(capture.Value)); }
					
					IEnumerable<int> commonNumbers = winningNumbers.Intersect(scratchedOffNumbers);
					_cardNumberToWinCount.Add(counter, commonNumbers.Count());
					counter++;
				}

				file.Close();
			}
			else { Console.WriteLine($"no file found at {path}"); }
			
		}

		private void ProcessCardCopies()
		{
			//one copy of each card initially
			for (int i = 0; i < _cardNumberToWinCount.Count; i++)
			{
				_cardCounts.Add(i, 1);
			}
			foreach (int lineNumber in _cardNumberToWinCount.Keys)
			{
				for (int i = 0; i < _cardCounts[lineNumber]; i++)
				{
					for (int j = lineNumber + 1; j <= lineNumber + _cardNumberToWinCount[lineNumber]; j++)
					{
						_cardCounts[j]++;
					}
				}
			}

			foreach (int lineNumber in _cardCounts.Keys)
			{
				_totalNumberOfCards += _cardCounts[lineNumber];
			}
			Console.WriteLine($"totalNumberOfCards is {_totalNumberOfCards}");
		}


	}
}