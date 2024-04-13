/*
 * https://adventofcode.com/2023/day/10
 * 
 * For Day 1, read in an undirected, unweighted graph with one cycle; find the cycle given its starting point;
 * find the farthest point included in the cycle and return the number of steps from
 * starting point to farthest point (half the length of the cycle). I thought this would require
 * actually constructing the graph, but found a naive solution that just required a 2d array and
 * tracking the direction of "movement" along the path. It could probably be optimized by checking
 * character pairs rather than the massive switch statement.
 *
 * In Day 2, find the number of tiles enclosed by the loop (cycle). A starting point, indirectly suggested
 * by the problem, might be a kind of simple convex-hull approach: find the cycle, then start from the edges
 * of the graph and count inwards to find the tiles *not* enclosed by the path; then the enclosed tiles would
 * just be the ones that weren't part of the loop or outside of it. But the loop can have "holes" like this:
 * L7L7																									F-JI
 * -JOL		<- hole that is outside the loop (O)					L7IF		<- hole that is inside the loop (I)
 * OOF-																									O|FJ
 * 
 * The next theory to try out is counting odd/even numbers of tiles that are between a given tile and
 * an edge of the map. Drawing this out has proven difficult.
 * 
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