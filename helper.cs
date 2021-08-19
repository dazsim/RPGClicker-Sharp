using Godot;
using System;


public class helper : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private System.Collections.Generic.Dictionary<int, string[]> MasterList;
	private System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, string[]>> Generators;
	private System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, string[]>> Characters;
	private System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, string[]>> Upgrades;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MasterList = importCSV("data/characters/master.txt");
		
		Characters = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, string[]>>();
		Generators = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, string[]>>();
		Upgrades = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, string[]>>();
		foreach(int key in MasterList.Keys){
			//GD.Print(MasterList[key]);
			var tmp = importCSV("data/characters/"+MasterList[key][1].ToLower()+".txt");
			
			Characters.Add(key,tmp);
			tmp = importCSV("data/generators/"+MasterList[key][1].ToLower()+".txt");
			Generators.Add(key,tmp);
			tmp = importCSV("data/upgrades/"+MasterList[key][1].ToLower()+".txt");
			Upgrades.Add(key,tmp);
			
		}
		/*foreach (int key in Generators.Keys){
			foreach(int k in Generators[key].Keys){
				//GD.Print(Generators[key][k]);
				for(int j = 0;j < Generators[key][k].Length;j++){
					//GD.Print(Generators[key][k][j]);
				}
			}
		}*/
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	public static System.Collections.Generic.Dictionary<int, string[]> importCSV(string csv_file)
	{
		var dict = new System.Collections.Generic.Dictionary<int,string[]>();
		var headers = new System.Collections.Generic.Dictionary<int,string[]>();
		//GD.Print(csv_file);
		//return dict;
		 
		var file = new File();
		
		file.Open(csv_file,Godot.File.ModeFlags.Read);
		var isHeader = true;
		var index = 0;
		while (!file.EofReached())
		{
			// read in new entry in csv
			if (isHeader)
			{
				var input = file.GetCsvLine();
				isHeader = false;
			} else
			{
				// process data
				var tempDict = file.GetCsvLine();
				
				dict.Add(index,tempDict);
				index++;
				
			}
			
		}
		file.Close();
		return dict;
		
	}
}
