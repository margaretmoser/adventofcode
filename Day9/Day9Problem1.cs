using System.Text.RegularExpressions;
namespace Day9;

public class Day9Problem1
{
	
	public void Run()
	{
		LoadData();
	}

	
	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		Regex nodePattern = new Regex(@"(\w*)\W\=\W\((\w*)\,\W(\w*)\)",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			file.ReadLine();
			while (file.ReadLine() is { } ln)
			{
				GroupCollection g = nodePattern.Match(ln).Groups;
				//Console.WriteLine("processed node "+newNode.ToString());
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}