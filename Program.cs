﻿using System;

namespace TestGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CGame())
                game.Run();
        }
    }
}