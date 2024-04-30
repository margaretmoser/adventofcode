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

	public void Run()
	{
		LoadData();
		FindPath();
		MarkNonPathTiles();
		PadCells();
		FloodFillExterior();
		CountEmptyInteriorCells();
	}
	
	void FindPath()
	{
		bool cycleFound = false;
		bool endFound = false;

		Day10Main.Coords currentPosition = _startPosition;
		Direction startDirection;
		SetStartDirectionAndTile();
		Direction currentDirection = startDirection;

		if (_tileMap != null)
		{
			while (!cycleFound)
			{
				var currentChar = _tileMap[currentPosition.X, currentPosition.Y];
				Tuple<Direction, char> mapCursor = new Tuple<Direction, char>(currentDirection, currentChar);
				_tilesInPath.Add(currentPosition);
				cycleFound = (currentChar == 'S');
				if (cycleFound)
				{
					Console.WriteLine("back to the beginning!");
					break;
				}
				
				if (currentChar == '.')
				{
					endFound = true;
				} else
				{
					switch (mapCursor)
					{
						case (Direction.Up, '|'):
						case (Direction.Right, 'J'):
						case (Direction.Left, 'L'):
							MoveUp();
							break;
						case (Direction.Up, 'F'):
						case (Direction.Right, '-'):
						case (Direction.Down, 'L'):
							MoveRight();
							break;
						case (Direction.Up, '7'):
						case (Direction.Down, 'J'):
						case (Direction.Left, '-'):
							MoveLeft();
							break;
						case (Direction.Right, '7'):
						case (Direction.Down, '|'):
						case (Direction.Left, 'F'):
							MoveDown();
							break;
						default:
							Console.WriteLine("pipe end found");
							endFound = true;
							break;
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

			char startTileChar = ToMapChar(new Tuple<Direction, Direction>(startDirection, currentDirection));
			_tileMap[_startPosition.X, _startPosition.Y] = startTileChar;
		}
		
		void SetStartDirectionAndTile()
		{
			if (currentPosition.Y > 0)
			{
				currentPosition.Y--;
				startDirection = Direction.Up;
			} else if (currentPosition.X < _originalMapWidth - 1)
			{
				currentPosition.X++;
				startDirection = Direction.Right;
			} else if (currentPosition.Y < _originalMapHeight - 1)
			{
				currentPosition.Y++;
				startDirection = Direction.Down;
			}
			else
			{
				currentPosition.X--;
				startDirection = Direction.Left;
			}
		}

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
	
	void MarkNonPathTiles()
	{
		for (int lineNo = 0; lineNo < _originalMapHeight; lineNo++)
		{
			for (int charNo = 0; charNo < _originalMapWidth; charNo++)
			{
				Day10Main.Coords tile = new Day10Main.Coords(charNo, lineNo);
				if (!_tilesInPath.Contains(tile))
				{
					if (_tileMap != null)
						_tileMap[charNo, lineNo] = '.';
				}
			}
		}
	}
	
	void PadCells()
	{
		if (_tileMap != null)
		{
			_paddedMap = new char[_originalMapWidth * 3, _originalMapHeight * 3];
			for (int originalLineNo = 0; originalLineNo < _originalMapHeight; originalLineNo++)
			{
				for (int originalCharNo = 0; originalCharNo < _originalMapWidth; originalCharNo++)
				{
					char[,] paddedBlock = ToPaddedBlock(_tileMap[originalCharNo, originalLineNo]);
					PlacePaddedBlock(_paddedMap, paddedBlock, originalCharNo * 3, originalLineNo * 3);
				}
			}
		}
	}
	
	void FloodFillExterior()
	{
		if (_paddedMap != null)
		{
			GFGFloodFill.FloodFill(_paddedMap,
				_originalMapWidth * 3, _originalMapHeight * 3,
				0, 0, EmptyChar, EmptyOutsideChar);
		}
	}
	
	void CountEmptyInteriorCells()
	{
		//process 3x3 padded blocks and find those that are just .s. Each corresponds to
		//an empty interior tile in the original map.
		if (_paddedMap != null)
		{
			int emptyInteriorBlocks = 0;
			char[,] paddedBlock = new char[3, 3];
			for (int lineNo = 0; lineNo < _originalMapHeight * 3; lineNo += 3)
			{
				for (int charNo = 0; charNo < _originalMapWidth * 3; charNo += 3)
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
						emptyInteriorBlocks++;
				}
			}
			Console.WriteLine("total empty blocks: " + emptyInteriorBlocks);
		}
	}
	
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
	
/*
 *		HELPER METHODS 
 */
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

	private char[,] ToPaddedBlock(char originalChar) => originalChar switch
	{
		'F'       => _fBlock,
		'J'				=> _jBlock,
		'L'				=> _ellBlock,
		'7'				=> _sevenBlock,
		'|'				=> _pipeBlock,
		'-'				=> _dashBlock,
		_					=> _emptyBlock
	};

	private static char ToMapChar(Tuple<Direction,Direction> startAndEnd) => startAndEnd switch
	{
		(Direction.Up, Direction.Up) or (Direction.Down,Direction.Down)       => '|',
		(Direction.Right,Direction.Right) or (Direction.Left,Direction.Left)  => '-',
		(Direction.Up,Direction.Left) or (Direction.Right, Direction.Down)    => 'L',
		(Direction.Up,Direction.Right) or (Direction.Left, Direction.Down)    => 'J',
		(Direction.Right,Direction.Up) or (Direction.Down,Direction.Left)     => 'F',
		(Direction.Left,Direction.Up) or (Direction.Down,Direction.Right)     => '7',
		_ => '!'
	};
	
	bool IsInteriorEmptyBlock(char[,] paddedBlock)
	{
		return (paddedBlock.Cast<char>().SequenceEqual(_emptyBlock.Cast<char>()));
	}
	
	/*
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
	*/
}