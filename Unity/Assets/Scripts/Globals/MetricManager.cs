using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MetricManager : MonoBehaviour {

	string scorerString = "", createText = "", totalText = "";
	List<string> text;

	int blueJumps = 0, redJumps = 0;

	void Start () { text = new List<string>(); }
	void Update () {}
	
	//When the game quits we'll actually write the file.
	void OnApplicationQuit()
	{
		GenerateMetricsString();
		string time = System.DateTime.UtcNow.ToString();
		//string dateTime = System.DateTime.Now.ToString(); //Get the time to tack on to the file name
		time = time.Replace("/", "-"); //Replace slashes with dashes, because Unity thinks they are directories..
		time = time.Replace(":", "."); //Replace slashes with dashes, because Unity thinks they are directories..
		string reportFile = "GameName_Metrics_" + time + ".txt"; 
		File.WriteAllText (reportFile, totalText);
		//In Editor, this will show up in the project folder root (with Library, Assets, etc.)
		//In Standalone, this will show up in the same directory as your executable
	}

	void GenerateMetricsString()
	{
		foreach (string str in text)
		{
			totalText = totalText + str + "\n\n";
		}
	}

	public void GenerateEntry()
	{
		createText = 
			"End of period: " + scorerString + '\n' +
			"Number of blue jumps: " + blueJumps + '\n' +
			"Number of red jumps: " + redJumps;
		text.Add(createText);
		createText = "";
		scorerString = "";
		blueJumps = 0;
		redJumps = 0;
	}

	public void AddToBlue()
	{
		blueJumps++;
	}
	public void AddToRed()
	{
		redJumps++;
	}
	public void setScorer(string scorer)
	{
		scorerString = scorer;
	}
}
