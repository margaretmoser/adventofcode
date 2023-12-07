using System.Text.RegularExpressions;
/*
 * https://adventofcode.com/2023/day/6
 * Rate optimization problem. Get x seconds to either pause and build acceleration or actually move; find
 * how many different strategies (i.e. not just minimum) for pause/go would win each race, for four races
 *
 * Learned about: concatenating strings is more annoying than I remembered. Used += instead of Join or Concat.
 */
namespace Day6;

public class Day6Problem2
{
	private Dictionary<long, long> races;
	private long productOfAllRaces = 1;
	public void Run()
	{
		races = new Dictionary<long, long>();
		ParseRaces();
		foreach (int time in races.Keys)
		{
			long thisRaceSolutions = GetSolutionsForRace(time, races[time]);
			productOfAllRaces *= thisRaceSolutions;
		}
		Console.WriteLine($"product of all races is {productOfAllRaces}");
	}

	long GetSolutionsForRace(long totalTime, long distance)
	{
		double firstRoot = QuadForm(-1, totalTime, -distance, true);
		double secondRoot = QuadForm(-1, totalTime, -distance, false);

		Console.WriteLine($"for time {totalTime} and distance {distance}, first root is {firstRoot} and second root is {secondRoot}");
		return (int)Math.Ceiling(secondRoot) - (int)Math.Ceiling(firstRoot);
	}
	static double QuadForm(long a, long b, long c, bool pos)
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
			string processedTime = "";
			string processedDistance = "";
			foreach (Capture time in times)
			{
				processedTime += time.Value;
			}
			foreach (Capture distance in distances)
			{
				processedDistance += distance.Value;
			}
			races.Add(long.Parse(processedTime), long.Parse(processedDistance));
			Console.WriteLine($"added time {processedTime} and distance {processedDistance}");
			
			file.Close();
		}
		else
		{
			Console.WriteLine($"no file found at {path}");
		}
	}
	
}

