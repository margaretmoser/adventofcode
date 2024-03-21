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