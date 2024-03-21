using System.Text.RegularExpressions;
namespace Day7;



public class Day7Problem2
{
	private List<HandBidTypeRank> processedHands;
	private List<char> cardValues;

	private int totalWinnings;
	public void Run()
	{
		processedHands = new List<HandBidTypeRank>();
		cardValues = new List<char>() { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
		LoadDataAndAssignHandType();
		SortAllHands(processedHands);
		CalculateTotalWinnings(processedHands);
	}

	void LoadDataAndAssignHandType()
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
				HandBidTypeRank newHand = new HandBidTypeRank(g[1].Value, Int32.Parse(g[2].Value));
				newHand.type = GetHandType(newHand.hand);
				processedHands.Add(newHand);
				//Console.WriteLine($"added {g[1].Value}, {g[2].Value} to dictionary; ");
				counter++;
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}

	void SortAllHands(List<HandBidTypeRank> theList)
	{
		theList.Sort((hand1, hand2) => CompareHands(hand1, hand2));
	}

	int CompareHands(HandBidTypeRank hand1, HandBidTypeRank hand2)
	{
		int compareType = hand1.type.CompareTo(hand2.type);
		if (compareType != 0)
			return compareType;
		//otherwise they are the same type so start comparing cards
		int compareChar = 0;
		for (int i = 0; i < hand1.hand.Length; i++)
		{
			compareChar = cardValues.IndexOf(hand1.hand[i]) - cardValues.IndexOf(hand2.hand[i]);
			if (compareChar != 0)
				return compareChar;
		}

		return -999;

	}

	void CalculateTotalWinnings(List<HandBidTypeRank> theList)
	{
		int i = 1;
		foreach (HandBidTypeRank hand in theList)
		{
			int scoreForThisHand = hand.bid * i;
			totalWinnings += scoreForThisHand;
			i++;
		}
		Console.WriteLine("Total winnings: "+totalWinnings.ToString());
	}

	HandType GetHandType(string hand)
	{
		//add logic here to deal with the new "Js are jokers" thing
		
		List<Tuple<string, int>> repeatsList = GroupRepeatedCharacters(hand);
		if (repeatsList[0].Item2 == 5)
		{
			return HandType.FiveOfAKind;
		} else if (repeatsList[0].Item2 == 4)
		{
			return HandType.FourOfAKind;
		} else if (repeatsList[0].Item2 == 3)
		{
			return (repeatsList[1].Item2 == 2 ? HandType.FullHouse : HandType.ThreeOfAKind);
			
		} else if (repeatsList[0].Item2 == 2)
		{
			return (repeatsList[1].Item2 == 2 ? HandType.TwoPair : HandType.OnePair);
		}
		else
		{
			return HandType.HighCard;
		}
	}

	List<Tuple<string,int>> GroupRepeatedCharacters(string theHand)
	{
		List<Tuple<string, int>> repeatsList = new List<Tuple<string, int>>();
		//Console.WriteLine("processing hand "+theHand);
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