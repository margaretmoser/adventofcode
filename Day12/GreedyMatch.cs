namespace Day12;

/*  Out of the box, this just returns true or false. Since it's not feasible to iterate all
 *  possible combinations, it needs to be able to count how many possible matches there are
 *  *per block* and then multiply. For example, the test input
 *					.??..??...?##. 1,1,3
 *  once unfolded is supposed to produce 16384 arrangements; this is 2^14 (via
 *  https://www.calculatorsoup.com/calculators/math/prime-factors.php).
 *
 *  There are two places to put each of the ones in the pattern, so the original pattern
 *	has 2^2 = 4 combinations. Adding the wildcard makes the pattern like this:
 *					.??..??...?##.? .??..??...?##.? .??..??...?##.? .??..??...?##.? .??..??...?##.?
 *  So the second, third, etc. copies have three places for the first one, and each has
 *  eight possible placements for the pair of ones.
 *				2^2 * 2^3 * 2^3 * 2^3 * 2^3 = 2^(2+3+3+3+3) = 2^14
 *
 *  But this is a simple example where the 3-block anchors each chunk of the pattern. Other
 *  patterns will add more complexity with the unfolding.
 *  
 */

public class GreedyMatch
{
	// Function to check if a string matches a pattern
	// containing single-character wildcard '?'
	static bool IsMatch(string text, string pattern)
	{
		int textLength = text.Length;
		int patternLength = pattern.Length;
		int i = 0, j = 0, startIndex = -1, match = 0;

		while (i < textLength) {
			if (j < patternLength
			    && (pattern[j] == '?'
			        || pattern[j] == text[i])) {
				// Characters match or '?' in pattern
				// matches any character.
				i++;
				j++;
			}
			else if (j < patternLength && pattern[j] == '*') {
				// Wildcard character '*', mark the current
				// position in the pattern and the text as a
				// proper match.
				startIndex = j;
				match = i;
				j++;
			}
			else if (startIndex != -1) {
				// No match, but a previous wildcard was
				// found. Backtrack to the last '*'
				// character position and try for a
				// different match.
				j = startIndex + 1;
				match++;
				i = match;
			}
			else {
				// If none of the above cases comply, the
				// pattern does not match.
				return false;
			}
		}

		// Consume any remaining '*' characters in the given
		// pattern.
		while (j < patternLength && pattern[j] == '*') {
			j++;
		}

		// If we have reached the end of both the pattern
		// and the text, the pattern matches the text.
		return j == patternLength;
	}
}