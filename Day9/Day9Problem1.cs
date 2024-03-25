using System.Text.RegularExpressions;
namespace Day9;

public class Day9Problem1
{
	private List<List<long>> allDataSeries;
	private long sumOfExtrapolatedValues;

	//I know there is a better way
	private List<List<long>> derivedLists;
	
	public void Run()
	{
		allDataSeries = new List<List<long>>();
		
		LoadData();
		ProcessDataSeries();
	}

	void ProcessDataSeries()
	{
		long thisExtrapolatedValue = -1;
		int lineNumber = 1;
		foreach (List<long> dataSeries in allDataSeries)
		{
			thisExtrapolatedValue = GetExtrapolatedValue(dataSeries);
			sumOfExtrapolatedValues += thisExtrapolatedValue;
			Console.WriteLine("for series "+lineNumber+", extrapolated value is "+thisExtrapolatedValue);
			lineNumber++;
		}
		
		Console.WriteLine("sum of extrapolated values is "+sumOfExtrapolatedValues);
	}
	
	long GetExtrapolatedValue(List<long> thisDataSeries)
	{
		bool reachedZero = false;
		derivedLists = new List<List<long>>();
		derivedLists.Add(thisDataSeries);
		List<long> thisList;
		int index = 0;
		while (!reachedZero)
		{
			thisList = derivedLists[index]
				.Skip(1)
				.Zip(derivedLists[index], (current, prev) => current - prev)
				.ToList();
			derivedLists.Add(thisList);
			reachedZero = thisList.Last() == 0;
			index++;
		}
		derivedLists.Reverse();
		derivedLists[0].Add(0);
		long sumOfLasts = 0;
		for (int i = 0; i < derivedLists.Count; i++)
		{
			// derivedLists[i].ForEach( x => Console.Write(x+" "));
			// Console.WriteLine();
			sumOfLasts += derivedLists[i].Last();
		}
		
		return sumOfLasts;
	}


	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		Regex dataSeriesPattern = new Regex(@"(-?\d*)",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			while (file.ReadLine() is { } ln)
			{
				List<long> thisDataSeries = new List<long>();
				string[] numbersAsStrings = ln.Split(" ");
				foreach (string s in numbersAsStrings)
				{
					Console.Write(s+", ");
					thisDataSeries.Add(Int32.Parse(s));
				}
				Console.WriteLine();
				allDataSeries.Add(thisDataSeries);
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}