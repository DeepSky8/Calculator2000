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
            double result;

            //This list gets two values from the user and stores them
            
            var userNumbers = new List<double>();
            userNumbers.Add(GetValue("Value 1: "));
            userNumbers.Add(GetValue("Value 2: "));
            var orderedUserNumbers = userNumbers.OrderByDescending(x => x);   

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("What kind of operation will this be?");
                Console.Write("(A)DD, (S)UBTRACT, (M)ULTIPLY, or (D)IVIDE? ");
                ConsoleKeyInfo selection = Console.ReadKey();
                string operation = selection.Key.ToString().ToUpper();

                switch (operation)
                {
                    case "A":
                        Console.WriteLine("\nADD\n");
                        result = AddValues(orderedUserNumbers);
                        break;

                    case "S":
                        Console.WriteLine("\nSUBTRACT\n");
                        //SubDivKind determines if the larger or smaller number should be positioned before the minus symbol
                        //SubDivKind then calls the actual subtraction method with the correct value placement.
                        result = SubDivKind(orderedUserNumbers, true);
                        break;

                    case "M":
                        Console.WriteLine("\nMULTIPLY\n");
                        result = MultValues(orderedUserNumbers);
                        break;

                    case "D":
                        Console.WriteLine("\nDIVIDE\n");
                        //SubDivKind determines if the larger or smaller number should be positioned before the divison symbol
                        //SubDivKind then calls the actual division method with the correct value placement.
                        result = SubDivKind(orderedUserNumbers, false);
                        break;

                    default:
                        Console.WriteLine("");
                        Console.WriteLine("Please select a valid operation.");
                        continue;
                }
                break;
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("The answer is " + result);
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

            Console.WriteLine("\n");
            Console.WriteLine("{0} subtracted from {1}:", value1, value2);
            Console.WriteLine("\n");

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
        /// This method takes two doubles in a list and returns the result of the first double / the second double
        /// </summary>
        /// <param name="value1">The larger number</param>
        /// <param name="value2">The smaller number</param>
        /// <returns>The result of value1 / value2</returns>
        private static double DivValues(List<double> numbers)
        {
            Console.WriteLine("\n");
            return (numbers.ElementAt(0) / numbers.ElementAt(1));
        }

        /// <summary>
        /// This method takes two doubles and a bool that specifies whether the user wants to subtract or divide the numbers.
        /// It requires user input to specify if the larger number should subtract/divide the smaller, or vice versa
        /// </summary>
        /// <param name="larger"></param>
        /// <param name="smaller"></param>
        /// <param name="shouldSubtract"></param>
        /// <returns></returns>
        private static double SubDivKind(List<double> orderedUserNumbers, bool shouldSubtract)
        {
            double result;

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
                //These if statements are a shortcut in case the entered numbers are the same.
                //It's silly for the user to choose which to subtract/divide from which when the answer will always be the same.
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
                    Console.WriteLine("\nIf you want to {0} {3} {2} {1}, please press 'Y\n", SubtractOrDivide(shouldSubtract), larger, fromBy, smaller);

                    ConsoleKeyInfo selection = Console.ReadKey();
                    string yesNo = selection.KeyChar.ToString().ToUpper();

                    string key = yesNo + SubtractOrDivide(shouldSubtract);

                    var map = new Dictionary<string, double>();
                    map.Add("Ysubtract", SubValues(orderedUserNumbers));
                    map.Add("Nsubtract", SubValues(orderedUserNumbers));
                    map.Add("Ydivide", DivValues(orderedUserNumbers));
                    map.Add("Ndivide", DivValues(orderedUserNumbers));

                    result = map.TryGetValue(key, out result);
                    whileCycle = false;


                }
            }
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