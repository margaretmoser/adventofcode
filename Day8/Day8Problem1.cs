using System.Text.RegularExpressions;
namespace Day8;

public class Day8Problem1
{
	private string? LRInstructionsLine;
	private List<Tuple<string, string, string>> nodes;
	public void Run()
	{
		nodes = new List<Tuple<string, string, string>>();
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
			LRInstructionsLine = file.ReadLine();
			Console.WriteLine("instructions: "+LRInstructionsLine);
			file.ReadLine();
			while (file.ReadLine() is { } ln)
			{
				GroupCollection g = nodePattern.Match(ln).Groups;
				Tuple<string, string, string> newNode =
					new Tuple<string, string, string>(g[1].Value, g[2].Value, g[3].Value);
				Console.WriteLine("processed node "+newNode.ToString());

			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}