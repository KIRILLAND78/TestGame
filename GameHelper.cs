using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    static class GameHelper
    {

        static public bool CheckBoundaries(Vector2 Position, Vector2 Size, int x, int y)
        {
            return (Position.X - (Size.X / 2) < x) && (Position.X + (Size.X / 2) > x) && (Position.Y + (Size.Y / 2) > y) && (Position.Y - (Size.Y / 2) < y);
        }
    }
}
