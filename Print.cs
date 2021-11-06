using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Print
    {
        public static void Text(string text, ConsoleColor color = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void GamingField(char[] gameField)
        {
            Console.Clear();

            //counting three cells in one line, before going to another
            int counter = 0;

            //controlling that this print will not be printing more than twice
            int state = 0;

            Text("  _____ _____ _____\n", ConsoleColor.DarkMagenta);
            Text(" |     |     |     |\n", ConsoleColor.DarkMagenta);

            foreach (var item in gameField)
            {
                Text(" |  ", ConsoleColor.DarkMagenta);
                Text($"{item} ");
                counter++;

                if (counter % 3 == 0)
                {
                    Text(" |", ConsoleColor.DarkMagenta);
                    Text("\n |_____|_____|_____|\n", ConsoleColor.DarkMagenta);
                    state++;

                    if (state <= 2) Text(" |     |     |     |\n", ConsoleColor.DarkMagenta);
                }
            }
        }
    }
}
