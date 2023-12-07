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
			using StreamReader file = new StreamReader(path);
			LoadSeeds(file);
			LoadMaps(file);
			MapSeeds();
			
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }

	}

	void MapSeeds()
	{
		//for (int indexInSeedList = 0; indexInSeedList < seeds.Count; indexInSeedList += 2)
		for (int indexInSeedList = 0; indexInSeedList < 2; indexInSeedList += 2)
		{
			long firstSeed = seeds[indexInSeedList];
			long secondSeed = seeds[indexInSeedList+1];
			Console.WriteLine($"firstSeed is {firstSeed}; secondSeed is {secondSeed}");

			for (long currentSeed = firstSeed; currentSeed <= firstSeed+secondSeed; currentSeed++)
			{
				long currentSrcValue = currentSeed;
				Console.WriteLine($"currentSrcValue is {currentSrcValue}");
				foreach (List<AlmanacMap> thisMapList in listOfMaps)
				{
					foreach (AlmanacMap map in thisMapList)
					{
						if (currentSrcValue >= map.srcMin && currentSrcValue < map.srcMin+map.rangeSize)
						{
							long newValue = (currentSrcValue - map.srcMin) + map.destMin;
							//Console.WriteLine($"found mapping, changing lookup value from {currentSrcValue} to {newValue}");
							currentSrcValue = newValue;
							// break;
						}
					}
				}
				//Console.WriteLine($"final value after lookups is {currentSrcValue}");
				if (nearestLocation == 0 || currentSrcValue < nearestLocation)
				{
					nearestLocation = currentSrcValue;
				}
				Console.WriteLine($"nearest location is now {nearestLocation}");
			}

			
			
		}
		Console.WriteLine($"nearest location is {nearestLocation}");
	}

	void LoadSeeds(StreamReader file)
	{
		Regex seedsPattern = new Regex(@"(?:.*\:\s+)(?:(\d+)(?:\s))+",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		string? seedLine = file.ReadLine();
		foreach (Capture seed in seedsPattern.Match(seedLine).Groups[1].Captures)
		{
			seeds.Add(long.Parse(seed.Value));
		}
	}

	void LoadMaps(StreamReader file)
	{
		Regex startOfNewSectionPattern = new Regex(@"(.*)\smap\:",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		Regex rangeMapPattern = new Regex(@"(\d+)");
		int counter = 2;
		List<AlmanacMap> thisMap = new List<AlmanacMap>();
		string nameOfMap = "";
		
		while (file.ReadLine() is { } ln)
		{
			if (startOfNewSectionPattern.IsMatch(ln))
			{
				nameOfMap = startOfNewSectionPattern.Match(ln).Groups[1].Value;
				Console.WriteLine($"******** {nameOfMap}"); 
				thisMap = new List<AlmanacMap>();
				listOfMaps.Add(thisMap);
			} else if (rangeMapPattern.IsMatch(ln)) {
				MatchCollection matches = rangeMapPattern.Matches(ln);
				AlmanacMap newRangeMap = new AlmanacMap(
					nameOfMap,
					long.Parse(matches[0].Value),
					long.Parse(matches[1].Value), 
					long.Parse(matches[2].Value)) ;
				thisMap.Add(newRangeMap);
			}
			counter++;
		}
	}

	struct AlmanacMap
	{
		public AlmanacMap(string nme, long dMin, long sMin, long sz)
		{
			name = nme;
			destMin = dMin;
			srcMin = sMin;
			rangeSize = sz;
		}

		public string name;
		public long destMin;
		public long srcMin;
		public long rangeSize;

		public override string ToString() =>
			$"name: {name}, destMin: {destMin}, srcMin: {srcMin}, offset: {rangeSize}";
	}
	
}