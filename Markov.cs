using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
	// Parses a txt file with name into a list data structure. Makes a Markov List. Generates a new unique name.
	public class Markov{
		private List<String> knownNames;
		private Dictionary<String,List<String>> markovList;
		private string order = "__";
		public Markov(){
			markovList = new Dictionary<String,List<String>>();
			knownNames = Parser.generateList();
		}	
		public void outputCommonNames(){
			foreach (string name in knownNames){
				Console.WriteLine(name);
			}
		}
		public void outputMarkovModel(){
			foreach (KeyValuePair<String,List<String>> lst in markovList){
				Console.Write(lst.Key + " ");
				foreach (string combo in lst.Value){
					Console.Write(combo);
				}
				Console.WriteLine();
			}
		}
		public void generateMarkovModel(){
			foreach (string name in knownNames){
				generateMarkovModelHelper(name);
			}
		}
		public void generateMarkovModelHelper(string name){
			int nextIndex = 1;
			for(int index=0; index < name.Length-1; index++){
				if(!name[nextIndex].ToString().Equals("_") && name[nextIndex].ToString().Equals("_")){
					markovList[name[index-1].ToString() + name[nextIndex-1].ToString()].Add("_");
					break;
				}
				
				if(markovList.ContainsKey(name[index].ToString() + name[nextIndex].ToString())){
					markovList[name[index].ToString() + name[nextIndex].ToString()].Add(name[nextIndex+1].ToString());	
				}
				else{
					markovList.Add(name[index].ToString() + name[nextIndex].ToString(), new List<String>());
					markovList[name[index].ToString() + name[nextIndex].ToString()].Add(name[nextIndex+1].ToString());
				}
				if(nextIndex != name.Length-2){
					nextIndex++;
				}
				
			}
		}
		public string generateNewName(){
			string name = order;
			string temp = name;
			Random r = new Random();
			while(true){
				int getNextNumber = r.Next(markovList[temp].Count);
				string next = markovList[temp].ElementAt(getNextNumber);
				if(!next.Equals("_")){
					name+= next;
					temp+= next;
				}
				else{
					if(checkIfUniqueName(name) == true){
						break;
					}
					name = order;
					temp = name;
				}
				if(!temp.Equals("__")){
					temp = temp.Substring(1);
				}
				
			}
			name = name.Substring(2);
			return name;
		}
		public bool checkIfUniqueName(String name){
			name = name + "_";
			if(knownNames.Contains(name)){
				return false;
			}
			return true;
		}
	}
