using System;
namespace DriverApplication
{	// Runner class to run the Markov Model.
	class Driver{
		static void Main(String[] args){
			Markov m = new Markov();
			m.generateMarkovModel();
			m.generateNewName();
		}
	}
}