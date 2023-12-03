using System.Text.RegularExpressions;

namespace Day3
{
	class Day3Problem1
	{
		static void Main()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
			if (File.Exists(path))
			{
				// Read file using StreamReader. Reads file line by line
				using StreamReader file = new StreamReader(path);
				int counter = 0;

				Regex gameIdPattern = new Regex(@"^Game (\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
				while (file.ReadLine() is { } ln && counter < 10)
				{
					Console.WriteLine($"processing line: " + ln);

					counter++;
				}

				file.Close();
			}
			else
			{
				Console.WriteLine($"no file found at {path}");
			}
		}
	}
}