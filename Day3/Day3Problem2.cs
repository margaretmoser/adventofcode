﻿using System.Text.RegularExpressions;
/*
 * https://adventofcode.com/2023/day/3#part2
 * 
 * Mostly just the inverse of the part-number matching in Part 1
 * 1) match the literal *
 * 2) get indices of part numbers in line n-1, n, n+1; check if the * falls between
 *		(index of matched digits-1) and (index of matched digits+match length+1)
 * 3) count results of part 2 to see if there are two adjacent part numbers
 */

namespace Day3
{
	class Day3Problem2
	{
		static void Main()
		{
			List<MatchCollection> partNumberMatches = new List<MatchCollection>();
			int sumOfGearRatios = 0;
			
			var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
			if (File.Exists(path))
			{
				// Read file using StreamReader. Reads file line by line
				using StreamReader file = new StreamReader(path);
				int counter = 0;

				Regex asteriskPattern = new Regex(@"\*",
					RegexOptions.Compiled | RegexOptions.IgnoreCase);
				Regex partNumberPattern = new Regex(@"(\d+)",
					RegexOptions.Compiled | RegexOptions.IgnoreCase);

				//there is probably a way to do this that only goes through the input file once, but
				//make it work then make it better
				while (file.ReadLine() is { } ln)
				{
					partNumberMatches.Add(partNumberPattern.Matches(ln));
					counter++;
				}

				int indexOfLastLineRead = counter - 1;
				//restart StreamReader at beginning of file
				file.DiscardBufferedData();
				file.BaseStream.Seek(0, SeekOrigin.Begin);
				counter = 0;
				int firstIndexToCheckForAsterisk = -1;
				int lastIndexToCheckForAsterisk = -1;
				while (file.ReadLine() is { } ln)
				{
					foreach (Match asteriskMatch in asteriskPattern.Matches(ln))
					{
						//check lines n-1, n, n+1 for nearby symbols
						for (int i = Math.Max(0, counter-1); i <= Math.Min(indexOfLastLineRead, counter+1); i++)
						{
							//Console.WriteLine($"checking symbols in line {i}");
							List<int> matchingParts = new List<int>();
							foreach (Match partNumberMatch in partNumberMatches[i])
							{
								firstIndexToCheckForAsterisk = (Math.Max(0, partNumberMatch.Index - 1));
								lastIndexToCheckForAsterisk = partNumberMatch.Index + partNumberMatch.Length;
								
								if (asteriskMatch.Index >= firstIndexToCheckForAsterisk &&
								    asteriskMatch.Index <= lastIndexToCheckForAsterisk)
								{
									matchingParts.Add(Int32.Parse(partNumberMatch.Value));
									Console.WriteLine($"found an adjacent part on line {i}: "+partNumberMatch.Value);

									Console.Write("matchingParts now contains: ");
									matchingParts.ForEach(x => Console.Write(x+", "));
									Console.WriteLine();
								}
								
							}

							if (matchingParts.Count == 2)
							{
								//what happens if a * is adjacent to three part numbers?
								Console.WriteLine("found at least two matching parts; this is a gear");
								sumOfGearRatios += matchingParts[0] * matchingParts[1];
							}
							
						}
						
					}

					counter++;
				}
				Console.WriteLine($"Sum of part numbers adjacent to gears is {sumOfGearRatios}");

				file.Close();
			}
			else
			{
				Console.WriteLine($"no file found at {path}");
			}
		}
	}
}