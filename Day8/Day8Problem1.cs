using System.Text.RegularExpressions;
namespace Day8;

public class Day8Problem1
{
	private const string START_NODE = "AAA";
	private const string STOP_NODE = "ZZZ";
	
	private string? LRInstructionsLine;
	private List<Dictionary<string,string>> nodes;

	public void Run()
	{
		nodes = new List<Dictionary<string,string>>();
		LoadData();
		TraverseNodes();
	}

	void TraverseNodes()
	{
		int currentInstructionIndex = 0;
		int stepsTaken = 1;
		bool reachedTheEnd = false;
		string currentInstruction;
		Dictionary<string,string>? currentNode = nodes.FirstOrDefault(
			theNode => theNode["nodeId"] == START_NODE
		);
		
		while (!reachedTheEnd)
		{
			currentInstruction = LRInstructionsLine[currentInstructionIndex].ToString();

			Console.WriteLine("for node "+currentNode["nodeId"]+", get "+currentInstruction+", which is "
			                  +currentNode[currentInstruction]);
			currentNode = nodes.FirstOrDefault(
				theNode => theNode["nodeId"] == currentNode[currentInstruction]);
			if (currentNode["nodeId"] == STOP_NODE)
			{
				Console.WriteLine("reached ZZZ with "+stepsTaken+" steps");
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
				nodes.Add(newNode);
			}
			file.Close();
		}
		else { Console.WriteLine($"no file found at {path}"); }
	}
	
}