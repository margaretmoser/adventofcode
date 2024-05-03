using System.Text.RegularExpressions;
namespace Day12;

public class Day12Problem1
{
	private const bool UseTestInput = true;
	
	private const char WorkingSpringChar = '.';
	private const char DamagedSpringChar = '#';


	public void Run()
	{
		LoadData();
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
				
				Console.WriteLine();
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}