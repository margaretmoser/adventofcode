namespace Day11;

public class Day11Problem2
{
	private const int ExpansionFactor = 1000000;
	private const char GalaxyChar = '#';
	private char[,]? _rawMap;
	private readonly List<int> _rowsToDuplicate = new List<int>();
	private List<int> _columnsToDuplicate = new List<int>();
	private readonly List<Day11Main.Coords> _originalGalaxyPositions = new List<Day11Main.Coords>();
	private readonly Queue<Day11Main.Coords> _expandedGalaxyPositions = new Queue<Day11Main.Coords>();
	
	public void Run()
	{
		LoadData();
		CreateExpandedMap();
		SumPathLengths();
	}

	private void CreateExpandedMap()
	{
		if (_rawMap != null)
		{
			int originalRowCount = _rawMap.GetLength(1);
			int originalColumnCount = _rawMap.GetLength(0);
			_columnsToDuplicate = FindColsWithGalaxies(originalColumnCount, originalRowCount);
			foreach (Day11Main.Coords galaxyPosition in _originalGalaxyPositions)
			{
				Day11Main.Coords newCoords = GetAdjustedGalaxyPosition(galaxyPosition);
				_expandedGalaxyPositions.Enqueue(newCoords);
				Console.WriteLine("galaxy "+galaxyPosition+" => "+newCoords);
			}
		}
	}

	void SumPathLengths()
	{
		long totalOfAllPathLengths = 0;
		while (_expandedGalaxyPositions.Count > 0)
		{
			Day11Main.Coords galaxyPosition = _expandedGalaxyPositions.Dequeue();
			foreach (Day11Main.Coords otherGalaxyPosition in _expandedGalaxyPositions)
			{
				long shortestPathToThisGalaxy = Math.Abs(galaxyPosition.X - otherGalaxyPosition.X) + 
				                       Math.Abs(galaxyPosition.Y - otherGalaxyPosition.Y);
				totalOfAllPathLengths += shortestPathToThisGalaxy;
			}
		}
		Console.WriteLine("total of all paths is "+totalOfAllPathLengths);
		
	}

	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		if (File.Exists(path))
		{
			string[] textAsLines = File.ReadAllLines(path);
			int lineLength = textAsLines[0].Length;
			_rawMap = new char[textAsLines.Length, lineLength];
			int lineNo = 0;
			foreach (string inputLine in textAsLines)
			{
				var rowHasGalaxy = false;
				var charNo = 0;
				foreach (char c in inputLine)
				{
					_rawMap[charNo,lineNo] = c;
					if (c == GalaxyChar)
					{
						rowHasGalaxy = true;
						_originalGalaxyPositions.Add(new Day11Main.Coords(charNo,lineNo));
					}
					charNo++;
				}
				if (!rowHasGalaxy) _rowsToDuplicate.Add(lineNo);
				lineNo++;
			}
			PrintMap(_rawMap);
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}

	
	/*
	 * helper methods
	 */
	
	List<int> FindColsWithGalaxies(int originalColumnCount, int originalRowCount)
	{
		List<int> colsToDuplicate = new List<int>();
		if (_rawMap != null)
		{
			for (int originalCharNo = 0; originalCharNo < originalColumnCount; originalCharNo++)
			{
				var columnHasGalaxy = false;
				for (int originalLineNo = 0; originalLineNo < originalRowCount; originalLineNo++)
				{
					columnHasGalaxy = columnHasGalaxy || _rawMap[originalCharNo, originalLineNo] == GalaxyChar;
				}

				if (!columnHasGalaxy) colsToDuplicate.Add(originalCharNo);
			}
		}
		return colsToDuplicate;
	}

	Day11Main.Coords GetAdjustedGalaxyPosition(Day11Main.Coords originalPos)
	{
		Day11Main.Coords newCoords = new Day11Main.Coords();
		int newColPosition = _columnsToDuplicate.TakeWhile(dupeCol => dupeCol < originalPos.X).Count();
		newCoords.X = originalPos.X + newColPosition*(ExpansionFactor-1);
		int newRowPosition = _rowsToDuplicate.TakeWhile(dupeRow => dupeRow < originalPos.Y).Count();
		newCoords.Y = originalPos.Y + newRowPosition*(ExpansionFactor-1);
		return newCoords;
	}
	
	void PrintMap(char[,] theMap)
	{
		for (int lineNo = 0; lineNo < theMap.GetLength(1); lineNo++)
		{
			for (int charNo = 0; charNo < theMap.GetLength(0); charNo++)
			{
				Console.Write(theMap[charNo,lineNo]);
			}
			Console.WriteLine();
		}
	}
	
}