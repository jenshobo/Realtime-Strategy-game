using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace testing
{
    class Program
    {
        public static UpdateStats update = new UpdateStats();

        static void Main(string[] args)
        {
            WriteColor write = new WriteColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                        __        __                 __                   __  __      __        ");
            Console.WriteLine("   ____ ___  ____ _____/ /__     / /_  __  __       / /__  ____  _____   / / / /___  / /_  ____ ");
            Console.WriteLine("  / __ `__ \\/ __ `/ __  / _ \\   / __ \\/ / / /  __  / / _ \\/ __ \\/ ___/  / /_/ / __ \\/ __ \\/ __ \"");
            Console.WriteLine(" / / / / / / /_/ / /_/ /  __/  / /_/ / /_/ /  / /_/ /  __/ / / (__  )  / __  / /_/ / /_/ / /_/ /");
            Console.WriteLine("/_/ /_/ /_/\\__,_/\\__,_/\\___/  /_.___/\\__, /   \\____/\\___/_/ /_/____/  /_/ /_/\\____/_.___/\\____/ ");
            Console.WriteLine("                                    /____/                                                      ");

            write.Write("/BLoading please wait");
            Console.Write(Environment.NewLine);
            write.Write("/Bcreating map, takes long to make maps look better, but currently the map looks bad");

            int[,] arr = new int[50, 200];
            int health = 100;

            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            int q = 0;

            // create the grid with thread sleep to prefent errors
            for (int x = 0; x < rowLength; x++)
            {
                for (int y = 0; y < colLength; y++)
                {
                    int rr = 0;
                    while (true)
                    {
                        Random r = new Random();
                        rr = r.Next(0, 3);

                        if (rr + 1 == q || rr == q || rr - 1 == q)
                            break;
                    }
                    arr[x, y] = rr;
                    q = rr;

                    Thread.Sleep(1);
                }
            }

            write.Write("/G done");
            Thread.Sleep(100);

            // gets random location for starter castle
            int castleLocationX;
            int castleLocationY;
            while (true)
            {
                Random rnX = new Random();
                castleLocationX = rnX.Next(0, arr.GetLength(0));

                Random rnY = new Random();
                castleLocationY = rnY.Next(0, arr.GetLength(1));

                if (arr[castleLocationX, castleLocationY] != 0)
                {
                    arr[castleLocationX, castleLocationY] = 4;
                    break;
                }
            }

            // get random location for enemy base
            int enemyCastleLocationX;
            int enemyCastleLocationY;
            while (true)
            {
                Random rnX = new Random();
                enemyCastleLocationX = rnX.Next(0, arr.GetLength(0));

                Random rnY = new Random();
                enemyCastleLocationY = rnY.Next(0, arr.GetLength(1));

                if (arr[enemyCastleLocationX, enemyCastleLocationY] != 0 && enemyCastleLocationX != castleLocationX && enemyCastleLocationY != castleLocationY)
                {
                    arr[enemyCastleLocationX, enemyCastleLocationY] = 8;
                    break;
                }
            }

            Console.Clear();

            bool b = false;

            for (int x = 0; x < rowLength; x++)
            {
                Console.SetCursorPosition(0, x + 3);

                if (b)
                {
                    b = false;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    b = true;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.WriteLine(x + 1);
            }

            for (int a = 1; a <= arr.GetLength(1); a++)
            {
                string s = a.ToString();
                char[] cha = s.ToCharArray();

                if (b)
                {
                    b = false;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    b = true;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                for (int w = 0; w < cha.Length; w++)
                {
                    Console.SetCursorPosition(a + 1, w);
                    Console.Write(cha[w]);
                }
            }

            string color;

            for (int i = 0; i < rowLength; i++)
            {
                Console.SetCursorPosition(2, i + 3);

                for (int j = 0; j < colLength; j++)
                {
                    if (arr[i, j] == 0)
                        color = "/B";
                    else if (arr[i, j] == 1)
                        color = "/Y";
                    else if (arr[i, j] == 2)
                        color = "/G";
                    else if (arr[i, j] == 3)
                        color = "/R";
                    else
                        color = "/M";

                    write.Write(color + arr[i, j].ToString());
                }
                Console.Write(Environment.NewLine);

                if (b)
                    b = false;
                else
                    b = true;
            }

            update.GetItems();

            Thread t = new Thread(new ThreadStart(UpdatingOther));

            t.Start();

            bool createArmy = false;
            bool createScout = false;
            bool createCastle = false;
            bool sendArmy = false;
            bool sendScout = false;

            // acual game
            while (true)
            {
                List<MoveableItem> list = update.Update(health, castleLocationX + 1, castleLocationY + 1, createArmy, createScout, createCastle, sendArmy, sendScout);

                if (list != null)
                {
                    foreach (MoveableItem item in list)
                    {
                        if (item.positionX != castleLocationX && item.PositionY != castleLocationY)
                        {
                            arr[item.positionX, item.PositionY] = 9;
                            Console.SetCursorPosition(item.positionX + 1, item.PositionY + 2);
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(arr[item.positionX, item.PositionY]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }

                createArmy = false;
                createScout = false;
                createCastle = false;
                sendArmy = false;
                sendScout = false;

                Console.SetCursorPosition(0, 54);
                Console.Write("enter command: ");
                string input = Console.ReadLine();

                for (int i = input.Length; i > 0; i--)
                {
                    Console.SetCursorPosition(i + 15, 54);

                    Console.Write("\b \b");
                }

                string m = input.ToLower();

                switch (m)
                {
                    case "createarmy": createArmy = true; break;
                    case "createscouts": createScout = true; break;
                    case "sendarmy": sendArmy = true; break;
                    case "sendscouts": sendScout = true; break;
                    case "createcastle": createCastle = true; break;
                }
            }
        }

        public static void UpdatingOther()
        {
            while (true)
            {
                Thread.Sleep(10000);

                update.GetItems();
            }
        }
    }
}