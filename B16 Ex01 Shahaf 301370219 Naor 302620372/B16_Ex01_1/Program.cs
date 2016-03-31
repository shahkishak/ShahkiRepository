namespace B16_Ex01_1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        private const int NUMBER = 6;
        private static int[] m_DecimalArray = new int[NUMBER];
        private static int m_CounterOfBits = 0;
        private static int m_CounterOfUpHillSeries = 0;
        private static int m_CounterOfDownHillSeries = 0;
        
        public static void Main()
        {
            ProgramLauncher();
            Console.WriteLine("Press any kye to exit");
            Console.ReadLine();
        }

        private static void ProgramLauncher()
        {
            ShowInstruction();
            ReciveDecimalNumbers();
            ConverToBinary();
            CheckUpAndDownSeries();
            ShowStatistic();
        }

        private static void ShowInstruction()
        {
            Console.WriteLine(
@"Welcome to exrecise number 1.
Please enter six 3-digit numbers. After each number, press Enter");
        }

        private static void ReciveDecimalNumbers()
        {
            int userNumberSelection;
            string userInput;

            for (int i = 0; i < NUMBER; i++)
            {
                // Validation with two levels- 1. as a string to obtain 3 digits number, 2. as an integer to validate positive number. 
                userInput = Console.ReadLine();
                int.TryParse(userInput, out userNumberSelection);
                while (userNumberSelection <= 0 || userInput.Length != 3)
                {
                    Console.WriteLine("Input is not legit, please re-enter correct-form number");
                    userInput = Console.ReadLine();
                    int.TryParse(userInput, out userNumberSelection);
                }

                m_DecimalArray[i] = userNumberSelection;
            }
        }

        private static void ConverToBinary()
        {
            // Use Stack as data structure to easily pull the bits back from end to begining.
            Stack<int> divisionStack = new Stack<int>();
            int dividedArrayMember;

            Console.WriteLine("Binary convertion:");

            // Using the well known mod-by-2 algorithem for converting number to binary.
            for (int i = 0; i < NUMBER; i++)
            {
                dividedArrayMember = m_DecimalArray[i];
                while (dividedArrayMember != 0)
                {
                    m_CounterOfBits++;
                    divisionStack.Push(dividedArrayMember % 2);
                    dividedArrayMember /= 2;
                }

                while (divisionStack.Count != 0)
                {
                    Console.Write(divisionStack.Pop());
                }

                if (i != (NUMBER - 1))
                {
                    Console.Write(", ");    
                }
            }

            Console.Write("." + Environment.NewLine + Environment.NewLine);
        }

        private static void ShowStatistic()
        {
            string statisticMsg;

            statisticMsg = string.Format(
@"Here are your statistics:
The average bits per number is: {0}.
Ascending series of numbers input is: {1}.
Descending series of numbers input is: {2}.",
                             m_CounterOfBits / 6, m_CounterOfUpHillSeries, m_CounterOfDownHillSeries);
            Console.WriteLine(statisticMsg);
        }

        private static void CheckUpAndDownSeries()
        {
            int dividedArrayMember;
            bool flagForUpHillSeries;
            bool flagForDownHillSeries;

            for (int i = 0; i < NUMBER; i++)
            {
                flagForUpHillSeries = true;
                flagForDownHillSeries = true;
                dividedArrayMember = m_DecimalArray[i];

                // Checking 3 cases: 1. number lower then 10, 2. number between 10 to 99, 3. number with 3 positive digits.
                // First case, if number lower then 10, number cant be ascending/descending.
                if ((dividedArrayMember / 10) == 0)
                {
                    flagForUpHillSeries = false;
                    flagForDownHillSeries = false;
                }
                else
                {
                    // Second case, if number between 10 to 99, only ascending series can be found.
                    if ((dividedArrayMember / 100) == 0)
                    {
                        flagForDownHillSeries = false;
                        flagForUpHillSeries = (dividedArrayMember % 10) <= (dividedArrayMember / 10) ? false : flagForUpHillSeries;
                    }
                    else
                    {
                        // Third case, checking all option for ascending/desending series.
                        while ((dividedArrayMember / 10) != 0)
                        {
                            flagForDownHillSeries = (dividedArrayMember % 10) >= ((dividedArrayMember / 10) % 10) ? false : flagForDownHillSeries;
                            flagForUpHillSeries = (dividedArrayMember % 10) <= ((dividedArrayMember / 10) % 10) ? false : flagForUpHillSeries;
                            dividedArrayMember /= 10;
                        }        
                    }
                }

                if (flagForDownHillSeries)
                {
                    m_CounterOfDownHillSeries++;
                }
                else
                {
                    if (flagForUpHillSeries)
                    {
                        m_CounterOfUpHillSeries++;
                    }
                }
            }
        }
    }
}