using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

List<int> collatedDigitsFromEachLine = new List<int>();

String[] digitNamesArray = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
List<String> digitNames = new List<String>(digitNamesArray);

Dictionary<string, string> namedDigits = new Dictionary<string, string>();
for (int i = 0; i < 9; i++)
{
	namedDigits.Add(digitNames[i], (i+1).ToString());
}


var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt"); 
if (File.Exists(path))
{
	// Read file using StreamReader. Reads file line by line
	using StreamReader file = new StreamReader(path);
	int counter = 0;
	
	Regex firstDigitcatchAllPattern = new Regex(@"\d|one|two|three|four|five|six|seven|eight|nine",
		RegexOptions.Compiled | RegexOptions.IgnoreCase);

	Regex lastDigitcatchAllPattern = new Regex(@"\d|one|two|three|four|five|six|seven|eight|nine",
		RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
	
	Regex digitPattern = new Regex(@"\d+");
	
	while (file.ReadLine() is { } ln)
	{
		Console.WriteLine("processing line "+counter+" with value "+ln);

		string firstDigitMatch = firstDigitcatchAllPattern.Match(ln).Value;
		if (!digitPattern.IsMatch(firstDigitMatch))
		{
			firstDigitMatch = namedDigits[firstDigitMatch];
		}
		string lastDigitMatch = lastDigitcatchAllPattern.Match(ln).Value;
		if (!digitPattern.IsMatch(lastDigitMatch))
		{
			lastDigitMatch = namedDigits[lastDigitMatch];
		}
		string combinedValue = String.Concat(firstDigitMatch, lastDigitMatch);
		Console.WriteLine("Combined two-digit value is "+combinedValue);

		collatedDigitsFromEachLine.Add(Int32.Parse(combinedValue));

		counter++;
	}
	file.Close();

	int total = collatedDigitsFromEachLine.Sum(x => Convert.ToInt32(x));
	Console.WriteLine("Total in file is "+total);
	
}
else
{
	Console.WriteLine($"no file found at {path}");
}




/*
 The old way, which did not correctly match things like "threeight"
 
//create a list of all matches, converting strings into strings of digits along the way
		List<string> processedMatches = new List<string>();
		
		MatchCollection matches = catchAllPattern.Matches(ln);
		foreach (Match match in matches)
		{
			Console.Write("{0} found at index {1}, ", match.Value, match.Index);
			if (digitPattern.IsMatch(match.Value))
			{
				processedMatches.Add((match.Value));
			}
			else
			{
				processedMatches.Add(namedDigits[match.Value].ToString());
			}
		}
		//Console.Write("processed matches: ");
		// foreach (int processedNumber in processedMatches)
		// {
		// 	Console.Write(processedNumber+", ");
		// }
		
		
		string firstDigitMatch = processedMatches[0].Substring(0, 1);
		Console.Write("first digit match is "+firstDigitMatch+", ");
		

		string lastDigitMatch = processedMatches.Last().Substring(processedMatches.Last().Length - 1, 1);
		Console.WriteLine("last digit match is "+lastDigitMatch);
		*/