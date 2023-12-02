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
	int gameNumber = -1;
	bool isPossible = false;
	int sumOfPossibleGameIds;
	
	Regex redCubePattern = new Regex(@"(\d+) red", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	Regex greenCubePattern = new Regex(@"(\d+) green", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	Regex blueCubePattern = new Regex(@"(\d+) blue", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	
	while (file.ReadLine() is { } ln && counter < 10)
	{
		Console.WriteLine("processing line " + counter + " with value " + ln);
		Console.Write("RED: ");
		foreach (Match match in redCubePattern.Matches(ln))
		{
			Console.Write("found " + match.Groups[1].Captures[0] + " at index "+match.Index+", ");
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