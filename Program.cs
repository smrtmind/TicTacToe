using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            string exitTheGame = string.Empty;
            char[] field = new char[9];
            
            while (exitTheGame != "n")
            {
                //this array is needed to compare it with indexes in char array
                int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                int result = 0;
                int amounOfEmptyCells = 9;
                exitTheGame = string.Empty;

                //filling a gaming field with numbers
                for (int i = 0; i < field.Length; i++)
                    field[i] = Convert.ToChar((i + 1).ToString());

                while (true)
                {
                    result = PlayerTurn(field, numbers, XO: 'X', "\n  Crosses turn: ", ref amounOfEmptyCells, ConsoleColor.Blue);
                    if (result == 1 || result == 2 || result == 3) break;

                    result = PlayerTurn(field, numbers, XO: 'O', "\n  Zeroes turn: ", ref amounOfEmptyCells, ConsoleColor.DarkGreen);
                    if (result == 1 || result == 2 || result == 3) break;
                }

                DrawGamingField(field);

                if (result == 1) Print("\n  Crosses won\n\n", ConsoleColor.Blue);
                if (result == 2) Print("\n  Zeroes won\n\n", ConsoleColor.DarkGreen);
                if (result == 3) Print("\n  Draw\n\n", ConsoleColor.DarkRed);

                while (exitTheGame != "n" && exitTheGame != "y")
                {
                    Print("  Play again? [y] / [n]: ");
                    exitTheGame = Console.ReadLine();
                }
            }
        }

        public static void DrawGamingField(char[] gameField)
        {
            Console.Clear();

            //counting three cells in one line, before going to another
            int counter = 0;

            //controlling that this print will not be printing more than twice
            int state = 0;

            Print("  _____ _____ _____\n", ConsoleColor.DarkMagenta);
            Print(" |     |     |     |\n", ConsoleColor.DarkMagenta);

            foreach (var item in gameField)
            {
                Print(" |  ", ConsoleColor.DarkMagenta);
                Print($"{item.ToString()} ");
                counter++;

                if (counter % 3 == 0)
                {
                    Print(" |", ConsoleColor.DarkMagenta);
                    Print("\n |_____|_____|_____|\n", ConsoleColor.DarkMagenta);
                    state++;

                    if (state <= 2) Print(" |     |     |     |\n", ConsoleColor.DarkMagenta);
                }
            }
        }

        public static int PlayerTurn(char[] field, int[] numbers, char XO, string playerTurn, ref int amountOfEmptyCells, ConsoleColor color)
        {
            bool cellIsEmpty = false;

            //searching for empty cell
            while (!cellIsEmpty)
            {
                int input = 0;
                cellIsEmpty = false;

                //parsing the number to compare it with numbers in array
                while (input < 1 || input > 9)
                {
                    DrawGamingField(field);
                    Print(playerTurn, color);

                    int.TryParse(Console.ReadLine(), out input);
                }

                for (int i = 0; i < field.Length; i++)
                {
                    if (input == numbers[i])
                    {
                        cellIsEmpty = true;
                        field[i] = XO;
                        numbers[i] = 0;

                        amountOfEmptyCells--;
                        break;
                    }

                    else if (numbers[input - 1] == 0)
                    {
                        Print("  this cell is not empty", ConsoleColor.Red);
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }

            //searching for winning combination
            if (field[0] == XO && field[1] == XO && field[2] == XO ||
                field[3] == XO && field[4] == XO && field[5] == XO ||
                field[6] == XO && field[7] == XO && field[8] == XO ||
                field[0] == XO && field[3] == XO && field[6] == XO ||
                field[1] == XO && field[4] == XO && field[7] == XO ||
                field[2] == XO && field[5] == XO && field[8] == XO ||
                field[0] == XO && field[4] == XO && field[8] == XO ||
                field[2] == XO && field[4] == XO && field[6] == XO)
            {
                if (XO == 'X') return 1;
                if (XO == 'O') return 2;
            }

            //if there are no more empty cells, it is must be draw
            if (amountOfEmptyCells == 0) return 3;

            //otherwise continue playing
            else return -1;
        }

        public static void Print(string text, ConsoleColor color = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
