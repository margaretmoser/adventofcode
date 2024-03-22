using System.Text.RegularExpressions;
namespace Day8;

public class Day8Problem2
{
	private const string START_NODE = "AAA";
	private const string STOP_NODE = "ZZZ";
	
	private string? LRInstructionsLine;
	private List<Dictionary<string,string>> allNodes;
	private List<Dictionary<string,string>> startingNodes;
	
	public void Run()
	{
		allNodes = new List<Dictionary<string,string>>();
		startingNodes = new List<Dictionary<string, string>>();
		LoadData();
		TraverseNodes();
	}


	
	void TraverseNodes()
	{
		int currentInstructionIndex = 0;
		int stepsTaken = 1;
		bool reachedTheEnd = false;
		string currentInstruction;

		List<Dictionary<string, string>> nodeSet = startingNodes;
		
		while (!reachedTheEnd)
		{
		
			currentInstruction = LRInstructionsLine[currentInstructionIndex].ToString();
			Dictionary<string, string> currentNode;

			for (int i = 0; i < nodeSet.Count; i++)
			{
				currentNode = nodeSet[i];
				// Console.WriteLine("for node "+currentNode["nodeId"]+", get "+currentInstruction+", which is "
				//                   +currentNode[currentInstruction]);
				nodeSet[i] = allNodes.FirstOrDefault(
					theNode => theNode["nodeId"] == currentNode[currentInstruction]);;
			}
			
			// int nodesWithTerminalZs = (from n in nodeSet
			// 	where n["nodeId"][2] == 'A'
			// 	select n).ToList().Count();
			
			if (nodeSet.All(n => n["nodeId"][2] == 'Z'))
			{
				Console.WriteLine("all nodes have Zs with "+stepsTaken+" steps");
				return;
			}

			stepsTaken++;
			currentInstructionIndex++;
			if (currentInstructionIndex >= LRInstructionsLine.Length)
			{
				currentInstructionIndex = 0;
			}
		}
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