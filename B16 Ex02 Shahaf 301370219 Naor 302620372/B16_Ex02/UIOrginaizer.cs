using System.Threading;

namespace B16_Ex02
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class UIOrginaizer
    {
        public void WelcomeMsg()
        {
            Console.WriteLine(
@"=================== Welcome to 4-In-A-Row! ===================

The rules are simple: The first competitor to get a 4 coins
in a row (vertival/ horizonal/ diagonal), wins!

Any competitor can quit at any time by pressing 'Q' button.
Good Luck!

Please choose the size of the game
table (it should be no smaller the 4X4 and not greater 8X8");
        }

        public int ColumnOrRowSelection(string i_RowOrColumnUserChoose)
        {
            bool isParseSucceded;
            int columnOrRowSelectios;

            Console.Write(string.Format("{0}: ", i_RowOrColumnUserChoose));
            isParseSucceded = int.TryParse(Console.ReadLine(), out columnOrRowSelectios);
            while (!isParseSucceded || columnOrRowSelectios < 4 || columnOrRowSelectios > 8)
            {
                Console.Write(@"Invalid Size, please choose legit size for {0} (between 4 to 8): ", i_RowOrColumnUserChoose);
                isParseSucceded = int.TryParse(Console.ReadLine(), out columnOrRowSelectios);
            }

            return columnOrRowSelectios;
        }

        public int VerusPlayerOrComputer()
        {
            bool isParseSucceded;
            int userCompetitionChoise = 0;
            Console.WriteLine(
@"Press 1 to play agianst the computer, or 2 to play agianst other competitor");
            isParseSucceded = int.TryParse(Console.ReadLine(), out userCompetitionChoise);
            while (!isParseSucceded || (userCompetitionChoise != 1  && userCompetitionChoise != 2))
            {
                Console.Write("Invalid Choise, please choose Correctly: ");
                isParseSucceded = int.TryParse(Console.ReadLine(), out userCompetitionChoise);
            }
            return userCompetitionChoise;
        }

        public void DrawBoard(int i_RowsSize, int i_ColumnsSize, string[][] i_GameBoard)
        {

            //Printing out index of each column
            Ex02.ConsoleUtils.Screen.Clear();
            Console.Write(Environment.NewLine);
            Console.Write(" ");
            for (int n = 1; n <= i_ColumnsSize; n++)
            {
                Console.Write(" {0}  ", n);
            }

            //Printing out the game board 
            Console.Write(Environment.NewLine);
            for (int i = 0; i < i_RowsSize; i++)
            {
                Console.Write("|");
                for (int j = 0; j < i_ColumnsSize; j++) 
                {
                    Console.Write(" {0} |", i_GameBoard[i][j]);
                }

                // Printing out seperating lines "================"
                Console.Write(Environment.NewLine);
                for (int k = 0; k < i_ColumnsSize; k++)
                {
                    Console.Write("====");
                }
                Console.Write(Environment.NewLine);
            }
        }

        public void ShowTurnToUser(int i_MyTurn)
        {
            if (i_MyTurn == 1 || i_MyTurn == 2)
            {
            Console.Write(
@"Player number {0} is now playing! Please choose a column index to insert coin: ", i_MyTurn);
            }
            else
            {
                Console.Write(
@"Computer is now playing");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(500);
                }
            }
        }

        public int TakeDecisionFromUser(string i_RowOrColumnUserChoose, int i_ColumnsInBoard)
        {
            bool isParseSucceded = false;
            bool invalidSize = false;
            int rowOrColumnOrQuit = 0;
            string rowOrColumnOrQuitString;
            while (!isParseSucceded || rowOrColumnOrQuit < 1 || rowOrColumnOrQuit > i_ColumnsInBoard)
            {
                if (invalidSize)
                {
                    Console.Write(@"Invalid Size, please choose legit size for {0} (between 1 to {1}): ", i_RowOrColumnUserChoose, i_ColumnsInBoard);
                }
                rowOrColumnOrQuitString = Console.ReadLine();
                isParseSucceded = int.TryParse(rowOrColumnOrQuitString, out rowOrColumnOrQuit);
                if (rowOrColumnOrQuitString.Equals("q") || rowOrColumnOrQuitString.Equals("Q"))
                {
                    rowOrColumnOrQuit = 0;
                    break;
                }

                invalidSize = true;
            }
                
            return rowOrColumnOrQuit;
        }

        public bool CheckIfNextRound()
        {
            Console.WriteLine(Environment.NewLine + "New round? Choose 'y' for new round or any other key to finish the contest: ");
            string nextRound = Console.ReadLine();
            return nextRound.Equals("y") || nextRound.Equals(nextRound.ToUpper());
        }
    }
}