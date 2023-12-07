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
		Console.WriteLine($"product of all races is {productOfAllRaces}");
	}

	int GetSolutionsForRace(int totalTime, int distance)
	{
		double firstRoot = QuadForm(-1, totalTime, -distance, true);
		double secondRoot = QuadForm(-1, totalTime, -distance, false);

		Console.WriteLine($"for time {totalTime} and distance {distance}, first root is {firstRoot} and second root is {secondRoot}");
		return (int)Math.Ceiling(secondRoot) - (int)Math.Ceiling(firstRoot);
	}
	static double QuadForm(int a, int b, int c, bool pos)
	{
		var preRoot = b * b - 4 * a * c;
		if (preRoot < 0)
		{
			return double.NaN;
		}
		else
		{
			var sgn = pos ? 1.0 : -1.0;
			return (sgn * Math.Sqrt(preRoot) - b) / (2.0 * a);
		}
	}
	
	void ParseRaces()
	{
		Regex numbersPattern = new Regex(@"(?:.*\:\s+)(?:(\d+)+\s*)+",
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
				counter++;
			}
			
			file.Close();
		}
		else
		{
			Console.WriteLine($"no file found at {path}");
		}
	}
	
}

