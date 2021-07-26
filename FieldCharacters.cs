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
        public bool black=true;
        public Texture2D sprite;
        public Texture2D sprite_w;
        public Color color = Color.White;
        public Vector2 Position;
        public float Orientation=0;
        public bool IsExpired;
        public Vector2 Size = new Vector2(64, 64);
        public bool chosen = false;
        
        public virtual bool CanGo(int x, int y)
        {

            return false;
        }


        public abstract void Update();
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (black)
            {
                spriteBatch.Draw(sprite, Position, null, color, Orientation, default, 1f, 0, 0);
            }
            else
            {
                spriteBatch.Draw(sprite_w, Position, null, color, Orientation, default, 1f, 0, 0);
            }

            if (chosen)
            {
                spriteBatch.Draw(Assets.chosenTexture, Position, null, color, Orientation, default, 1f, 0, 0);
            }
        }
    }
    class Pawn : Entity
    {
        public Pawn(Vector2 position, bool blackd)
        {
            sprite = Assets.pawnTexture;
            sprite_w = Assets.pawnTexture_w;
            Position = position;
            black = blackd;
        }

        public override bool CanGo(int x, int y)
        {
            if (x == Position.X)
            {if (black)
                {
                    if ((y == Position.Y + 64) || ((Position.Y==64) &&(y == Position.Y + 128)))
                    {
                        return true;
                    }
                }
                else
                {
                    if ((y == Position.Y - 64) || (y == Position.Y - 128))
                    {
                        return true;
                    }
                }


            }
            return false;
        }



        public override void Update()
        {
            //Если кнопка нажата
            if (Mouse.GetState().LeftButton==ButtonState.Pressed) {
                chosen = false;
                //Kiri: ЗАменить Mouse.GetState() на проверку через игрока!!!!
                if (GameHelper.CheckBoundaries(Position, Size, Player.posx, Player.posy)){
                    chosen = true;
                };
            }

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if ((CanGo(Player.posx,Player.posy))&&(chosen))
                {
                    Position.X = Player.posx;
                    Position.Y = Player.posy;
                }
            
            
            }

                //Kiri: управление стрелками!
                //if (chosen == true)
                //{
                //    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                //    {
                //        Position += new Vector2(0, 4);//Вниз
                //    }
                //    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                //    {
                //        Position += new Vector2(0, -4);//Вверх
                //    }
                //    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                //    {
                //        Position += new Vector2(4, 0);//Вправо
                //    }
                //    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                //    {
                //        Position += new Vector2(-4, 0);//Влево
                //    }
                //}
            }
    }
}
