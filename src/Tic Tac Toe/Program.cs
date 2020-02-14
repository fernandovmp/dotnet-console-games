using System;
using System.Collections.Generic;

namespace Tic_Tac_Toe
{
    class Program
    {
        const string ROW_SEPARATOR = "    |     |    \n"
                + " ---+-----+---";
        const string ROW_SPACE = "    |     |";
        private static bool gameOver;
        private static bool playerTurn = true;
        private static readonly char[,] board = new char[3, 3]
        {
            { ' ', ' ', ' '},
            { ' ', ' ', ' '},
            { ' ', ' ', ' '}
        };
        private static readonly Random random = new Random();
        static void Main(string[] args)
        {
            while (!gameOver)
            {
                Console.Clear();
                DrawBoard();
                if (playerTurn)
                {
                    PlayerMove();
                }
                else
                {
                    ComputerMove();
                }
                playerTurn = !playerTurn;
                CheckResult();
            }
        }

        private static void PlayerMove()
        {
            int row = 0, column = 0;
            ConsoleKeyInfo keyPressed;
            bool invalid;
            do
            {
                invalid = false;
                Console.Clear();
                DrawBoard();
                Console.WriteLine("\nChoose a valid position and press enter.");
                Console.SetCursorPosition(column * 6 + 1, row * 4 + 1);
                keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        row = Math.Abs((row - 1) % 3);
                        break;
                    case ConsoleKey.DownArrow:
                        row = Math.Abs((row + 1) % 3);
                        break;
                    case ConsoleKey.RightArrow:
                        column = Math.Abs((column + 1) % 3);
                        break;
                    case ConsoleKey.LeftArrow:
                        column = Math.Abs((column - 1) % 3);
                        break;
                    case ConsoleKey.Enter: break;
                    case ConsoleKey.Escape:
                        gameOver = true;
                        Console.Clear();
                        Console.WriteLine("Tic Tac Toe was closed");
                        break;
                    default:
                        invalid = true;
                        break;
                }
                if (invalid || board[row, column] != ' ') continue;
            } while (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Escape);

            if (!gameOver)
            {
                board[row, column] = 'X';
            }

        }

        private static void ComputerMove()
        {
            var validMoves = new List<(int Row, int Column)>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        validMoves.Add((i, j));
                    }
                }
            }
            int move = random.Next(0, validMoves.Count);
            var (Row, Column) = validMoves[move];
            board[Row, Column] = 'O';
        }

        private static void CheckResult()
        {
            if (CheckResultFor('X'))
            {
                EndGame("You win!");
                return;
            }
            if (CheckResultFor('O'))
            {
                EndGame("You lose!");
                return;
            }
            if (IsBoardFull())
            {
                EndGame("Draw!");
            }
        }

        private static bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ') return false;
                }
            }
            return true;
        }

        private static bool CheckResultFor(char value) =>
            (board[0, 0] == value && board[0, 1] == value && board[0, 2] == value)
            || (board[1, 0] == value && board[1, 1] == value && board[1, 2] == value)
            || (board[2, 0] == value && board[2, 1] == value && board[2, 2] == value)
            || (board[0, 0] == value && board[1, 1] == value && board[2, 2] == value)
            || (board[0, 0] == value && board[1, 0] == value && board[2, 0] == value)
            || (board[0, 1] == value && board[1, 1] == value && board[2, 1] == value)
            || (board[0, 2] == value && board[1, 2] == value && board[2, 2] == value)
            || (board[2, 0] == value && board[1, 1] == value && board[0, 2] == value);

        private static void EndGame(string message)
        {
            gameOver = true;
            Console.Clear();
            Console.WriteLine(message);
        }

        private static void DrawBoard()
        {
            Console.WriteLine();
            DrawLine(0);
            Console.WriteLine(ROW_SEPARATOR);
            Console.WriteLine(ROW_SPACE);
            DrawLine(1);
            Console.WriteLine(ROW_SEPARATOR);
            DrawLine(2);
            Console.WriteLine(ROW_SPACE);
        }

        private static void DrawLine(int line) =>
            Console.WriteLine($" {board[line, 0]}  |  {board[line, 1]}  |  {board[line, 2]}");
    }
}
