/*
 * https://adventofcode.com/2023/day/8
 * In Day 1, a straightforward traversal of a cyclic directed graph following
 * a provided set of instructions, looping the instructions until a specified
 * end node is reached. Return the number of steps required to reach end node. The
 * only thing that was hard was choosing the right data structure for the nodes; in the
 * end I settled on a List of Dictionaries for ease of both inserting the data and
 * finding the node I needed using Linq.
 * 
 * In Day 2, solve the same problem but for multiple start/end node pairs. This
 * was not difficult in principle but the naive solution required literally millions
 * of steps and would not finish in a reasonable timeframe (like several decades).
 *
 * I did not think about solving it as a series of cycles that would eventually converge.
 * But the Reddit sub did, so it was straightforward after that - count the steps
 * needed for each node to get to its endpoint and then find the least common
 * multiple to determine when all the traversers ("ghosts") would reach their endpoint at the
 * same time.
 * 
 * LCM implementation using GCD and Euclidean algorithm
 * https://stackoverflow.com/questions/147515/least-common-multiple-for-3-or-more-numbers/29717490#29717490
 * 
 * 
 * Learned about: FirstOrDefault (rather than Where), LCM/GCD, "from" syntax for Linq queries
 */

namespace Day8;
class Day8Main
{
	static void Main()
	{
		// Day8Problem1 problem1 = new Day8Problem1();
		// problem1.Run();
		Day8Problem2 problem2 = new Day8Problem2();
		problem2.Run();
	}
			
}