using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        public static int amountOfEmptyCells = 9;
        public static char[] field = new char[9];
        //this array is needed to compare it with indexes in char array
        public static int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            string exitTheGame = string.Empty;

            while (exitTheGame != "n")
            {
                int result = 0;
                exitTheGame = string.Empty;

                //filling a gaming field with numbers
                for (int i = 0; i < field.Length; i++)
                    field[i] = Convert.ToChar((i + 1).ToString());

                while (true)
                {
                    result = PlayerTurn('X');
                    if (result > 0 && result <= 3) break;

                    result = PlayerTurn('O');
                    if (result > 0 && result <= 3) break;
                }

                DrawGamingField(field);

                if (result == 1) Print("\n  Crosses won\n\n", ConsoleColor.Blue);
                if (result == 2) Print("\n  Zeroes won\n\n", ConsoleColor.DarkGreen);
                if (result == 3) Print("\n  Draw\n\n", ConsoleColor.DarkRed);

                while (exitTheGame != "n" && exitTheGame != "y")
                {
                    Print("  Play again? [y] / [n]: ");
                    exitTheGame = Console.ReadLine();
                    ResetResults();
                }
            }

            void ResetResults()
            {
                field = new char[9];
                amountOfEmptyCells = 9;
                numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
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

        public static int PlayerTurn(char symbol)
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
                    if (symbol == 'X')
                        Print("\n  Crosses turn: ", ConsoleColor.Blue);
                    else
                        Print("\n  Zeroes turn: ", ConsoleColor.DarkGreen);

                    int.TryParse(Console.ReadLine(), out input);
                }

                for (int i = 0; i < field.Length; i++)
                {
                    if (input == numbers[i])
                    {
                        cellIsEmpty = true;
                        field[i] = symbol;
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
            if (field[0] == symbol && field[1] == symbol && field[2] == symbol ||
                field[3] == symbol && field[4] == symbol && field[5] == symbol ||
                field[6] == symbol && field[7] == symbol && field[8] == symbol ||
                field[0] == symbol && field[3] == symbol && field[6] == symbol ||
                field[1] == symbol && field[4] == symbol && field[7] == symbol ||
                field[2] == symbol && field[5] == symbol && field[8] == symbol ||
                field[0] == symbol && field[4] == symbol && field[8] == symbol ||
                field[2] == symbol && field[4] == symbol && field[6] == symbol)
            {
                if (symbol == 'X') return 1;
                if (symbol == 'O') return 2;
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
