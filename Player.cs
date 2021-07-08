using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
namespace TestGame
{
    static public class Player
    {
        static public int x;
        static public int y;
        static public int posx;
        static public int posy;


        static public void update()
        {
            x = Mouse.GetState().X;
            y = Mouse.GetState().Y;
        }
        static public void updatepos(int xx, int yy, int size)
        {//kiri: Здесь мы правим позицию зеленого квадратика
            posx = ((x-xx)/size)*size;
            posy = ((y - yy) / size) * size;
        }


    }
}
