using System.Text.RegularExpressions;
/*
 * https://adventofcode.com/2023/day/4
 * use c# list comparison to count winning numbers per line
 * for each winning number, add a copy of the next card after this one, continue until no winning numbers
 * return total number of cards.
 *
 * This looked like it needed recursion, but it actually just needed a table of values.
 *
 * Learned about: SortedDictionary, creating/running non-static classes in a basic console app
 */

namespace Day4
{
	class Day4Problem2
	{
		static void Main()
		{
			Day4Problem2CardProcessor cardProcessor = new Day4Problem2CardProcessor();
			cardProcessor.Run();
		}
			
	}
}