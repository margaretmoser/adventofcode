namespace Day10;
using System.Text.RegularExpressions;

public class Day10Problem2

{
	private char[,] tileMap;
	private Day10Main.Coords startPosition;

	private List<Day10Main.Coords> tilesInPath;
	private char emptyChar = '.';
	
	public void Run()
	{
		LoadData();
		TraverseMap();
		MarkNonPathTiles();
		
		PadCells(tileMap);
		//AttemptLassoAlgo();
	}

	void PadCells(char[,] tileMap)
	{
		int rawMapWidth = tileMap.GetLength(0);
		int rawMapHeight = tileMap.GetLength(1);
		char[,] paddedMap = new char[rawMapWidth * 3, rawMapHeight * 3];
		
		char[,] emptyBlock = {{'.','.','.'},{'.','.','.'},{'.','.','.'}};
		char[,] fBlock = {{'.','.','.'},{'.','F','-'},{'.','|','.'}};
		char[,] jBlock = {{'.','|','.'},{'-','J','.'},{'.','.','.'}};
		char[,] ellBlock = {{'.','|','.'},{'.','L','-'},{'.','.','.'}};
		char[,] sevenBlock = {{'.','.','.'},{'-','7','.'},{'.','|','.'}};
		char[,] pipeBlock = {{'.','|','.'},{'.','|','.'},{'.','|','.'}};
		char[,] dashBlock = {{'.','.','.'},{'-','-','-'},{'.','.','.'}};
		
		
		char[,] paddedBlock;
		for (int originalLineNo = 0; originalLineNo < rawMapHeight; originalLineNo++)
		{
			for (int originalCharNo = 0; originalCharNo < rawMapWidth; originalCharNo++)
			{
				switch (tileMap[originalCharNo,originalLineNo])
				{
					case 'F': paddedBlock = fBlock;
						break;
					case 'J': paddedBlock = jBlock;
						break;
					case 'L': paddedBlock = ellBlock;
						break;
					case '7': paddedBlock = sevenBlock;
						break;
					case '|': paddedBlock = pipeBlock;
						break;
					case '-': paddedBlock = dashBlock;
						break;
					default: paddedBlock = emptyBlock;
						break;
				}
				PlacePaddedBlock(paddedMap, paddedBlock, originalCharNo*3, originalLineNo*3);
			}
		}

		Console.WriteLine("\n"+"Padded map:");
		for (int lineNo = 0; lineNo < paddedMap.GetLength(1); lineNo++)
		{
			for (int charNo = 0; charNo < paddedMap.GetLength(0) - 1; charNo++)
			{
				Console.Write(paddedMap[charNo,lineNo]);
			}
			Console.WriteLine();
		}
		Console.WriteLine();Console.WriteLine();

	}

	void PlacePaddedBlock(char[,] paddedMap, char[,] paddedBlock, int charNo, int lineNo)
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				//reversed x/y because it's too hard to reverse the padded block defs
				paddedMap[charNo + j, lineNo + i] = paddedBlock[i, j];
			}
		}
	}

	void MarkEdges(char[,] tileMap, char borderChar)
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
					} else if (charNo == tileMap.GetLength(0) -2)
					{
						//Console.Write("last char? charNo is "+charNo+" and next character is "+nextChar);
						if (nextChar != emptyChar)
						{
							tileMap[charNo + 1, lineNo] = borderChar;
						}
					}
				}
				else
				{
					if (charNo == 0 && currentChar != emptyChar)
					{
						tileMap[charNo, lineNo] = borderChar;
						isSectionOfPipe = true;
					}
					if ((currentChar == emptyChar && nextChar != emptyChar))
					{
						tileMap[charNo+1, lineNo] = borderChar;
						isSectionOfPipe = true;
					}
					//Console.Write("line "+lineNo+", char "+charNo);
					
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
	void AttemptLassoAlgo()
	{
		char emptyChar = '.';
		char borderChar = 'x';
		char outsideMarkerChar = 'O';
		char insideMarkerChar = 'I';

		Console.WriteLine("Marking edges");
		MarkEdges(tileMap, borderChar);
		Console.WriteLine();

		//looping over the array for the billionth time
		Console.WriteLine("Marking inside tiles");
		for (int lineNo = 0; lineNo < tileMap.GetLength(1); lineNo++)
		{
			Regex interiorTilePattern = new Regex(@"(.+)(?:\s)(\d+)",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);
			Console.WriteLine();
		}


	}

	void MarkNonPathTiles()
	{
		Console.WriteLine("processing non-path tiles. Updated map:");
		for (int lineNo = 0; lineNo < tileMap.GetLength(1); lineNo++)
		{
			for (int charNo = 0; charNo < tileMap.GetLength(0); charNo++)
			{
				Day10Main.Coords tile = new Day10Main.Coords(charNo, lineNo);
				if (!tilesInPath.Contains(tile))
				{
					tileMap[charNo, lineNo] = '.';
				}
				Console.Write(tileMap[charNo,lineNo]);
			}
			Console.WriteLine();
		}
		Console.WriteLine();
		
	}

	
	//Naive and extremely verbose solution, but it works
	void TraverseMap()
	{
		char currentChar;
	  Direction startDirection = Direction.Up;
	  
		bool cycleFound = false;
		bool endFound = false;

		Day10Main.Coords currentPosition = startPosition;
		SetStartDirectionAndTile(tileMap.GetLength(1), tileMap.GetLength(0),
			ref startDirection, ref currentPosition);
		Direction currentDirection = startDirection;
		
		tilesInPath = new List<Day10Main.Coords>();
		
		while (!cycleFound)
		{
			currentChar = tileMap[currentPosition.X, currentPosition.Y];
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

		char startTileChar = ToMapChar(new Tuple<Direction,Direction>(startDirection, currentDirection));
		tileMap[startPosition.X, startPosition.Y] = startTileChar;

	}
	
	void SetStartDirectionAndTile(int mapWidth, int mapHeight, ref Direction startDirection, ref Day10Main.Coords firstPos)
	{
		if (firstPos.Y > 0)
		{
			firstPos.Y--;
			startDirection = Direction.Up;
		} else if (firstPos.X < mapWidth - 1)
		{
			firstPos.X++;
			startDirection = Direction.Right;
		} else if (firstPos.Y < mapHeight - 1)
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

	public static char ToMapChar(Tuple<Direction,Direction> startAndEnd) => startAndEnd switch
	{
		(Direction.Up, Direction.Up) or (Direction.Down,Direction.Down)       => '|',
		(Direction.Right,Direction.Right) or (Direction.Left,Direction.Left)  => '-',
		(Direction.Up,Direction.Left) or (Direction.Right, Direction.Down)    => 'L',
		(Direction.Up,Direction.Right) or (Direction.Left, Direction.Down)    => 'J',
		(Direction.Right,Direction.Up) or (Direction.Down,Direction.Left)     => 'F',
		(Direction.Left,Direction.Up) or (Direction.Down,Direction.Right)     => '7',
		_ => '!'
	};
	
	
	
	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input_test.txt");
		
		if (File.Exists(path))
		{
			
			string[] textAsLines = File.ReadAllLines(path);
			int lineLength = textAsLines[0].Length;
			tileMap = new char[lineLength,textAsLines.Length];
			int lineNo = 0;
			foreach (string inputLine in textAsLines)
			{
				int charNo = 0;
				foreach (char c in inputLine)
				{
					Console.Write(c);
					tileMap[charNo,lineNo] = c;
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