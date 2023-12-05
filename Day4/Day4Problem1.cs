using System.Text.RegularExpressions;
/*
 * https://adventofcode.com/2023/day/4
 * use c# list comparison to count winning numbers per line
 * get 2^n-1 points for number of winners each line
 * sum
 *
 * Learned about: fancier regexp with matching/non-matching groups, List.Intersect weirdness
 */

namespace Day4
{
	class Day4Problem1
	{
		static void MainOld()
		{
			
			int sumOfPointsForAlLCards = 0;
			Regex numbersPattern = new Regex(@"(?:.*\:\s+)(?:(\d+)+\s+)+(?:\|\s+)(?:(\d+)+(?:\s|\n)+)+",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);
			
			var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
			if (File.Exists(path))
			{
				using StreamReader file = new StreamReader(path);
				int counter = 0;
				while (file.ReadLine() is { } ln)
				{
					Console.WriteLine($"processing line {counter}: " + ln);
					ln = String.Concat(ln, " ");
					float pointsForThisCard = 0.5f;
					
					List<int> winningNumbers = new List<int>();
					List<int> scratchedOffNumbers = new List<int>();

					//Group 0 has the whole string, Group 1 has the winning numbers, Group 2 has the scratched numbers
					Console.Write("Winning numbers: ");
					foreach (Capture capture in numbersPattern.Match(ln).Groups[1].Captures)
					{
						Console.Write(capture.Value + ", ");
						winningNumbers.Add(Int32.Parse(capture.Value));
					}
					Console.WriteLine();
					
					Console.Write("Scratched off numbers: ");
					foreach (Capture capture in numbersPattern.Match(ln).Groups[2].Captures)
					{
						Console.Write(capture.Value + ", ");
						scratchedOffNumbers.Add(Int32.Parse(capture.Value));
					}
					Console.WriteLine();
					//TIL: this does not at all do what it looks like it does
					//List<int> commonNumbers = winningNumbers.Intersect(scratchedOffNumbers).ToList();
					IEnumerable<int> commonNumbers = winningNumbers.Intersect(scratchedOffNumbers);
					Console.Write("common numbers: ");
					foreach (int number in commonNumbers)
					{
						Console.Write(number + ", ");
						pointsForThisCard *= 2;
					}
					Console.WriteLine("points for this card: "+pointsForThisCard);
					if (pointsForThisCard > .6f)
					{
						sumOfPointsForAlLCards += (int) pointsForThisCard;
					}

					counter++;
				}
				Console.WriteLine($"Sum of points for all cards is {sumOfPointsForAlLCards}");

				file.Close();
			}
			else
			{
				Console.WriteLine($"no file found at {path}");
			}
		}
	}
}