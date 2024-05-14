namespace Day12;
using System.Text.RegularExpressions;

public class SpringRecord
{
	private readonly bool _printPatterns = true;
	private const int UnfoldCopyCount = 5;
	
	private readonly string _springCharacters;
	private readonly string _blockCountStringForDebugging;
	private readonly int[] _blockCountArray;

	private string _blocksPatternString = "";

	private Regex? _blockPattern;
	//List of characters to substitute in place of '?'
	private static readonly List<char> ValidChars = new List<char>() { Day12Main.WorkingSpringChar, Day12Main.DamagedSpringChar };
	//Valid combinations for this record
	private int _validCombinations;
	

	public SpringRecord(string springs, string blocks)
	{
		springs = Regex.Replace(springs, @"\.{2,}", @".");
		Console.WriteLine("springs "+springs);
		Console.WriteLine("unfolded "+UnfoldSpringRecord(springs, Day12Main.UnknownSpringChar));
		_springCharacters = UnfoldSpringRecord(springs, Day12Main.UnknownSpringChar);
		Console.WriteLine("blocks "+blocks);
		Console.WriteLine("unfolded "+UnfoldSpringRecord(blocks, ','));
		_blockCountStringForDebugging = UnfoldSpringRecord(blocks, ',');
		_blockCountArray = Array.ConvertAll(_blockCountStringForDebugging.Split(','), int.Parse);
	}
	
	string UnfoldSpringRecord(string originalRecord, char separationChar)
	{
		string unfolded = originalRecord;
		for (int i = 0; i < UnfoldCopyCount - 1; i++)
		{
			unfolded += separationChar + originalRecord;
		}
		//return unfolded;
		return originalRecord;
	}

	public int SolveRecord()
	{
		_blocksPatternString = ".";
		for (int i = 0; i < _blockCountArray.Length; i++)
		{
			//ex:	3,2,1 => "###.##.#"
			//.??#??.?.??#???
			for (int j = 0; j < _blockCountArray[i]; j++)
			{
				_blocksPatternString += '#';
			}
			_blocksPatternString += ".";
		}
		Console.WriteLine("generated block pattern is "+_blocksPatternString);
		
		Console.WriteLine("valid combos for springs "+_springCharacters+
		                  " and block pattern "+_blockCountStringForDebugging+":");

		//_blockPattern = new Regex(_blocksPatternString, RegexOptions.Compiled);
		
		//TestCombinations(_springCharacters, String.Empty);
		
		
		
		Console.WriteLine("Total: "+_validCombinations);
		return _validCombinations;
	}

	//from https://stackoverflow.com/a/8844897
	void TestCombinations(string mask, string combination)
	{
		if (mask.Length <= 0)
		{
			//No more chars left in the mask, add this combination to the solution list.
			if (_blockPattern != null && _blockPattern.IsMatch(combination))
			{
				if (_printPatterns) Console.WriteLine(combination);
				_validCombinations++;
			}
			return;
		}

		if (mask[0] != Day12Main.UnknownSpringChar)
		{
			//This is not a wildcard, append the first character of the mask
			//to the combination string and call the function again with 
			//the remaining x characters of the mask.
			TestCombinations(mask.Substring(1), combination + mask[0]);
		}
		else
		{
			//This is a wildcard, so for each of the valid substitution chars,
			//append the char to the combination string and call again
			//with the remaining x chars of the mask.
			TestCombinations(mask.Substring(1), combination+Day12Main.WorkingSpringChar);
			TestCombinations(mask.Substring(1), combination+Day12Main.DamagedSpringChar);
			// ValidChars.ForEach(c => TestCombinations(mask.Substring(1), combination + c));
		}
	}
}