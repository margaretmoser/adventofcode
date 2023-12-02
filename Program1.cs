using System.Text.RegularExpressions;


class Program1 {
	Dictionary<string, Color> colorLookup = new Dictionary<string, Color>
	{
		{ "red", Color.Red },
		{ "green", Color.Green },
		{ "blue", Color.Blue }
	};

	Dictionary<Color, int> constraint = new Dictionary<Color, int>
	{
		{ Color.Red, 12 },
		{ Color.Green, 13 },
		{ Color.Blue, 14 }
	};

	void Main()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		if (File.Exists(path))
		{
			// Read file using StreamReader. Reads file line by line
			using StreamReader file = new StreamReader(path);
			//int counter = 0;
			bool isPossible = true;
			int sumOfPossibleGameIds = 0;

			Regex gameIdPattern = new Regex(@"^Game (\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			while (file.ReadLine() is { } ln)
			{

				var gameId = Int32.Parse(gameIdPattern.Match(ln).Groups[1].Captures[0].Value);
				Console.WriteLine($"processing {gameId}: " + ln);

				//for each color (red, green, blue)
				foreach (var colorString in colorLookup.Keys)
				{
					var cubePattern = new Regex(@"(\d+) " + colorString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
					//get the matches in this line
					foreach (Match match in cubePattern.Matches(ln))
					{
						//Console.WriteLine("matched "+match.Value);
						int matchedNumber = Int32.Parse(match.Groups[1].Captures[0].Value);
						//if this match makes the game impossible then break
						isPossible = matchedNumber <= constraint[colorLookup[colorString]];

						if (!isPossible) break;
					}

					//if this color makes the game impossible then break
					if (!isPossible)
					{
						Console.WriteLine($"Color " + colorString +
						                  $" makes game id {gameId} impossible, sum is now {sumOfPossibleGameIds}");
						break;
					}

				}

				//after all the colors are processed for this line
				if (isPossible)
				{
					sumOfPossibleGameIds += gameId;
					Console.WriteLine($"Game id {gameId} is possible, sum is now {sumOfPossibleGameIds}");
				}

				//counter++;

			}

			file.Close();
		}
		else
		{
			Console.WriteLine($"no file found at {path}");
		}
	}
}