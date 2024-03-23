using System.Text.RegularExpressions;
namespace Day9;

public class Day9Problem1
{
	private List<List<long>> allDataSeries;
	
	public void Run()
	{
		allDataSeries = new List<List<long>>();
		LoadData();
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