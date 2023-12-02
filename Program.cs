using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

Dictionary<string, Color> colorLookup = new Dictionary<string, Color>();
colorLookup.Add("red", Color.Red);
colorLookup.Add("green", Color.Green);
colorLookup.Add("blue", Color.Blue);

Dictionary<Color, int> constraint = new Dictionary<Color, int>();
constraint.Add(Color.Red, 12);
constraint.Add(Color.Green, 13);
constraint.Add(Color.Blue, 14);

var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt"); 
if (File.Exists(path))
{
	// Read file using StreamReader. Reads file line by line
	using StreamReader file = new StreamReader(path);
	int counter = 0;
	int gameId = -1;
	bool isPossible = true;
	int sumOfPossibleGameIds = 0;
	
	Regex gameIdPattern = new Regex(@"^Game (\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	Regex redCubePattern = new Regex(@"(\d+) red", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	Regex greenCubePattern = new Regex(@"(\d+) green", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	Regex blueCubePattern = new Regex(@"(\d+) blue", RegexOptions.Compiled | RegexOptions.IgnoreCase);

	Regex[] cubePatterns = { redCubePattern, greenCubePattern, blueCubePattern };
	
	while (file.ReadLine() is { } ln && counter < 10)
	{
		
		gameId = Int32.Parse(gameIdPattern.Match(ln).Groups[1].Captures[0].Value);
		Console.WriteLine($"processing {gameId}: " + ln);
		
		//This loop logic is new and almost certainly wrong but I am going to bed
		foreach (Regex cubePattern in cubePatterns)
		{
			foreach (Match match in cubePattern.Matches(ln))
			{
				int matchedNumber = Int32.Parse(match.Groups[1].Captures[0].Value);
				isPossible = matchedNumber <= constraint[Color.Red];
				if (!isPossible) break;
			}

			Console.WriteLine();
			if (isPossible)
			{
				sumOfPossibleGameIds += gameId;
			}
			else
			{
				Console.WriteLine($"GAME WITH ID {gameId} IS NOT POSSIBLE");
				isPossible = true;
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


enum Color
{
	Red, Green, Blue
}