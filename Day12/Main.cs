/*
 * https://adventofcode.com/2023/day/12
 *
 * Count potential solutions to a block-placement puzzle.
 *
 * Initial solution is just going to brute-force it by finding all the permutations
 * of the target string and seeing how many match the source pattern. This is already
 * a little tricky. Perhaps there is a better way.
 *
 * Learned about:
 *
 * review basic constructor syntax
 * https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors
 *
 * Split string into array of ints
 * https://stackoverflow.com/questions/911717/split-string-convert-tolistint-in-one-line
 *
 * This kind of problem (finding different subsets k of a set n) is called k-combinations
 * https://www.baeldung.com/cs/generate-k-combinations
 *
 * It's important to test your input regex for things like "two-digit number at the end of the line"
 * h/t
 * https://www.reddit.com/r/adventofcode/comments/18gg5u3/2023_day_12_part_1_sigh/
 *
 * Learned a little about Rider memory profiling
 * https://www.jetbrains.com/help/rider/Profile_applications_memory.html#dm-run-a-profiling-session
 * 
 */

namespace Day12;
class Day12Main
{
	
	public const char WorkingSpringChar = '.';
	public const char DamagedSpringChar = '#';
	public const char UnknownSpringChar = '?';
	static void Main()
	{
		// Day12Problem1 problem1 = new Day12Problem1();
		// problem1.Run();
		Day12Problem2 problem2 = new Day12Problem2();
		problem2.Run();
	}
			
}