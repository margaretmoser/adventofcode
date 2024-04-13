/*
 * https://adventofcode.com/2023/day/10
 * 
 * Read in an undirected, unweighted graph with one cycle; find the cycle given its starting point;
 * find the farthest point included in the cycle and return the number of steps from
 * starting point to farthest point (half the length of the cycle).
 *
 * 
 * Learned about:
 *      (reminder: comma and plus do different things in Console Write/WriteLine)
 *			https://stackoverflow.com/questions/49334797/what-is-the-difference-between-comma-and-plus-in-string-arrays
 */

namespace Day10;

public enum Direction
{
	Up, Right, Down, Left
}
class Day10Main
{
	static void Main()
	{
		Day10Problem1 problem1 = new Day10Problem1();
		problem1.Run();
		// Day10Problem2 problem2 = new Day10Problem2();
		// problem2.Run();
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