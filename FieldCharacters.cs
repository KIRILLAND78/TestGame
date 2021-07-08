using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    abstract class Entity
    {
        public Texture2D sprite;
        public Color color = Color.White;
        public Vector2 Position;
        public float Orientation=0;
        public bool IsExpired;
        public Vector2 Size = new Vector2(64, 64);
        public bool chosen = false;



        public abstract void Update();
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, null, color, Orientation, Size / 2f, 1f, 0, 0);
        }
    }
    class Pawn : Entity
    {
        public Pawn(Vector2 position)
        {
            sprite = Assets.pawnTexture;
            Position = position;
        }


        //Kiri: Draw можем даже не писать, т.к. он наследуется от Entity. Как удобна нынче жизнь!!!



        public override void Update()
        {
            //Если кнопка нажата
            if (Mouse.GetState().LeftButton==ButtonState.Pressed) {

                chosen = false;
                //Kiri: ЗАменить Mouse.GetState() на проверку через игрока!!!!
                if (GameHelper.CheckBoundaries(Position, Size, Mouse.GetState().X, Mouse.GetState().Y)){
                    chosen = true;
                };
            }


            //Kiri: управление стрелками!
            if (chosen == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    Position += new Vector2(0, 4);//Вниз
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    Position += new Vector2(0, -4);//Вверх
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    Position += new Vector2(4, 0);//Вправо
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    Position += new Vector2(-4, 0);//Влево
                }
            }
        }
    }
}
