using System.Globalization;

namespace B16_Ex02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameLogic
    {
        private readonly int r_NumberOfRows;
        private readonly int r_NumberOfColumn;
        private readonly int r_NumberOfPlayers;
        private const int k_MinimumOfCoinsSizeNeddedToWin = 3;
        private int m_PlayerOneScore = 0;
        private int m_PlayerTwoScore = 0;
        private string[][] m_GameBoard;

        public GameLogic(int i_NumberOfRows, int i_NumberOfColumn, int i_NumberOfPlayers)
        {
            r_NumberOfPlayers = i_NumberOfPlayers;
            r_NumberOfRows = i_NumberOfRows;
            r_NumberOfColumn = i_NumberOfColumn;
            m_GameBoard = new string[r_NumberOfRows][];
        }

        // Setting properties for Row/Column size, GameBoard and number of players.
        public int GetRow
        {
            get { return r_NumberOfRows; }
        }

        public int GetColumn
        {
            get { return r_NumberOfColumn; }
        }

        public string[][] GetBoard
        {
            get { return m_GameBoard; }
        }

        public int GetNumberOfPlayers
        {
            get { return r_NumberOfPlayers; }
        }

        public void InitialysBorad()
        {
            for (int i = 0; i < r_NumberOfRows; i++)
            {
                m_GameBoard[i] = new string[r_NumberOfColumn];
                for (int j = 0; j < r_NumberOfColumn; j++)
                {
                    m_GameBoard[i][j] = " ";
                }
            }
        }

        public bool CheckIfColumnFull(int i_ColumnChoise)
        {
            bool isColumnFull = false;
            isColumnFull = !m_GameBoard[0][i_ColumnChoise - 1].Equals(" ");
            return isColumnFull;
        }

        public bool CheckIfBoardIsFull()
        {
            bool isBoardFull = true;
            for (int columnIterator = 0; columnIterator < r_NumberOfColumn; columnIterator++)
            {
                isBoardFull = !m_GameBoard[0][columnIterator].Equals(" ");
                if (!isBoardFull)
                {
                    break;
                }
            }

            return isBoardFull;
        }

        public void ComputerCoinInsertion()
        {
            int randomlySelectedColumn = new Random().Next(1, r_NumberOfColumn + 1);
            while (this.CheckIfColumnFull(randomlySelectedColumn))
            {
                randomlySelectedColumn = new Random().Next(1, r_NumberOfColumn + 1);
            }

            this.GeneralCoinInsertion("O", randomlySelectedColumn);
        }

        public bool CheckIfWinner(int i_MyTurn)
        {
            bool isWinner = false;
            string playerSigniture;
            if (i_MyTurn == 1)
            {
                playerSigniture = "X";
            }
            else
            {
                playerSigniture = "O";
            }

            for (int columnIndex = 0; columnIndex < r_NumberOfColumn - 1; columnIndex++)
            {
                for (int rowIndex = r_NumberOfRows - 1; rowIndex >= 0; rowIndex--)
                {
                    if (m_GameBoard[rowIndex][columnIndex].Equals(playerSigniture))
                    {
                        if (rowIndex >= k_MinimumOfCoinsSizeNeddedToWin)
                        {
                            isWinner = this.CheckUpperVerticalCoinsForWinner(rowIndex, columnIndex, isWinner, playerSigniture);
                        }
                        if ((r_NumberOfColumn - 1) - columnIndex >= k_MinimumOfCoinsSizeNeddedToWin)
                        {
                            isWinner = this.CheckRightHorizonalCoinsForWinner(rowIndex, columnIndex, isWinner, playerSigniture);
                        }
                        if ((rowIndex >= k_MinimumOfCoinsSizeNeddedToWin) && ((r_NumberOfColumn - 1) - columnIndex >= k_MinimumOfCoinsSizeNeddedToWin))
                        {
                            isWinner = this.CheckUpperRightDiagonalCoinsForWinner(rowIndex, columnIndex, isWinner, playerSigniture);
                        }
                        if (((r_NumberOfRows - 1) - rowIndex >= k_MinimumOfCoinsSizeNeddedToWin) && ((r_NumberOfColumn - 1) - columnIndex >= k_MinimumOfCoinsSizeNeddedToWin))
                        {
                            isWinner = this.CheckBottomRightDiagonalCoinsForWinner(rowIndex, columnIndex, isWinner, playerSigniture);
                        }
                        if (isWinner)
                        {
                            break;
                        }

                    }
                }
                if (isWinner)
                {
                    if (i_MyTurn == 1)
                    {
                        m_PlayerOneScore++;
                    }
                    else
                    {
                        m_PlayerTwoScore++;
                    }
                    break;
                }
            }
            return isWinner;
        }

        private bool CheckUpperVerticalCoinsForWinner(int i_RowIndex, int i_ColumnIndex, bool i_IsWinner, string i_PlayerSigniture)
        {
            int counterOfCoins = 0;
            if (!i_IsWinner)
            {
                for (int i = 0; i <= k_MinimumOfCoinsSizeNeddedToWin; i++)
                {
                    if (m_GameBoard[i_RowIndex][i_ColumnIndex].Equals(i_PlayerSigniture))
                    {
                        counterOfCoins++;
                        i_RowIndex--;
                    }
                }
                i_IsWinner = counterOfCoins == k_MinimumOfCoinsSizeNeddedToWin + 1;
            }

            return i_IsWinner;
        }

        private bool CheckRightHorizonalCoinsForWinner(int i_RowIndex, int i_ColumnIndex, bool i_IsWinner, string i_PlayerSigniture)
        {
            int counterOfCoins = 0;
            if (!i_IsWinner)
            {
                for (int i = 0; i <= k_MinimumOfCoinsSizeNeddedToWin; i++)
                {
                    if (m_GameBoard[i_RowIndex][i_ColumnIndex].Equals(i_PlayerSigniture))
                    {
                        counterOfCoins++;
                        i_ColumnIndex++;
                    }
                }
                i_IsWinner = counterOfCoins == k_MinimumOfCoinsSizeNeddedToWin + 1;
            }

            return i_IsWinner;
        }

        private bool CheckUpperRightDiagonalCoinsForWinner(int i_RowIndex, int i_ColumnIndex, bool i_IsWinner, string i_PlayerSigniture)
        {
            int counterOfCoins = 0;
            if (!i_IsWinner)
            {
                for (int i = 0; i <= k_MinimumOfCoinsSizeNeddedToWin; i++)
                {
                    if (m_GameBoard[i_RowIndex][i_ColumnIndex].Equals(i_PlayerSigniture))
                    {
                        counterOfCoins++;
                        i_RowIndex--;
                        i_ColumnIndex++;
                    }
                }
                i_IsWinner = counterOfCoins == k_MinimumOfCoinsSizeNeddedToWin + 1;
            }

            return i_IsWinner;
        }

        private bool CheckBottomRightDiagonalCoinsForWinner(int i_RowIndex, int i_ColumnIndex, bool i_IsWinner, string i_PlayerSigniture)
        {
            int counterOfCoins = 0;
            if (!i_IsWinner)
            {
                for (int i = 0; i <= k_MinimumOfCoinsSizeNeddedToWin; i++)
                {
                    if (m_GameBoard[i_RowIndex][i_ColumnIndex].Equals(i_PlayerSigniture))
                    {
                        counterOfCoins++;
                        i_RowIndex++;
                        i_ColumnIndex++;
                    }
                }
                i_IsWinner = counterOfCoins == k_MinimumOfCoinsSizeNeddedToWin + 1;
            }

            return i_IsWinner;
        }

        public void GeneralCoinInsertion(string i_Signiture, int i_SelectedColumn)
        {
            for (int i = 0; i < r_NumberOfRows - 1; i++)
            {
                if (!m_GameBoard[i + 1][i_SelectedColumn - 1].Equals(" ") && m_GameBoard[i][i_SelectedColumn - 1].Equals(" "))
                {
                    m_GameBoard[i][i_SelectedColumn - 1] = i_Signiture;
                    return;
                }
            }

            m_GameBoard[r_NumberOfRows - 1][i_SelectedColumn - 1] = i_Signiture;
        }

        public void ShowScoreBoard()
        {
            Console.WriteLine(
@"========= Score Board =========
Player One: {0}
Player Two: {1}", m_PlayerOneScore, m_PlayerTwoScore);
        }
    }
}