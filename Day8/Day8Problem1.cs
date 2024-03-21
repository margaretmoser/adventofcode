using System.Text.RegularExpressions;
namespace Day8;

public class Day8Problem1
{
	public void Run()
	{
		
	}
	
	void LoadDataAndAssignHandType()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		Regex handAndBidPattern = new Regex(@"(.+)(?:\s)(\d+)",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			int counter = 0;
			while (file.ReadLine() is { } ln && counter < 10000)
			{
				GroupCollection g = handAndBidPattern.Match(ln).Groups;
				// HandBidTypeRank newHand = new HandBidTypeRank(g[1].Value, Int32.Parse(g[2].Value));
				// newHand.type = GetHandType(newHand.hand);
				// processedHands.Add(newHand);
				//Console.WriteLine($"added {g[1].Value}, {g[2].Value} to dictionary; ");
				counter++;
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}