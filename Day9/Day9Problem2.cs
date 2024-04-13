using System.Text.RegularExpressions;
namespace Day9;

public class Day9Problem2
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
		derivedLists[0].Insert(0,0);
		long extrapolatedValue = 0;
		for (int i = 1; i < derivedLists.Count; i++)
		{
			thisList = derivedLists[i]; 
			thisList.Insert(0, thisList[0] - derivedLists[i - 1][0]);
			extrapolatedValue = thisList.First();
		}
		
		return extrapolatedValue;
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