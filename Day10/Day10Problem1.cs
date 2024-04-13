namespace Day10;
public class Day10Problem1
{
	private char[,] rawMap;
	private Day10Main.Coords startPosition;
	private Day10Main.Coords currentPosition;
	
	LinkedList<char> linkedMap = new LinkedList<char>();
	
	public void Run()
	{
		LoadData();
		TraverseMap();
	}


	//Naive and extremely verbose solution, but it works
	void TraverseMap()
	{
		char currentChar;
		bool cycleFound = false;
		bool endFound = false;
		int pathLength = 0;
		currentPosition = startPosition;
		//forcing right for initial test; come back and iterate over up, down, left
		Direction currentDirection = Direction.Up;
		currentPosition.Y--;
		
		Console.WriteLine("current position is "+currentPosition);
		while (!cycleFound)
		{
			currentChar = rawMap[currentPosition.X, currentPosition.Y];
			Console.Write(currentChar);
			pathLength++;
			if (currentChar == 'S')
			{
				Console.WriteLine("back to the beginning; cycle length is "+pathLength+
				                  " and most distant point is "+pathLength/2+" steps away");
				cycleFound = true;
				break;
			} else if (currentChar == '.')
			{
				Console.WriteLine("pipe end found, should exit loop and start again from S");
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
							Console.WriteLine("pipe end found, should exit loop and start again from S");
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
							Console.WriteLine("pipe end found, should exit loop and start again from S");
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
							Console.WriteLine("pipe end found, should exit loop and start again from S");
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
							Console.WriteLine("pipe end found, should exit loop and start again from S");
							endFound = true;
							break;
					}
				}
			}

			if (endFound)
			{
				currentPosition = startPosition;
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
	
	
	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		
		if (File.Exists(path))
		{
			
			string[] textAsLines = File.ReadAllLines(path);
			int lineLength = textAsLines[0].Length;
			rawMap = new char[textAsLines.Length, lineLength];
			int i = 0;
			foreach (string inputLine in textAsLines)
			{
				int j = 0;
				foreach (char c in inputLine)
				{
					rawMap[j,i] = c;
					if (c == 'S')
					{
						startPosition = new Day10Main.Coords(j, i);
					}
					j++;
				}
				i++;
			}
			Console.WriteLine("start position is "+startPosition.ToString());
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}