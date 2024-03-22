/*
 * https://adventofcode.com/2023/day/7
 * Rank hands of cards using two heuristics in day 1:
 *		1. Poker ranking (five of a kind, two pair, etc.)
 *		2. High card L to R
 * Then in day 2, modify the heuristics so that
 *		1. Poker ranking includes Jacks as wild cards
 *		2. Adjust high-card sort order so J is lowest
 * Challenging, but easier than it looked. Key insight was that I needed to
 * count *and group* repeated characters rather than using a regex strategy,
 * because regexes can't efficiently determine things like "which character in
 * this string is repeated the most". Thanks to
 * (Linq GroupBy)
 * https://stackoverflow.com/questions/5069687/find-character-with-most-occurrences-in-string/5069735#5069735
 * (CompareTo and thus a way to write a custom compare without IComparator)
 * https://www.c-sharpcorner.com/UploadFile/afenster/lambda-expressions-are-wonderful/
 * https://learn.microsoft.com/en-us/dotnet/api/system.string.compareto?view=net-8.0
 * 
 * Learned about: custom compare functions, Linq OrderBy, Sort and GroupBy, Tuples, IGrouping;
 * structs are pass-by-value by default
 */

namespace Day7
{
	
	enum HandType {
		FiveOfAKind = 7,
		FourOfAKind = 6,
		FullHouse = 5,
		ThreeOfAKind = 4,
		TwoPair = 3,
		OnePair = 2,
		HighCard = 1
	}

	struct HandBidTypeRank
	{
		public string hand;
		public int bid;
		public HandType type;
		public int rank;

		public HandBidTypeRank(string hand, int bid)
		{
			this.hand = hand;
			this.bid = bid;
		}
	}
	class Day7Main
	{
		static void Main()
		{
			Day7Problem1 problem1 = new Day7Problem1();
			problem1.Run();
			// Day7Problem2 problem2 = new Day7Problem2();
			// problem2.Run();
		}
			
	}
}