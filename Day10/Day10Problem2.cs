namespace Day10;

public class Day10Problem2

{
	private char[,]? _tileMap;
	private int _originalMapWidth;
	private int _originalMapHeight;
	private char[,]? _paddedMap;
	private Day10Main.Coords _startPosition;

	private List<Day10Main.Coords> _tilesInPath = new List<Day10Main.Coords>();
	private const char EmptyChar = '.';
	private const char EmptyOutsideChar = '+';

	private readonly char[,] _emptyBlock = {{'.','.','.'},{'.','.','.'},{'.','.','.'}};
	private readonly char[,] _fBlock = {{'.','.','.'},{'.','F','-'},{'.','|','.'}};
	private readonly char[,] _jBlock = {{'.','|','.'},{'-','J','.'},{'.','.','.'}};
	private readonly char[,] _ellBlock = {{'.','|','.'},{'.','L','-'},{'.','.','.'}};
	private readonly char[,] _sevenBlock = {{'.','.','.'},{'-','7','.'},{'.','|','.'}};
	private readonly char[,] _pipeBlock = {{'.','|','.'},{'.','|','.'},{'.','|','.'}};
	private readonly char[,] _dashBlock = {{'.','.','.'},{'-','-','-'},{'.','.','.'}};

	private IEnumerable<char>? _emptyBlockSeq;
	
	public void Run()
	{
		LoadData();
		TraverseMap();
		MarkNonPathTiles();
		
		PadCells();
		FloodFillExterior();
		CountEmptyInteriorCells();
	}


	void PadCells()
	{
		_paddedMap = new char[_originalMapWidth * 3, _originalMapHeight * 3];

		for (int originalLineNo = 0; originalLineNo < _originalMapHeight; originalLineNo++)
		{
			for (int originalCharNo = 0; originalCharNo < _originalMapWidth; originalCharNo++)
			{
				char[,] paddedBlock;
				switch (_tileMap[originalCharNo,originalLineNo])
				{
					case 'F': paddedBlock = _fBlock;
						break;
					case 'J': paddedBlock = _jBlock;
						break;
					case 'L': paddedBlock = _ellBlock;
						break;
					case '7': paddedBlock = _sevenBlock;
						break;
					case '|': paddedBlock = _pipeBlock;
						break;
					case '-': paddedBlock = _dashBlock;
						break;
					default: paddedBlock = _emptyBlock;
						break;
				}
				PlacePaddedBlock(_paddedMap, paddedBlock, originalCharNo*3, originalLineNo*3);
			}
		}
	}

