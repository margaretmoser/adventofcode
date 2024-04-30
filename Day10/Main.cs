/*
 * https://adventofcode.com/2023/day/10
 * 
 * For Day 1, read in an undirected, unweighted graph with one cycle; find the cycle given its starting point;
 * find the farthest point included in the cycle and return the number of steps from
 * starting point to farthest point (half the length of the cycle). I thought this would require
 * actually constructing the graph, but found a naive solution that just required a 2d array and
 * tracking the direction of "movement" along the path.
 *
 * In Day 2, find the number of tiles enclosed by the loop (cycle). I attempted a solution based on flood fill
 * from the outside, but this didn't account for internal holes. Then I searched a lot and eventually discovered
 * that this problem is the same thing as drawing a lasso around pixels in Photoshop/other raster graphics software.
 * Found an approach to that, with pictures, credited below. But I had something wrong. The "real" solution,
 * probably intended by the author, is to use Pick's theorem/shoelace formula. But I went with a much slower
 * but (for me) more intuitive approach: expand the grid so that it's easy to detect corners, then flood-fill
 * from the outside, then traverse the expanded grid looking for empty cells. It is very far from efficient
 * or elegant, but it does work and I learned a lot of useful C# things doing it that way.
 * 
 * 
 * Learned about:
 *      (reminder: comma and plus do different things in Console Write/WriteLine)
 *			https://stackoverflow.com/questions/49334797/what-is-the-difference-between-comma-and-plus-in-string-arrays
 * 
 *			traverse a [,] style 2d array
 *			https://stackoverflow.com/questions/8184306/iterate-through-2-dimensional-array-c-sharp
 *
 *			compare multidimensional arrays by casting them to enumerables and using SequenceEqual
 *			https://stackoverflow.com/questions/12446770/how-to-compare-multidimensional-arrays-in-c
 * 
 *			use File.ReadAllLines to get length (no link)
 *
 *			C# switch can handle structs
 *			https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression
 *
 *			C# expression lambda to map tuple to output
 *			https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#expression-lambdas
 *
 *			didn't use, but two-pass connected-component labeling exists:
 *			https://stackoverflow.com/questions/4025980/count-the-number-of-holes-in-a-bitmap?rq=3
 * 
 *			pass an enum variable (or anything else) by reference (though I factored this out)
 *			https://bytes.com/topic/c-sharp/answers/253344-passing-enumeration-variable-ref
 * 
 *			raster/pixel/lasso-type approach with scanlines (though I ended up using flood fill)
 *			https://gamedev.stackexchange.com/questions/141460/how-can-i-fill-the-interior-of-a-closed-loop-on-a-tile-map
 * 
 *			basic four-direction flood fill using a queue
 *			https://www.geeksforgeeks.org/flood-fill-algorithm/
 * 
 *			local functons in C#
 *			https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions
 *
 *			DotNetFiddle exists
 *			https://dotnetfiddle.net/OnYtq7
 *
 *			Some other home-rolled approaches which are pretty neat
 *			https://www.reddit.com/r/adventofcode/comments/18fuqdd/2023_day_10_part_2_hint/
 *
 *			Useful description of Pick's theorem and shoelace formula for calculating area of a polygon within a grid
 *			(though I didn't use this)
 *			https://www.reddit.com/r/adventofcode/comments/18f1sgh/2023_day_10_part_2_advise_on_part_2/
 *
 *			Box-drawing characters!
 *			https://en.wikipedia.org/wiki/Box-drawing_characters
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
		// Day10Problem1 problem1 = new Day10Problem1();
		// problem1.Run();
		Day10Problem2 problem2 = new Day10Problem2();
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