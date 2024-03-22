using System.Text.RegularExpressions;
namespace Day8;

public class Day8Problem2
{

	private string? LRInstructionsLine;
	private List<Dictionary<string,string>> allNodes;
	private List<Dictionary<string,string>> startingNodes;
	private Dictionary<string, long> pathLengths;
	
	public void Run()
	{
		allNodes = new List<Dictionary<string,string>>();
		startingNodes = new List<Dictionary<string, string>>();
		pathLengths = new Dictionary<string, long>();
		LoadData();
		TraverseNodes();
	}


	
	void TraverseNodes()
	{
		for (int i = 0; i < startingNodes.Count; i++)
		{
			//follow the path individually for each starting node and determine
			//how many steps it will take
			pathLengths.Add(startingNodes[i]["nodeId"], CountStepsToEndForNode(startingNodes[i]));
		}

		foreach (string nodeId in pathLengths.Keys)
		{
			Console.WriteLine("Node " + nodeId + " required " + pathLengths[nodeId].ToString() + " steps");
		}
		
		Console.WriteLine("Total steps needed is "+CalculateLCM(pathLengths.Values.ToList()));
		
	}

	int CountStepsToEndForNode(Dictionary<string, string> currentNode)
	{
		int currentInstructionIndex = 0;
		int stepsTaken = 1;
		string currentInstruction;
		
		while (true)
		{
			currentInstruction = LRInstructionsLine[currentInstructionIndex].ToString();

			Console.WriteLine("for node "+currentNode["nodeId"]+", get "+currentInstruction+", which is "
			                  +currentNode[currentInstruction]);
			currentNode = allNodes.FirstOrDefault(
				theNode => theNode["nodeId"] == currentNode[currentInstruction]);
			if (currentNode["nodeId"][2] == 'Z')
			{
				Console.WriteLine("reached end with "+stepsTaken+" steps");
				return stepsTaken;
			}

			stepsTaken++;
			currentInstructionIndex++;
			if (currentInstructionIndex >= LRInstructionsLine.Length)
			{
				currentInstructionIndex = 0;
			}
		}
		return -1;
	}

	long CalculateLCM(List<long> numbers)
	{
		//thanks to https://stackoverflow.com/questions/147515/least-common-multiple-for-3-or-more-numbers/29717490#29717490
		return numbers.Aggregate(lcm);
	}
	
	static long lcm(long a, long b)
	{
		return Math.Abs(a * b) / GCD(a, b);
	}
	static long GCD(long a, long b)
	{
		return b == 0 ? a : GCD(b, a % b);
	}
	
	void LoadData()
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
		Regex nodePattern = new Regex(@"(\w*)\W\=\W\((\w*)\,\W(\w*)\)",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);
		
		if (File.Exists(path))
		{
			using StreamReader file = new StreamReader(path);
			LRInstructionsLine = file.ReadLine();
			Console.WriteLine("instructions: "+LRInstructionsLine);
			file.ReadLine();
			while (file.ReadLine() is { } ln)
			{
				GroupCollection g = nodePattern.Match(ln).Groups;
				Dictionary<string, string> newNode = new Dictionary<string, string>();
				newNode.Add("nodeId", g[1].Value);
				newNode.Add("L", g[2].Value);
				newNode.Add("R", g[3].Value);
				//Console.WriteLine("processed node "+newNode.ToString());
				allNodes.Add(newNode);
				if (newNode["nodeId"][2] == 'A')
				{
					startingNodes.Add(newNode);
				}
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}

/* just keeping this bc it might be useful sometime
		startingNodes = (from n in nodes
			where n["nodeId"][2] == 'A'
			select n).ToList();
*/