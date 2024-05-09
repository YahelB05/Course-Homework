using OOP_Practice.NumericalExpressionExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Practice.NumericalExpressionExercise
{
    internal class NumericalExpression
    {
        private long Value { get; set; }
        private ILanguage Language { get; set; }

        public NumericalExpression(long value, ILanguage language)
        {
            Value = value;
            Language = language;
        }

        // Default language = english, if no other language was given in the other overloaded constructor - this will be chosen.
        public NumericalExpression(long value)
        {
            Value = value;
            Language = new EnglishLanguage();
        }

        // Copy Constructor Overload
        public NumericalExpression(NumericalExpression other)
        {
            Value = other.Value;
            Language = other.Language;
        }

        public long GetValue()
        {
            return Value;
        }

        /// <summary>
        /// Calculates the sum of letters that every number in the range of [0 <= value] (in their literal form) have combined (without spaces).
        /// </summary>
        /// <param name="value">Max number</param>
        /// <param name="Language">Language to translate the number to (in literal representation).</param>
        /// <returns>Sum of letters</returns>
        public static long SumLetters(long value, ILanguage Language = null)
        {
            long sum = 0;
            NumericalExpression numericalExpression;

            if (Language != null)
                numericalExpression = new NumericalExpression(0, Language);
            else
                numericalExpression = new NumericalExpression(0);

            for (long i = 0; i <= value; i++)
            {
                numericalExpression.Value = i;
                sum += numericalExpression.ToString().Replace(" ", string.Empty).Length;
            }

            return sum;
        }

        // Polymorphism: Method Overloading - Method with the same name, different parameters and implementation.
        // The runtime environment will choose the correct method to call with the given parameters.
        public static long SumLetters(NumericalExpression numericalExpression)
        {
            long sum = 0;
            NumericalExpression tempNumericalExpression = new NumericalExpression(numericalExpression);
            for (long i = numericalExpression.Value; i >= 0; i--)
            {
                tempNumericalExpression.Value = i;
                sum += tempNumericalExpression.ToString().Replace(" ", string.Empty).Length;
            }

            return sum;
        }

        public override string ToString()
        {
            return Language.ToLiteral(Value);
        }
    }
}
