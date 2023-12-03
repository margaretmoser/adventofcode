using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

/*Part 2: find the minimum cubes needed for a particular game
 https://adventofcode.com/2023/day/2#part2
*/
class Program2
{
	static Dictionary<string, Color> colorLookup = new Dictionary<string, Color>
	{
		{ "red", Color.Red },
		{ "green", Color.Green },
		{ "blue", Color.Blue }
	};


	static void Main()

	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		if (File.Exists(path))
		{
			// Read file using StreamReader. Reads file line by line
			using StreamReader file = new StreamReader(path);
			int counter = 0;
			int sumOfPowers = 0;

			Regex gameIdPattern = new Regex(@"^Game (\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			while (file.ReadLine() is { } ln)
			{
				Dictionary<Color, int> minSpecForThisGame = new Dictionary<Color, int>();

				var gameId = Int32.Parse(gameIdPattern.Match(ln).Groups[1].Captures[0].Value);
				Console.WriteLine($"processing {gameId}: " + ln);

				//for each color (red, green, blue)
				foreach (var colorString in colorLookup.Keys)
				{
					var cubePattern = new Regex(@"(\d+) " + colorString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
					//get the matches in this line
					int numberOfThisColorNeeded = 0;
					foreach (Match match in cubePattern.Matches(ln))
					{
						//Console.WriteLine("matched "+match.Value);
						int matchedNumber = Int32.Parse(match.Groups[1].Captures[0].Value);
						numberOfThisColorNeeded = Math.Max(numberOfThisColorNeeded, matchedNumber);

					}
					Console.WriteLine(colorString+" cubes needed for this game: "+numberOfThisColorNeeded);
					minSpecForThisGame.Add(colorLookup[colorString], numberOfThisColorNeeded);

				}
				
				//find the "power" for this game, which is the product of all three colors' minimum needed, r*g*b
				int powerOfThisGame = 1;
				foreach (Color color in minSpecForThisGame.Keys)
				{
					powerOfThisGame *= minSpecForThisGame[color];
				}
				Console.WriteLine($"Power of this game is {powerOfThisGame}");
				sumOfPowers += powerOfThisGame;

				counter++;

			}

			file.Close();
			Console.WriteLine($"Sum of powers is {sumOfPowers}");
		}
		else
		{
			Console.WriteLine($"no file found at {path}");
		}
	}
}

enum Color
{
	Red, Green, Blue
}