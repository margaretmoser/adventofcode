using System.Text.RegularExpressions;
/*
 * https://adventofcode.com/2023/day/4
 * use c# list comparison to count winning numbers per line
 * get 2^n-1 points for number of winners each line
 * sum
 */

namespace Day4
{
	class Day4Problem1
	{
		static void Main()
		{
			
			int sumOfPointsForAlLCards = 0;
			const string listSeparator = "|";
			Regex numbersPattern = new Regex(@"(?:.*\:\s)(?:(\d+)+\s+)+(?:\|\s)(?:(\d+)+\s+)+",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);
			
			var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
			if (File.Exists(path))
			{
				using StreamReader file = new StreamReader(path);
				int counter = 0;
				while (file.ReadLine() is { } ln && counter < 5)
				{
					Console.WriteLine($"processing line {counter}: " + ln);
					foreach (Group winningNumberMatchGroup in numbersPattern.Match(ln).Groups)
					{
						//Group 1 has the whole string, Group 2 has the winning numbers, Group 3 has the scratched numbers
						Console.WriteLine("Processing group");
						foreach (Capture capture in winningNumberMatchGroup.Captures)
						{
							Console.Write(capture.Value + ", ");
						}
						Console.WriteLine();
					}
					Console.WriteLine();



					string[] splitLine = ln.Split(listSeparator);
					List<string> winningNumbers = new List<string>(splitLine[0].Split(" "));
					List<string> scratchedOffNumbers = new List<string>(splitLine[1].Split(" "));
					

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