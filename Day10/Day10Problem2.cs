namespace Day10;
public class Day10Problem2

{
	private char[,] rawMap;
	private Day10Main.Coords startPosition;
	private Day10Main.Coords currentPosition;

	private List<Day10Main.Coords> tilesInPath;
	
	public void Run()
	{
		LoadData();
		TraverseMap();
		MarkNonPathTiles();
		AttemptLassoAlgo();
	}

	void MarkEdges(char[,] tileMap, char emptyChar, char borderChar)
	{
		bool isSectionOfPipe = false;
		
		for (int lineNo = 0; lineNo < tileMap.GetLength(1); lineNo++)
		{
			for (int charNo = 0; charNo < tileMap.GetLength(0) - 1; charNo++)
			{
				char currentChar = tileMap[charNo, lineNo];
				char nextChar = tileMap[charNo + 1, lineNo];
				
				if (isSectionOfPipe)
				{
					if (nextChar == emptyChar)
					{
						tileMap[charNo, lineNo] = borderChar;
						isSectionOfPipe = false;
					}
				}
				else
				{
					if (currentChar == emptyChar && nextChar != emptyChar)
					{
						tileMap[charNo+1, lineNo] = borderChar;
						isSectionOfPipe = true;
					}
				}

				Console.Write(tileMap[charNo,lineNo]);
				if (charNo + 1 == tileMap.GetLength(0) - 1)
				{
					Console.Write(tileMap[charNo+1,lineNo]);
				}
			}

			isSectionOfPipe = false;
			Console.WriteLine();
		}
	}
	
	
	//FIXME: this works for the first test input, but fails on the second
	//It looks like it is not correctly marking tiles around the edges of the
	//map that are also "edges" (borders) of pipe sections
	void AttemptLassoAlgo()
	{
		char emptyChar = '.';
		char borderChar = 'x';
		char outsideMarkerChar = 'O';
		char insideMarkerChar = 'I';

		Console.WriteLine("Marking edges");
		MarkEdges(rawMap, emptyChar, borderChar);
		Console.WriteLine();

		int edgesEncounteredThisLine = 0;
		char currentChar;
		
		//looping over the array for the billionth time
		Console.WriteLine("Marking inside tiles");
		for (int lineNo = 0; lineNo < rawMap.GetLength(1); lineNo++)
		{
			for (int charNo = 0; charNo < rawMap.GetLength(0) - 1; charNo++)
			{
				currentChar = rawMap[charNo, lineNo];
				if (currentChar == borderChar)
				{
					edgesEncounteredThisLine++;
				}
				if (currentChar == emptyChar)
				{
					if (edgesEncounteredThisLine % 2 == 1)
					{
						rawMap[charNo, lineNo] = insideMarkerChar;
					}
					else
					{
						rawMap[charNo, lineNo] = outsideMarkerChar;
					}
				}
				Console.Write(rawMap[charNo,lineNo]);
				if (charNo + 1 == rawMap.GetLength(0) - 1)
				{
					Console.Write(rawMap[charNo+1,lineNo]);
				}
			}

			edgesEncounteredThisLine = 0;
			Console.WriteLine();
		}

	}

