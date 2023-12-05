using System.Text.RegularExpressions;

namespace Day5;

public class Day5Problem1
{
	private long nearestLocation;
	private List<long> seeds = new List<long>();
	private List<List<AlmanacMap>> listOfMaps = new List<List<AlmanacMap>>();
	public void Run()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt"); 
		if (File.Exists(path))
		{
			// Read file using StreamReader. Reads file line by line
			using StreamReader file = new StreamReader(path);
			int counter = 0;
	
			Regex seedsPattern = new Regex(@"(?:.*\:\s+)(?:(\d+)(?:\s))+",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);
			
			Regex startOfNewSectionPattern = new Regex(@"(.*)\smap\:",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);
			
			Regex rangeMapPattern = new Regex(@"(\d+)");

			string? seedLine = file.ReadLine();
			foreach (Capture seed in seedsPattern.Match(seedLine).Groups[1].Captures)
			{
				//Console.WriteLine($"capture: {seed.Value}");
				seeds.Add(long.Parse(seed.Value));
			}

			List<AlmanacMap> thisMap = new List<AlmanacMap>();
			while (file.ReadLine() is { } ln)
			{
				Console.WriteLine("processing line "+counter+" with value "+ln);
				if (startOfNewSectionPattern.IsMatch(ln))
				{
					Console.WriteLine("********" + startOfNewSectionPattern.Match(ln).Groups[1].Value); 
					thisMap = new List<AlmanacMap>();
					listOfMaps.Add(thisMap);
				} else if (rangeMapPattern.IsMatch(ln)) {
					//line contains a range map
					MatchCollection matches = rangeMapPattern.Matches(ln);
					AlmanacMap newRangeMap = new AlmanacMap(
						long.Parse(matches[1].Value), long.Parse(matches[1].Value) + long.Parse(matches[2].Value),
						long.Parse(matches[2].Value), long.Parse(matches[0].Value));
					thisMap.Add(newRangeMap);
				}
				counter++;
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }

	}

	struct AlmanacMap
	{
		public AlmanacMap(long sMin, long sMax, long offs, long dMin)
		{
			srcMin = sMin;
			srcMax = sMax;
			offset = offs;
			destMin = dMin;
		}
		public long srcMin;
		public long srcMax;
		public long offset;
		public long destMin;
	}
	
}