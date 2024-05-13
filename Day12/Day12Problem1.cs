using System.Text.RegularExpressions;
namespace Day12;

public class Day12Problem1
{
	public const bool UseTestInput = false;
	public const bool printPatterns = false;
	
	private const bool UseLineRange = false;
	private const int startingLine = 900;
	private const int maxLines = 100;
	private List<SpringRecord> _records = new List<SpringRecord>();
	private int _maxBlockSize = 0;
	private readonly Dictionary<int, string[]> _potentialPatterns = new Dictionary<int, string[]>();
	//List of characters to combine to find valid combinations
	private readonly List<char> _validChars = new List<char>() { Day12Main.UnknownSpringChar, Day12Main.DamagedSpringChar };
	//List of combinations generated
	private List<string> _combinations = new List<string>();

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
		//7273 is too high
	}

	void GeneratePatterns()
	{
		for (int i = 0; i < 15; i++)
		{
			string[] thePatterns = new string[i];
			_combinations = new List<string>();
			string starterString = "".PadLeft(i+1, Day12Main.UnknownSpringChar);
			// 1 = #,?
			// 2 = ##, #?, ?#, ??
			// 3 = ###, #?#, #??, ##?, ?##, ??#, ?#?, ???

		}
	}
	
	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), UseTestInput? "input_test.txt":"input.txt");
		Regex springsPattern = new Regex(@"([\.\?\#]+)\W((?:\d+\,)*\d+)",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		if (File.Exists(path))
		{
			int linesRead = 0;
			using StreamReader file = new StreamReader(path);
			while (file.ReadLine() is { } ln && (!UseLineRange || linesRead < maxLines))
			{
				if (!UseLineRange || linesRead >= startingLine)
				{
					GroupCollection gc = springsPattern.Match(ln).Groups;
					string springs = gc[1].Value;
					string blockPattern = gc[2].Value;
					Console.WriteLine("sending block pattern to SpringRecord: "+blockPattern);
					_records.Add(new SpringRecord(springs, blockPattern));
				}

				linesRead++;
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}

	
}