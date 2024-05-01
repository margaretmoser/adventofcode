/*
 * https://adventofcode.com/2023/day/11
 *
 *
 *
 *
 * Learned about:
 *
 * Duplicate every element in a List using LINQ
 * https://stackoverflow.com/questions/18080060/create-duplicate-items-in-a-list
 *
 */

namespace Day11;

public enum Direction
{
	Up, Right, Down, Left
}

class Day11Main
{
	static void Main()
	{
		Day11Problem1 problem1 = new Day11Problem1();
		problem1.Run();
		// Day11Problem2 problem2 = new Day11Problem2();
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