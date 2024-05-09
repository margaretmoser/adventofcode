namespace Day12;
using System.Text.RegularExpressions;

public class SpringRecord
{
	private readonly string _springCharacters;
	private readonly string _blockCountStringForDebugging;
	private int[] blockCountArray;
	//List of characters to substitute in place of '?'
	private readonly List<char> _validChars = new List<char>() { Day12Main.WorkingSpringChar, Day12Main.DamagedSpringChar };
	//List of combinations generated
	private readonly List<string> _permutations = new List<string>();

	public SpringRecord(string springs, string blocks)
	{
		_springCharacters = springs;
		_blockCountStringForDebugging = blocks;
		blockCountArray = Array.ConvertAll(blocks.Split(','), int.Parse);
	}

	public int SolveRecord()
	{
		int validPermutations = 0;
		string blocksPatternString = "^\\.*";
		for (int i = 0; i < blockCountArray.Length; i++)
		{
			//ex:	^\.*[\#]{3}[\.\s]+[\#]{2}[\.\s]+[\#]{1}[\.]*$
			blocksPatternString += "[\\#]{" + blockCountArray[i].ToString() + "}[\\.]";
			blocksPatternString += (i < blockCountArray.Length - 1) ? "+" : "*$";
		}
		//Console.WriteLine("block pattern is "+blocksPatternString);
		Regex blocksPattern = new Regex(blocksPatternString,
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		GenerateCombos(_springCharacters, String.Empty);
		Console.WriteLine("valid combos for springs "+_springCharacters+
		                  " and block pattern "+_blockCountStringForDebugging+":");

		foreach (string permutation in _permutations)
		{
			if (Regex.IsMatch(permutation, blocksPatternString))
			{
				if (Day12Problem1.UseTestInput || Day12Problem1.UseMaxLines) Console.WriteLine(permutation);
				validPermutations++;
			}
		}
		Console.WriteLine("Total: "+validPermutations.ToString());
		Console.WriteLine();


		return validPermutations;
	}

	//from https://stackoverflow.com/a/8844897
	void GenerateCombos(string mask, string combination)
	{
		if (mask.Length <= 0)
		{
			//No more chars left in the mask, add this combination to the solution list.
			_permutations.Add(combination);
			return;
		}

		if (mask[0] != Day12Main.UnknownSpringChar)
		{
			//This is not a wildcard, append the first character of the mask
			//to the combination string and call the function again with 
			//the remaining x characters of the mask.
			GenerateCombos(mask.Substring(1), combination + mask[0]);
		}
		else
		{
			//This is a wildcard, so for each of the valid substitution chars,
			//append the char to the combination string and call again
			//with the remaining x chars of the mask.
			_validChars.ForEach(c => GenerateCombos(mask.Substring(1), combination + c));
		}
	}

	
	
}