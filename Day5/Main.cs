/*
 * https://adventofcode.com/2023/day/5
 *
 * Wow, a lot of words to describe a range-mapping problem. That said it's not obvious what an elegant answer
 * would be. Maybe Actions?
 *
 * Take an input seed number, send it through several maps to get a location number. Find the
 * nearest location where the initial seeds could be planted.
 *
 * Learned about: lazy evaluation of MatchCollection in C#; not all matches are pulled until you access the Count
 * property so you can't directly pull something using an index (matches[i]) even if you know it's there
 */

namespace Day5
{
	class Day5Main
	{
		static void Main()
		{
			Day5Problem1 problem1 = new Day5Problem1();
			problem1.Run();
		}
			
	}
}