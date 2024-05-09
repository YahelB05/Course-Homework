using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Practice.NumericalExpressionExercise
{
    internal class EnglishLanguage : ILanguage
    {
        // ------------------------------------ Static Fields ------------------------------------
        private static readonly Dictionary<int, string> digitToLiteral = new Dictionary<int, string>
        {
            {0, "Zero"},
            {1, "One"},
            {2, "Two"},
            {3, "Three"},
            {4, "Four"},
            {5, "Five"},
            {6, "Six"},
            {7, "Seven"},
            {8, "Eight"},
            {9, "Nine"}
        };

        private static readonly Dictionary<int, string> tenTwentyToLiteral = new Dictionary<int, string>
        {
            {10, "Ten"},
            {11, "Eleven"},
            {12, "Twelve"},
            {13, "Thirteen"},
            {14, "Fourteen"},
            {15, "Fifteen"},
            {16, "Sixteen"},
            {17, "Seventeen"},
            {18, "Eighteen"},
            {19, "Nineteen"}
        };

        private static readonly Dictionary<int, string> tensToLiteral = new Dictionary<int, string>
        {
            {2, "Twenty"},
            {3, "Thirty"},
            {4, "Forty"},
            {5, "Fifty"},
            {6, "Sixty"},
            {7, "Seventy"},
            {8, "Eighty"},
            {9, "Ninety"}
        };

        private static readonly Dictionary<int, string> numberPartPositionToTitle = new Dictionary<int, string>
        {
            {2, "Thousand"},
            {3, "Million"},
            {4, "Billion"},
            {5, "Trillion"}
        };

        // ------------------------------------ Interface Implementation -------------------------

        /// <summary>
        /// Maps a number to its literal string representation.
        /// </summary>
        /// <param name="value">The number to be mapped to its literal string representation</param>
        /// <returns>Number's literal string representation</returns>
        public string ToLiteral(long value)
        {
            // If the given long is contained inside one of the HashMaps - return it immediately without calculations:
            if (ReturnImmediateLiteral(value) != null)
                return ReturnImmediateLiteral(value);

            // Calculate the Literal to the given long:
            List<int> numberParts = new List<int>();
            string numberStr = Convert.ToString(value);

            // Spread the number into parts:
            // "12,355,999" -> ["999", "355", "12"]
            string currNumber = "";
            int threeCounter = 0;
            for (int i = numberStr.Length - 1; i >= 0; i--)
            {
                if (threeCounter == 3)
                {
                    numberParts.Add(Convert.ToInt32(currNumber));
                    currNumber = "";
                    threeCounter = 0;
                }
                currNumber = numberStr[i] + currNumber;
                threeCounter++;
            }

            if (!string.IsNullOrEmpty(currNumber))
                numberParts.Add(Convert.ToInt32(currNumber));

            // Handle number parts - each number part gets mapped to a string literal and gets a title if needed (million/billion etc...):
            List<string> literals = new List<string>();
            for (int i = numberParts.Count - 1; i >= 0; i--)
            {
                if (numberParts.ElementAt(i) == 0)
                    continue;

                string title = numberPartPositionToTitle.TryGetValue(i + 1, out string text) ? text : "";
                literals.Add($"{HandleNumberPart(numberParts.ElementAt(i))} {title}".Trim());
            }

            // Joins all the number parts and their titles into one string:
            return string.Join(" ", literals.ToArray());
        }

        /// <summary>
        /// Maps a number between 0 <= numberPart <= 999 to its literal string representation.
        /// </summary>
        /// <param name="numberPart">Number between 0 <= numberPart <= 999 to be mapped to a string literal</param>
        /// <returns>Number's literal string representation</returns>
        private string HandleNumberPart(int numberPart)
        {
            List<string> literals = new List<string>();
            if (numberPart / 100 > 0)
                literals.Add(digitToLiteral[numberPart / 100] + " Hundred");
            if (tenTwentyToLiteral.ContainsKey(numberPart % 100))
            {
                literals.Add(tenTwentyToLiteral[numberPart % 100]);
            }
            else
            {
                if (numberPart % 100 / 10 > 0)
                    literals.Add(tensToLiteral[numberPart % 100 / 10]);
                if (numberPart % 10 > 0)
                    literals.Add(digitToLiteral[numberPart % 10]);
            }

            return string.Join(" ", literals.ToArray());
        }

        /// <summary>
        /// Checks if the given number exists in one of the static Dictionaries as a key.
        /// </summary>
        /// <param name="number">Number to check if exists in one of the Dictionaries</param>
        /// <returns>If exists, returns the value of the key-value pair. Otherwise, returns null.</returns>
        private string ReturnImmediateLiteral(long number)
        {
            try
            {
                string literal = null;
                if (digitToLiteral.ContainsKey(Convert.ToInt32(number)))
                    literal = digitToLiteral[Convert.ToInt32(number)];
                else if (tenTwentyToLiteral.ContainsKey(Convert.ToInt32(number)))
                    literal = tenTwentyToLiteral[Convert.ToInt32(number)];

                return literal;
            }
            catch (OverflowException) // if the given long is larger than Int32.MaxValue
            {
                return null;
            }
        }
    }
}
