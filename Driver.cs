using System;
namespace DriverApplication
{	// Runner class to run the Markov Model.
	class Driver{
		static void Main(String[] args){
			Markov m = new Markov();
			m.generateMarkovModel();

			Console.WriteLine("=== Name Generator ===");
			Console.WriteLine("Generate unique names based on the sample list.");

			while(true){
				Console.WriteLine();
				Console.WriteLine("1) Generate a new name");
				Console.WriteLine("2) Generate multiple names");
				Console.WriteLine("3) Start web UI");
				Console.WriteLine("4) Exit");
				Console.Write("Choose an option: ");

				string choice = Console.ReadLine();
				if(choice == "1"){
					string name = m.generateNewName();
					Console.WriteLine("Generated: " + name);
				}
				else if(choice == "2"){
					Console.Write("How many names? ");
					string countInput = Console.ReadLine();
					if(int.TryParse(countInput, out int count) && count > 0){
						for(int i = 0; i < count; i++){
							string name = m.generateNewName();
							Console.WriteLine((i + 1) + ". " + name);
						}
					}
					else{
						Console.WriteLine("Please enter a positive number.");
					}
				}
				else if(choice == "3"){
					WebServer server = new WebServer(m, "web");
					server.Start();
				}
				else if(choice == "4"){
					Console.WriteLine("Goodbye!");
					break;
				}
				else{
					Console.WriteLine("Invalid option. Please choose 1, 2, 3, or 4.");
				}
			}
		}
	}
}
