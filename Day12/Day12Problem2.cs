using System.Text.RegularExpressions;
namespace Day12;

public class Day12Problem2
{
	private readonly bool _useTestInput = true;
	private readonly List<SpringRecord> _records = new List<SpringRecord>();

	public void Run()
	{
		LoadData();
		CountValidCombinations();
	}

	void CountValidCombinations()
	{
		int totalCombinations = 0;
		foreach (SpringRecord record in _records)
		{
			totalCombinations += record.SolveRecord();
			Console.WriteLine("running total: "+totalCombinations);
		}
		Console.WriteLine("Total valid combinations for all records: "+totalCombinations.ToString());
	}

	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), _useTestInput? "input_test.txt":"input.txt");
		Regex springsPattern = new Regex(@"([\.\?\#]+)\W((?:\d+\,)*\d+)",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			while (file.ReadLine() is { } ln)
			{
				GroupCollection gc = springsPattern.Match(ln).Groups;
				string springs = gc[1].Value;
				string blockPattern = gc[2].Value;
				_records.Add(new SpringRecord(springs, blockPattern));
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
}