	void PlacePaddedBlock(char[,] paddedMap, char[,] paddedBlock, int charNo, int lineNo)
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				//reversed x/y in map vs block, because I don't feel like reversing
				//the padded block definitions
				paddedMap[charNo + j, lineNo + i] = paddedBlock[i, j];
			}
		}
	}


	void CountEmptyInteriorCells()
	{
		//process 3x3 padded blocks and find those that are just .s. Each corresponds to
		//an empty interior tile in the original map.

		_emptyBlockSeq = _emptyBlock.Cast<char>();
		int emptyInteriorBlocks = 0;
		char[,] paddedBlock = new char[3, 3];
		for (int lineNo = 0; lineNo < _originalMapHeight*3; lineNo += 3)
		{
			for (int charNo = 0; charNo < _originalMapWidth*3; charNo += 3)
			{
				for (int blockLineNo = 0; blockLineNo < 3; blockLineNo++)
				{
					for (int blockCharNo = 0; blockCharNo < 3; blockCharNo++)
					{
						char testChar = _paddedMap[charNo + blockCharNo, lineNo + blockLineNo];
						paddedBlock[blockCharNo, blockLineNo] = testChar;
					}
				}

				if (IsInteriorEmptyBlock(paddedBlock))
				{
					emptyInteriorBlocks++;
				}
			}
		}
		Console.WriteLine("total empty blocks: "+emptyInteriorBlocks);
	}

	bool IsInteriorEmptyBlock(char[,] paddedBlock)
	{
		return (paddedBlock.Cast<char>().SequenceEqual(_emptyBlockSeq));
	}


	void FloodFillExterior()
	{
		GFGFloodFill.FloodFill(_paddedMap,
			_originalMapWidth*3, _originalMapHeight*3,
			0, 0, EmptyChar, EmptyOutsideChar);
	}
	
	void MarkNonPathTiles()
	{
		for (int lineNo = 0; lineNo < _originalMapHeight; lineNo++)
		{
			for (int charNo = 0; charNo < _originalMapWidth; charNo++)
			{
				Day10Main.Coords tile = new Day10Main.Coords(charNo, lineNo);
				if (!_tilesInPath.Contains(tile))
				{
					_tileMap[charNo, lineNo] = '.';
				}
			}
		}
	}

	
	//Naive and extremely verbose solution, but it works
	void TraverseMap()
	{
		char currentChar;
	  Direction startDirection = Direction.Up;
	  
		bool cycleFound = false;
		bool endFound = false;

		Day10Main.Coords currentPosition = _startPosition;
		SetStartDirectionAndTile(_originalMapWidth, _originalMapHeight,
			ref startDirection, ref currentPosition);
		Direction currentDirection = startDirection;

		while (!cycleFound)
		{
			currentChar = _tileMap[currentPosition.X, currentPosition.Y];
			Tuple<Direction, char> mapCursor = new Tuple<Direction, char>(currentDirection, currentChar);
			_tilesInPath.Add(currentPosition);
			if (currentChar == 'S')
			{
				Console.WriteLine("back to the beginning!");
				cycleFound = true;
				break;
			} else if (currentChar == '.')
			{
				endFound = true;
			}
			else
			{
				if (currentDirection == Direction.Up)
				{
					switch (currentChar)
					{
						case '|': //move up
							MoveUp();
							break;
						case 'F': //move right
							MoveRight();
							break;
						case '7': //move left
							MoveLeft();
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
							MoveRight();
							break;
						case '7': //move down
							MoveDown();
							break;
						case 'J': //move up
							MoveUp();
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
							MoveDown();
							break;
						case 'L': //move right
							MoveRight();
							break;
						case 'J': //move left
							MoveLeft();
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
							MoveLeft();
							break;
						case 'F': //move down
							MoveDown();
							break;
						case 'L': //move up
							MoveUp();
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
				currentPosition = _startPosition;
				_tilesInPath = new List<Day10Main.Coords>();
				switch (currentDirection)
				{
					case Direction.Up:
						MoveRight();
						break;
					case Direction.Right:
						MoveDown();
						break;
					case Direction.Down:
						MoveLeft();
						break;
					case Direction.Left:
						Console.WriteLine("error, have already checked all directions");
						break;
				}
				startDirection = currentDirection;
				endFound = false;
			}
		}

		char startTileChar = ToMapChar(new Tuple<Direction,Direction>(startDirection, currentDirection));
		_tileMap[_startPosition.X, _startPosition.Y] = startTileChar;

		void MoveLeft()
		{
			currentPosition.X -= 1;
			currentDirection = Direction.Left;
		}

		void MoveUp()
		{
			currentPosition.Y -= 1;
			currentDirection = Direction.Up;
		}

		void MoveRight()
		{
			currentPosition.X += 1;
			currentDirection = Direction.Right;
		}

		void MoveDown()
		{
			currentPosition.Y += 1;
			currentDirection = Direction.Down;
		}
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
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		
		if (File.Exists(path))
		{
			string[] textAsLines = File.ReadAllLines(path);
			int lineLength = textAsLines[0].Length;
			_tileMap = new char[lineLength,textAsLines.Length];
			int lineNo = 0;
			foreach (string inputLine in textAsLines)
			{
				int charNo = 0;
				foreach (char c in inputLine)
				{
					_tileMap[charNo,lineNo] = c;
					if (c == 'S')
					{
						_startPosition = new Day10Main.Coords(charNo,lineNo);
					}
					charNo++;
				}
				lineNo++;
			}
			_originalMapWidth = _tileMap.GetLength(0);
			_originalMapHeight = _tileMap.GetLength(1);
		}
		
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
	void PrintMap(char[,] theMap)
	{
		for (int lineNo = 0; lineNo < theMap.GetLength(1); lineNo++)
		{
			for (int charNo = 0; charNo < theMap.GetLength(0) - 1; charNo++)
			{
				Console.Write(theMap[charNo,lineNo]);
			}
			Console.WriteLine();
		}
	}
	
}