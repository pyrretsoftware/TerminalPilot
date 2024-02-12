using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalPilot.Commands
{
    public class Animations
    {
        //a simple console spinner
        public static bool SpinnerActive { get; set; }
        public static void Spinner()
        {
            int counter = 0;
            while (SpinnerActive)
            {
                if (counter == 0)
                {
                    Console.Write("/");
                    counter++;
                }
                else if (counter == 1)
                {
                    Console.Write("-");
                    counter++;
                }
                else if (counter == 2)
                {
                    Console.Write("\\");
                    counter++;
                }
                else if (counter == 3)
                {
                    Console.Write("|");
                    counter = 0;
                }
                System.Threading.Thread.Sleep(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }
    }
}
