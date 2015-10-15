using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {

        static void Main(string[] args)
        {

            //This val stores the final answer.
            double result = 0;

            //This list gets two values from the user and stores them

            var userNumbers = new List<double>();
            userNumbers.Add(GetValue("Value 1: "));
            userNumbers.Add(GetValue("Value 2: "));

            var largeToSmall = new Dictionary<bool, List<double>>();
            largeToSmall[true] = userNumbers.OrderByDescending(x => x).ToList();
            //largeToSmall[false] = userNumbers.OrderBy(x => x).ToList();

            bool whileCycle = true;

            while (whileCycle)
            {
                Console.WriteLine("");
                Console.WriteLine("What kind of operation will this be?");
                Console.Write("(A)DD, (S)UBTRACT, (M)ULTIPLY, or (D)IVIDE? \n");
                ConsoleKeyInfo selection = Console.ReadKey();
                string operation = selection.Key.ToString().ToUpper();

                switch (operation)
                {
                    case "A":
                        Console.WriteLine("\nADD\n");
                        result = AddValues(largeToSmall[true]);
                        whileCycle = false;
                        break;

                    case "S":
                        Console.WriteLine("\nSUBTRACT\n");
                        //SubDivKind determines if the larger or smaller number should be positioned before the minus symbol
                        //SubDivKind then calls the actual subtraction method with the correct value placement.
                        result = SubDivKind(largeToSmall[true], true);
                        whileCycle = false;
                        break;

                    case "M":
                        Console.WriteLine("\nMULTIPLY\n");
                        result = MultValues(largeToSmall[true]);
                        whileCycle = false;
                        break;

                    case "D":
                        Console.WriteLine("\nDIVIDE\n");
                        //SubDivKind determines if the larger or smaller number should be positioned before the divison symbol
                        //SubDivKind then calls the actual division method with the correct value placement.
                        result = SubDivKind(largeToSmall[true], false);
                        whileCycle = false;
                        break;

                    default:
                        Console.WriteLine("");
                        Console.WriteLine("Please select a valid operation.");
                        result = 0;
                        continue;
                }
                break;
            }

            Console.WriteLine("\n\nThe answer is " + result);
        }

        /// <summary>
        /// This method uses a string to prompt the user for a double.
        /// If the user input does not parse as a double, the user is prompted to enter a double
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        private static double GetValue(string label)
        {
            double value = 0;
            var isInvalid = true;

            while (isInvalid)
            {
                Console.Write(label);
                string input = Console.ReadLine();
                isInvalid = !Double.TryParse(input, out value);
                if (isInvalid)
                {
                    Console.WriteLine("That input does not parse as a number");
                }
            }

            return value;
        }

        /// <summary>
        /// This method adds the first double in a list to the second double in the list
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static double AddValues(List<double> numbers)
        {
            Console.WriteLine("\n");
            return numbers.ElementAt(0) + numbers.ElementAt(1);
        }

        /// <summary>
        /// This method subtracts the first double in a list from the second double
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private static double SubValues(List<double> numbers)
        {
            double value1 = numbers.ElementAt(0);
            double value2 = numbers.ElementAt(1);

            Console.WriteLine("\n{0} subtracted from {1}:\n", value1, value2);

            return (value2 - value1);
        }

        /// <summary>
        /// This method multiplies the first two doubles in a list and returns a double
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private static double MultValues(List<double> numbers)
        {
            Console.WriteLine("\n");
            return (numbers.ElementAt(0) * numbers.ElementAt(1));
        }


        /// <summary>
        /// This method takes two doubles in a list and returns the result of value1 / value2
        /// </summary>
        /// <param name="value1">The larger number</param>
        /// <param name="value2">The smaller number</param>
        /// <returns>The result of value1 / value2</returns>
        private static double DivValues(List<double> numbers)
        {
            double value1 = numbers.ElementAt(0);
            double value2 = numbers.ElementAt(1);

            double result = value1 / value2;
            return result;

        }

        /// <summary>
        /// This method takes two doubles and a bool. The bool determines subtraction (true) or division (false).
        /// It requires user input to specify if the larger number should subtract/divide the smaller, or vice versa
        /// </summary>
        /// <param name="larger"></param>
        /// <param name="smaller"></param>
        /// <param name="shouldSubtract"></param>
        /// <returns></returns>
        private static double SubDivKind(List<double> orderedUserNumbers, bool shouldSubtract)
        {
            double result = 0;

            //This var controls the While cycle
            bool whileCycle = true;

            //This section takes the order list of doubles and assigns them to the appropriate vars for user interaction
            double larger = orderedUserNumbers.ElementAt(0);
            double smaller = orderedUserNumbers.ElementAt(1);

            //This section handles the grammer for the user interaction strings
            string fromBy;
            if (shouldSubtract)
            {
                fromBy = "from";
            }
            else
            {
                fromBy = "by";
            }

            //This while loop ensures that the user submits the correct input when choosing which number to subtract from or divide by.
            while (whileCycle)
            {

                //If the user is doing division, and has entered a "0" as one of the values, the operation should return "0".
                //Otherwise the operation should proceed as coded.
                if (!shouldSubtract && (larger == 0 || smaller == 0))
                {
                    return result;
                }
                else
                {

                    //If the entered numbers are the same, the user should not choose whether to subtract or divide one by the other.
                    if (larger == smaller && shouldSubtract)
                    {
                        result = 0;
                        whileCycle = false;
                    }
                    if (larger == smaller && !shouldSubtract)
                    {
                        result = 1;
                        whileCycle = false;
                    }
                    else
                    {
                        Console.WriteLine("\n\nDo you want to {0} {1} {2} {3}?\n\n", SubtractOrDivide(shouldSubtract), larger, fromBy, smaller);
                        Console.WriteLine("\nIf you want to {0} {1} {2} {3}, please press 'Y'", SubtractOrDivide(shouldSubtract), larger, fromBy, smaller);
                        Console.WriteLine("\nIf you want to {0} {3} {2} {1}, please press 'N'\n", SubtractOrDivide(shouldSubtract), larger, fromBy, smaller);

                        ConsoleKeyInfo selection = Console.ReadKey();
                        string yesNo = selection.KeyChar.ToString().ToUpper();

                        if (yesNo == "Y" && shouldSubtract == true)
                        {
                            result = SubValues(orderedUserNumbers);
                            whileCycle = false;
                            break;
                        }
                        if (yesNo == "Y" && !shouldSubtract == true)
                        {
                            result = DivValues(orderedUserNumbers);
                            whileCycle = false;
                            break;
                        }
                        if (yesNo == "N" && shouldSubtract == true)
                        {
                            result = SubValues(orderedUserNumbers.OrderBy(x => x).ToList());
                            whileCycle = false;
                            break;
                        }
                        if (yesNo == "N" && !shouldSubtract == true)
                        {
                            result = DivValues(orderedUserNumbers.OrderBy(x => x).ToList());
                            whileCycle = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nPlease select a valid option.\n");
                            continue;
                        }

                    }

                }


            }

            return result;

        }

        /// <summary>
        /// This is a boolean switch. If 'shouldSubtract' has previously been set to 'yes',
        /// then it will return the string "subtract". If 'no', then "divide".
        /// </summary>
        /// <param name="shouldSubtract"></param>
        /// <returns></returns>
        public static string SubtractOrDivide(bool shouldSubtract)
        {
            return shouldSubtract ? "subtract" : "divide";
        }
    }
}