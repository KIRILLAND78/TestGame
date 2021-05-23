using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
namespace TestGame
{
    class Player
    {
        public int x;
        public int y;
        public int posx;
        public int posy;
        public int PlayerSpeed = 1;


        public void update()
        {
            x = Mouse.GetState().X;
            y = Mouse.GetState().Y;
        }
        public void updatepos(int xx, int yy, int size)
        {
            posx = ((x-xx)/size)*size* PlayerSpeed;
            posy = ((y - yy) / size) * size * PlayerSpeed;
        }


    }
}
