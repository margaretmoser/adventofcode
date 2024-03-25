/*
 * https://adventofcode.com/2023/day/9
 * 
 * Extrapolate the last value in a series based on recursively finding the
 * difference between each element. Like this:
 *  1   3   6  10  15  21  _28
 *    2   3   4   5   6   _7
 *      1   1   1   1   _1
 *        0   0   0   _0
 *  where the _s are extrapolated and 28 is the final value.
 *
 * Learned about: some LINQ stuff though I didn't end up using much.
 *
 * Combining/calculating elements based on previous elements in collection:
 * https://stackoverflow.com/questions/4460106/how-do-access-previous-item-in-list-using-linq
 *
 * The fact that there is no version of ForEach in C# that provides the index as I am used
 * to from using libraries like UnderscoreJS.
 */

namespace Day9;
class Day9Main
{
	static void Main()
	{
		// Day9Problem1 problem1 = new Day9Problem1();
		// problem1.Run();
		Day9Problem2 problem2 = new Day9Problem2();
		problem2.Run();
	}
			
}