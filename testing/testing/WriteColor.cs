using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    class WriteColor
    {
        public void Write(string s)
        {
            bool q = false;

            s.ToCharArray();
            foreach (char c in s)
            {
                if (c != '/' && q == false)
                {
                    Console.Write(c);
                }
                else if (q == false)
                {
                    q = true;
                }
                else
                {
                    q = false;
                    if (c == 'R')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (c == 'B')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (c == 'G')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (c == 'Y')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (c == 'M')
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}