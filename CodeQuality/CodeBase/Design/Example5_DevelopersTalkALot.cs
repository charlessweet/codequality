using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaughingMonkey.CodeQuality.Design
{
    public class Example5_DevelopersTalkALot
    {
        public class UnfortunatelyLongClass
        {
            /// <summary>
            /// Converts the first number represented to the base represented.
            /// </summary>
            /// <remarks>
            /// Only supports up to base 10.
            /// </remarks>
            public string AddTwoNumbersRepresentedAsStringsAndConvertBases(string firstNumber, string secondNumber, string firstBase,
                string secondBase)
            {
                int firstBaseInt = 0;
                if (!Int32.TryParse(firstBase, out firstBaseInt))
                    throw new ArgumentOutOfRangeException("firstBase", "The number could not be parsed.");

                int secondBaseInt = 0;
                if (!Int32.TryParse(secondBase, out secondBaseInt))
                    throw new ArgumentOutOfRangeException("secondBase", "The number could not be parsed.");

                int firstInt = 0;
                if(!Int32.TryParse(firstNumber, out firstInt))
                    throw new ArgumentOutOfRangeException("firstNumber","The number could not be parsed.");
                int firstNumberBase10 = 0;
                double firstNumberConverted = 0;
                int exponent = 0;
                for(int i = firstNumber.Length - 1; i > -1; i--)
                {
                    int digit = Int32.Parse(firstNumber[i] + "");
                    firstNumberConverted += digit * Math.Pow(firstBaseInt, exponent);
                    exponent++;
                }
                firstNumberBase10 = (int)firstNumberConverted;

                int secondInt = 0;
                if (!Int32.TryParse(secondNumber, out secondInt))
                    throw new ArgumentOutOfRangeException("secondNumber", "The number could not be parsed.");
                int secondNumberBase10 = 0;
                double secondNumberConverted = 0;
                exponent = 0;
                for (int i = secondNumber.Length - 1; i > -1; i--)
                {
                    int digit = Int32.Parse(secondNumber[i] + "");
                    secondNumberConverted += digit * Math.Pow(secondBaseInt, exponent);
                    exponent++;
                }
                secondNumberBase10 = (int)secondNumberConverted;

                //we'll return the number in the second base
                //so conversion goes from first to second base
                int added = firstNumberBase10 + secondNumberBase10;
                int numDigits = (int)Math.Log(added, secondBaseInt);
                StringBuilder sb = new StringBuilder(numDigits);
                for(int i = numDigits; i > -1; i--)
                {
                    int threshold = (int)Math.Pow(secondBaseInt, i);
                    if(added > threshold)
                    {
                        int digit = added / threshold;//integer division
                        sb.Append(digit);
                        added -= threshold * digit;
                    }
                    else
                    {
                        sb.Append(0);
                    }
                }
                return sb.ToString();
            }
        }

        public class RefactoredToShorterClass
        {
            /// <summary>
            /// Converts the first number represented to the base represented.
            /// </summary>
            /// <remarks>
            /// Only supports up to base 10.
            /// </remarks>
            public string AddTwoNumbersRepresentedAsStringsAndConvertBases(string firstNumber, string secondNumber, string firstBase,
                string secondBase)
            {
                int firstBaseInt = ValidateParameter("firstBase", firstBase);

                int secondBaseInt = ValidateParameter("secondBase", secondBase);

                ValidateParameter("firstNumber", firstNumber);

                ValidateParameter("secondNumber", secondNumber);

                int firstNumberBase10 = this.ConvertToBase10(firstNumber, firstBaseInt);

                int secondNumberBase10 = this.ConvertToBase10(secondNumber, secondBaseInt);

                //we'll return the number in the second base
                //so conversion goes from first to second base
                int added = firstNumberBase10 + secondNumberBase10;

                return ConvertToBaseString(added, secondBaseInt);
            }
            public int ValidateParameter(string parameterName, string parameterValue)
            {
                int parameterInt = 0;
                if (!Int32.TryParse(parameterValue, out parameterInt))
                    throw new ArgumentOutOfRangeException(parameterName, "The number could not be parsed.");
                return parameterInt;
            }

            public string ConvertToBaseString(int number, int targetBase)
            {
                int numDigits = (int)Math.Log(number, targetBase);
                StringBuilder sb = new StringBuilder(numDigits);
                for (int i = numDigits; i > -1; i--)
                {
                    int threshold = (int)Math.Pow(targetBase, i);
                    if (number > threshold)
                    {
                        int digit = number / threshold;//integer division
                        sb.Append(digit);
                        number -= threshold * digit;
                    }
                    else
                    {
                        sb.Append(0);
                    }
                }
                return sb.ToString();
            }

            public int ConvertToBase10(string number, int sourceBase)
            {
                double numberConverted = 0;
                int exponent = 0;
                for (int i = number.Length - 1; i > -1; i--)
                {
                    int digit = Int32.Parse(number[i] + "");
                    numberConverted += digit * Math.Pow(sourceBase, exponent);
                    exponent++;
                }
                return (int)numberConverted;
            }
        }
    }
}
