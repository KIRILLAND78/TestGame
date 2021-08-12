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
        public Texture2D sprite_b;
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
        public virtual bool CanAttack(int x, int y) {
            return false;
        }

        public void Update()
        {//Здесь я сделал апдейт чтобы не переписывать его каждый мать его раз ааааааааааааааааааааааааааааааааа
            //по-хорошему надо было сделать интерфейс
            //или промежуточный класс, но кому не пофиг, честно?
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                chosen = false;
                if (GameHelper.CheckBoundaries(Position, Size, Player.posx, Player.posy))
                {
                    chosen = true;
                };
            }
            if ((Mouse.GetState().RightButton == ButtonState.Pressed) && (black == EntityManager.hodblack))
            {
                if ((CanGo(Player.posx, Player.posy)) && (chosen)&&!((Position.Y == Player.posy) && (Position.X== Player.posx)))
                {
                    if (EntityManager.FindEntity(Player.posx, Player.posy) != null)
                    {
                        EntityManager.FindEntity(Player.posx, Player.posy).IsExpired = true;
                    }
                    Position.X = Player.posx;
                    Position.Y = Player.posy;

                    EntityManager.MoveDone();
                }
            }
        }



        public virtual void Draw(SpriteBatch spriteBatch)
        {if (chosen)
            {
                for (int a = 0; a <= 448; a += 64)
                {
                    for (int b = 0; b <= 448; b += 64)
                    {
                        if (CanGo(a, b))
                        {
                            spriteBatch.Draw(Assets.chooseTexture, new Vector2(a,b), null, Color.Gray, Orientation, default, 1f, 0, 1);
                        }
                    }

                }
            }
            if (black)
            {
                spriteBatch.Draw(sprite_b, Position, null, color, Orientation, default, 1f, 0, 0);
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
            sprite_b = Assets.pawnTexture;
            sprite_w = Assets.pawnTexture_w;
            Position = position;
            black = blackd;
        }
        public override bool CanAttack(int x, int y)
        {
            if ((x == Position.X + 64) || (x == Position.X - 64))
            {
                if (black && y == Position.Y + 64)
                {
                    return true;
                }
                if (!black && y == Position.Y - 64)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool CanGo(int x, int y)
        {
            if (x == Position.X&&((black&&EntityManager.FindEntity((int)Position.X,(int)Position.Y+64)==null)|| (!black && EntityManager.FindEntity((int)Position.X, (int)Position.Y - 64) == null)))
            {if (black)
                {
                    if ((y == Position.Y + 64) || ((Position.Y==64) &&(EntityManager.FindEntity(x, y)==null) &&(y == Position.Y + 128)))
                    {
                        return true;
                    }
                }
                else
                {
                    if ((y == Position.Y - 64) || ((Position.Y == 384) && (EntityManager.FindEntity(x, y - 128) == null) && y == Position.Y - 128))
                    {
                        return true;
                    }
                }
            }
            if (EntityManager.FindEntity(x, y)!=null&&(EntityManager.FindEntity(x, y).black!=black)&&((x==Position.X+64) || (x == Position.X - 64))){
                if (black && y == Position.Y + 64)
                {
                    return true;
                }
                if (!black && y == Position.Y - 64)
                {
                    return true;
                }
            }
            return false;
        }
    }

    class Tower : Entity
    {
        public Tower(Vector2 position, bool blackd)
        {
            sprite_b = Assets.towerTexture;
            sprite_w = Assets.towerTexture_w;
            Position = position;
            black = blackd;
        }

        public override bool CanGo(int x, int y)
        {for (int a = (int)Position.X-64; a >= 0; a -= 64)
            {
                if (EntityManager.FindEntity(a, y) != null && (EntityManager.FindEntity(a, y).black) == black) break;
                if (a == x && Position.Y == y) {
                    return true;
                 }
                if (EntityManager.FindEntity(a, y) != null && (EntityManager.FindEntity(a, y).black) != black) break;
            }
            for (int a = (int)Position.X+64; a <= 448; a += 64)
            {
                if (EntityManager.FindEntity(a, y) != null && (EntityManager.FindEntity(a, y).black) == black) break;
                if (a == x && Position.Y == y)
                {
                    return true;
                }
                if (EntityManager.FindEntity(a, y) != null && (EntityManager.FindEntity(a, y).black) != black) break;
            }//левоправо

            for (int a = (int)Position.Y-64; a >= 0; a -= 64)
            {
                if (EntityManager.FindEntity(x, a) != null && (EntityManager.FindEntity(x, a).black) == black) break;
                if (a == y && Position.X == x)
                {
                    return true;
                }
                if (EntityManager.FindEntity(x, a) != null && (EntityManager.FindEntity(x, a).black) != black) break;
            }
            for (int a = (int)Position.Y+64; a <= 448; a += 64)
            {
                if (EntityManager.FindEntity(x, a) != null && (EntityManager.FindEntity(x, a).black) == black) break;
                if (a == y && Position.X == x)
                {
                    return true;
                }
                if (EntityManager.FindEntity(x, a) != null && (EntityManager.FindEntity(x, a).black) != black) break;
            }//верхвниз
            return false;
        }
    }
    class Bishop : Entity
    {
        public Bishop(Vector2 position, bool blackd)
        {
            sprite_b = Assets.bishopTexture;
            sprite_w = Assets.bishopTexture_w;
            Position = position;
            black = blackd;
        }

        public override bool CanGo(int x, int y)
        { int b;
            int a;
            for (b= (int)Position.Y - 64, a = (int)Position.X - 64; b >= 0 && a >= 0; a -= 64, b -= 64)
            {
                if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) == black) break;
                if (a == x && b == y)
                {
                    return true;
                }
                if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) != black) break;
            }
            for (b = (int)Position.Y + 64, a = (int)Position.X - 64; b <= 448 && a >= 0; a -= 64, b += 64)
            {
                if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) == black) break;
                if (a == x && b == y)
                {
                    return true;
                }
                if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) != black) break;
            }//левые диагонали


            for (b = (int)Position.Y - 64, a = (int)Position.X + 64; b >= 0 && a <= 448; a += 64, b -= 64)
            {
                if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) == black) break;
                if (a == x && b == y)
                {
                    return true;
                }
                if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) != black) break;
            }
            for (b = (int)Position.Y + 64, a = (int)Position.X + 64; b <= 448 && a <= 448; a += 64, b += 64)
            {
                if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) == black) break;
                if (a == x && b == y)
                {
                    return true;
                }
                if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) != black) break;
            }//правые диагонали
            return false;
        }

    }
    class Horse : Entity
    {
        public Horse(Vector2 position, bool blackd)
        {
            sprite_b = Assets.horseTexture;
            sprite_w = Assets.horseTexture_w;
            Position = position;
            black = blackd;
        }

        public override bool CanGo(int x, int y)
        {
            int b = (int)Position.Y + 128;
            int a = (int)Position.X - 64;
            if (b <= 448 && a >= 0 && (a == x && b == y) && (EntityManager.FindEntity(a, b) == null || (EntityManager.FindEntity(a, b) != null && EntityManager.FindEntity(a, b).black != black))) { return true; }
            a = (int)Position.X + 64;
            if (b <= 448 && a <= 448 && (a == x && b == y) && (EntityManager.FindEntity(a, b) == null || (EntityManager.FindEntity(a, b) != null && EntityManager.FindEntity(a, b).black != black))) { return true; }
            b = (int)Position.Y + 64;
            a = (int)Position.X - 128;
            if (b <= 448 && a >= 0 && (a == x && b == y) && (EntityManager.FindEntity(a, b) == null || (EntityManager.FindEntity(a, b) != null && EntityManager.FindEntity(a, b).black != black))) { return true; }
            a = (int)Position.X + 128;
            if (b <= 448 && a <= 448 && (a == x && b == y) && (EntityManager.FindEntity(a, b) == null || (EntityManager.FindEntity(a, b) != null && EntityManager.FindEntity(a, b).black != black))) { return true; }
            b = (int)Position.Y - 64;
            a = (int)Position.X - 128;
            if (b >= 0 && a >= 0 && (a == x && b == y) && (EntityManager.FindEntity(a, b) == null || (EntityManager.FindEntity(a, b) != null && EntityManager.FindEntity(a, b).black != black))) { return true; }
            a = (int)Position.X + 128;
            if (b <= 448 && a <= 448 && (a == x && b == y) && (EntityManager.FindEntity(a, b) == null || (EntityManager.FindEntity(a, b) != null && EntityManager.FindEntity(a, b).black != black))) { return true; }
            b = (int)Position.Y - 128;
            a = (int)Position.X - 64;
            if (b >= 0 && a >= 0 && (a == x && b == y) && (EntityManager.FindEntity(a, b) == null || (EntityManager.FindEntity(a, b) != null && EntityManager.FindEntity(a, b).black != black))) { return true; }
            a = (int)Position.X + 64;
            if (b >= 0 && a <= 448 && (a == x && b == y) && (EntityManager.FindEntity(a, b) == null || (EntityManager.FindEntity(a, b) != null && EntityManager.FindEntity(a, b).black != black))) return true;
            return false;
        } }
        class King : Entity
        {
            public King(Vector2 position, bool blackd)
            {
                sprite_b = Assets.kingTexture;
                sprite_w = Assets.kingTexture_w;
                Position = position;
                black = blackd;
        }

        public override bool CanGo(int x, int y)
            {
                if (!EntityManager.TileIsDangerous(x,y,black)&& (Position.Y - 64 ==y || Position.Y == y || Position.Y + 64 == y) && !(Position.X == x && Position.Y == y) && (Position.X - 64 == x || Position.X == x || Position.X + 64 == x) && (EntityManager.FindEntity(x, y) == null || (EntityManager.FindEntity(x, y) != null && EntityManager.FindEntity(x, y).black != black))) { return true; }
                return false;
            }
        }
        class Queen : Entity
        {
            public Queen(Vector2 position, bool blackd)
            {
                sprite_b = Assets.queenTexture;
                sprite_w = Assets.queenTexture_w;
                Position = position;
                black = blackd;
        }

        public override bool CanGo(int x, int y)
            {
                int b;
                int a;
                for (a = (int)Position.X - 64; a >= 0; a -= 64)
                {
                    if (EntityManager.FindEntity(a, y) != null && (EntityManager.FindEntity(a, y).black) == black) break;
                    if (a == x && Position.Y == y)
                    {
                        return true;
                    }
                    if (EntityManager.FindEntity(a, y) != null && (EntityManager.FindEntity(a, y).black) != black) break;
                }
                for (a = (int)Position.X + 64; a <= 448; a += 64)
                {
                    if (EntityManager.FindEntity(a, y) != null && (EntityManager.FindEntity(a, y).black) == black) break;
                    if (a == x && Position.Y == y)
                    {
                        return true;
                    }
                    if (EntityManager.FindEntity(a, y) != null && (EntityManager.FindEntity(a, y).black) != black) break;
                }//левоправо

                for (a = (int)Position.Y - 64; a >= 0; a -= 64)
                {
                    if (EntityManager.FindEntity(x, a) != null && (EntityManager.FindEntity(x, a).black) == black) break;
                    if (a == y && Position.X == x)
                    {
                        return true;
                    }
                    if (EntityManager.FindEntity(x, a) != null && (EntityManager.FindEntity(x, a).black) != black) break;
                }
                for (a = (int)Position.Y + 64; a <= 448; a += 64)
                {
                    if (EntityManager.FindEntity(x, a) != null && (EntityManager.FindEntity(x, a).black) == black) break;
                    if (a == y && Position.X == x)
                    {
                        return true;
                    }
                    if (EntityManager.FindEntity(x, a) != null && (EntityManager.FindEntity(x, a).black) != black) break;
                }//верхвниз
                for (b = (int)Position.Y - 64, a = (int)Position.X - 64; b >= 0 && a >= 0; a -= 64, b -= 64)
                {
                    if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) == black) break;
                    if (a == x && b == y)
                    {
                        return true;
                    }
                    if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) != black) break;
                }
                for (b = (int)Position.Y + 64, a = (int)Position.X - 64; b <= 448 && a >= 0; a -= 64, b += 64)
                {
                    if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) == black) break;
                    if (a == x && b == y)
                    {
                        return true;
                    }
                    if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) != black) break;
                }//левые диагонали


                for (b = (int)Position.Y - 64, a = (int)Position.X + 64; b >= 0 && a <= 448; a += 64, b -= 64)
                {
                    if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) == black) break;
                    if (a == x && b == y)
                    {
                        return true;
                    }
                    if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) != black) break;
                }
                for (b = (int)Position.Y + 64, a = (int)Position.X + 64; b <= 448 && a <= 448; a += 64, b += 64)
                {
                    if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) == black) break;
                    if (a == x && b == y)
                    {
                        return true;
                    }
                    if (EntityManager.FindEntity(a, b) != null && (EntityManager.FindEntity(a, b).black) != black) break;
                }//правые диагонали

                return false;
            }
        }
    }

