using System.Text.RegularExpressions;
/*
 * https://adventofcode.com/2023/day/6
 * Rate optimization problem. Get x seconds to either pause and build acceleration or actually move; find
 * how many different strategies (i.e. not just minimum) for pause/go would win each race, for four races
 *
 * Learned about: 
 */
namespace Day6;

public class Day6Problem1
{
	private Dictionary<int, int> races;
	private int productOfAllRaces = 1;
	public void Run()
	{
		races = new Dictionary<int, int>();
		ParseRaces();
		foreach (int time in races.Keys)
		{
			int thisRaceSolutions = GetSolutionsForRace(time, races[time]);
			productOfAllRaces *= thisRaceSolutions;
		}
	}

	int GetSolutionsForRace(int time, int distance)
	{
		return 0;
	}
	
	void ParseRaces()
	{
		Regex numbersPattern = new Regex(@"(?:.*\:\s+)(?:(\d+)+\s+)+",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			string? timesLine = file.ReadLine();
			string? distanceLine = file.ReadLine();
			int counter = 0;
			CaptureCollection times = numbersPattern.Match(timesLine).Groups[1].Captures;
			CaptureCollection distances = numbersPattern.Match(distanceLine).Groups[1].Captures;
			foreach (Capture time in times)
			{
				races.Add(Int32.Parse(time.Value), Int32.Parse(distances[counter].Value));
				Console.WriteLine($"added time {time} and distance {distances[counter].Value}");
			}
			
			file.Close();
		}
		else
		{
			Console.WriteLine($"no file found at {path}");
		}
	}
	
}

