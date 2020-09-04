using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    class MoveableItem
    {
        public int positionX;
        public int PositionY;
        public int health = 100;

        public MoveableItem(int positionX_, int positiony_)
        {
            positionX = positionX_;
            PositionY = positiony_;
        }
    }
}
