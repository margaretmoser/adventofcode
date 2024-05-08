using System.Text.RegularExpressions;
namespace Day12;

public class Day12Problem1
{
	private const bool UseTestInput = true;
	private List<SpringRecord> _records = new List<SpringRecord>();
	private int _maxBlockSize = 0;
	private readonly Dictionary<int, string[]> _potentialPatterns = new Dictionary<int, string[]>();
	//List of characters to permute to find valid combinations
	private readonly List<char> _validChars = new List<char>() { Day12Main.UnknownSpringChar, Day12Main.DamagedSpringChar };
	//List of combinations generated
	private List<string> _permutations = new List<string>();

	public void Run()
	{
		LoadData();
		//GeneratePatterns();
	}

	void GeneratePatterns()
	{
		for (int i = 0; i < 15; i++)
		{
			string[] thePatterns = new string[i];
			_permutations = new List<string>();
			string starterString = "".PadLeft(i+1, Day12Main.UnknownSpringChar);
			// 1 = #,?
			// 2 = ##, #?, ?#, ??
			// 3 = ###, #?#, #??, ##?, ?##, ??#, ?#?, ???

		}
	}
	
	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), UseTestInput? "input_test.txt":"input.txt");
		Regex springsPattern = new Regex(@"([\.\?\#]+)\W((?:\d+\,)*\d)",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			while (file.ReadLine() is { } ln)
			{
				GroupCollection gc = springsPattern.Match(ln).Groups;

				string springs = gc[1].Value;
				Console.Write("springs: "+springs+", ");
				
				string blockPattern = gc[2].Value;
				Console.WriteLine("block pattern: "+blockPattern);

				_records.Add(new SpringRecord(springs, blockPattern));
				
				Console.WriteLine();
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}



	
	//from https://stackoverflow.com/a/8844897
/*
	List<string> GenerateCombos(string mask, string combination)
	{
		
		if (mask.Length <= 0)
		{
			//No more chars left in the mask, add this combination to the solution list.
			_permutations.Add(combination);
			return _permutations;
		}
		
		_validChars.ForEach(c => GenerateCombos(mask.Substring(1), combination + c));
		


	}
	*/

	
}