	void MarkNonPathTiles()
	{
		Console.WriteLine("processing non-path tiles. Updated map:");
		for (int lineNo = 0; lineNo < rawMap.GetLength(1); lineNo++)
		{
			for (int charNo = 0; charNo < rawMap.GetLength(0); charNo++)
			{
				Day10Main.Coords tile = new Day10Main.Coords(charNo, lineNo);
				if (!tilesInPath.Contains(tile))
				{
					rawMap[charNo, lineNo] = '.';
				}
				Console.Write(rawMap[charNo,lineNo]);
			}
			Console.WriteLine();
		}
		Console.WriteLine();
		
	}

	
	//Naive and extremely verbose solution, but it works
	void TraverseMap()
	{
		char currentChar;
		bool cycleFound = false;
		bool endFound = false;
		Direction currentDirection = Direction.Up;

		currentPosition = startPosition;
		
		SetStartDirectionAndTile(rawMap, ref currentDirection, ref currentPosition);
		
		tilesInPath = new List<Day10Main.Coords>();
		
		while (!cycleFound)
		{
			currentChar = rawMap[currentPosition.X, currentPosition.Y];
			tilesInPath.Add(currentPosition);
			if (currentChar == 'S')
			{
				Console.WriteLine("back to the beginning!");
				Console.WriteLine();
				Console.WriteLine();
				cycleFound = true;
				break;
			} else if (currentChar == '.')
			{
				Console.WriteLine("pipe end found");
				endFound = true;
			}
			else
			{
				if (currentDirection == Direction.Up)
				{
					switch (currentChar)
					{
						case '|': //move up
							currentPosition.Y -= 1;
							currentDirection = Direction.Up;
							break;
						case 'F': //move right
							currentPosition.X += 1;
							currentDirection = Direction.Right;
							break;
						case '7': //move left
							currentPosition.X -= 1;
							currentDirection = Direction.Left;
							break;
						default:
							Console.WriteLine("pipe end found");
							endFound = true;
							break;
					}
				}
				else if (currentDirection == Direction.Right)
				{
					switch (currentChar)
					{
						case '-': //move right
							currentPosition.X += 1;
							currentDirection = Direction.Right;
							break;
						case '7': //move down
							currentPosition.Y += 1;
							currentDirection = Direction.Down;
							break;
						case 'J': //move up
							currentPosition.Y -= 1;
							currentDirection = Direction.Up;
							break;
						default:
							Console.WriteLine("pipe end found");
							endFound = true;
							break;
					}
				}
				else if (currentDirection == Direction.Down)
				{
					switch (currentChar)
					{
						case '|': //move down
							currentPosition.Y += 1;
							currentDirection = Direction.Down;
							break;
						case 'L': //move right
							currentPosition.X += 1;
							currentDirection = Direction.Right;
							break;
						case 'J': //move left
							currentPosition.X -= 1;
							currentDirection = Direction.Left;
							break;
						default:
							Console.WriteLine("pipe end found");
							endFound = true;
							break;
					}
				}
				else if (currentDirection == Direction.Left)
				{
					switch (currentChar)
					{
						case '-': //move left
							currentPosition.X -= 1;
							currentDirection = Direction.Left;
							break;
						case 'F': //move down
							currentPosition.Y += 1;
							currentDirection = Direction.Down;
							break;
						case 'L': //move up
							currentPosition.Y -= 1;
							currentDirection = Direction.Up;
							break;
						default:
							Console.WriteLine("pipe end found");
							endFound = true;
							break;
					}
				}
			}

			if (endFound)
			{
				currentPosition = startPosition;
				tilesInPath = new List<Day10Main.Coords>();
				switch (currentDirection)
				{
					case Direction.Up:
						currentDirection = Direction.Right;
						currentPosition.X++;
						break;
					case Direction.Right:
						currentDirection = Direction.Down;
						currentPosition.Y++;
						break;
					case Direction.Down:
						currentDirection = Direction.Left;
						currentPosition.X--;
						break;
					case Direction.Left:
						Console.WriteLine("error, have already checked all directions");
						break;
				}
				endFound = false;
			}
		}
	}
	
	void SetStartDirectionAndTile(char[,] tiles, ref Direction startDirection, ref Day10Main.Coords firstPos)
	{
		if (firstPos.Y > 0)
		{
			firstPos.Y--;
			startDirection = Direction.Up;
		} else if (firstPos.X < tiles.GetLength(0) - 1)
		{
			firstPos.X++;
			startDirection = Direction.Right;
		} else if (firstPos.Y < tiles.GetLength(1) - 1)
		{
			firstPos.Y++;
			startDirection = Direction.Down;
		}
		else
		{
			firstPos.X--;
			startDirection = Direction.Left;
		}
	}
	
	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input_test.txt");
		
		if (File.Exists(path))
		{
			
			string[] textAsLines = File.ReadAllLines(path);
			int lineLength = textAsLines[0].Length;
			rawMap = new char[lineLength,textAsLines.Length];
			int lineNo = 0;
			foreach (string inputLine in textAsLines)
			{
				int charNo = 0;
				foreach (char c in inputLine)
				{
					Console.Write(c);
					rawMap[charNo,lineNo] = c;
					if (c == 'S')
					{
						startPosition = new Day10Main.Coords(charNo,lineNo);
					}
					charNo++;
				}
				lineNo++;
				Console.WriteLine();
			}
			Console.WriteLine("start position is "+startPosition.ToString());
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}