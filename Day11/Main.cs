/*
 * https://adventofcode.com/2023/day/11
 *
 * Expand a sparse matrix by inserting rows and columns, place points in their new
 * relative positions. Then calculate the shortest path between each point using
 * 4-direction movement and sum the paths between all pairs.
 *
 * Learned about:
 *
 * Duplicate every element in a List using LINQ (though I didn't need it)
 * https://stackoverflow.com/questions/18080060/create-duplicate-items-in-a-list
 *
 * Practiced using a Queue in a very simple way
 * https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-8.0
 *
 * Using TakeWhile to find where to insert an element into an already-sorted collection. This
 * could also have been done with SortedSet probably
 * https://stackoverflow.com/questions/12172162/how-to-insert-item-into-list-in-order
 *
 * Use the right data type aaaarrrgh
 * https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
 *
 */

namespace Day11;

class Day11Main
{
	static void Main()
	{
		// Day11Problem1 problem1 = new Day11Problem1();
		// problem1.Run();
		Day11Problem2 problem2 = new Day11Problem2();
		problem2.Run();
	}



	public struct Coords
	{
		public Coords(int x, int y)
		{
			X = x;
			Y = y;
		}
		public int X { get; set;  }
		public int Y { get; set;  }
		// public override string ToString() => $"({X}, {Y})";
		public override string ToString()
		{
			string result = $"({X}, {Y})";
			return result;
		}
	}
	

			
}