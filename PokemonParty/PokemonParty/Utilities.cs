using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PokemonParty.Net.Utilities
{
    /// <summary>
    /// Utility Methods that can be imported as needed
    /// </summary>
    public class Utils
    {

        public static void ShowMessage(string message)
        {
            Console.Write(message);
        }

        #region Input Validations
        public static int ProcessIntInput(string message)
        {
            //bool isValid = false;
            ConsoleKeyInfo inputKeyInfo;
            int doCounter = 0;
            int parsedKey = 0;

            do
            {
                if (doCounter > 0)
                {
                    message = "\r\nPlease choose from 1 - 5 only: ";
                }

                ShowMessage(message);
                inputKeyInfo = Console.ReadKey(false);

                if (Char.IsDigit(inputKeyInfo.KeyChar))
                {
                    parsedKey = Convert.ToInt32(inputKeyInfo.KeyChar.ToString());
                }

                doCounter++;

            }
            while (parsedKey > 5 || parsedKey < 1);

            return parsedKey;
        }


        public static bool Confirmation(string message)
        {
            bool isConfirmed = false;
            ConsoleKey response;

            do
            {
                ShowMessage(message);
                response = Console.ReadKey(false).Key;
            }
            while (response != ConsoleKey.Y && response != ConsoleKey.N);

            isConfirmed = (response == ConsoleKey.Y);
            return isConfirmed;
        }

        public static string RegexValidation(string input)
        {
            input = input.ToLower();
            string[] pokemonTypes = { "dark","normal","fire","fighting","water","flying","grass","poison","electric","ground", "psychic", "rock", "ice", "bug", "dragon", "ghost", "steel", "fairy" };
            string regExPattern = String.Join("|", pokemonTypes);

            Regex regexValidation = new Regex(@"^("+regExPattern+")$");

            string validInput = (regexValidation.IsMatch(input)) ? input : "";

            return validInput;
        }

        #endregion
    }
}
