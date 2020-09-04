using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    class UpdateStats
    {
        int armyCount = 0;
        int scoutCount = 0;
        int goldCount = 100;
        int humanCount = 150;
        int castleCount = 1;

        List<MoveableItem> armies = new List<MoveableItem>();
        List<MoveableItem> scouts = new List<MoveableItem>();

        public List<MoveableItem> Update(int health, int basePositionX, int basePositionY, bool army, bool scout, bool castle, bool sendArmy, bool sendScout)
        {
            if (army && humanCount >= 100 && goldCount >= 75)
            {
                army = false;
                armyCount++;
                armies.Add(new MoveableItem(basePositionX, basePositionY));
                humanCount = humanCount - 100;
                goldCount = goldCount - 75;
            }

            if (scout && humanCount >= 50 && goldCount >= 35)
            {
                scout = false;
                scoutCount++;
                scouts.Add(new MoveableItem(basePositionX, basePositionY));
                humanCount = humanCount - 50;
                goldCount = goldCount - 35;
            }

            if (castle && humanCount >= 200 && goldCount >= 1000)
            {
                // string input = Console.ReadLine();
                // zorg er hier dan voor dat er alleen een castle gemaakt kan worden als de doorgegeven cordinaten een leger op staat

                castle = false;
                castleCount++;
                humanCount = humanCount - 200;
                goldCount = goldCount - 1000;
            }

            if (sendArmy && armies.Count > 0 && goldCount >= 25)
            {
                Console.Write("from: ");
                string input = Console.ReadLine();

                Console.SetCursorPosition(input.Length + 21, 54);

                for (int i = input.Length; i > 0; i--)
                    Console.Write("\b \b");

                string[] p = input.Split(' ');

                //if (armies.Contains(new MoveableItem(Convert.ToInt32(p[0]), Convert.ToInt32(p[1]))))
                //{
                    int y = armies.IndexOf(new MoveableItem(Convert.ToInt32(p[0]), Convert.ToInt32(p[1])));

                    Console.Write("to: ");
                    input = Console.ReadLine();

                    Console.SetCursorPosition(input.Length + 25, 54);

                    for (int i = input.Length; i > 0; i--)
                        Console.Write("\b \b");

                    p = input.Split(' ');

                    armies[0].positionX = Convert.ToInt32(p[0]); // moet eigenlijk y zijn niet 0
                    armies[0].PositionY = Convert.ToInt32(p[1]); // moet eigenlijk y zijn niet 0

                return armies;
                //}
            }

            if (sendScout && scouts.Count > 0 && goldCount >= 15)
            {
                string input = Console.ReadLine();

                for (int i = input.Length; i > 0; i--)
                {
                    Console.SetCursorPosition(i + 15, 54);

                    Console.Write("\b \b");
                }
            }

            Console.SetCursorPosition(203, 1);
            Console.Write("castle position: " + basePositionX + ", " + basePositionY);
            Console.SetCursorPosition(203, 3);
            Console.Write("health:----------" + health);
            Console.SetCursorPosition(203, 9);
            Console.Write("armies:----------" + armyCount);
            Console.SetCursorPosition(203, 11);
            Console.Write("scouts:----------" + scoutCount);
            Console.SetCursorPosition(203, 13);
            Console.Write("castles:---------" + castleCount);

            Console.SetCursorPosition(0, 56);
            Console.WriteLine("command list:");
            Console.WriteLine("createArmy:---creates an army on main castle");
            Console.WriteLine("createScouts:-creates a scout on main castle");
            Console.WriteLine("sendArmy:-----takes two cordinates and sends army from A to B");
            Console.WriteLine("sendScouts:---takes two cordinates and sends scouts from A to B");
            Console.WriteLine("createCastle:-creates a castle on location of an amry unit");

            return null;
        }

        public void GetItems()
        {
            goldCount = goldCount + 10;
            humanCount = humanCount + 15;

            int x = Console.CursorLeft;

            Console.SetCursorPosition(203, 5);
            Console.Write("gold:------------" + goldCount);
            Console.SetCursorPosition(203, 7);
            Console.Write("population:------" + humanCount);

            Console.SetCursorPosition(x, 54);
        }
    }
}