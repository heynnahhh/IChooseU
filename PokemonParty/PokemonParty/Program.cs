using PokemonParty.Net.Utilities;
using PokeAPI.Net;
using System.Text.RegularExpressions;

namespace PokemonParty.Net
{
    class Program
    {
        /// <summary>
        /// Entrypoint of the console app
        /// </summary>
        static async Task Main(string[] args)
        {
            //TO DO: Save in text file and create utility to read from file
            Console.WriteLine("Welcome to IChooseU: A Pokemon Team Generator! \r\n" +
                "This app shall give you a Team of 5 random Pokemons depending on the type you want to choose.");

            bool shouldCreateTeam = false;

            do
            {
                string message = "\r\nAre you ready to choose your Pokemons? [y/n] ";

                bool isReady = Utils.Confirmation(message);
                string generatedTeam = String.Empty;


                if (isReady)
                {
                    Console.Clear();
                    generatedTeam = await OnStart();

                    if (generatedTeam != string.Empty)
                    {
                        Console.WriteLine("\r\nCongrats you have created your Pokemon Team! \r\n" +
                        "Team Members: {0}", generatedTeam);
                    }
                    else
                    {
                        Console.WriteLine("\r\nThere was a problem generating your team. Please try again..");
                    }

                    bool isHappy = Utils.Confirmation("\r\nAre you satisfied with your team? [y/n] ");

                    if (!isHappy)
                    {
                        shouldCreateTeam = Utils.Confirmation("\r\nDo you wish to generate another team? [y/n] ");
                    }
                    else
                    {
                        shouldCreateTeam = false;
                    }
                }
                else
                {
                    shouldCreateTeam = false;
                }

            }
            while (shouldCreateTeam);

            Console.WriteLine("\r\nThank you for using IChooseU. Have a nice day!");

        }

        /// <summary>
        /// Initialization of Inputs
        /// </summary>
        private static async Task<string> OnStart()
        {
            //TO DO: Get types from pokeapi
            Console.WriteLine("There are currently 18 types of Pokemon. \r\n" +
                "Normal, Fire, Fighting, Water, Flying, Grass, Poison, Electric, Ground \r\n" +
                "Psychic, Rock, Ice, Bug, Dragon, Ghost, Dark, Steel and Fairy...");
            
            await Task.Delay(3000);
            Console.WriteLine("\r\nOut of these, You can pick 1 up to 5 types to assemble your team.");
            
            await Task.Delay(1000);
            string message = "\r\nInput how many types you wish to choose [1-5]: ";

            int typeCount = Utils.ProcessIntInput(message);
            string pokemonTeam = String.Empty;

            try
            {
                int counter = 1;
                List<string> typeList = new List<string>();
                string validInput = String.Empty;

                while (counter <= typeCount)
                {
                    Console.Write("\r\nInput Pokemon type {0}: ", counter);
                    validInput = Utils.RegexValidation(Console.ReadLine());
                    if(validInput != string.Empty)
                    {
                        typeList.Add(validInput); //input validation here
                        counter++;
                    }
                    else
                    {
                        Console.WriteLine("Please choose a valid type from the list");
                    }
                }

                if (typeList.Count > 0)
                {
                    Console.WriteLine("\r\nPlease wait while I generate your team...");
                    pokemonTeam = await PokeAPIWebRequest.GenerateTeamAsync(typeList);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pokemonTeam;

        } 
        
    }
}