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


	void TraverseMap()
	{
		currentPosition = startPosition;
		char upChar, rightChar, downChar, leftChar;
		// come back and generalize this; for the moment just start by looking right
		// string[] directions = { "up", "right", "down", "left" };
		// int currentDirectionFromStart = 0;
		//
		// for (int i = 0; i < directions.Length; i++)
		// {
		// 	
		// }
		
			upChar = rawMap[currentPosition.X, currentPosition.Y - 1];
			rightChar = rawMap[currentPosition.X + 1, currentPosition.Y];
			downChar = rawMap[currentPosition.X, currentPosition.Y + 1];
			leftChar = rawMap[currentPosition.X - 1, currentPosition.Y];

			if (upChar == '|') //move up
			{
				currentPosition.Y -= 1;
			}
			else if (upChar == 'F') //move up and right
			{	
				currentPosition.Y -= 1; currentPosition.X += 1;
			}

			
			if ('Y' == 'S')
			{
				Console.WriteLine("back to the beginning");
				//cycleFound = true;
			}
		}
	}
	
	
	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input_test.txt");
		
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
					rawMap[i,j] = c;
					if (c == 'S')
					{
						startPosition = new Day10Main.Coords(i, j);
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