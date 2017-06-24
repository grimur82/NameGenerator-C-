using System;
using System.Collections;
using System.Collections.Generic;
// Basic parser for a text file to data.
public abstract class Parser{
	public static List<String> generateList(){
		List<String> names = new List<String>();
		string[] getLines = System.IO.File.ReadAllLines(@"test.txt");
		string order = "__";
		string endLine = "_";
		foreach(string line in getLines){
			names.Add(order+line+endLine);
		}
		return names;
	}
}