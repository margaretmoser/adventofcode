using System.Text.RegularExpressions;
namespace Day7;

public class Day7Problem1
{
	private Dictionary<string, string> hands;
	public void Run()
	{
			LoadHands();
		
	}

	void LoadHands()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt"); 
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
}