using System.Text.RegularExpressions;
namespace Day7;

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

public class Day7Problem1
{
	private Dictionary<string, int> hands;
	private List<HandBidTypeRank> processedHands;

	private int totalWinnings;
	public void Run()
	{
		processedHands = new List<HandBidTypeRank>();
		hands = new Dictionary<string, int>();
		LoadHandsAndBids();
		
	}

	void LoadHandsAndBids()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		Regex handAndBidPattern = new Regex(@"(.+)(?:\s)(\d+)",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			int counter = 0;
			while (file.ReadLine() is { } ln && counter < 10000)
			{
				GroupCollection g = handAndBidPattern.Match(ln).Groups;
				hands.Add(g[1].Value, Int32.Parse(g[2].Value));
				HandBidTypeRank newHand = new HandBidTypeRank(g[1].Value, Int32.Parse(g[2].Value));
				newHand.type = GetHandType(newHand.hand);
				processedHands.Add(newHand);
				Console.WriteLine($"added {g[1].Value}, {g[2].Value} to dictionary; ");
				counter++;
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}

	string GetHigherHand(string hand1, string hand2)
	{
		return "";
	}

	HandType GetHandType(string hand)
	{
		List<Tuple<string, int>> repeatsList = GroupRepeatedCharacters(hand);
		if (repeatsList[0].Item2 == 5)
		{
			// hand.type = HandType.FiveOfAKind;
			return HandType.FiveOfAKind;
		} else if (repeatsList[0].Item2 == 4)
		{
			//hand.type = HandType.FourOfAKind;
			return HandType.FourOfAKind;
		} else if (repeatsList[0].Item2 == 3)
		{
			//hand.type = repeatsList[1].Item2 == 2 ? HandType.FullHouse : HandType.ThreeOfAKind;
			return (repeatsList[1].Item2 == 2 ? HandType.FullHouse : HandType.ThreeOfAKind);
			
		} else if (repeatsList[0].Item2 == 2)
		{
			//hand.type = repeatsList[1].Item2 == 2 ? HandType.TwoPair : HandType.OnePair;
			return (repeatsList[1].Item2 == 2 ? HandType.TwoPair : HandType.OnePair);
		}
		else
		{
			//hand.type = HandType.HighCard;
			return HandType.HighCard;
		}
	}

	List<Tuple<string,int>> GroupRepeatedCharacters(string theHand)
	{
		List<Tuple<string, int>> repeatsList = new List<Tuple<string, int>>();
		Console.WriteLine("processing hand "+theHand);
		var sortedByFrequency = theHand.GroupBy(x => x)
			.OrderByDescending(x => x.Count())
			.ToList();
		
		foreach (var thing in sortedByFrequency)
		{
			repeatsList.Add(new Tuple<string, int>(thing.Key.ToString(), thing.Count()));
		}
		return repeatsList;

	}
	
	
	
	
}