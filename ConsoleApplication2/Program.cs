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
            List<double> compared = new List<double>();
            compared.Add(GetValue("Value 1: "));
            compared.Add(GetValue("Value 2: "));

            //These doubles use the built-in comparing method to assign the larger and smaller of the numbers to the appropriate val.
            double larger = compared.Max();
            double smaller = compared.Min();

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
                        Console.WriteLine("");
                        Console.WriteLine("ADD");
                        Console.WriteLine("");
                        result = AddValues(larger, smaller);
                        break;

                    case "S":
                        Console.WriteLine("");
                        Console.WriteLine("SUBTRACT");
                        Console.WriteLine("");
                        //SubDivKind determines if the larger or smaller number should be positioned before the minus symbol
                        //SubDivKind then calls the actual subtraction method with the correct value placement.
                        result = SubDivKind(larger, smaller, "subtract");
                        break;

                    case "M":
                        Console.WriteLine("");
                        Console.WriteLine("MULTIPLY");
                        Console.WriteLine("");
                        result = MultValues(larger, smaller);
                        break;

                    case "D":
                        Console.WriteLine("");
                        Console.WriteLine("DIVIDE");
                        Console.WriteLine("");
                        //SubDivKind determines if the larger or smaller number should be positioned before the divison symbol
                        //SubDivKind then calls the actual division method with the correct value placement.
                        result = SubDivKind(larger, smaller, "divide");
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

        private static double GetValue(string label)
        {
            double value;

            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();
                if (Double.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("That input does not parse as a number");
                }
            }
        }

        private static double AddValues(double value1, double value2)
        {
            Console.WriteLine("");
            return value1 + value2;
        }

        private static double SubValues(double value1, double value2)
        {
            Console.WriteLine("");
            Console.WriteLine(value1 + " subtracted from " + value2 + ":");
            Console.WriteLine("");

            return (value2 - value1);
        }

        private static double MultValues(double value1, double value2)
        {
            Console.WriteLine("");
            return (value1 * value2);
        }

        private static double DivValues(double value1, double value2)
        {
            Console.WriteLine("");
            return (value1 / value2);
        }

        private static double SubDivKind(double larger, double smaller, string thisOperation)
        {
            //This section handles the grammer for the method strings
            string fromBy;
            
            if (thisOperation == "subtract")
            {
                fromBy = "from";
            }
            else
            {
                fromBy = "by";
            }

            //This while loop ensures that the user submits the correct input when choosing which number to subtract from or divide by.
            while (true)
            {
                //These if statements are a shortcut in case the entered numbers are the same.
                //It's silly for the user to choose which to subtract/divide from which when the answer will always be the same.
                if (larger == smaller && thisOperation == "subtract")
                {
                    return 0;
                }
                if (larger == smaller && thisOperation == "divide")
                {
                    return 1;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Do you want to " + thisOperation + " " + larger + " " + fromBy + " " + smaller + "?");
                    Console.WriteLine();
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("If you want to " + thisOperation + " " + larger + " " + fromBy + " " + smaller + " please press 'Y'");
                    Console.WriteLine("");
                    Console.WriteLine("If you want to " + thisOperation + " " + smaller + " " + fromBy + " " + larger + ", please press 'N'");
                    Console.WriteLine("");
                    ConsoleKeyInfo selection = Console.ReadKey();
                    string yesNo = selection.KeyChar.ToString().ToUpper();

                    if (yesNo == "Y" && thisOperation == "subtract")
                    {
                        return SubValues(larger, smaller);
                    }
                    if (yesNo == "Y" && thisOperation == "divide")
                    {
                        return DivValues(larger, smaller);
                    }
                    if (yesNo == "N" && thisOperation == "subtract")
                    {
                        return SubValues(smaller, larger);
                    }
                    if (yesNo == "N" && thisOperation == "divide")
                    {
                        return DivValues(smaller, larger);
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Please select a valid option.");
                    }
                }
            }
        }

    }
}