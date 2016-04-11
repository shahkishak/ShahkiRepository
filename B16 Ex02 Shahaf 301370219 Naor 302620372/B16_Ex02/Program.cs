namespace B16_Ex02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        public static void Main()
        {
            GameLaunher();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        private static void GameLaunher()
        {
            UIOrginaizer ui = new UIOrginaizer();
            ui.WelcomeMsg();
            GameLogic game = new GameLogic(ui.ColumnOrRowSelection("Rows"), ui.ColumnOrRowSelection("Columns"), ui.VerusPlayerOrComputer());
            game.InitialysBorad();
            ui.DrawBoard(game.GetRow, game.GetColumn, game.GetBoard);
            GameManager(ui, game);
        }

        private static void GameManager(UIOrginaizer io_UI, GameLogic io_Game)
        {
            int MyTurn;
            bool isQuitPressed = false;
            bool isWinner = false;
            bool isBoardFull = false;
            bool isNextRound = true;

            while (isNextRound)
            {

                // Setting primary randomly turn
                MyTurn = new Random().Next(1, 3);
                while (!isBoardFull)
                {
                    if (io_Game.GetNumberOfPlayers == 1)
                    {
                        if (MyTurn == 1)
                        {
                            isQuitPressed = PlayTurnByUser(io_Game, io_UI, MyTurn);
                            isWinner = io_Game.CheckIfWinner(MyTurn);
                        }
                        else
                        {
                            PlayTurnByComputer(io_Game, io_UI);
                            isWinner = io_Game.CheckIfWinner(MyTurn);
                        }
                    }
                    else
                    {
                        isQuitPressed = PlayTurnByUser(io_Game, io_UI, MyTurn);
                        isWinner = io_Game.CheckIfWinner(MyTurn);
                    }
                    isBoardFull = io_Game.CheckIfBoardIsFull();
                    if (isBoardFull || isWinner || isQuitPressed)
                    {
                        break;
                    }
                    if (MyTurn == 1)
                    {
                        MyTurn = 2;
                    }
                    else
                    {
                        MyTurn = 1;
                    }
                }

                if (isWinner)
                {
                    Console.WriteLine("Player Number {0} wins!!!", MyTurn);
                }
                else
                {
                    if (isBoardFull)
                    {
                        Console.WriteLine("Game board is full. Draw!");
                    }
                    else
                    {
                        Console.WriteLine("You chose to quit the game.");
                    }
                }
                io_Game.ShowScoreBoard();
                isNextRound = io_UI.CheckIfNextRound();
                if (isNextRound)
                {
                    io_Game.InitialysBorad();
                    io_UI.DrawBoard(io_Game.GetRow, io_Game.GetColumn, io_Game.GetBoard);
                }
            }
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("========= Final Score =========" + Environment.NewLine);
            io_Game.ShowScoreBoard();
        }

        private static void PlayTurnByComputer(GameLogic io_Game, UIOrginaizer io_UI)
        {
            io_UI.ShowTurnToUser(0);
            io_Game.ComputerCoinInsertion();
            Ex02.ConsoleUtils.Screen.Clear();
            io_UI.DrawBoard(io_Game.GetRow, io_Game.GetColumn, io_Game.GetBoard);
        }

        private static bool PlayTurnByUser(GameLogic io_Game, UIOrginaizer io_UI, int i_MyTurn)
        {
            int UserInsertionColumnSelection = 0;
            bool isColumnFull;
            bool isQuitPressed = false;

            io_UI.ShowTurnToUser(i_MyTurn);
            UserInsertionColumnSelection = io_UI.TakeDecisionFromUser("Column", io_Game.GetColumn);
            isQuitPressed = UserInsertionColumnSelection == 0;
            if (isQuitPressed)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                return isQuitPressed;
            }
            isColumnFull = io_Game.CheckIfColumnFull(UserInsertionColumnSelection);
            while (isColumnFull)
            {
                Console.Write("Column {0} is full. Please choose another Column: ", UserInsertionColumnSelection);
                UserInsertionColumnSelection = io_UI.TakeDecisionFromUser("Column", io_Game.GetColumn);
                isColumnFull = io_Game.CheckIfColumnFull(UserInsertionColumnSelection);
            }

            if (i_MyTurn == 1)
            {
                io_Game.GeneralCoinInsertion("X", UserInsertionColumnSelection);
            }
            else
            {
                io_Game.GeneralCoinInsertion("O", UserInsertionColumnSelection);
            }

            io_UI.DrawBoard(io_Game.GetRow, io_Game.GetColumn, io_Game.GetBoard);
            return isQuitPressed;
        }
    }
}